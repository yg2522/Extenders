using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public static class Log4NetExtenders
    {
        /// <summary>
        /// Will map properties directly to the log4net parameter
        /// </summary>
        /// <typeparam name="T">any object who's public properties are mapped in the log4net configs</typeparam>
        /// <param name="logObject">the item to be logged</param>
        /// <returns>the item unchanged</returns>
        public static T ToLog<T>(this T logObject)
        {
            // To get the mapped property be sure to use log4net.Layout.RawPropertyLayout as the layout type and the value is the property name
            // EX: 
            // <layout type="log4net.Layout.RawPropertyLayout">
            //  <key value = "name" />
            // </ layout >
            var type = logObject.GetType();
            foreach (var property in type.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public))
            {
                //log4net.LogicalThreadContext.Properties[property.Name] = type.GetProperty(property.Name).GetValue(logObject); //uncomment with log4net reference
            }

            return logObject;
        }
    }
}
