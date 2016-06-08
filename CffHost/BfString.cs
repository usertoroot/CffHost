using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHost
{
    public struct BfString
    {
        public byte[] Text;

        public override string ToString()
        {
            return Encoding.UTF8.GetString(Text);
        }
    }
}
