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
//## TemplateUtilityTypes
using REF_ENTITY = ENTITY_NAME;
using SimpleType = System.Double;

//## TEMPLATE: ClassForwardDeclaration (not really required in C#)
//## TEMPLATE: BeginDefinedTypes
//## TEMPLATE: DefinedType
//## TEMPLATE: BeginEnumerations

    //
    // Enumerations
    //
    public class Enums
    {
//## TEMPLATE: BeginEnumeration

        public enum ENUMERATION_NAME
        {
//## EnumerationElement
            ENUMERATION_ELEMENT = 1234,
//## EndEnumeration
            _NULL = -1
        };
        public static string[] ENUMERATION_NAME_ = { "ENUMERATION_STRING_VALUES" };
//## TEMPLATE: BeginEntities
    }

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
        public new static ENTITY_NAME Create(SdaiModel model) { SdaiInstance inst = ifcengine.sdaiCreateInstanceBN(model, "ENTITY_NAME"); Debug.Assert(inst!=0); return new ENTITY_NAME(inst); }

//## GetSimpleAttribute
        
        //public SimpleType? get_ATTR_NAME() { SimpleType value; if (0 != ifcengine.sdaiGetAttrBN(m_instance, "ATTR_NAME", ifcengine.sdaiTYPE, out value)) return value; else return null; } 
//## SetSimpleAttribute
        //public void set_ATTR_NAME(SimpleType value) { ifcengine.sdaiPutAttrBN (m_instance, "ATTR_NAME", ifcengine.sdaiTYPE, ref value); }
//## GetSimpleAttributeString
        
        public string get_attr_NAME() { return getString("ATTR_NAME"); } 
//## SetSimpleAttributeString
        public void set_ATTR_NAME(string value) { ifcengine.sdaiPutAttrBN (m_instance, "ATTR_NAME", ifcengine.sdaiSTRING, value); }
//## GetEntityAttribute

        public REF_ENTITY get_Attr_NAME() { SdaiInstance inst = 0; ifcengine.sdaiGetAttrBN(m_instance, "ATTR_NAME", ifcengine.sdaiINSTANCE, out inst); return inst != 0 ? new REF_ENTITY(inst) : null; } 
//## SetEntityAttribute
        public void set_Attr_NAME(REF_ENTITY inst) { SdaiInstance i = inst;  ifcengine.sdaiPutAttrBN(m_instance, "ATTR_NAME", ifcengine.sdaiINSTANCE, i); }
//## GetEnumAttribute

        public Enums.ENUMERATION_NAME? get_ATtr_NAME() { int v = getENUM("ATTR_NAME", Enums.ENUMERATION_NAME_); if (v >= 0) return (Enums.ENUMERATION_NAME) v; else return null; }
//## SetEnumAttribute
        public void set_ATTR_NAME(Enums.ENUMERATION_NAME value) { string val = Enums.ENUMERATION_NAME_[(int)value]; ifcengine.sdaiPutAttrBN(m_instance, "ATTR_NAME", ifcengine.sdaiENUM, val); }
//## EndEntity
    }

//## GetEntityAttributeImplementation
//## SetEntityAttributeImplementation
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
        protected Entity(SdaiInstance instance, string entityName)
        {
            m_instance = instance;
            //System.Diagnostics.Debug.Assert(entityName == null/*do not check*/ || ifcengine.sdaiIsKindOf(instance, entityName));
        }


        /// <summary>
        /// Conversion to instance handle, so the object of the class can be used anywhere where a handle required
        /// </summary>
        public static implicit operator SdaiInstance(Entity instance) => instance.m_instance;

        public static Entity Create(SdaiModel model) { System.Diagnostics.Debug.Assert(false); return null; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attrName"></param>
        /// <returns></returns>
        protected string getString (string attrName, Int64 valueType = ifcengine.sdaiSTRING)
        {
            IntPtr ptr = IntPtr.Zero;
            if (0!=ifcengine.sdaiGetAttrBN(m_instance, attrName, valueType, out ptr))
            {
                var name = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(ptr);
                return name;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attrName"></param>
        /// <param name="rEnumValues"></param>
        /// <returns></returns>
        protected int getENUM(string attrName, string[] rEnumValues)
        {
            string value = getString(attrName, ifcengine.sdaiENUM);

            if (value != null)
            {

                for (int i = 0; i < rEnumValues.Length; i++)
                {
                    if (value == rEnumValues[i])
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

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
    }
}

