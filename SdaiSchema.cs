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

        public class Definitions : Dictionary <enum_express_declaration, DefinitionsList> { }

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

            m_definitions.Add(enum_express_declaration.__DEFINED_TYPE, new DefinitionsList());
            m_definitions.Add(enum_express_declaration.__ENUM, new DefinitionsList());
            m_definitions.Add(enum_express_declaration.__SELECT, new DefinitionsList());
            m_definitions.Add(enum_express_declaration.__ENTITY, new DefinitionsList());

            CollectDefinitions();
        }

        /// <summary>
        /// 
        /// </summary>
        private void CollectDefinitions()
        {
            Int64 it = 0;
            while (0 != (it = ifcengine.engiGetNextDeclarationIterator(m_model, it)))
            {
                SdaiInstance defenition = ifcengine.engiGetDeclarationFromIterator(m_model, it);

                var name = GetNameOfEntity(defenition);
                var type = ifcengine.engiGetDeclarationType(defenition);

                var defs = m_definitions[type];
                defs.Add(name, defenition);
            }
        }

        public void ToConsole()
        {
            Console.WriteLine("-------- Extracted shcema ----------------");

            Console.WriteLine("============= Defined Types ====================");
            var definedTypes = m_definitions[enum_express_declaration.__DEFINED_TYPE];
            if (definedTypes != null)
                foreach (var def in definedTypes)
                {
                    var type = new SdaiDefinedType(def.Key, def.Value);
                    Console.WriteLine (type.ToString());
                }

            Console.WriteLine("============= Enumerations ====================");
            var enums = m_definitions[enum_express_declaration.__ENUM];
            if (enums != null)
                foreach (var enm in enums)
                {
                    var e = new SdaiEnum(enm.Key, enm.Value);
                    Console.WriteLine (e.ToString());
                }

            Console.WriteLine("============= Selects ====================");
            var sels = m_definitions[enum_express_declaration.__SELECT];
            if (sels != null)
                foreach (var sel in sels)
                {
                    var s = new SdaiSelect(sel.Key, sel.Value); ;
                    Console.WriteLine(s.ToString());
                }

            Console.WriteLine("============= Entities ====================");
            var entities = m_definitions[enum_express_declaration.__ENTITY];
            if (entities != null)
                foreach (var entity in entities)
                {
                    var e = new SdaiEntity(entity.Key, entity.Value); ;
                    Console.WriteLine(e.ToString());
                }
        }
    
    }
}
