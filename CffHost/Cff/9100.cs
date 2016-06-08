using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHost
{
    public struct _9100Entry
    {
        public byte Index1;
        public BfString Text1;
        public BfWString Text2;
    }

    public struct _9100
    {
        public _9100Entry[] Entries;
    }
}
