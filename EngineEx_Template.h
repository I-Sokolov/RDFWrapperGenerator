//
// Helper classes (C++ wrappers)
//
#ifndef __RDF_LTD__NAMESPACE_NAME_H
#define __RDF_LTD__NAMESPACE_NAME_H

#include    <assert.h>
#include	"ifcengine.h"

namespace NAMESPACE_NAME
{
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
        T Value() const { assert(m_value); if (m_value) return *m_value; else return (T) 0; }

        virtual Nullable<T>& operator=(const Nullable<T>& src)
        {
            if (m_value) { delete m_value; }
            m_value = NULL;
            if (src.m_value) { m_value = new T(*(src.m_value)); }
            return *this;
        }
    };

    //
    //
    static int EnumerationNameToIndex(const char* rEnumValues[], const char* value)
    {
        if (value) {

            for (int i = 0; rEnumValues[i]; i++) {
                if (0 == _stricmp(value, rEnumValues[i])) {
                    return i;
                }
            }
        }

        return -1;
    }



    /// <summary>
    /// Helper class to access SELET data
    /// </summary>
    class SelectAccess
    {
    protected:
        SdaiInstance m_instance;
        const char* m_attrName;

    protected:
        //
        SelectAccess(SdaiInstance instance, const char* attrName) { m_instance = instance; m_attrName = attrName; }

        //
        template <typename T> Nullable<T> getSimpleValue(const char* typeName, int_t sdaiType)
        {
            Nullable<T> ret;
            void* adb = sdaiCreateEmptyADB();

            if (sdaiGetAttrBN(m_instance, m_attrName, sdaiADB, &adb)) {
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
        template <typename T> void setSimpleValue(const char* typeName, int_t sdaiType, T value)
        {
            void* adb = sdaiCreateADB(sdaiType, &value);
            sdaiPutADBTypePath(adb, 1, typeName);
            sdaiPutAttrBN(m_instance, m_attrName, sdaiADB, adb);
            sdaiDeleteADB(adb);
        }

        //
        const char* getStringValue(const char* typeName)
        {
            const char* ret = NULL;
            void* adb = sdaiCreateEmptyADB();

            if (sdaiGetAttrBN(m_instance, m_attrName, sdaiADB, &adb)) {
                char* path = sdaiGetADBTypePath(adb, 0);
                if (path && 0 == _stricmp(path, typeName)) {
                    sdaiGetADBValue(adb, sdaiSTRING, &ret);
                }
            }

            sdaiDeleteADB(adb);
            return ret;
        }

        //
        void setStringValue(const char* typeName, const char* value)
        {
            void* adb = sdaiCreateADB(sdaiSTRING, value);
            sdaiPutADBTypePath(adb, 1, typeName);
            sdaiPutAttrBN(m_instance, m_attrName, sdaiADB, adb);
            sdaiDeleteADB(adb);
        }

        //
        int getEnumerationValue(const char* typeName, const char* rEnumValues[])
        {
            int ret = -1;

            void* adb = sdaiCreateEmptyADB();

            if (sdaiGetAttrBN(m_instance, m_attrName, sdaiADB, &adb)) {
                char* path = sdaiGetADBTypePath(adb, 0);
                if (path && 0 == _stricmp(path, typeName)) {
                    const char* value = NULL;
                    sdaiGetADBValue(adb, sdaiENUM, &value);
                    ret = EnumerationNameToIndex(rEnumValues, value);
                }
            }

            sdaiDeleteADB(adb);
            return ret;
        }

        //
        void setEnumerationValue(const char* typeName, const char* value)
        {
            void* adb = sdaiCreateADB(sdaiENUM, value);
            sdaiPutADBTypePath(adb, 1, typeName);
            sdaiPutAttrBN(m_instance, m_attrName, sdaiADB, adb);
            sdaiDeleteADB(adb);
        }

        //
        int64_t getEntityInstance(const char* typeName)
        {
            int64_t ret = 0;
            int64_t inst = 0;

            if (sdaiGetAttrBN(m_instance, m_attrName, sdaiINSTANCE, &inst)) {
                SdaiEntity instType = sdaiGetInstanceType(inst);
                SdaiModel model = engiGetEntityModel(instType);
                SdaiEntity requiredType = sdaiGetEntity(model, typeName);
                if (instType == requiredType) {
                    ret = inst;
                }
            }

            return ret;
        }

        //
        void setEntityInstance(const char* typeName, int64_t inst)
        {
            sdaiPutAttrBN(m_instance, m_attrName, sdaiINSTANCE, (void*) inst);
        }

    };


    /// <summary>
    /// Provides utility methods to interact with a generic entity instnace
    /// You also can use object of this class instead of int64_t handle of the instance in any place where the handle is required
    /// </summary>
    class Entity
    {
    protected:
        SdaiInstance m_instance;

    public:
        Entity(SdaiInstance instance, const char* entityName)
        {
            m_instance = instance;
#ifdef _DEBUG
            if (m_instance != 0 && entityName != NULL) {
                SdaiEntity instType = sdaiGetInstanceType(m_instance);
                SdaiModel model = engiGetEntityModel(instType);
                SdaiEntity entity = sdaiGetEntity(model, entityName);
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
            return EnumerationNameToIndex(rEnumValues, value);
        }
    };


    //
    // Entities forward declarations
    //
//## TemplateUtilityTypes    - this section just to make templates syntax correc

    typedef double      SimpleType;
    typedef const char* StringType;
    typedef int         SelectType;
    typedef SdaiEntity  REF_ENTITY;    

#define sdaiTYPE sdaiREAL

//## TEMPLATE: ClassForwardDeclaration
    class ENTITY_NAME;
//## TEMPLATE: BeginDefinedTypes

    //
    // Defined types
    // 
//## TEMPLATE: DefinedType
    typedef SimpleType DEFINED_TYPE_NAME;
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
//## TEMPLATE: SelectAccessorBegin

    class TYPE_NAME_accessor : protected SelectAccess
    {
    public:
        TYPE_NAME_accessor(SdaiInstance instance, const char* attrName) : SelectAccess(instance, attrName) {}
//## SelectGetSimpleValue
        Nullable<SimpleType> select_SimpleType() { return getSimpleValue<SimpleType>("TypeNameUpper", sdaiTYPE); }
//## SelectSetSimpleValue
        void select_SimpleType(SimpleType value) { setSimpleValue("TypeNameUpper", sdaiTYPE, value); }
//## SelectGetStringValue
        StringType select_StringType() { return getStringValue("TypeNameUpper"); }
//## SelectSetStringValue
        void select_StringType(StringType value) { setStringValue("TypeNameUpper", value); }
//## SelectGetEntity
        REF_ENTITY select_REF_ENTITY();
//## SelectSetEntity
        void select_REF_ENTITY(REF_ENTITY inst);
//## SelectGetEnumeration
        Nullable<ENUMERATION_NAME> select_ENUMERATION_NAME() { int v = getEnumerationValue("TypeNameUpper", ENUMERATION_NAME_); if (v >= 0) return (ENUMERATION_NAME) v; else return Nullable<ENUMERATION_NAME>(); }
//## SelectSetEnumeration
        void select_ENUMERATION_NAME(ENUMERATION_NAME value) { const char* val = ENUMERATION_NAME_[value]; setEnumerationValue("TypeNameUpper", val); }
//## SelectNested
        TYPE_NAME_accessor select_TYPE_NAME() { return TYPE_NAME_accessor(m_instance, m_attrName); }
//## SelectGetAsDouble
        Nullable<double> as_double() { double val = 0; if (sdaiGetAttrBN(m_instance, m_attrName, sdaiREAL, &val)) return val; else return Nullable<double>(); }
//## SelectGetAsInt
        Nullable<int64_t> as_int() { int64_t val = 0; if (sdaiGetAttrBN(m_instance, m_attrName, sdaiINTEGER, &val)) return val; else return Nullable<int64_t>(); }
//## SelectGetAsBool
        Nullable<bool> as_bool() { bool val = 0; if (sdaiGetAttrBN(m_instance, m_attrName, sdaiBOOLEAN, &val)) return val; else return Nullable<bool>(); }
//## SelectGetAsString
        const char* as_text() { const char* val = NULL; sdaiGetAttrBN(m_instance, m_attrName, sdaiSTRING, &val); return val; }
//## SelectGetAsInstance
        SdaiInstance as_instance() { SdaiInstance val = NULL; sdaiGetAttrBN(m_instance, m_attrName, sdaiINSTANCE, &val); return val; }
//## SelectAccessorEnd
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

        Nullable<SimpleType> get_ATTR_NAME() { SimpleType val = 0; if (sdaiGetAttrBN(m_instance, "ATTR_NAME", sdaiTYPE, &val)) return val; else return Nullable<SimpleType>(); }
//## SetSimpleAttribute
        void set_ATTR_NAME(SimpleType value) { sdaiPutAttrBN(m_instance, "ATTR_NAME", sdaiTYPE, &value); }
//## GetSimpleAttributeString

        StringType get_attr_NAME() { return get_sdaiSTRING("ATTR_NAME"); }
//## SetSimpleAttributeString
        void set_ATTR_NAME(StringType value) { sdaiPutAttrBN(m_instance, "ATTR_NAME", sdaiSTRING, value); }
//## GetEntityAttribute

        REF_ENTITY get_Attr_NAME();
//## SetEntityAttribute
        void set_Attr_NAME(REF_ENTITY inst);
//## GetEnumAttribute

        Nullable<ENUMERATION_NAME> get_ATtr_NAME() { int v = get_sdaiENUM("ATTR_NAME", ENUMERATION_NAME_); if (v >= 0) return (ENUMERATION_NAME)v; else return Nullable<ENUMERATION_NAME>(); }
//## SetEnumAttribute
        void set_ATTR_NAME(ENUMERATION_NAME value) { const char* val = ENUMERATION_NAME_[value]; sdaiPutAttrBN(m_instance, "ATTR_NAME", sdaiENUM, val); }
//## SelectAccessor
        TYPE_NAME_accessor getOrset_ATTR_NAME() { return TYPE_NAME_accessor(m_instance, "ATTR_NAME"); }
//## EndEntity
    };

//## SelectGetEntityImplementation
    REF_ENTITY TYPE_NAME_accessor::select_REF_ENTITY() { return getEntityInstance("TypeNameUpper"); }
//## SelectSetEntityImplementation
    void TYPE_NAME_accessor::select_REF_ENTITY(REF_ENTITY inst) { setEntityInstance("TypeNameUpper", inst); }
//## GetEntityAttributeImplementation
    REF_ENTITY ENTITY_NAME::get_Attr_NAME() { SdaiInstance inst = 0; sdaiGetAttrBN(m_instance, "ATTR_NAME", sdaiINSTANCE, &inst); return inst; }
//## SetEntityAttributeImplementation
    void ENTITY_NAME::set_Attr_NAME(REF_ENTITY inst) { SdaiInstance i = inst;  sdaiPutAttrBN(m_instance, "ATTR_NAME", sdaiINSTANCE, (void*)i); }
//## TEMPLATE: EndFile template part

}

#endif //__RDF_LTD__NAMESPACE_NAME_H
