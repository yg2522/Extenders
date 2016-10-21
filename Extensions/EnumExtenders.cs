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
        public static string GetDescription(this Enum value, string seperatorFormat = null)
        {
            StringBuilder description = new StringBuilder();
            var type = value.GetType();
            List<FieldInfo> fieldinfos = new List<FieldInfo>();
            bool isFlag = type.IsDefined(typeof(FlagsAttribute));

            if (isFlag)
            {
                //for flags we need to check if we defined a composite flag or not
                if (Enum.IsDefined(type, value)) //get composite flag
                {
                    FieldInfo info = type.GetField(value.ToString());
                    if (null == info)
                        throw new InvalidOperationException(string.Format("{0} does not contain value {1}", type, value.ToString()));
                    fieldinfos.Add(info);
                }
                else //get all flag components
                {
                    //get each flag that the enum contains
                    foreach (var val in Enum.GetValues(type))
                    {
                        if ((Convert.ToInt64(val) != 0 && Enum.IsDefined(type, val))) //include all other numbers that aren't zero and defined
                        {
                            FieldInfo info = type.GetField(val.ToString());
                            fieldinfos.Add(info);
                        }
                    }
                }
            }
            else
            {
                //regular enum so just check if the value is defined
                if (Enum.IsDefined(type, value)) 
                {
                    FieldInfo info = type.GetField(value.ToString());
                    if (null == info)
                        throw new InvalidOperationException(string.Format("{0} does not contain value {1}", type, value.ToString()));
                    fieldinfos.Add(info);
                }
            }

            foreach (var fi in fieldinfos) //go through all the fields to make our description
            {
                DescriptionAttribute[] attributes =
                    (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute), false);

                var valuedescription = (attributes.Length > 0) ? attributes[0].Description : value.ToString(); //if there is a description use it, otherwise use the value itself

                if (description.Length == 0)
                {
                    description.Append(valuedescription);
                }
                else
                {
                    description.AppendFormat(seperatorFormat ?? ", {0}", valuedescription);
                }
            }
            
            return description.ToString();
        }

        public static bool ContainsFlag(this Enum baseVal, Enum checkVal)
        {
            var type = baseVal.GetType();
            return (Enum.IsDefined(type, checkVal));
        }
    }
}
