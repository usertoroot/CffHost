using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHost
{
    public struct Token
    {
        public byte Unknown1;
        public byte Unknown2;
    }

    public struct TokenContainer
    {
        public byte Version;
        public Token[] Tokens;
    }

    public struct CardConditionReferenceContainer
    {
        public byte Version;
        public int[] CardConditionIdentifiers;
    }
    
    public struct CardSpellReferenceContainer
    {
        public byte Version;
        public int[] SpellIdentifiers;
    }

    public struct Card
    {
        public byte Version;
        public int Unknown1;
        public int Unknown2;
        public byte Unknown3;
        public byte Unknown4;
        public byte Unknown5;
        public byte Unknown6;
        public byte Unknown7;
        public int Unknown8;
        public int Unknown9;
        public byte Unknown10;
        public byte Unknown11;
        public byte Unknown12;
        public byte Unknown13;
        public byte Unknown14;
        public byte Unknown15;
        public byte Unknown16;
        public byte Unknown17;
        public byte Unknown18;
        public byte Unknown19;
        public byte Unknown20;

        public TokenContainer TokenContainer;
        public CardConditionReferenceContainer CardConditionReferenceContainer;
        public CardSpellReferenceContainer CardSpellReferenceContainer;

        public int[] Unknown21;
    }

    public struct _9016
    {
        public Card[] Entries;
    }
}
