using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Utility.Extenders
{
    public static class XmlExtenders
    {
        public static string ElementValue(this XElement element, string name, string defaultValue = "")
        {
            var child = element.Element(name);
            if (null == child)
                return defaultValue;

            if (null == defaultValue && string.IsNullOrWhiteSpace(child.Value))
                return null;

            return child.Value;
        }

        public static int ElementValue(this XElement element, string name, int defaultValue)
        {
            var child = element.Element(name);
            if (null == child)
                return defaultValue;

            int value;
            if (int.TryParse(child.Value, out value))
                return value;

            return defaultValue;
        }

#if DEBUG
        public static string ToSqlVariable(this XElement x, string variableName)
        {
            StringBuilder sb = new StringBuilder();
            using (var writer = XmlWriter.Create(sb, new XmlWriterSettings() { Indent = false, NewLineHandling = NewLineHandling.None, OmitXmlDeclaration = true }))
            {
                x.WriteTo(writer);
            }
            return string.Format("declare @{0} xml = N'{1}';", variableName, sb.ToString().Replace("'", "''"));
        }
#endif


        public static T Map<T>(this XElement thisElement)
        {
            var type = typeof(T);
            return (T)thisElement.Map(type);
        }

        /// <summary>
        /// Will try to map element to an object type without the need for serialization.  Usage as follows:
        /// Subelements of the same name are assumed to be an Enumerable with the property name being the element name
        /// Distinct Subelements are assumed to be just a property get/set with the property name being the element name
        /// All properties must have a get/set and empty constructors for all associated object types
        /// </summary>
        /// <param name="thisElement">The element to cast</param>
        /// <param name="type">the object type to map the element to</param>
        /// <returns></returns>
        public static object Map(this XElement thisElement, Type type)
        {
            object item;
            if (type.IsClass && type != typeof(string))
            {
                //create an instance of the type of object we want to return
                item = type.GetInstance();
                foreach(var element in thisElement.Elements().DistinctBy(x => x.Name.ToString()))
                {
                    var propertyName = element.Name.ToString();
                    var propertyType = item.GetPropertyType(propertyName);
                    if (thisElement.Elements().Count(x => x.Name.ToString() == propertyName) == 1)
                    {
                        //set the property of the item to the mapped type
                        item.SetProperty(propertyName, element.Map(propertyType));
                    }
                    else
                    {
                        
                        //get the list of objects first
                        var sameElements = thisElement.Elements().Where(x => x.Name.ToString() == propertyName).ToList();
                        var genericType = propertyType.GetGenericArguments().First();
                        var genericArrayType = genericType.MakeArrayType();
                        var genericArray = (Array)Activator.CreateInstance(genericArrayType, sameElements.Count );
                        for(int i = 0; i < sameElements.Count; i++)
                        {
                            //get the value of what needs to be added
                            var propertyValue = sameElements[i].Map(genericType);

                            //set the array value
                            genericArray.SetValue(propertyValue, i);
                        }
    
                        //create the specific property type using the array
                        if (!propertyType.IsArray)
                        {
                            var enumerableProperty = propertyType.GetInstance(genericArray);

                            //set the property
                            item.SetProperty(propertyName, enumerableProperty);
                        }
                        else
                            //set the property
                            item.SetProperty(propertyName, genericArray);
                    }
                }
            }
            else
            {
                //this isn't a complex type so we just get the property and try to convert it to the type we need
                var itemstring = thisElement.Value;
                item = Convert.ChangeType(itemstring, type);
            }

            return item;
        }
    }
}
