using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHost
{
    public struct SpellParameter
    {
        public int Unknown1;
        public int Unknown2;
        public int Unknown3;
    }

    public struct SpellParametersContainer
    {
        public byte Version;
        public SpellParameter[] SpellParameters;
    }

    public struct SpellEffect
    {
        public byte Unknown1;
        public BfString Unknown2;
    }

    public struct SpellEffectsContainer
    {
        public byte Version;
        public SpellEffect[] SpellEffects;
    }

    public struct SpellConditionReferenceContainer
    {
        public byte Version;
        public int[] SpellConditionIdentifiers;
    }

    public struct SpellRuleReferenceContainer
    {
        public byte Version;
        public int[] SpellRuleIdentifiers;
    }

    public struct Spell
    {
        public byte Version;
        public int Unknown2;
        public int Unknown3;
        public short Unknown4;
        public short Unknown5;
        public byte Unknown6;
        public byte Unknown7;
        public int Unknown8;
        public int Unknown9;
        public int Unknown10;
        public int Unknown11;
        public int Unknown12;
        public byte Unknown13;
        public int Unknown14;
        public int Unknown15;
        public int Unknown16;
        public int Unknown17;
        public BfString Unknown18;
        public BfString Unknown19;
        public BfString Unknown20;
        public SpellParametersContainer SpellParametersContainer;
        public SpellEffectsContainer SpellEffectsContainer;
        public SpellConditionReferenceContainer SpellConditionReferenceContainer;
        public SpellRuleReferenceContainer SpellRuleReferenceContainer;

        [LengthType(LengthType.Int16)]
        public int[] Unknown21;

        [LengthType(LengthType.Int16)]
        public int[] Unknown22;
    }

    public struct _9000
    {
        public Spell[] Entries;
    }
}
