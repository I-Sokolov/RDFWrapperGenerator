using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ClsId = System.Int64;

namespace RDFWrappers
{
    class RdfSchema : Schema
    {
        public class PropertyDefinition
        {
            public Int64 id;
            public Int64 type;
            public List<ClsId> resrtictions = new List<ClsId>();

            public string DataType()
            {
                switch (type)
                {
                    case RDF.engine.OBJECTPROPERTY_TYPE: return "Instance";
                    case RDF.engine.DATATYPEPROPERTY_TYPE_BOOLEAN: return "bool";
                    case RDF.engine.DATATYPEPROPERTY_TYPE_CHAR: return "string";
                    case RDF.engine.DATATYPEPROPERTY_TYPE_INTEGER: return "Int64";
                    case RDF.engine.DATATYPEPROPERTY_TYPE_DOUBLE: return "double";
                }
                throw new ApplicationException("Unknown property type");
            }

            public bool IsObject() { return type == RDF.engine.OBJECTPROPERTY_TYPE; }

            public List<ClsId> Restrictions() { return resrtictions; }
        }

        public class RdfClassProperty : Schema.ClassProperty
        {
            RdfSchema schema;
            string name;
            Int64 cardinalityMin;
            Int64 cardinalityMax;

            public RdfClassProperty (RdfSchema schema, string name, Int64 cardMin, Int64 cardMax)
            {
                this.schema = schema;
                this.name = name;
                cardinalityMin = cardMin;
                cardinalityMax = cardMax;
            }

            public string Name() { return name; }
            public string CSDataType() { return Definition().DataType(); }
            public bool IsObject() { return Definition().IsObject(); }
            public Int64 CardinalityMin() { return cardinalityMin; }
            public Int64 CardinalityMax() { return cardinalityMax; }
            public List<ClsId> Restrictions() { return Definition().Restrictions(); }

            private PropertyDefinition Definition()
            {
                return schema.m_properties[name];
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private Int64 m_model = 0;

        public SortedList<string, PropertyDefinition> m_properties = new SortedList<string, PropertyDefinition>();


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
                    var clsprop = new RdfClassProperty(this, prop.Key, min, max);
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

                PropertyDefinition prop = new PropertyDefinition();
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
