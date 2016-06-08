using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHost
{
    public struct AbilityParameter
    {
        public int Unknown1;
        public int Unknown2;
        public int Unknown3;
    }

    public struct AbilityParameterContainer
    {
        public byte Version;
        public AbilityParameter[] AbilityParameters;
    }

    public struct AbilityEffect
    {
        public byte Unknown1;
        public BfString Unknown2;
    }

    public struct AbilityEffectsContainer
    {
        public byte Version;
        public AbilityEffect[] SpellEffects;
    }

    public struct Ability
    {
        public byte Unknown1;
        public int Unknown2;
        public int Unknown3;
        public int Unknown4;
        public byte Unknown5;
        public byte Unknown6;
        public int Unknown7;
        public int Unknown8;
        public BfString Unknown9;
        public int Unknown10;

        [LengthType(LengthType.Int16)]
        public int[] Unknown11;

        public AbilityParameterContainer AbilityParameterContainer;
        public AbilityEffectsContainer AbilityEffectsContainer;
    }

    public struct _9015
    {
        public Ability[] Entries;
    }
}
