using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHost
{
    public struct UnknownStructure1
    {
        public byte Unknown1;
        public byte Unknown2;
    }

    public struct UnknownStructure2
    {
        public int Unknown1;
        public byte Unknown2;
    }

    public struct SquadEffect
    {
        public byte Unknown1;
        public BfString Unknown2;
    }

    public struct SquadEffectsContainer
    {
        public byte Version;
        public SquadEffect[] SpellEffects;
    }

    public struct Squad
    {
        public byte Version;
        public int Unknown1;
        public int Unknown2;
        public byte Unknown3;
        public int Unknown4;
        public int Unknown5;
        public int Unknown6;
        public int Unknown7;
        public int Unknown8;
        public int Unknown9;
        public int Unknown10;
        public int Unknown11;
        public byte Unknown12;
        public BfString Unknown13;
        public BfString Unknown14;
        public BfString Unknown15;
        public int Unknown16;
        public UnknownStructure1[] Unknown17;
        public UnknownStructure2[] Unknown18;
        public int[] Unknown19;
        public int[] Unknown20;
        public SquadEffectsContainer Unknown21;
        public int[] Unknown22;
    }

    public struct _9018
    {
        public Squad[] Entries;
    }
}
