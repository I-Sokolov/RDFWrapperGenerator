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
//## TemplateUtilityTypes    - this section just to make templates syntax correc

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
        ENUMERATION_NAME___unk = -1
    };
    static const char* ENUMERATION_NAME_[] = {"ENUMERATION_STRING_VALUES", NULL};
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
        Nullable<T>(const Nullable<T>& src) { if (src.m_value) m_value = new T(*src.m_value); else m_value = NULL; }

        virtual ~Nullable<T>() { if (m_value) { delete m_value; } };

        bool IsNull() const { return !m_value; }
        T Value() const { assert(m_value); if (m_value) return *m_value; else return (T)0; }

        virtual Nullable<T>& operator=(const Nullable<T>& src)
        {
            if (m_value) { delete m_value; }
            m_value = NULL;
            if (src.m_value) { m_value = new T(*(src.m_value)); }
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
        // 
        // 
        const char* get_sdaiSTRING(const char* attrName)
        {
            const char* str = NULL;
            if (sdaiGetAttrBN(m_instance, attrName, sdaiSTRING, (void*) &str))
                return str;
            else
                return NULL;
        }

        //
        //
        int get_sdaiENUM(const char* attrName, const char* rEnumValues[])
        {
            const char* value = NULL;
            sdaiGetAttrBN(m_instance, attrName, sdaiENUM, (void*) &value);

            if (value) {

                for (int i = 0; rEnumValues[i]; i++) {
                    if (0 == _stricmp(value, rEnumValues[i])) {
                        return i;
                    }
                }
            }

            return -1;
        }

        //
        //
        template <typename T> Nullable<T> getSelectValue(const char* attrName, const char* typeName, int_t sdaiType)
        {
            Nullable<T> ret;
            void* adb = sdaiCreateEmptyADB();
            
            if (sdaiGetAttrBN(m_instance, attrName, sdaiADB, &adb)) {
                char* path = sdaiGetADBTypePath(adb, 0);
                if (path && 0 == _stricmp(path, typeName)) {
                    T val = (T) 0;
                    sdaiGetADBValue(adb, sdaiType, &val);
                    ret = val;
                }
            }

            sdaiDeleteADB(adb);          
            return ret;
        }

        //
        //
        template <typename T> void setSelectValue(const char* attrName, const char* typeName, int_t sdaiType, T value)
        {
            void* adb = sdaiCreateADB(sdaiType, &value);
            sdaiPutADBTypePath(adb, 1, typeName);
            sdaiPutAttrBN(m_instance, attrName, sdaiADB, adb);
            sdaiDeleteADB(adb);
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
//## GetEnumAttribute

        Nullable<ENUMERATION_NAME> get_ATtr_NAME() { int v = get_sdaiENUM("ATTR_NAME", ENUMERATION_NAME_); if (v >= 0) return (ENUMERATION_NAME) v; else return Nullable<ENUMERATION_NAME>(); }
//## SetEnumAttribute
        void set_ATTR_NAME(ENUMERATION_NAME value) { const char* val = ENUMERATION_NAME_[value]; sdaiPutAttrBN(m_instance, "ATTR_NAME", sdaiENUM, val); }
//## GetSelectSimpleAttribute
        Nullable<double> get_ATTR_NAME_TYPE_NAME() { return getSelectValue<double>("ATTR_NAME", "TYPE_NAME", sdaiREAL); }
//## SetSelectSimpleAttribute
        void set_ATTR_NAME_TYPE_NAME(double value) { setSelectValue("ATTR_NAME", "TYPE_NAME", sdaiREAL, value); }
//## EndEntity
    };

//## GetEntityAttributeImplementation

    REF_ENTITY ENTITY_NAME::get_Attr_NAME() { SdaiInstance inst = 0; sdaiGetAttrBN(m_instance, "ATTR_NAME", sdaiINSTANCE, &inst); return inst; }
//## SetEntityAttributeImplementation
    void ENTITY_NAME::set_Attr_NAME(REF_ENTITY inst) { SdaiInstance i = inst;  sdaiPutAttrBN(m_instance, "ATTR_NAME", sdaiINSTANCE, (void*)i); }
//## TEMPLATE: EndFile template part

}

#endif //__RDF_LTD__NAMESPACE_NAME_H
