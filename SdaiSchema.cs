using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDF;

namespace RDFWrappers
{
    class SdaiSchema : Schema
    {
        private Int64 m_model = 0;

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

                var cls = new Class();
                cls.id = entityId;

                CollectClassParents(cls);

                //CollectClassProperties(cls);

                m_classes.Add(name, cls);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cls"></param>
        private void CollectClassParents (Class cls)
        {
            var parentId = ifcengine.engiGetEntityParent(cls.id);
            if (parentId != 0)
            {
                cls.parents.Add(parentId);
            }
        }
    }
}
