//
// Helper classes (C++ wrappers)
//
#ifndef __RDF_LTD__NAMESPACE_NAME_H
#define __RDF_LTD__NAMESPACE_NAME_H

#include    <assert.h>
#include    <list>
#include    <set>
#include    <string>

#include	"ifcengine.h"
#include    "engineinline.h"

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
            assert(entityName == NULL/*do not check*/ || IsInstanceOfClass(instance, entityName));
        }


        /// <summary>
        /// Conversion  to instance handle, so the object of the class can be used anywhere where a handle required
        /// </summary>
        operator SdaiInstance() const { return m_instance; }

    protected:
        //
        //
        int getENUM(const char* attrName, const char* rEnumValues[])
        {
            const char* value = NULL;
            sdaiGetAttrBN(m_instance, attrName, sdaiENUM, (void*) &value);
            return EnumerationNameToIndex(rEnumValues, value);
        }

        //
        //
        template <typename T> void getListOfSimple(std::list<T>& lst, const char* attrName, int_t sdaiType)
        {
            int_t* aggr = NULL;
            sdaiGetAttrBN(m_instance, attrName, sdaiAGGR, &aggr);
            int_t  cnt = sdaiGetMemberCount(aggr);
            for (int_t i = 0; i < cnt; i++) {
                T val = 0;
                /*must be if*/ engiGetAggrElement(aggr, i, sdaiType, &val); 
                    lst.push_back(val);
            }
        }

        //
        //
        void getListOfString(std::list<std::string>& lst, const char* attrName)
        {
            int_t* aggr = NULL;
            sdaiGetAttrBN(m_instance, attrName, sdaiAGGR, &aggr);
            int_t  cnt = sdaiGetMemberCount(aggr);
            for (int_t i = 0; i < cnt; i++) {
                const char* val = NULL;
                /*must be if*/ engiGetAggrElement(aggr, i, sdaiSTRING, &val);
                lst.push_back(val);
            }
        }

        //
        //
        template <typename T, typename List> void setListOfSimple(List const& lst, const char* attrName, int_t sdaiType)
        {
            SdaiInstance entity = sdaiGetInstanceType(m_instance);
            if (entity) {
                void* attr = sdaiGetAttrDefinition(entity, attrName);
                if (attr) {
                    int_t* aggr = sdaiCreateAggr(m_instance, attr);
                    for (auto it = lst.begin(); it != lst.end(); it++) {
                        T val = *it;
                        sdaiAppend((int_t) aggr, sdaiType, &val);
                    }
                    //sdaiPutAttr(m_instance, attr, sdaiAGGR, &aggr);
                }
            }
        }

        //
        //
        void setListOfString(std::list<std::string> const& lst, const char* attrName)
        {
            SdaiInstance entity = sdaiGetInstanceType(m_instance);
            if (entity) {
                void* attr = sdaiGetAttrDefinition(entity, attrName);
                if (attr) {
                    int_t* aggr = sdaiCreateAggr(m_instance, attr);
                    for (auto it = lst.begin(); it != lst.end(); it++) {
                        const char* val = it->c_str();
                        sdaiAppend((int_t) aggr, sdaiSTRING, val);
                    }
                    //sdaiPutAttr(m_instance, attr, sdaiAGGR, &aggr);
                }
            }
        }

        //
        //
        template <typename T, typename List> void setListOfRef(List const& lst, const char* attrName, int_t sdaiType)
        {
            SdaiInstance entity = sdaiGetInstanceType(m_instance);
            if (entity) {
                void* attr = sdaiGetAttrDefinition(entity, attrName);
                if (attr) {
                    int_t* aggr = sdaiCreateAggr(m_instance, attr);
                    for (auto it = lst.begin(); it != lst.end(); it++) {
                        T val = *it;
                        sdaiAppend((int_t) aggr, sdaiType, val);
                    }
                    //sdaiPutAttr(m_instance, attr, sdaiAGGR, &aggr);
                }
            }
        }

        //
        //
        template <typename T> void setListOfSimple(const T* arr, size_t n, const char* attrName, int_t sdaiType)
        {
            std::list<T> lst;
            for (int i = 0; i < n; i++) {
                lst.push_back(arr[i]);
            }
            setListOfSimple<T>(lst, attrName, sdaiType);
        }

        //
        //
        void setListOfString(const char** arr, size_t n, const char* attrName)
        {
            std::list<std::string> lst;
            for (int i = 0; i < n; i++) {
                std::string s(arr[i]);
                lst.push_back(s);
            }
            setListOfString(lst, attrName);
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

#define sdaiTYPE  sdaiREAL
#define AGGR_TYPE list

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

        StringType get_attr_NAME() { StringType val = NULL; if (sdaiGetAttrBN(m_instance, "ATTR_NAME", sdaiSTRING, &val)) return val; else return NULL; }
//## SetSimpleAttributeString
        void set_ATTR_NAME(StringType value) { sdaiPutAttrBN(m_instance, "ATTR_NAME", sdaiSTRING, value); }
//## GetEntityAttribute

        REF_ENTITY get_Attr_NAME();
//## SetEntityAttribute
        void set_Attr_NAME(REF_ENTITY inst);
//## GetEnumAttribute

        Nullable<ENUMERATION_NAME> get_ATtr_NAME() { int v = getENUM("ATTR_NAME", ENUMERATION_NAME_); if (v >= 0) return (ENUMERATION_NAME)v; else return Nullable<ENUMERATION_NAME>(); }
//## SetEnumAttribute
        void set_ATTR_NAME(ENUMERATION_NAME value) { const char* val = ENUMERATION_NAME_[value]; sdaiPutAttrBN(m_instance, "ATTR_NAME", sdaiENUM, val); }
//## SelectAccessor
        TYPE_NAME_accessor getOrset_ATTR_NAME() { return TYPE_NAME_accessor(m_instance, "ATTR_NAME"); }
//## AggregationGetSimple

        void get_ATTr_NAME(std::list<SimpleType>& lst) { getListOfSimple(lst, "ATTR_NAME", sdaiTYPE); }
//## AggregationSetSimple
        void set_ATTr_NAME(std::AGGR_TYPE<SimpleType> const& lst) { setListOfSimple<SimpleType>(lst, "ATTR_NAME", sdaiTYPE); }
        void set_ATTr_NAME(const SimpleType* arr, size_t n) { setListOfSimple<SimpleType>(arr, n, "ATTR_NAME", sdaiTYPE); }
//## AggregationGetString

        void get_ATTr_NAME(std::list<std::string>& lst) { getListOfString(lst, "ATTR_NAME"); }
//## AggregationSetString
        void set_ATTr_NAME(std::AGGR_TYPE<std::string> const& lst) { setListOfString(lst, "ATTR_NAME"); }
        void set_ATTr_NAME(const char** arr, size_t n) { setListOfString(arr, n, "ATTR_NAME"); }
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
