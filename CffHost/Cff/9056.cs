using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHost
{
    public struct Product
    {
        public byte Version;
        public int Unknown1;
        public short Unknown2;
    }

    public struct _9056
    {
        public Product[] Entries;
    }
}
