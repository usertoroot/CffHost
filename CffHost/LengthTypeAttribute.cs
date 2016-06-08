using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHost
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class LengthTypeAttribute : Attribute
    {
        public LengthType LengthType
        {
            get;
            private set;
        }

        public LengthTypeAttribute(LengthType lengthType)
        {
            LengthType = lengthType;
        }
    }
}
