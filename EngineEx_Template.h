//
// Helper classes (C++ wrappers)
//
#ifndef __RDF_LTD__NAMESPACE_NAME_H
#define __RDF_LTD__NAMESPACE_NAME_H

#include    <assert.h>
#include	"ifcengine.h"

namespace NAMESPACE_NAME
{
    //
    // Entities forward declarations
    //
//## TemplateUtilityTypes
    typedef const char* StringType;
    typedef SdaiEntity  REF_ENTITY;
//## TEMPLATE: ClassForwardDeclaration
    class ENTITY_NAME;
//## TEMPLATE: BeginDefinedTypes

    //
    // Defined types
    // 
//## TEMPLATE: DefinedType
    typedef double DEFINED_TYPE_NAME;
//## TEMPLATE: BeginEnumerations

    //
    // Enumerations
    //
//## BeginEnumeration

    enum ENUMERATION_NAME
    {
//## EnumerationElement
        ENUMERATION_NAME_ENUMERATION_ELEMENT=1234,
//## EndEnumeration
        ENUMERATION_NAME__NULL = -1
    };
//## TEMPLATE: BeginEntities

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    template <typename T> class Nullable
    {
    protected:
        T* m_value;

    public:
        Nullable<T>() : m_value(NULL) {}
        Nullable<T>(T value) { m_value = new T(value); }

        virtual ~Nullable<T>()
        {
            if (m_value) {
                delete m_value;
            }
        };

        bool IsNull() const { return !m_value; }
        T Value() const { assert(m_value); if (m_value) return *m_value; else return 0; }

        virtual Nullable<T>& operator=(const Nullable<T>& src)
        {
            if (m_value) {
                delete m_value;
            }

            m_value = NULL;

            if (src.m_value) {
                m_value = new T(*(src.m_value));
            }

            return *this;
        }
    };

    /// <summary>
    /// Provides utility methods to interact with a generic entity instnace
    /// You also can use object of this class instead of int64_t handle of the instance in any place where the handle is required
    /// </summary>
    class Entity
    {
    protected:
        /// <summary>
        /// underlyed instance handle
        /// </summary>
        SdaiInstance m_instance;

    public:

        Entity(SdaiInstance instance, const char* entityName)
        {
            m_instance = instance;
#ifdef _DEBUG
            if (m_instance != 0 && entityName != NULL) {
                SdaiEntity instType = sdaiGetInstanceType (m_instance);
                SdaiModel model =  engiGetEntityModel (instType);
                SdaiEntity entity = sdaiGetEntity (model, entityName);
                assert(instType == entity);
            }
#endif
        }


        /// <summary>
        /// Conversion  to instance handle, so the object of the class can be used anywhere where a handle required
        /// </summary>
        operator SdaiInstance() const { return m_instance; }

    protected:
        /// 
        /// 
        const char* get_sdaiSTRING(const char* attrName)
        {
            const char* str = NULL;
            if (sdaiGetAttrBN(m_instance, attrName, sdaiSTRING, (void*) &str))
                return str;
            else
                return NULL;
        }
    };

//## TEMPLATE: BeginEntity

    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class ENTITY_NAME
    /// You also can use object of this C++ class instead of int64_t handle of the OWL instance in any place where the handle is required
    /// </summary>
    class ENTITY_NAME : public virtual /*PARENT_NAME*/Entity
    {
    public:
        /// <summary>
        /// Constructs object of this C++ class that wraps existing instance
        /// </summary>
        /// <param name="instance">An instance to interact with</param>
        ENTITY_NAME(SdaiInstance instance = NULL, const char* entityName = NULL)
            : Entity(instance, entityName ? entityName : "ENTITY_NAME")
        {}

//## EntityCreateMethod
        /// <summary>
        /// Create new instace of ENTITY_NAME and returns object of this C++ class to interact with
        /// </summary>
        static ENTITY_NAME Create(SdaiModel model) { SdaiInstance inst = sdaiCreateInstanceBN(model, "ENTITY_NAME"); assert(inst); return inst; }
        
//## GetSimpleAttribute

        Nullable<double> get_ATTR_NAME() { double val = 0; if (sdaiGetAttrBN(m_instance, "ATTR_NAME", sdaiREAL, &val)) return val; else return Nullable<double>(); }
//## SetSimpleAttribute
        void set_ATTR_NAME(double value) { sdaiPutAttrBN(m_instance, "ATTR_NAME", sdaiREAL, &value); }
//## GetSimpleAttributeString

        StringType get_attr_NAME() { return get_sdaiSTRING("ATTR_NAME"); }
//## SetSimpleAttributeString
        void set_ATTR_NAME(StringType value) { sdaiPutAttrBN(m_instance, "ATTR_NAME", sdaiSTRING, value); }
//## GetEntityAttribute

        REF_ENTITY get_Attr_NAME();
//## SetEntityAttribute
        void set_Attr_NAME(REF_ENTITY inst);
//## EndEntity
    };

//## GetEntityAttributeImplementation

    REF_ENTITY ENTITY_NAME::get_Attr_NAME() { SdaiInstance inst = 0; sdaiGetAttrBN(m_instance, "ATTR_NAME", sdaiINSTANCE, &inst); return inst; }
//## SetEntityAttributeImplementation
    void ENTITY_NAME::set_Attr_NAME(REF_ENTITY inst) { SdaiInstance i = inst;  sdaiPutAttrBN(m_instance, "ATTR_NAME", sdaiINSTANCE, (void*)i); }
//## TEMPLATE: EndFile template part

}

#endif //__RDF_LTD__NAMESPACE_NAME_H
