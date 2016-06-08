using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHost
{
    public struct _9012Entry
    {
        public byte Version;
        public int Unknown1;
        public byte Unknown2;
        public byte Unknown3;

        public BfString Unknown4;
        public BfString Unknown5;
    }

    public struct _9012
    {
        public _9012Entry[] Entries;
    }
}
