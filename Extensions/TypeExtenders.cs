using System;
using System.Linq;

namespace Utility.Extenders
{
    public static class TypeExtenders
    {
        public static bool IsType(this Type type, Type baseType)
        {
            Type tempType = type;

            while (tempType.BaseType != null)
            {
                if (tempType.BaseType == baseType)
                    return true;
                tempType = tempType.BaseType;
            }

            return false;
        }

        public static T GetInstance<T>(this Type type, params object[] constructorParams)
        {
            return (T)Activator.CreateInstance(type, constructorParams);
        }

        public static object GetInstance(this Type type, params object[] constructorParams)
        {
            return Activator.CreateInstance(type, constructorParams);
        }

        public static object GetDefault(this Type type)
        {
            if (type.IsValueType)
                return Activator.CreateInstance(type);

            return null;
        }
    }
}
