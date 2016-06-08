using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHost
{
    public struct _9101Entry
    {
        public int Index1;
        public byte Index2;
        public BfWString Text;
    }

    public struct _9101
    {
        public _9101Entry[] Entries;
    }
}
