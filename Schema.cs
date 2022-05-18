using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ClsId = System.Int64;

namespace RDFWrappers
{
    abstract class Schema
    {
        public interface ClassProperty
        {
            public abstract string Name();
            public abstract string CSDataType();
            public abstract bool IsObject();
            public abstract Int64 CardinalityMin();
            public abstract Int64 CardinalityMax();
            public abstract List<ClsId> Restrictions();
        }

        public class Class
        {
            public ClsId id;
            public List<ClsId> parents = new List<ClsId>();
            public List<ClassProperty> properties = new List<ClassProperty>();
        }

        public SortedList<string, Class> m_classes = new SortedList<string, Class>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clsid"></param>
        /// <returns></returns>
        abstract public string GetNameOfClass(Int64 clsid);

        /// <summary>
        /// 
        /// </summary>
        public void ToConsole()
        {
            Console.WriteLine("-------- Extracted shcema ----------------");
            foreach (var cls in m_classes)
            {
                Console.Write("{0}:", cls.Key);
                foreach (var parent in cls.Value.parents)
                {
                    Console.Write(" {0}", GetNameOfClass(parent));
                }
                Console.WriteLine();

                foreach (var prop in cls.Value.properties)
                {
                    Console.Write("    {0}: {1}", prop.Name(), prop.CSDataType()!=null ? prop.CSDataType() : "<not supported>");
                    if (prop.Restrictions().Count > 0)
                    {
                        Console.Write("[");
                        foreach (var r in prop.Restrictions())
                        {
                            string n = GetNameOfClass(r);
                            Console.Write("{0} ", n);
                        }
                        Console.Write("]");
                    }
                    Console.WriteLine(" ({0}-{1})", prop.CardinalityMin(), prop.CardinalityMax());
                }
            }
            Console.WriteLine();
        }

    }
}
