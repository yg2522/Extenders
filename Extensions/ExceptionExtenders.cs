using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extenders
{
    public static class ExceptionExtenders
    {
        public static Exception GetInnerMostException(this Exception e)
        {
            if (e.InnerException == null)
                return e;
            else
                return e.InnerException.GetInnerMostException();
        }
    }
}
