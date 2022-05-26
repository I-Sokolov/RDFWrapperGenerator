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
        static public string GetNameOfEntity(Int64 clsid)
        {
            var ptrName = IntPtr.Zero;
            ifcengine.engiGetEntityName(clsid, ifcengine.sdaiSTRING, out ptrName);
            var name = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(ptrName);

            return name;
        }

        public class DefinedType
        {
            public SdaiInstance id;
        };

        public class Enumeration
        {
            public SdaiInstance id;
        }

        public class Select
        {
            public SdaiInstance id;
        }

        /// <summary>
        /// 
        /// </summary>
        public class Entity
        {
            public SdaiInstance id;
            public List<SdaiInstance> supertypes = new List<SdaiInstance>();
            public List<SdaiAttribute> attributes = new List<SdaiAttribute>();
        }

        public SortedList<string, Entity> m_entities = new SortedList<string, Entity>();

        private Int64 m_model = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="schemaName"></param>
        public SdaiSchema (string schemaName)
        {
            m_model = ifcengine.sdaiCreateModelBN(1, "", schemaName);

            CollectEntities();
        }

        /// <summary>
        /// 
        /// </summary>
        private void CollectEntities()
        {
            //var cnt = ifcengine.engiGetEntityCount(m_model);
            //for (int i = 0; i < cnt; i++)
            Int64 it = 0;
            while (0!=(it = ifcengine.engiNextDefinitionIterator (m_model, it)))
            {
                //Int64 entityId = ifcengine.engiGetEntityElement(m_model, i);
                Int64 entityId = ifcengine.engiGetDefinitionFromIterator(m_model, it);

                var name = GetNameOfEntity(entityId);

                //if (name.Equals ("B_Spline_Function", StringComparison.OrdinalIgnoreCase))
                {
                    var entity = new Entity();
                    entity.id = entityId;

                    CollectSupertypes(entity);

                    CollectAttributes(entity);

                    m_entities.Add(name, entity);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        private void CollectSupertypes(Entity entity)
        {
            int ind = 0;

            while (true)
            {
                var parentId = ifcengine.engiGetEntityParentEx(entity.id, ind++);
                if (parentId == 0)
                    break;
                entity.supertypes.Add(parentId);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cls"></param>
        private void CollectAttributes(Entity entity)
        {
            var nattr = ifcengine.engiGetEntityNoArguments(entity.id);
            for (int i = 0; i<nattr; i++)
            {
                IntPtr ptrName = IntPtr.Zero;
                Int64 definingEntity, domainEntity;
                ifcengine.enum_express_attr_type attrType;
                ifcengine.enum_express_aggr aggrType;
                byte inverse, nestedAggr, optional, unique;
                Int64 cardinalityMin, cardinalityMax;

                byte ok = ifcengine.engiGetEntityProperty
                                (entity.id, i,
                                out ptrName,
                                out definingEntity, out inverse,
                                out attrType, out domainEntity,
                                out aggrType, out nestedAggr,
                                out cardinalityMin, out cardinalityMax,
                                out optional, out unique
                                );
                System.Diagnostics.Debug.Assert(ok!=0);

                if (ok != 0)
                {
                    var prop = new SdaiAttribute
                    {
                        name = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(ptrName),
                        definingEntity = definingEntity,
                        inverse = inverse != 0 ? true : false,
                        attrType = attrType,
                        domainEntity = domainEntity,
                        aggrType = aggrType,
                        nestedAggr = nestedAggr != 0 ? true : false,
                        cardinalityMin = cardinalityMin,
                        cardinalityMax = cardinalityMax,
                        optional = optional != 0 ? true : false,
                        unique = unique != 0 ? true : false
                    };

                    entity.attributes.Add(prop);
                }
            }
        }

        public void ToConsole()
        {
            Console.WriteLine("-------- Extracted shcema ----------------");
            foreach (var entity in m_entities)
            {
                //if (entity.Key == "IfcRoot")
                {
                    var definition = ifcengine.engiGetDefinitionType(entity.Value.id);
                    
                    Console.Write("{0} {1}:", entity.Key, definition.ToString());
                    foreach (var parent in entity.Value.supertypes)
                    {
                        Console.Write(" {0}", GetNameOfEntity(parent));
                    }
                    Console.WriteLine();

                    foreach (var attr in entity.Value.attributes)
                    {
                        Console.WriteLine("    {0}", attr.ToString());
                    }
                }
            }
            Console.WriteLine();
        }

    }
}
