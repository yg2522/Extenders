using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace loctool.Utility.Extenders
{
    public static class ObjectExtenders
    {
        public static Type GetPropertyType(this object item, string propertypath, char delimiter = '.')
        {
            var propertyname = propertypath.Split(delimiter);

            object property = item;
            for (int i = 0; i < propertyname.Length; i++)
            {
                var propertyinfo = property.GetType().GetProperty(propertyname[i]);
                if (propertyinfo == null)
                    break;

                if (i < propertyname.Length - 1)
                    property = propertyinfo.GetValue(property);
                else
                    return propertyinfo.PropertyType;
            }
            
            return default(Type);
        }

        public static T GetProperty<T>(this object item, string propertypath, char delimiter = '.')
        {
            var propertyname = propertypath.Split(delimiter);

            object property = item;
            for (int i = 0; i < propertyname.Length; i++)
            {
                var propertyinfo = property.GetType().GetProperty(propertyname[i]);
                if (propertyinfo == null)
                    return default(T);

                property = propertyinfo.GetValue(property);

            }

            if (property is T)
                return (T)property;

            return default(T);
        }

        public static void SetProperty(this object item, string propertypath, object value, char delimiter = '.')
        {
            var propertyname = propertypath.Split(delimiter);

            object property = item;
            for (int i = 0; i < propertyname.Length - 1; i++)
            {
                var propertyinfo = property.GetType().GetProperty(propertyname[i]);
                if (propertyinfo == null)
                    return;

                property = propertyinfo.GetValue(property);

            }

            property.GetType().InvokeMember(propertyname.LastOrDefault(), BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty, Type.DefaultBinder, property, new[] { value });
        }

        public static IEnumerable<string> GetPropertyNames(this object item)
        {
            return item.GetType().GetProperties().Select(x => x.Name).ToArray();
        }
    }
}
