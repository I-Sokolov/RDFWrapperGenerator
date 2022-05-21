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
            string name;
            Int64 attrType;
            List<ClsId> restrictions = new List<ClsId>();

            public SdaiClassProperty (string name, Int64 attrType)
            {
                this.name = name;
                this.attrType = attrType;
            }

            public string Name() { return name; }

            public string CSDataType()
            {
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
            }
            
            public bool IsObject() { return attrType == ifcengine.sdaiINSTANCE; }
            public Int64 CardinalityMin() { return 1; }
            public Int64 CardinalityMax() { return 1; }
            public List<ClsId> Restrictions() { return restrictions; }
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

            //ifcengine.engiGetEntityName(clsid, ifcengine.sdaiUNICODE, out ptrName);
            //name = System.Runtime.InteropServices.Marshal.PtrToStringUni(ptrName);
            //ifcengine.engiGetEntityName(clsid, ifcengine.sdaiUNICODE, out ptrName);
            //name = System.Runtime.InteropServices.Marshal.PtrToStringUni(ptrName);

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
                IntPtr attributeNamePtr = IntPtr.Zero;
                ifcengine.engiGetEntityArgumentName(cls.id, i, ifcengine.sdaiSTRING, out attributeNamePtr);
                string attributeName = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(attributeNamePtr);

                Int64 attributeType = 0;
                ifcengine.engiGetEntityArgumentType(cls.id, i, out attributeType);

                var prop = new SdaiClassProperty(attributeName, attributeType);
                cls.properties.Add(prop);
            }
        }
    }
}
