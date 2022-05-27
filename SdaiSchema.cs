using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDF;

using SdaiInstance = System.Int64;

namespace RDFWrappers
{
    class SdaiSchema
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clsid"></param>
        /// <returns></returns>
        static public string GetNameOfEntity(SdaiInstance clsid)
        {
            if (clsid!=0)
            {
                var ptrName = IntPtr.Zero;
                ifcengine.engiGetEntityName(clsid, ifcengine.sdaiSTRING, out ptrName);
                var name = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(ptrName);

                return name;
            }
            else
            {
                return "";
            }
        }

        public class DefinitionsList : SortedList<string, SdaiInstance> { }

        public class Definitions : Dictionary <ifcengine.enum_express_definition, DefinitionsList> { }

        /// <summary>
        /// 
        /// </summary>
        public Definitions m_definitions = new Definitions();

        private Int64 m_model = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="schemaName"></param>
        public SdaiSchema (string schemaName)
        {
            m_model = ifcengine.sdaiCreateModelBN(1, "", schemaName);

            m_definitions.Add(ifcengine.enum_express_definition.__DEFINED_TYPE, new DefinitionsList());
            m_definitions.Add(ifcengine.enum_express_definition.__ENUM, new DefinitionsList());
            m_definitions.Add(ifcengine.enum_express_definition.__SELECT, new DefinitionsList());
            m_definitions.Add(ifcengine.enum_express_definition.__ENTITY, new DefinitionsList());

            CollectDefinitions();
        }

        /// <summary>
        /// 
        /// </summary>
        private void CollectDefinitions()
        {
            Int64 it = 0;
            while (0 != (it = ifcengine.engiNextDefinitionIterator(m_model, it)))
            {
                SdaiInstance defenition = ifcengine.engiGetDefinitionFromIterator(m_model, it);

                var name = GetNameOfEntity(defenition);
                var type = ifcengine.engiGetDefinitionType(defenition);

                var defs = m_definitions[type];
                defs.Add(name, defenition);
            }
        }

        public void ToConsole()
        {
            Console.WriteLine("-------- Extracted shcema ----------------");

            Console.WriteLine("============= Defined Types ====================");
            var definedTypes = m_definitions[ifcengine.enum_express_definition.__DEFINED_TYPE];
            if (definedTypes != null)
                foreach (var def in definedTypes)
                {
                    var type = new SdaiDefinedType(def.Key, def.Value);
                    Console.WriteLine (type.ToString());
                }

            Console.WriteLine("============= Enumerations ====================");
            var enums = m_definitions[ifcengine.enum_express_definition.__ENUM];
            if (enums != null)
                foreach (var enm in enums)
                {
                    var e = new SdaiEnum(enm.Key, enm.Value);
                    Console.WriteLine (e.ToString());
                }

            Console.WriteLine("============= Selects ====================");
            var sels = m_definitions[ifcengine.enum_express_definition.__SELECT];
            if (sels != null)
                foreach (var sel in sels)
                {
                    var s = new SdaiSelect(sel.Key, sel.Value); ;
                    Console.WriteLine(s.ToString());
                }

            Console.WriteLine("============= Entities ====================");
            var entities = m_definitions[ifcengine.enum_express_definition.__ENTITY];
            if (entities != null)
                foreach (var entity in entities)
                {
                    var e = new SdaiEntity(entity.Key, entity.Value); ;
                    Console.WriteLine(e.ToString());
                }
        }
    
    }
}
