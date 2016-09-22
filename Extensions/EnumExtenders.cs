using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Extenders
{
    public static class EnumExtenders
    {
        /// <summary>
        /// Used to get the description attribute from System.ComponentModel
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            StringBuilder description = new StringBuilder();
            var type = value.GetType();
            List<FieldInfo> fieldinfos = new List<FieldInfo>();

            //get singular value
            if (Convert.ToInt64(value) == 0 || (Convert.ToInt64(value) & Convert.ToInt64(value) - 1) == 0)
            {
                FieldInfo info = type.GetField(value.ToString());
                if (null == info)
                    throw new InvalidOperationException(string.Format("{0} does not contain value {1}", type, value.ToString()));
                fieldinfos.Add(info);
            }
            else
            {
                //get each flag that the enum contains
                foreach(var val in Enum.GetValues(type))
                {
                    if (Convert.ToInt64(val) != 0 && (Convert.ToInt64(val) & Convert.ToInt64(value)) == Convert.ToInt64(val))
                    {
                        FieldInfo info = type.GetField(val.ToString());
                        fieldinfos.Add(info);
                    }
                }
            }

            foreach (var fi in fieldinfos)
            {
                DescriptionAttribute[] attributes =
                  (DescriptionAttribute[])fi.GetCustomAttributes(
                  typeof(DescriptionAttribute), false);

                var valuedescription = (attributes.Length > 0) ? attributes[0].Description : value.ToString();

                if(description.Length == 0)
                {
                    description.Append(valuedescription);
                }
                else
                {
                    description.AppendFormat(", {0}", valuedescription);
                }
            }
            
            return description.ToString();
        }

        public static bool ContainsFlag(this Enum type, Enum value) 
        {
            return ((Convert.ToInt64(type) & Convert.ToInt64(value)) != 0);
        }
    }
}
