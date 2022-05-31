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
        /// Conversion to instance handle, so the object of the class can be used anywhere where a handle required
        /// </summary>
        operator SdaiInstance() const { return m_instance; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="attrName"></param>
        /// <returns></returns>
        const char* get_sdaiSTRING(const char* attrName)
        {
            const char* str = NULL;
            sdaiGetAttrBN(m_instance, attrName, sdaiSTRING, (void*) &str);
            return str;
        }

        void put_sdaiSTRING(const char* attrName, const char* value)
        {
            sdaiPutAttrBN(m_instance, attrName, sdaiSTRING, value);
        }

        double get_sdaiREAL(const char* attrName)
        {
            double val = NULL;
            sdaiGetAttrBN(m_instance, attrName, sdaiREAL, &val);
            return val;
        }

        void put_sdaiREAL(const char* attrName, double value)
        {
            sdaiPutAttrBN(m_instance, attrName, sdaiREAL, &value);
        }

        bool get_sdaiBOOLEAN(const char* attrName)
        {
            bool val = NULL;
            sdaiGetAttrBN(m_instance, attrName, sdaiBOOLEAN, &val);
            return val;
        }

        void put_sdaiBOOLEAN(const char* attrName, bool value)
        {
            sdaiPutAttrBN(m_instance, attrName, sdaiBOOLEAN, &value);
        }

        int64_t get_sdaiLOGICAL(const char* attrName)
        {
            int64_t val = NULL;
            sdaiGetAttrBN(m_instance, attrName, sdaiLOGICAL, &val);
            return val;
        }

        void put_sdaiLOGICAL(const char* attrName, int64_t value)
        {
            sdaiPutAttrBN(m_instance, attrName, sdaiLOGICAL, &value);
        }

        int64_t get_sdaiINTEGER(const char* attrName)
        {
            int64_t val = NULL;
            sdaiGetAttrBN(m_instance, attrName, sdaiINTEGER, &val);
            return val;
        }

        void put_sdaiINTEGER(const char* attrName, int64_t value)
        {
            sdaiPutAttrBN(m_instance, attrName, sdaiINTEGER, &value);
        }



#if 0
        /// <summary>
        /// Get property id from property name
        /// </summary>
        int64_t GetPropertyId(const char* name, int64_t checkCardinality = -1)
        {
            int64_t model = GetModel(m_instance);
            assert(model != 0);

            int64_t propId = GetPropertyByName(model, name);
            assert(propId != 0);

#ifdef _DEBUG
            if (propId) {
                int64_t clsId = GetInstanceClass(m_instance);
                int64_t minCard = 0, maxCard = 0;
                GetPropertyRestrictionsConsolidated(clsId, propId, &minCard, &maxCard);
                assert(minCard >= 0); //property assigned to the class
                if (checkCardinality > 0) { //chek cardinatity when set property
                    assert(checkCardinality >= minCard && (checkCardinality <= maxCard || maxCard < 0)); //cardinality is in range
                }
            }
#endif

            return propId;
        }

        ///<summary></summary>
        template<typename TElem> void SetDatatypeProperty(const char* name, TElem* values, int64_t count)
        {
            int64_t propId = GetPropertyId(name, count);
            int64_t res = ::SetDatatypeProperty(m_instance, propId, values, count);
            assert(res == 0);
        }


        ///<summary>The method returns pointer to inernal buffer, a caller should not free or change it.</summary>
        template<typename TElem> const TElem*  GetDatatypeProperty(const char* name, int64_t* pCount)
        {
            int64_t propId = GetPropertyId(name);

            TElem* values = NULL;
            int64_t count = 0;
            int64_t res = ::GetDatatypeProperty(m_instance, propId, (void**)&values, &count);
            assert(res == 0);

            if (pCount) {
                *pCount = count;
            }

            if (count > 0) {
                return values;
            }
            else {
                return NULL;
            }
        }


        ///<summary></summary>
        template<class TInstance> void SetObjectProperty(const char* name, const TInstance* instances, int64_t count)
        {
            int64_t propId = GetPropertyId(name, count);
            int64_t res = ::SetObjectProperty(m_instance, propId, (int64_t*)instances, count);
            assert(res == 0);
        }

        ///<summary>The method returns pointer to inernal buffer, a caller should not free or change it.</summary>
        template<class TInstance> const TInstance* GetObjectProperty(const char* name, int64_t* pCount)
        {
            int64_t propId = GetPropertyId(name);

            int64_t* values = NULL;
            int64_t count = 0;
            int64_t res = ::GetObjectProperty(m_instance, propId, &values, &count);
            assert(res == 0);

            if (pCount) {
                *pCount = count;
            }

            if (count > 0) {
                return (TInstance*)values;
            }
            else {
                return NULL;
            }
        }
#endif
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
        
//## TEMPLATE: SetSimpleAttribute
        void set_ATTR_NAME(double value) { put_sdaiREAL ("ATTR_NAME", value); }
//## TEMPLATE: GetSimpleAttribute
        ///
        double get_ATTR_NAME() { return get_sdaiREAL("ATTR_NAME"); }
//## TEMPLATE SetDataArrayProperty
        ///<summary>Sets values of PROPERTY_NAME. OWL cardinality CARDINALITY_MIN..CARDINALITY_MAX</summary>
        //void set_PROPERTY_NAME(double* values, int64_t count) { SetDatatypeProperty ("PROPERTY_NAME", values, count); }
//## TEMPLATE GetDataProperty
        ///<summary>Gets a value of PROPERTY_NAME, returns NULL is the property was not set. The method returns pointer to inernal buffer, a caller should not free or change it.</summary>
        //const double* get_PROPERTY_NAME() { return GetDatatypeProperty<double>("PROPERTY_NAME", NULL); }
//## TEMPLATE GetDataArrayProperty
        ///<summary>Gets values array of PROPERTY_NAME. OWL cardinality CARDINALITY_MIN..CARDINALITY_MAX. The method returns pointer to inernal buffer, a caller should not free or change it.</summary>
        //const double* get_PROPERTY_NAMEasType(int64_t* pCount) { return GetDatatypeProperty<double>("PROPERTY_NAME", pCount); }
//## TEMPLATE: SetObjectProperty
        ///<summary>Sets relationship from this instance to an instance of Entity</summary>
        //void set_PROPERTY_NAME(const Entity& instance) { SetObjectProperty<Entity>("PROPERTY_NAME", &instance, 1); }
//## TEMPLATE SetObjectArrayProperty
        ///<summary>Sets relationships from this instance to an array of Entity. OWL cardinality CARDINALITY_MIN..CARDINALITY_MAX</summary>
        //void set_PROPERTY_NAME(const Entity* instances, int64_t count) { SetObjectProperty<Entity>("PROPERTY_NAME", instances, count); }
//## TEMPLATE GetObjectProperty
        ///<summary>Get related instance. The method returns pointer to inernal buffer, a caller should not free or change it</summary>
        //const Entity* get_PROPERTY_NAMEasTYPe() { return GetObjectProperty<Entity>("PROPERTY_NAME", NULL); }
//## TEMPLATE GetObjectArrayProperty
        ///<summary>Get an array of related instances. OWL cardinality CARDINALITY_MIN..CARDINALITY_MAX. The method returns pointer to inernal buffer, a caller should not free or change it.</summary>
        //const Entity* get_PROPERTY_NAMEasTYPE(int64_t* pCount) { return GetObjectProperty<Entity>("PROPERTY_NAME", pCount); }
//## TEMPLATE GetObjectArrayPropertyInt64
        ///<summary>Get an array of related instance handles. OWL cardinality CARDINALITY_MIN..CARDINALITY_MAX. The method returns pointer to inernal buffer, a caller should not free or change it.</summary>
        //const int64_t* get_PROPERTY_NAME_int64(int64_t* pCount) { return GetObjectProperty<int64_t>("PROPERTY_NAME", pCount); }
//## TEMPLATE: EndEntity
    };
//## TEMPLATE: EndFile template part
}

#endif //__RDF_LTD__NAMESPACE_NAME_H
