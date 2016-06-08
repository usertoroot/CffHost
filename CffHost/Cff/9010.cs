using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHost
{
    public struct UnknownStructure5
    {
        public int Unknown1;
        public int Unknown2;
    }

    public struct MapLoot
    {
        public byte Version;
        public int Unknown1;
        public UnknownStructure5[] Unknown3;
    }

    public struct _9010
    {
        public MapLoot[] Entries;
    }
}
