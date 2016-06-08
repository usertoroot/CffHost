using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHost
{
    public struct _9113Entry
    {
        public byte Index1;
        public BfString Text1;
        public BfString Text2;
        public BfWString Text;
    }

    public struct _9113
    {
        public _9113Entry[] Entries;
    }
}
