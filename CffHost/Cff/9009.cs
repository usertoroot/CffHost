using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHost
{
    public struct UnknownStructure3
    {
        public int Unknown1;
        public byte Unknown2;
        public byte Unknown3;
        public int Unknown4;
    }

    public struct UnknownStructure4
    {
        public int Unknown1;
        public int Unknown2;
        public byte Unknown3;
    }

    public struct MapHub
    {
        public byte Version;
        public int Unknown1;
        public byte Unknown2;
        public UnknownStructure3[] Unknown3;
        public UnknownStructure4[] Unknown4;
        public int[] Unknown5;
    }

    public struct _9009
    {
        public MapHub[] Entries;
    }
}
