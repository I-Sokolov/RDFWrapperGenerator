//
// Helper classes (C# wrappers)
//
using System;
using System.Diagnostics;
using RDF;

using SdaiModel = System.Int64;
using SdaiInstance = System.Int64;
using SdaiEntity = System.Int64;

namespace NAMESPACE_NAME
{
//## TEMPLATE: ClassForwardDeclaration (not really required in C#)
//## TEMPLATE: BeginDefinedTypes
//## TEMPLATE: DefinedType
//## TEMPLATE: BeginEnumerations

    //
    // Enumerations
    //
//## TEMPLATE: BeginEnumeration

    enum ENUMERATION_NAME
    {
//## EnumerationElement
        ENUMERATION_ELEMENT=1234,
//## EndEnumeration
        _NULL = -1
    };
//## TEMPLATE: BeginEntities

    //
    // Entities
    //
//## TEMPLATE: BeginEntity

    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class ENTITY_NAME
    /// You also can use object of this C# class instead of int64_t handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class ENTITY_NAME : /*PARENT_NAME*/Entity
    {
        /// <summary>
        /// Constructs object of this C# class that wraps existing instance
        /// </summary>
        public ENTITY_NAME(SdaiInstance instance, string entityName = null)
            : base(instance, entityName != null ? entityName : "ENTITY_NAME")
        {
        }

//## EntityCreateMethod
        /// <summary>
        /// Create new instace of ENTITY_NAME and returns object of this C++ class to interact with
        /// </summary>
        public static ENTITY_NAME Create(SdaiModel model) { SdaiInstance inst = ifcengine.sdaiCreateInstanceBN(model, "ENTITY_NAME"); Debug.Assert(inst!=0); return new ENTITY_NAME(inst); }

//## TEMPLATE StartPropertiesBlock
       //
       // Properties with known cardinality restrictions to PROPERTIES_OF_CLASS
       //

//## TEMPLATE: SetDataProperty
        ///<summary>Sets value of PROPERTY_NAME</summary>
        //public void set_PROPERTY_NAME(double value) { SetDatatypeProperty ("PROPERTY_NAME", value); }
//## TEMPLATE SetDataArrayProperty
        ///<summary>Sets values of PROPERTY_NAME. OWL cardinality CARDINALITY_MIN..CARDINALITY_MAX</summary>
        //public void set_PROPERTY_NAME(double[] values) { SetDatatypeProperty ("PROPERTY_NAME", values); }
//## TEMPLATE GetDataProperty
        ///<summary>Gets value of PROPERTY_NAME, returns null is the property was not set</summary>
        //public double? get_PROPERTY_NAME() { var arr = GetDatatypeProperty_double("PROPERTY_NAME"); return (arr != null && arr.Length > 0) ? (double?)arr[0] : null; }
//## TEMPLATE GetDataArrayProperty
        ///<summary>Gets values of PROPERTY_NAME. OWL cardinality CARDINALITY_MIN..CARDINALITY_MAX</summary>
        //public double[] get_PROPERTY_NAMEasType() { return GetDatatypeProperty_double("PROPERTY_NAME"); }
//## TEMPLATE: SetObjectProperty
        ///<summary>Sets relationship from this instance to an instance of Entity</summary>
        //public void set_PROPERTY_NAME(Entity instance) { SetObjectProperty("PROPERTY_NAME", instance); }
//## TEMPLATE SetObjectArrayProperty
        ///<summary>Sets relationships from this instance to an array of Entity. OWL cardinality CARDINALITY_MIN..CARDINALITY_MAX</summary>
        //public void set_PROPERTY_NAME(Entity[] instances) { SetObjectProperty("PROPERTY_NAME", instances); }
//## TEMPLATE GetObjectProperty
        ///<summary>Get related instance</summary>
        /*public Entity get_PROPERTY_NAMEasTYPe() 
        {
            var propId = GetPropertyId("PROPERTY_NAME");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Entity(values[0], null);
            }
            else
            {
                return null;
            }
        }*/
//## TEMPLATE GetObjectArrayProperty
        ///<summary>Get an array of related instances. OWL cardinality CARDINALITY_MIN..CARDINALITY_MAX</summary>
        /*public Entity[] get_PROPERTY_NAMEasTYPE() 
        {
            var propId = GetPropertyId("PROPERTY_NAME");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                var ret = new Entity[card];
                for (int i = 0; i < card; i++)
                {
                    ret[i] = new Entity(values[i], null);
                }

                return ret;
            }
            else
            {
                return null;
            }
        }*/
//## TEMPLATE GetObjectArrayPropertyInt64
        ///<summary>Get an array of handles of related instances. OWL cardinality CARDINALITY_MIN..CARDINALITY_MAX</summary>
        /*public Int64[] get_PROPERTY_NAME_Int64()  
        {
            var propId = GetPropertyId("PROPERTY_NAME");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                return values;
            }
            else
            {
                return null;
            }
        }*/
//## TEMPLATE: EndEntity
    }

//## TEMPLATE: EndFile 
    /// <summary>
    /// Provides utility methods to interact with a generic entity instnace
    /// You also can use object of this class instead of int64_t handle of the instance in any place where the handle is required
    /// </summary>
    public class Entity : IEquatable<Entity>, IComparable, IComparable<Entity>
    {
        /// <summary>
        /// underlyed instance handle
        /// </summary>
        protected SdaiInstance m_instance = 0;

        /// <summary>
        /// Constructs object that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Entity(SdaiInstance instance, string entityName)
        {
            m_instance = instance;
#if DEBUG
            if (m_instance != 0 && entityName != null)
            {
                SdaiEntity instType = ifcengine.sdaiGetInstanceType(m_instance);
                SdaiModel model = ifcengine.engiGetEntityModel(instType);
                SdaiEntity entity = ifcengine.sdaiGetEntity(model, entityName);
                Debug.Assert(instType == entity);
            }
#endif
        }


        /// <summary>
        /// Conversion to instance handle, so the object of the class can be used anywhere where a handle required
        /// </summary>
        public static implicit operator SdaiInstance(Entity instance) => instance.m_instance;

        /// <summary>
        /// 
        /// </summary>
        public static bool operator ==(Entity i1, Entity i2) => (Equals(i1, i2));

        /// <summary>
        /// 
        /// </summary>
        public static bool operator !=(Entity i1, Entity i2) => (!(i1 == i2));

        /// <summary>
        /// 
        /// </summary>
        public override bool Equals(Object obj)
        {
            return Equals(obj as Entity);
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Equals(Entity other)
        {
            return (other == null) ? false : (other.m_instance == m_instance);
        }

        /// <summary>
        /// 
        /// </summary>
        public int CompareTo(object obj)
        {
            return CompareTo(obj as Entity);
        }

        /// <summary>
        /// 
        /// </summary>
        public int CompareTo(Entity other)
        {
            return (other == null) ? 1 : m_instance.CompareTo(other.m_instance);
        }

        /// <summary>
        /// 
        /// </summary>
        public override int GetHashCode()
        {
            return m_instance.GetHashCode();
        }

#if _NOT_NOW
        /// <summary>
        /// Get property id from property name
        /// </summary>
        public Int64 GetPropertyId(string name, Int64 checkCardinality = -1)
        {
            var model = engine.GetModel(m_instance);
            System.Diagnostics.Debug.Assert(model != 0);

            var propId = engine.GetPropertyByName(model, name);
            System.Diagnostics.Debug.Assert(propId != 0);

#if DEBUG
            if (propId != 0)
            {
                var clsId = engine.GetInstanceClass(m_instance);
                Int64 minCard = 0, maxCard = 0;
                engine.GetPropertyRestrictionsConsolidated(clsId, propId, out minCard, out maxCard);
                System.Diagnostics.Debug.Assert(minCard >= 0); //property assigned to the class
                if (checkCardinality > 0)
                { //chek cardinatity when set property
                    System.Diagnostics.Debug.Assert(checkCardinality >= minCard && (checkCardinality <= maxCard || maxCard < 0)); //cardinality is in range
                }
            }
#endif

            return propId;
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetDatatypeProperty(string name, double value)
        {
            var propId = GetPropertyId(name, 1);
            var res = engine.SetDatatypeProperty(m_instance, propId, ref value, 1);
            System.Diagnostics.Debug.Assert(res == 0);
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetDatatypeProperty(string name, double[] values)
        {
            var propId = GetPropertyId(name, values.Length);
            var res = engine.SetDatatypeProperty(m_instance, propId, values, values.Length);
            System.Diagnostics.Debug.Assert(res == 0);
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetDatatypeProperty(string name, Int64 value)
        {
            var propId = GetPropertyId(name, 1);
            var res = engine.SetDatatypeProperty(m_instance, propId, ref value, 1);
            System.Diagnostics.Debug.Assert(res == 0);
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetDatatypeProperty(string name, Int64[] values)
        {
            var propId = GetPropertyId(name, values.Length);
            var res = engine.SetDatatypeProperty(m_instance, propId, values, values.Length);
            System.Diagnostics.Debug.Assert(res == 0);
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetDatatypeProperty(string name, bool value)
        {
            var propId = GetPropertyId(name, 1);
            byte v = (byte)(value ? 1 : 0);
            var res = engine.SetDatatypeProperty(m_instance, propId, ref v, 1);
            System.Diagnostics.Debug.Assert(res == 0);
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetDatatypeProperty(string name, bool[] values)
        {
            byte[] bytes = new byte[values.Length];
            for (int i = 0; i < values.Length; i++)
                bytes[i] = values[i] ? (byte)1 : (byte)0;

            var propId = GetPropertyId(name, values.Length);
            var res = engine.SetDatatypeProperty(m_instance, propId, bytes, values.Length);
            System.Diagnostics.Debug.Assert(res == 0);
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetDatatypeProperty(string name, string value)
        {
            var propId = GetPropertyId(name, 1);
            var res = engine.SetDatatypeProperty(m_instance, propId, ref value, 1);
            System.Diagnostics.Debug.Assert(res == 0);
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetDatatypeProperty(string name, string[] values)
        {
            var propId = GetPropertyId(name, values.Length);
            var res = engine.SetDatatypeProperty(m_instance, propId, values, values.Length);
            System.Diagnostics.Debug.Assert(res == 0);
        }

        /// <summary>
        /// 
        /// </summary>
        public double[] GetDatatypeProperty_double(string name)
        {
            var propId = GetPropertyId(name);

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetDatatypeProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new double[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                return values;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Int64[] GetDatatypeProperty_Int64(string name)
        {
            var propId = GetPropertyId(name);

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetDatatypeProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                return values;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool[] GetDatatypeProperty_bool(string name)
        {
            var propId = GetPropertyId(name);

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetDatatypeProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new byte[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                var bools = new bool[card];
                for (int i = 0; i < card; i++)
                {
                    bools[i] = values[i] != 0;
                }

                return bools;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string[] GetDatatypeProperty_string(string name)
        {
            var propId = GetPropertyId(name);

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetDatatypeProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new IntPtr[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                var strings = new string[card];
                for (int i = 0; i < (int)card; i++)
                {
                    strings[i] = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(values[i]);
                }
                return strings;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetObjectProperty(string name, Int64 instance)
        {
            var propId = GetPropertyId(name);
            var res = engine.SetObjectProperty(m_instance, propId, ref instance, 1);
            System.Diagnostics.Debug.Assert(res == 0);
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetObjectProperty(string name, Entity[] instances)
        {
            var inst = new Int64[instances.Length];
            for (int i = 0; i < instances.Length; i++)
                inst[i] = instances[i];

            SetObjectProperty(name, inst);
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetObjectProperty(string name, Int64[] instances)
        {
            var propId = GetPropertyId(name);

            var inst = new Int64[instances.Length];
            for (int i = 0; i < instances.Length; i++)
                inst[i] = instances[i];

            var res = engine.SetObjectProperty(m_instance, propId, ref inst[0], inst.Length);
            System.Diagnostics.Debug.Assert(res == 0);
        }
#endif
    }
}

