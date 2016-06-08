using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHost
{
    public static class BinaryReaderExtension
    {
        public static string ReadBfString(this BinaryReader reader)
        {
            int length = reader.ReadInt32();
            return Encoding.ASCII.GetString(reader.ReadBytes(length));
        }

        public static object ReadStructure(this BinaryReader reader, Type t)
        {
            object o = Activator.CreateInstance(t);

            if (t.IsValueType && !t.IsEnum)
            {
                if (t == typeof(Byte))
                {
                    return reader.ReadByte();
                }
                else if (t == typeof(Int16))
                {
                    return reader.ReadInt16();
                }
                else if(t == typeof(Int32))
                {
                    return reader.ReadInt32();
                }
                else if (t == typeof(Int64))
                {
                    return reader.ReadInt64();
                }
                else if (t == typeof(float))
                {
                    return reader.ReadSingle();
                }
                else
                {
                    foreach (var f in t.GetFields())
                    {
                        if (f.FieldType.IsCustomValueType())
                            f.SetValue(o, reader.ReadStructure(f.FieldType));
                        else if (f.FieldType.IsArray)
                        {
                            int length;
                            object[] attrs = f.GetCustomAttributes(typeof(LengthTypeAttribute), false);
                            if (attrs.Length > 0)
                            {
                                LengthTypeAttribute lengthTypeAttribute = (LengthTypeAttribute)attrs[0];
                                switch (lengthTypeAttribute.LengthType)
                                {
                                    case LengthType.Int8:
                                        length = reader.ReadByte();
                                        break;
                                    case LengthType.Int16:
                                        length = reader.ReadInt16();
                                        break;
                                    case LengthType.Int32:
                                        length = reader.ReadInt32();
                                        break;
                                    default:
                                        length = reader.ReadInt32();
                                        break;
                                }
                            }
                            else
                                length = reader.ReadInt32();

                            Array a = Array.CreateInstance(f.FieldType.GetElementType(), length);

                            for (int i = 0; i < length; i++)
                                a.SetValue(reader.ReadStructure(f.FieldType.GetElementType()), i);

                            f.SetValue(o, a);
                        }
                        else
                            f.SetValue(o, reader.ReadStructureValue(f.FieldType));
                    }

                    return o;
                }
            }
            else
                return reader.ReadStructureValue(t);
        }

        public static object ReadStructureValue(this BinaryReader reader, Type t)
        {
            if (t == typeof(string))
                return reader.ReadBfString();
            else if (t == typeof(byte))
                return reader.ReadByte();
            else if (t == typeof(short))
                return reader.ReadInt16();
            else if (t == typeof(int))
                return reader.ReadInt32();
            else if (t == typeof(long))
                return reader.ReadInt64();
            else if (t == typeof(float))
                return reader.ReadSingle();
            else
                throw new Exception("Unknown type in structure.");
        }

        public static T ReadStructure<T>(this BinaryReader reader) where T : struct
        {
            Type t = typeof(T);
            return (T)reader.ReadStructure(t);
        }

        public static T[] ReadStructures<T>(this BinaryReader reader) where T : struct
        {
            Type t = typeof(T);
            int length = reader.ReadInt32();

            T[] ts = new T[length];

            for (int i = 0; i < length; i++)
                ts[i] = (T)reader.ReadStructure(t);

            return ts;
        }
    }
}
