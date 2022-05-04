﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDFWrappers
{
    class Schema
    {

        class Property
        {
            public string name;
            public Int64 id;
            public Int64 type;
            public List<Int64> resrtictions = new List<Int64>();

            public string TypeName()
            {
                switch (type)
                {
                    case Engine.x86_64.OBJECTPROPERTY_TYPE:             return "Instance";
                    case Engine.x86_64.DATATYPEPROPERTY_TYPE_BOOLEAN:   return "bool";
                    case Engine.x86_64.DATATYPEPROPERTY_TYPE_CHAR:      return "string";
                    case Engine.x86_64.DATATYPEPROPERTY_TYPE_INTEGER:   return "long";
                    case Engine.x86_64.DATATYPEPROPERTY_TYPE_DOUBLE:    return "double";
                }
                throw new ApplicationException("Unknown property type");
            }

            public bool IsObject() { return type == Engine.x86_64.OBJECTPROPERTY_TYPE; }
        }

        class PropertyCardinality
        {
            public Property prop;
            public Int64 min;
            public Int64 max;
        }

        class Class
        {
            public string name;
            public Int64 id;
            public List<Int64> parents = new List<Int64>();
            public List<PropertyCardinality> properties = new List<PropertyCardinality>();
        }

        private Int64 m_model = 0;
        private List<Class> m_classes;
        private List<Property> m_properties;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public Schema (Int64 model)
        {
            m_model = model;

            m_properties = CollectAllProperties();
            m_classes = CollectClasses();
        }

        private string GetNameOfClass (Int64 clsid)
        {
            IntPtr namePtr = IntPtr.Zero;
            Engine.x86_64.GetNameOfClass(clsid, out namePtr);
            return System.Runtime.InteropServices.Marshal.PtrToStringAnsi(namePtr);
        }

        private List<Class> CollectClasses ()
        {
            var list = new List<Class>();

            Int64 it = Engine.x86_64.GetClassesByIterator(m_model, 0);
            while (it != 0)
            {
                var cls = new Class();
                cls.name = GetNameOfClass(it);
                cls.id = Engine.x86_64.GetClassByName(m_model, cls.name);
                System.Diagnostics.Debug.Assert(cls.id == it);
                
                Console.Write("{0}:", cls.name);
                
                CollectClassParents(cls);
                Console.WriteLine();

                CollectClassProperties(cls);

                list.Add(cls);

                it = Engine.x86_64.GetClassesByIterator(m_model, it);
            }

            return list;
        }

        private void CollectClassParents (Class cls)
        {
            Int64 parent = Engine.x86_64.GetParentsByIterator(cls.id, 0);
            while (parent!=0)
            {
                cls.parents.Add(parent);
                Console.Write(" {0}", GetNameOfClass (parent));

                parent = Engine.x86_64.GetParentsByIterator(cls.id, parent);
            }
        }

        private void CollectClassProperties (Class cls)
        {
            foreach (var prop in m_properties)
            {
                Int64 min, max;
                Engine.x86_64.GetClassPropertyCardinalityRestriction(cls.id, prop.id, out min, out max);

                if (min >= 0 && max > 0)
                {
                    var clsprop = new PropertyCardinality();

                    clsprop.min = min;
                    clsprop.max = max;

                    cls.properties.Add(clsprop);

                    Console.Write("    {0}: {1}", prop.name, prop.TypeName());
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
        }

        private List<Property> CollectAllProperties()
        {
            var list = new List<Property>();

            Int64 it = Engine.x86_64.GetPropertiesByIterator(m_model, 0);
            while (it != 0)
            {
                IntPtr namePtr = IntPtr.Zero;
                Engine.x86_64.GetNameOfProperty(it, out namePtr);

                Property prop = new Property();
                prop.name = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(namePtr);
                prop.id = Engine.x86_64.GetPropertyByName(m_model, prop.name);
                prop.type = Engine.x86_64.GetPropertyType(prop.id);

                var restrict = Engine.x86_64.GetRangeRestrictionsByIterator(prop.id, 0);
                while (restrict != 0)
                {
                    prop.resrtictions.Add(restrict);
                    restrict = Engine.x86_64.GetRangeRestrictionsByIterator(prop.id, restrict);
                }

                list.Add(prop);

                it = Engine.x86_64.GetPropertiesByIterator(m_model, it);
            }

            return list;

        }

    }
}
