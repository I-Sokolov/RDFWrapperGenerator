using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDFWrappers
{
    class Schema
    {
        public class Property
        {
            public Int64 id;
            public Int64 type;
            public List<Int64> resrtictions = new List<Int64>();

            public string DataType(bool cs)
            {
                switch (type)
                {
                    case RDF.engine.OBJECTPROPERTY_TYPE: return "Instance";
                    case RDF.engine.DATATYPEPROPERTY_TYPE_BOOLEAN: return "bool";
                    case RDF.engine.DATATYPEPROPERTY_TYPE_CHAR: return cs ? "string" : "const char* const";
                    case RDF.engine.DATATYPEPROPERTY_TYPE_INTEGER: return cs ? "Int64" : "int64_t";
                    case RDF.engine.DATATYPEPROPERTY_TYPE_DOUBLE: return "double";
                }
                throw new ApplicationException("Unknown property type");
            }

            public bool IsObject() { return type == RDF.engine.OBJECTPROPERTY_TYPE; }
        }

        public class ClassProperty
        {
            public string name;
            public Int64 min;
            public Int64 max;
        }

        public class Class
        {
            public Int64 id;
            public List<Int64> parents = new List<Int64>();
            public List<ClassProperty> properties = new List<ClassProperty>();
        }

        public SortedList<string, Class> m_classes = new SortedList<string, Class>();
        public SortedList<string, Property> m_properties = new SortedList<string, Property>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clsid"></param>
        /// <returns></returns>
        virtual public string GetNameOfClass(Int64 clsid)
        {
            throw new ApplicationException ("Override " + System.Reflection.MethodBase.GetCurrentMethod().Name);            
        }


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

                foreach (var clsprop in cls.Value.properties)
                {
                    var prop = m_properties[clsprop.name];

                    Console.Write("    {0}: {1}", clsprop.name, prop.DataType(true));
                    if (prop.resrtictions.Count > 0)
                    {
                        Console.Write("[");
                        foreach (var r in prop.resrtictions)
                        {
                            string n = GetNameOfClass(r);
                            Console.Write("{0} ", n);
                        }
                        Console.Write("]");
                    }
                    Console.WriteLine(" ({0}-{1})", clsprop.min, clsprop.max);
                }
            }
            Console.WriteLine();
        }

    }
}
