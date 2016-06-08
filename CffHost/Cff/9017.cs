using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHost
{
    public struct CardCondition
    {
        public int Unknown1;
        public int Unknown2;
        public int Unknown3;
    }
    
    public struct CardConditionContainer
    {
        public byte Version;
        public CardCondition[] CardConditions;
    }

    public struct _9017Entry
    {
        public byte Version;
        public int Unknown1;
        public int Unknown2;
        public byte Unknown3;
        public int Unknown4;
        public CardConditionContainer CardConditionContainer;
    }

    public struct _9017
    {
        public _9017Entry[] Entries;
    }
}
