using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHost
{
    public static class BinaryWriterExtension
    {
        public static void WriteBfString(this BinaryWriter writer, string text)
        {
            if (string.IsNullOrEmpty(text))
                writer.Write((int)0);
            else
            {
                writer.Write((int)text.Length);
                writer.Write(Encoding.ASCII.GetBytes(text));
            }
        }

        public static void WriteStructures<T>(this BinaryWriter writer, T[] value)
        {
            writer.Write((int)value.Length);
            for (int i = 0; i < value.Length; i++)
                writer.WriteStructure(value[i]);
        }

        public static void WriteStructures<T>(this BinaryWriter writer, List<T> value)
        {
            writer.Write((int)value.Count);
            for (int i = 0; i < value.Count; i++)
                writer.WriteStructure(value[i]);
        }

        public static void WriteStructure(this BinaryWriter writer, object value)
        {
            Type t = value.GetType();
            if (t.IsCustomValueType())
            {
                foreach (var f in t.GetFields())
                {
                    if (f.FieldType.IsCustomValueType())
                        writer.WriteStructure(f.GetValue(value));
                    else if (f.FieldType.IsArray)
                    {
                        Array a = (Array)f.GetValue(value);
                        if (a == null || a.Length <= 0)
                            writer.Write((int)0);
                        else
                        {
                            writer.Write((int)a.Length);

                            for (int i = 0; i < a.Length; i++)
                                writer.WriteStructure(a.GetValue(i));
                        }
                    }
                    else if (f.FieldType.IsGenericType && f.FieldType.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        IList genericList = (IList)f.GetValue(value);
                        if (genericList == null || genericList.Count <= 0)
                            writer.Write((int)0);
                        else
                        {
                            writer.Write((int)genericList.Count);

                            for (int i = 0; i < genericList.Count; i++)
                                writer.WriteStructure(genericList[i]);
                        }
                    }
                    else
                        writer.WriteStructureValue(f.GetValue(value), f.FieldType);
                }
            }
            else
                writer.WriteStructureValue(value, t);
        }

        public static void WriteStructureValue(this BinaryWriter writer, object value, Type t)
        {
            if (t == typeof(string))
                writer.WriteBfString((string)value);
            else if (t == typeof(sbyte))
                writer.Write((sbyte)value);
            else if (t == typeof(byte))
                writer.Write((byte)value);
            else if (t == typeof(short))
                writer.Write((short)value);
            else if (t == typeof(ushort))
                writer.Write((ushort)value);
            else if (t == typeof(int))
                writer.Write((int)value);
            else if (t == typeof(uint))
                writer.Write((uint)value);
            else if (t == typeof(long))
                writer.Write((long)value);
            else if (t == typeof(ulong))
                writer.Write((ulong)value);
            else
                throw new Exception("Unknown type in structure.");
        }
    }
}
