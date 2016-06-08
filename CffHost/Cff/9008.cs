using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHost
{
    public struct MapFile
    {
        public byte Version;
        public int Unknown1;
        public byte Unknown2;
        public byte Unknown3;
        public BfString Unknown6;
        public BfString Unknown7;
        public BfString Unknown8;
        public int[] Unknown9;
        public int[] Unknown10;
    }

    public struct _9008
    {
        public MapFile[] Entries;
    }
}
