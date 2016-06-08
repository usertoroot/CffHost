using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHost
{
    public struct BoosterPack
    {
        public byte Version;
        public int Unknown1;
    }

    public struct _9055
    {
        public BoosterPack[] Entries;
    }
}
