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
    ///
    typedef const char* TextData;
    typedef int_t       IntData;


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
    enum class LOGICAL { False=0, True, Unknown };
    static TextData LOGICAL_VALUE_NAMES[] = {"F", "T", "U", NULL};

    //
    //
    static int EnumerationNameToIndex(TextData rEnumValues[], TextData value)
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
    /// Helper class to handle and access SELECT instance data
    /// </summary>
    class Select
    {
    protected:
        SdaiInstance m_instance;
        TextData m_attrName;
        void* m_adb;

    public:
        void* ADB() const { return m_adb; }

    protected:
        Select(SdaiInstance instance, TextData attrName = NULL, void* adb = NULL)
            : m_instance(instance), m_attrName(attrName), m_adb(adb)
        {
            if (!m_adb && m_instance && m_attrName) {
                m_adb = sdaiCreateEmptyADB();
                if (!sdaiGetAttrBN(m_instance, m_attrName, sdaiADB, &m_adb)) {
                    sdaiDeleteADB(m_adb);
                    m_adb = NULL;
                }
            }
        }

        //
        template <typename T> Nullable<T> getSimpleValue(TextData typeName, IntData sdaiType)
        {
            Nullable<T> ret;

            if (m_adb) {
                char* path = sdaiGetADBTypePath(m_adb, 0);
                if (path && 0 == _stricmp(path, typeName)) {
                    T val = (T) 0;
                    sdaiGetADBValue(m_adb, sdaiType, &val);
                    ret = val;
                }
            }
            return ret;
        }

        //
        template <typename T> void setSimpleValue(TextData typeName, IntData sdaiType, T value)
        {
            ReleaseADB();
            m_adb = sdaiCreateADB(sdaiType, &value);
            sdaiPutADBTypePath(m_adb, 1, typeName);
            OnSetValue();
        }

        //
        TextData getTextValue(TextData typeName)
        {
            TextData ret = NULL;

            if (m_adb) {
                char* path = sdaiGetADBTypePath(m_adb, 0);
                if (path && 0 == _stricmp(path, typeName)) {
                    sdaiGetADBValue(m_adb, sdaiSTRING, &ret);
                }
            }
            return ret;
        }

        //
        void setTextValue(TextData typeName, TextData value)
        {
            ReleaseADB();
            m_adb = sdaiCreateADB(sdaiSTRING, value);
            sdaiPutADBTypePath(m_adb, 1, typeName);
            OnSetValue();
        }

        //
        int getEnumerationValue(TextData typeName, TextData rEnumValues[])
        {
            int ret = -1;

            if (m_adb) {
                char* path = sdaiGetADBTypePath(m_adb, 0);
                if (path && 0 == _stricmp(path, typeName)) {
                    TextData value = NULL;
                    sdaiGetADBValue(m_adb, sdaiENUM, &value);
                    ret = EnumerationNameToIndex(rEnumValues, value);
                }
            }
            return ret;
        }

        //
        void setEnumerationValue(TextData typeName, TextData value)
        {
            ReleaseADB();
            m_adb = sdaiCreateADB(sdaiENUM, value);
            sdaiPutADBTypePath(m_adb, 1, typeName);
            OnSetValue();
        }

        //
        IntData getEntityInstance(TextData typeName)
        {
            assert(m_instance && m_attrName); //TODO how to keep instance in ADB

            IntData ret = 0;
            IntData inst = 0;
            if (sdaiGetAttrBN(m_instance, m_attrName, sdaiINSTANCE, &inst)) {
                if (sdaiIsKindOfBN (inst, typeName)){
                    ret = inst;
                }
            }
            return ret;
        }

        //
        void setEntityInstance(TextData typeName, IntData inst)
        {
            if (sdaiIsKindOfBN(inst, typeName)) {
                assert(m_instance && m_attrName); //TODO how to keep instance in ADB

                ReleaseADB();
                sdaiPutAttrBN(m_instance, m_attrName, sdaiINSTANCE, (void*) inst);
            }
            else {
                assert(0);
            }
        }

        //
        SdaiAggr getAggrValue(TextData typeName)
        {
            SdaiAggr ret = NULL;

            if (m_adb) {
                char* path = sdaiGetADBTypePath(m_adb, 0);
                if (path && 0 == _stricmp(path, typeName)) {
                    sdaiGetADBValue(m_adb, sdaiAGGR, &ret);
                }
            }
            return ret;
        }

        //
        void setAggrValue(TextData typeName, SdaiAggr value)
        {
            ReleaseADB();
            m_adb = sdaiCreateADB(sdaiAGGR, value);
            sdaiPutADBTypePath(m_adb, 1, typeName);
            OnSetValue();
        }

    private:
        void ReleaseADB() { m_adb = NULL; }

        void OnSetValue()
        {
            if (m_instance && m_attrName) {
                sdaiPutAttrBN(m_instance, m_attrName, sdaiADB, m_adb);
            }
        }
    };

    /// <summary>
    /// 
    /// </summary>
    class Aggregation
    {
    public:
        //
        void FromAttr(SdaiInstance instance, TextData attrName)
        {
            SdaiAggr aggr = NULL;
            sdaiGetAttrBN(instance, attrName, sdaiAGGR, &aggr);
            if (aggr) {
                FromSdaiAggr(instance, aggr);
            }
        }

        //
        virtual void FromSdaiAggr(SdaiInstance instance, SdaiAggr) = NULL;
    };

    /// <summary>
    /// 
    /// </summary>
    template <typename T, IntData sdaiType> class AggregationOfSimple : public Aggregation, public std::list<T>
    {
    public:
        //
        virtual void FromSdaiAggr(SdaiInstance /*instance*/, SdaiAggr aggr) override
        {
            IntData  cnt = sdaiGetMemberCount(aggr);
            for (IntData i = 0; i < cnt; i++) {
                T val = 0;
                engiGetAggrElement(aggr, i, sdaiType, &val);
                this->push_back(val);
            }
        }

        //
        SdaiAggr ToSdaiAggr(SdaiInstance instance, TextData attrName) const
        {
            SdaiAggr aggr = sdaiCreateAggrBN(instance, attrName);
            for (auto it = this->begin(); it != this->end(); it++) {
                T val = *it;
                sdaiAppend((IntData) aggr, sdaiType, &val);
            }
            return aggr;
        }

        //
        static SdaiAggr ToSdaiAggr(const T arr[], size_t cnt, SdaiInstance instance, TextData attrName)
        {
            AggregationOfSimple<T, sdaiType> lst;
            for (size_t i = 0; i < cnt; i++) {
                lst.push_back(arr[i]);
            }
            return lst.ToSdaiAggr(instance, attrName);
        }
    };

    /// <summary>
    /// 
    /// </summary>
    template <typename T> class AggregationOfText : public Aggregation, public std::list<T>
    {
    public:
        //
        virtual void FromSdaiAggr(SdaiInstance /*instance*/, SdaiAggr aggr) override
        {
            IntData  cnt = sdaiGetMemberCount(aggr);
            for (IntData i = 0; i < cnt; i++) {
                TextData val = 0;
                engiGetAggrElement(aggr, i, sdaiSTRING, &val);
                this->push_back(val);
            }
        }

        //
        SdaiAggr ToSdaiAggr(SdaiInstance instance, TextData attrName) const
        {
            SdaiAggr aggr = sdaiCreateAggrBN(instance, attrName);
            for (auto it = this->begin(); it != this->end(); it++) {
                TextData val = it->c_str();
                sdaiAppend((IntData) aggr, sdaiSTRING, val);
            }
            return aggr;
        }

        //
        static SdaiAggr ToSdaiAggr(TextData arr[], size_t cnt, SdaiInstance instance, TextData attrName)
        {
            AggregationOfText<T> lst;
            for (size_t i = 0; i < cnt; i++) {
                lst.push_back(arr[i]);
            }
            return lst.ToSdaiAggr(instance, attrName);
        }
    };

    /// <summary>
    /// 
    /// </summary>
    template <typename T> class AggregationOfInstance : public Aggregation, public std::list<T>
    {
    public:
        //
        virtual void FromSdaiAggr(SdaiInstance /*instance*/, SdaiAggr aggr) override
        {
            IntData  cnt = sdaiGetMemberCount(aggr);
            for (IntData i = 0; i < cnt; i++) {
                IntData val = 0;
                engiGetAggrElement(aggr, i, sdaiINSTANCE, &val);
                this->push_back(val);
            }
        }

        //
        SdaiAggr ToSdaiAggr(SdaiInstance instance, TextData attrName) const
        {
            SdaiAggr aggr = sdaiCreateAggrBN(instance, attrName);
            for (auto it = this->begin(); it != this->end(); it++) {
                IntData val = *it;
                sdaiAppend((IntData) aggr, sdaiINSTANCE, (void*)val);
            }
            return aggr;
        }

        //
        static SdaiAggr ToSdaiAggr(TextData arr[], size_t cnt, SdaiInstance instance, TextData attrName)
        {
            AggregationOfText<T> lst;
            for (size_t i = 0; i < cnt; i++) {
                lst.push_back(arr[i]);
            }
            return lst.ToSdaiAggr(instance, attrName);
        }
    };


    /// <summary>
    /// 
    /// </summary>
    template <typename T, TextData* rEnumValues> class AggregationOfEnum : public Aggregation, public std::list<T>
    {
    public:
        //
        virtual void FromSdaiAggr(SdaiInstance /*instance*/, SdaiAggr aggr) override
        {
            IntData  cnt = sdaiGetMemberCount(aggr);
            for (IntData i = 0; i < cnt; i++) {
                TextData value = 0;
                engiGetAggrElement(aggr, i, sdaiENUM, &value);
                int val = EnumerationNameToIndex(rEnumValues, value);
                if (val >= 0) {
                    this->push_back((T)val);
                }
            }
        }

        //
        SdaiAggr ToSdaiAggr(SdaiInstance instance, TextData attrName) const
        {
            SdaiAggr aggr = sdaiCreateAggrBN(instance, attrName);
            for (auto it = this->begin(); it != this->end(); it++) {
                T val = *it;
                TextData value = rEnumValues[(int)val];
                sdaiAppend((IntData) aggr, sdaiENUM, value);
            }
            return aggr;
        }

        //
        static SdaiAggr ToSdaiAggr(const T arr[], size_t cnt, SdaiInstance instance, TextData attrName)
        {
            AggregationOfEnum<T, rEnumValues> lst;
            for (size_t i = 0; i < cnt; i++) {
                lst.push_back(arr[i]);
            }
            return lst.ToSdaiAggr(instance, attrName);
        }
    };

    /// <summary>
    /// 
    /// </summary>
    template <typename T> class AggregationOfAggregation : public Aggregation, public std::list<T>
    {
    public:
        //
        virtual void FromSdaiAggr(SdaiInstance instance, SdaiAggr aggr) override
        {
            IntData  cnt = sdaiGetMemberCount(aggr);
            for (IntData i = 0; i < cnt; i++) {
                SdaiAggr nested = 0;
                engiGetAggrElement(aggr, i, sdaiAGGR, &nested);
                if (nested) {
                    this->push_back(T());
                    this->back().FromSdaiAggr(instance, nested);
                }
            }
        }

        //
        SdaiAggr ToSdaiAggr(SdaiInstance instance, TextData attrName) const
        {
            SdaiAggr aggr = sdaiCreateAggrBN(instance, attrName);
            for (auto it = this->begin(); it != this->end(); it++) {
                const T& val = *it;
                SdaiAggr nested = val.ToSdaiAggr(instance, NULL);
                sdaiAppend((IntData) aggr, sdaiAGGR, nested);
            }
            return aggr;
        }
    };
    
    template<typename T> class AggregationOfSelect : public Aggregation, public std::list<T>
    {
    public:
        virtual void FromSdaiAggr(SdaiInstance instance, SdaiAggr aggr) override
        {
            IntData  cnt = sdaiGetMemberCount(aggr);
            for (IntData i = 0; i < cnt; i++) {
                void* adb = 0;
                engiGetAggrElement(aggr, i, sdaiADB, &adb);
                if (adb) {
                    this->push_back(T(instance, NULL, adb));
                }
            }
        }

        //
        SdaiAggr ToSdaiAggr(SdaiInstance instance, TextData attrName) const
        {
            SdaiAggr aggr = sdaiCreateAggrBN(instance, attrName);
            for (auto it = this->begin(); it != this->end(); it++) {
                const T& val = *it;
                void* adb = val.ADB();
                if (adb) {
                    sdaiAppend((IntData) aggr, sdaiADB, adb);
                }
            }
            return aggr;
        }
    };


    /// <summary>
    /// Provides utility methods to interact with a generic entity instnace
    /// You also can use object of this class instead of IntData handle of the instance in any place where the handle is required
    /// </summary>
    class Entity
    {
    protected:
        SdaiInstance m_instance;

    public:
        Entity(SdaiInstance instance, TextData entityName)
        {
            m_instance = instance;

            if (m_instance != 0 && entityName != NULL) {
                if (!sdaiIsKindOfBN(m_instance, entityName)) {
                    m_instance = 0;
                }
            }
        }


        /// <summary>
        /// Conversion  to instance handle, so the object of the class can be used anywhere where a handle required
        /// </summary>
        operator SdaiInstance() const { return m_instance; }

    protected:
        //
        //
        int getENUM(TextData attrName, TextData rEnumValues[])
        {
            TextData value = NULL;
            sdaiGetAttrBN(m_instance, attrName, sdaiENUM, (void*) &value);
            return EnumerationNameToIndex(rEnumValues, value);
        }
    };


    //
    // Entities forward declarations
    //
//## TemplateUtilityTypes    - this section just to make templates syntax correc

    typedef double      SimpleType;
    typedef TextData TextType;
    typedef int         SelectType;
    typedef SdaiEntity  REF_ENTITY;    

#define sdaiTYPE  sdaiREAL
#define ENUMERATION_VALUES_ARRAY ENUMERATION_NAME_

//## TEMPLATE: ClassForwardDeclaration
    class ENTITY_NAME;
//## TEMPLATE: DefinedTypesBegin

    //
    // Defined types
    // 
//## TEMPLATE: DefinedType
    typedef SimpleType DEFINED_TYPE_NAME;
//## TEMPLATE: EnumerationsBegin

    //
    // Enumerations
    //
//## EnumerationBegin

    enum class ENUMERATION_NAME
    {
//## EnumerationElement
        ENUMERATION_ELEMENT=1234,
//## EnumerationEnd
        ___unk = -1
    };
    static TextData ENUMERATION_NAME_[] = {"ENUMERATION_STRING_VALUES", NULL};

//## TEMPLATE: AggrgarionTypesBegin

    //
    // Unnamed aggregations
    //
//## AggregationOfSimple
    typedef AggregationOfSimple<SimpleType, sdaiTYPE> AggregationType;
//## AggregationOfText
    typedef AggregationOfText<std::string> Aggregationtype;
//## AggregationOfInstance
    typedef AggregationOfInstance<SimpleType> AggregationTYpe;
//## AggregationOfEnum
    typedef AggregationOfEnum<ENUMERATION_NAME, ENUMERATION_NAME_> AggregationTyPe;
//## AggregationOfAggregation
    typedef AggregationOfAggregation<SimpleType> AggregationTYPe;
//## AggregationOfSelect
    typedef AggregationOfSelect<SimpleType> AggregationTYPE;
//## SelectsBegin

    //
    // SELECT TYPES
    // 
//## TEMPLATE: SelectAccessorBegin

    class GEN_TYPE_NAME_accessor : public Select
    {
    public:
        GEN_TYPE_NAME_accessor(SdaiInstance instance, TextData attrName = NULL, void* adb = NULL) : Select(instance, attrName, adb) {}
//## SelectSimpleGet
        Nullable<SimpleType> get_SimpleType() { return getSimpleValue<SimpleType>("TypeNameUpper", sdaiTYPE); }
//## SelectSimpleSet
        void set_SimpleType(SimpleType value) { setSimpleValue("TypeNameUpper", sdaiTYPE, value); }
//## SelectTextGet
        TextType get_TextType() { return getTextValue("TypeNameUpper"); }
//## SelectTextSet
        void set_TextType(TextType value) { setTextValue("TypeNameUpper", value); }
//## SelectEntityGet
        REF_ENTITY get_REF_ENTITY();
//## SelectEntitySet
        void set_REF_ENTITY(REF_ENTITY inst);
//## SelectEnumerationGet
        Nullable<ENUMERATION_NAME> get_ENUMERATION_NAME() { int v = getEnumerationValue("TypeNameUpper", ENUMERATION_VALUES_ARRAY); if (v >= 0) return (ENUMERATION_NAME) v; else return Nullable<ENUMERATION_NAME>(); }
//## SelectEnumerationSet
        void set_ENUMERATION_NAME(ENUMERATION_NAME value) { TextData val = ENUMERATION_VALUES_ARRAY[(int)value]; setEnumerationValue("TypeNameUpper", val); }
//## SelectAggregationGet
        void get_AggregationType(AggregationType& lst) { SdaiAggr aggr = getAggrValue("TypeNameUpper"); lst.FromSdaiAggr(m_instance, aggr); }
//## SelectAggregationSet
        void set_AggregationType(const AggregationType& lst) { SdaiAggr aggr = lst.ToSdaiAggr(m_instance, NULL); setAggrValue("TypeNameUpper", aggr); }
//## SelectAggregationSetArraySimple
        void set_AggregationType(const SimpleType arr[], size_t n) { SdaiAggr aggr = AggregationType::ToSdaiAggr(arr, n, m_instance, NULL); setAggrValue("TypeNameUpper", aggr); }
//## SelectAggregationSetArrayText
        void set_AggregationType(TextData arr[], size_t n) { Aggregationtype::ToSdaiAggr(arr, n, m_instance, m_attrName); }
//## SelectNested
        GEN_TYPE_NAME_accessor nestedSelectAccess_GEN_TYPE_NAME() { return GEN_TYPE_NAME_accessor(m_instance, m_attrName, m_adb); }
//## SelectGetAsDouble
        Nullable<double> as_double() { double val = 0; if (sdaiGetAttrBN(m_instance, m_attrName, sdaiREAL, &val)) return val; else return Nullable<double>(); }
//## SelectGetAsInt
        Nullable<IntData> as_int() { IntData val = 0; if (sdaiGetAttrBN(m_instance, m_attrName, sdaiINTEGER, &val)) return val; else return Nullable<IntData>(); }
//## SelectGetAsBool
        Nullable<bool> as_bool() { bool val = 0; if (sdaiGetAttrBN(m_instance, m_attrName, sdaiBOOLEAN, &val)) return val; else return Nullable<bool>(); }
//## SelectGetAsText
        TextData as_text() { TextData val = NULL; sdaiGetAttrBN(m_instance, m_attrName, sdaiSTRING, &val); return val; }
//## SelectGetAsEntity
        SdaiInstance as_instance() { SdaiInstance val = NULL; sdaiGetAttrBN(m_instance, m_attrName, sdaiINSTANCE, &val); return val; }
//## SelectAccessorEnd
    };

//## TEMPLATE: EntitiesBegin
    
    //
    // Entities
    // 
    
//## TEMPLATE: EntityBegin

    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class ENTITY_NAME
    /// You also can use object of this C++ class instead of IntData handle of the OWL instance in any place where the handle is required
    /// </summary>
    class ENTITY_NAME : public virtual /*PARENT_NAME*/Entity
    {
    public:
        /// <summary>
        /// Constructs object of this C++ class that wraps existing instance
        /// </summary>
        /// <param name="instance">An instance to interact with</param>
        ENTITY_NAME(SdaiInstance instance = NULL, TextData entityName = NULL)
            : Entity(instance, entityName ? entityName : "ENTITY_NAME")
        {}

//## EntityCreateMethod
        /// <summary>
        /// Create new instace of ENTITY_NAME and returns object of this C++ class to interact with
        /// </summary>
        static ENTITY_NAME Create(SdaiModel model) { SdaiInstance inst = sdaiCreateInstanceBN(model, "ENTITY_NAME"); assert(inst); return inst; }        
//## AttributeSimpleGet

        Nullable<SimpleType> get_ATTR_NAME() { SimpleType val = (SimpleType)0; if (sdaiGetAttrBN(m_instance, "ATTR_NAME", sdaiTYPE, &val)) return val; else return Nullable<SimpleType>(); }
//## AttributeSimpleSet
        void set_ATTR_NAME(SimpleType value) { sdaiPutAttrBN(m_instance, "ATTR_NAME", sdaiTYPE, &value); }
//## AttributeTextGet

       TextType get_attr_NAME() { TextType val = NULL; if (sdaiGetAttrBN(m_instance, "ATTR_NAME", sdaiSTRING, &val)) return val; else return NULL; }
//## AttributeTextSet
        void set_ATTR_NAME(TextType value) { sdaiPutAttrBN(m_instance, "ATTR_NAME", sdaiSTRING, value); }
//## AttributeEntityGet

        REF_ENTITY get_Attr_NAME();
//## AttributeEntitySet
        void set_Attr_NAME(REF_ENTITY inst);
//## AttributeEnumGet

        Nullable<ENUMERATION_NAME> get_ATtr_NAME() { int v = getENUM("ATTR_NAME", ENUMERATION_VALUES_ARRAY); if (v >= 0) return (ENUMERATION_NAME)v; else return Nullable<ENUMERATION_NAME>(); }
//## AttributeEnumSet
        void set_ATTR_NAME(ENUMERATION_NAME value) { TextData val = ENUMERATION_VALUES_ARRAY[(int)value]; sdaiPutAttrBN(m_instance, "ATTR_NAME", sdaiENUM, val); }
//## AttributeSelectAccessor
        GEN_TYPE_NAME_accessor getOrset_ATTR_NAME() { return GEN_TYPE_NAME_accessor(m_instance, "ATTR_NAME", NULL); }
//## AttributeAggregationGet

        void get_ATTr_NAME(AggregationType& lst) { lst.FromAttr (m_instance, "ATTR_NAME"); }
//## AttributeAggregationSet
        void set_ATTr_NAME(const AggregationType& lst) { lst.ToSdaiAggr(m_instance, "ATTR_NAME"); }
//## AttributeAggregationSetArraySimple
        void set_ATTr_NAME(const SimpleType arr[], size_t n) { AggregationType::ToSdaiAggr(arr, n, m_instance, "ATTR_NAME"); }
//## AttributeAggregationSetArrayText
        void set_ATTr_NAME(TextData arr[], size_t n) { Aggregationtype::ToSdaiAggr(arr, n, m_instance, "ATTR_NAME"); }
//## EntityEnd
    };

//## SelectEntityGetImplementation
    REF_ENTITY GEN_TYPE_NAME_accessor::get_REF_ENTITY() { return getEntityInstance("TypeNameUpper"); }
//## SelectEntitySetImplementation
    void GEN_TYPE_NAME_accessor::set_REF_ENTITY(REF_ENTITY inst) { setEntityInstance("TypeNameUpper", inst); }
//## AttributeEntityGetImplementation
    REF_ENTITY ENTITY_NAME::get_Attr_NAME() { SdaiInstance inst = 0; sdaiGetAttrBN(m_instance, "ATTR_NAME", sdaiINSTANCE, &inst); return inst; }
//## AttributeEntitySetImplementation
    void ENTITY_NAME::set_Attr_NAME(REF_ENTITY inst) { SdaiInstance i = inst;  sdaiPutAttrBN(m_instance, "ATTR_NAME", sdaiINSTANCE, (void*)i); }
//## TEMPLATE: EndFile template part

}

#endif //__RDF_LTD__NAMESPACE_NAME_H
