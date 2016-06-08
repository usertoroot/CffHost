using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHost
{
    public struct SpellRuleParameter
    {
        public int Unknown1;
        public int Unknown2;
        public int Unknown3;
    }

    public struct SpellRuleParameterContainer
    {
        public byte Version;
        public SpellRuleParameter[] SpellRuleParameters;
    }

    public struct SpellRule
    {
        public byte Version;
        public int Unknown2;
        public int Unknown3;
        public byte Unknown4;
        public BfString Unknown5;
        public BfString Unknown6;
        public SpellRuleParameterContainer SpelllRules;
    }

    public struct _9031
    {
        public SpellRule[] Entries;
    }
}
