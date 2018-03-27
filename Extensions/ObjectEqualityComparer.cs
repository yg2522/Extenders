using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public class ObjectEqualityComparer<T> : EqualityComparer<T>
    {
        public override bool Equals(T x, T y)
        {
            if (x != null)
            {
                return y != null && x.Equals(y);
            }
            return y == null;
        }

        public override int GetHashCode(T obj)
        {
            if (obj == null)
            {
                return 0;
            }
            return obj.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var objectEqualityComparer = obj as ObjectEqualityComparer<T>;
            return objectEqualityComparer != null;
        }

        public override int GetHashCode()
        {
            return GetType().Name.GetHashCode();
        }
    }
}
