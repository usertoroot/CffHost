using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHost
{
    public struct UnitArchive
    {
        public byte Version;
        public int WeaponType;
        public float Damage;
        public float MinimalRange;
        public float MaximalRange;
    }

    public struct Mesh
    {
        public int Unknown1;
        public byte Unknown2;
        public BfString Unknown3;
    }

    public struct MeshContainer
    {
        public byte Version;
        public Mesh[] Meshes;
    }

    public struct Unit
    {
        public byte Version;
        public int Unknown1;
        public float Unknown2;
        public byte Unknown3;
        public float Unknown4;
        public byte Unknown5;
        public short Unknown6;
        public byte Unknown7;
        public short Unknown8;
        public UnitArchive Archive;
        public float Unknown10;
        public MeshContainer MeshContainer;
    }

    public struct _9001
    {
        public Unit[] Entries;
    }
}
