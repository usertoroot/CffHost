using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHost
{
    public struct ArmorWeaponBalancing
    {
        public byte Version;
        public byte Unknown1;
        public byte Unknown2;
    }

    public struct _9023
    {
        public ArmorWeaponBalancing[] Entries;
    }
}
