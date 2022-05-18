using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDFWrappers
{
    class RdfSchema : Schema
    {
        private Int64 m_model = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public RdfSchema (Int64 model)
        {
            m_model = model;

            CollectProperties();
            CollectClasses();
        }

        override public string GetNameOfClass (Int64 clsid)
        {
            IntPtr namePtr = IntPtr.Zero;
            RDF.engine.GetNameOfClass(clsid, out namePtr);
            return System.Runtime.InteropServices.Marshal.PtrToStringAnsi(namePtr);
        }

        private void CollectClasses ()
        {
            Int64 clsid = RDF.engine.GetClassesByIterator(m_model, 0);
            while (clsid != 0)
            {
                string name = GetNameOfClass(clsid);

                var cls = new Class();
                cls.id = clsid;
                
                CollectClassParents(cls);

                CollectClassProperties(cls);

                m_classes.Add(name, cls);

                clsid = RDF.engine.GetClassesByIterator(m_model, clsid);
            }
        }

        private void CollectClassParents (Class cls)
        {
            Int64 parent = RDF.engine.GetClassParentsByIterator(cls.id, 0);
            while (parent!=0)
            {
                cls.parents.Add(parent);
                parent = RDF.engine.GetClassParentsByIterator(cls.id, parent);
            }
        }

        private void CollectClassProperties (Class cls)
        {
            foreach (var prop in m_properties)
            {
                Int64 min, max;
                RDF.engine.GetClassPropertyCardinalityRestriction(cls.id, prop.Value.id, out min, out max);

                if (min >= 0)
                {
                    var clsprop = new ClassProperty();

                    clsprop.name = prop.Key;
                    clsprop.min = min;
                    clsprop.max = max;

                    cls.properties.Add(clsprop);
                }
            }
        }

        private void CollectProperties()
        {
            Int64 propid = RDF.engine.GetPropertiesByIterator(m_model, 0);
            while (propid != 0)
            {
                IntPtr namePtr = IntPtr.Zero;
                RDF.engine.GetNameOfProperty(propid, out namePtr);

                string name = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(namePtr);

                Property prop = new Property();
                prop.id = propid;
                prop.type = RDF.engine.GetPropertyType(prop.id);

                var restrict = RDF.engine.GetRangeRestrictionsByIterator(prop.id, 0);
                while (restrict != 0)
                {
                    prop.resrtictions.Add(restrict);
                    restrict = RDF.engine.GetRangeRestrictionsByIterator(prop.id, restrict);
                }
                System.Diagnostics.Debug.Assert( //other cases not testes
                    prop.resrtictions.Count == 0 && !prop.IsObject()
                    || prop.resrtictions.Count == 1 && prop.IsObject());

                m_properties.Add(name, prop);

                propid = RDF.engine.GetPropertiesByIterator(m_model, propid);
            }
        }
    }
}
