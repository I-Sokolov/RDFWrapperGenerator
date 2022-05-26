using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDF;

using ClsId = System.Int64;

namespace RDFWrappers
{
    class SdaiSchema : Schema
    {
        private Int64 m_model = 0;

        public class SdaiClassProperty : Schema.ClassProperty
        {
            public string propertyName;
            public ClsId definingEntity;
            public bool inverse;
            public ifcengine.enum_express_attr_type attrType;
            public ClsId domainEntity;
            public ifcengine.enum_express_aggr aggrType;
            public bool nestedAggr;
            public Int64 cardinalityMin;
            public Int64 cardinalityMax;
            public bool optional;
            public bool unique;

            public SdaiClassProperty ()
            {
            }

            public string Name() { return propertyName; }

            public string CSDataType()
            {
                /*
                switch (attrType)
                {
                    case ifcengine.sdaiADB:
                        return null;
                    case ifcengine.sdaiAGGR:
                        return null;
                    case ifcengine.sdaiBINARY:
                        return null;
                    case ifcengine.sdaiBOOLEAN:
                        return "bool";
                    case ifcengine.sdaiENUM:
                        return null;
                    case ifcengine.sdaiINSTANCE:
                        return "Instance";
                    case ifcengine.sdaiINTEGER:
                        return "Int64";
                    case ifcengine.sdaiLOGICAL:
                        return "Int64";
                    case ifcengine.sdaiREAL:
                        return "double";
                    case ifcengine.sdaiSTRING:
                        return "string";
                    case ifcengine.sdaiUNICODE:
                        return null;
                    case ifcengine.sdaiEXPRESSSTRING:
                        return null;
                    case ifcengine.engiGLOBALID:
                        return "string";
                    default:
                        //System.Diagnostics.Debug.Assert(false);
                        return null; //usupporrted type
                }
                */
                return null;
            }
            
            public bool IsObject() { return false; }
            public Int64 CardinalityMin() { return 1; }
            public Int64 CardinalityMax() { return 1; }
            public List<ClsId> Restrictions() { return null; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="schemaName"></param>
        public SdaiSchema (string schemaName)
        {
            m_model = ifcengine.sdaiCreateModelBN(1, "", schemaName);

            CollectClasses();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clsid"></param>
        /// <returns></returns>
        override public string GetNameOfClass(Int64 clsid)
        {
            var ptrName = IntPtr.Zero;
            ifcengine.engiGetEntityName(clsid, ifcengine.sdaiSTRING, out ptrName);
            var name = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(ptrName);

            ifcengine.engiGetEntityName(clsid, ifcengine.sdaiUNICODE, out ptrName);
            name = System.Runtime.InteropServices.Marshal.PtrToStringUni(ptrName);
            ifcengine.engiGetEntityName(clsid, ifcengine.sdaiUNICODE, out ptrName);
            name = System.Runtime.InteropServices.Marshal.PtrToStringUni(ptrName);

            return name;
        }

        /// <summary>
        /// 
        /// </summary>
        private void CollectClasses()
        {
            var cnt = ifcengine.engiGetEntityCount(m_model);
            for (int i = 0; i < cnt; i++)
            {
                Int64 entityId = ifcengine.engiGetEntityElement(m_model, i);

                var name = GetNameOfClass(entityId);

                if (name.Equals ("B_Spline_Function", StringComparison.OrdinalIgnoreCase))
                {
                    var cls = new Class();
                    cls.id = entityId;

                    CollectClassParents(cls);

                    CollectClassProperties(cls);

                    m_classes.Add(name, cls);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cls"></param>
        private void CollectClassParents(Class cls)
        {
            int ind = 0;

            while (true)
            {
                var parentId = ifcengine.engiGetEntityParentEx(cls.id, ind++);
                if (parentId == 0)
                    break;
                cls.parents.Add(parentId);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cls"></param>
        private void CollectClassProperties(Class cls)
        {
            var nattr = ifcengine.engiGetEntityNoArguments(cls.id);
            for (int i = 0; i<nattr; i++)
            {
                IntPtr ptrName = IntPtr.Zero;
                ClsId definingEntity, domainEntity;
                ifcengine.enum_express_attr_type attrType;
                ifcengine.enum_express_aggr aggrType;
                byte inverse, nestedAggr, optional, unique;
                Int64 cardinalityMin, cardinalityMax;

                byte ok = ifcengine.engiGetEntityProperty
                                (cls.id, i,
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
                    var prop = new SdaiClassProperty
                    {
                        propertyName = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(ptrName),
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

                    cls.properties.Add(prop);
                }
            }
        }
    }
}
