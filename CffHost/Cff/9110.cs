using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHost
{
    public struct _9110Entry
    {
        public int Index1;
        public byte Index2;
        public BfWString Text;
    }

    public struct _9110
    {
        public _9110Entry[] Entries;
    }
}
