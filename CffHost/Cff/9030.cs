using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHost
{
    public struct SpellConditionParameter
    {
        public int Unknown1;
        public int Unknown2;
        public int Unknown3;
    }

    public struct SpellConditionParameterContainer
    {
        public byte Version;
        public SpellConditionParameter[] SpellConditionParameters;
    }

    public struct SpellCondition
    {
        public byte Version;
        public int Unknown1;
        public int Unknown2;
        public byte Unknown4;
        public int Unknown5;
        public BfString Unknown6;
        public BfString Unknown7;
        public SpellConditionParameterContainer SpellConditionParameterContainer;
    }

    public struct _9030
    {
        public ArmorWeaponBalancing[] Entries;
    }
}
