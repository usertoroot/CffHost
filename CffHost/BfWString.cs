using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHost
{
    public struct BfWString
    {
        public short[] Text;

        public override string ToString()
        {
            byte[] result = new byte[Text.Length * sizeof(short)];
            Buffer.BlockCopy(Text, 0, result, 0, result.Length);

            return Encoding.Unicode.GetString(result);
        }
    }
}
