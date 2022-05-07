//
// Helper classes (C++ wrappers)
//

#ifndef ASSERT
#define ASSERT assert
#endif

typedef const char* string;
#define null NULL

namespace RDF
{
//## TEMPLATE: ClassForwardDeclaration
    class CLASS_NAME;
//## TEMPLATE: BeginAllClasses


    /// <summary>
    /// Provides utility methods to interact with a genetic instnace of OWL class
    /// You also can use object of this class instead of __int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    class Instance
    {
    protected:
        /// <summary>
        /// underlyed instance handle
        /// </summary>
        __int64 m_instance;

    public:
        /// <summary>
        /// Create an isnatnce of specified class
        /// </summary>
        static __int64 Create(__int64 model, string className, string instanseName)
        {
            __int64 clsid = GetClassByName(model, className);
            ASSERT(clsid != 0);

            __int64 instance = CreateInstance(clsid, instanseName);
            ASSERT(instance != 0);

            return instance;
        }

        /// <summary>
        /// Create an isnatnce of specified class
        /// </summary>
        static __int64 Create(__int64 model, string className) { return Create(model, className, null); }

        /// <summary>
        /// Constructs object that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="chekClassName">Expected OWL class of the isnatnce, used for diagnostic (optionally)</param>
        Instance(__int64 instance, string cls)
        {
            m_instance = instance;
#ifdef _DEBUG
            if (m_instance != 0 && cls != null) {
                auto clsid1 = GetInstanceClass(m_instance);
                auto model = GetModel(m_instance);
                auto clsid2 = GetClassByName(model, cls);
                ASSERT(clsid1 == clsid2);
            }
#endif
        }


        /// <summary>
        /// Conversion to instance handle, so the object of the class can be used anywhere where a handle required
        /// </summary>
        operator __int64() { return m_instance; }

        /// <summary>
        /// Get property id from property name
        /// </summary>
        __int64 GetPropertyId(string name)
        {
            auto model = GetModel(m_instance);
            ASSERT(model != 0);

            auto propId = GetPropertyByName(model, name);
            ASSERT(propId != 0);

            return propId;
        }

        /// <summary>
        /// 
        /// </summary>
        template<typename TElem> void SetDatatypeProperty(string name, TElem* values, __int64 count)
        {
            auto propId = GetPropertyId(name);
            auto res = ::SetDatatypeProperty(m_instance, propId, values, count);
            ASSERT(res == 0);
        }


        /// <summary>
        /// 
        /// </summary>
        template<typename TElem> TElem* GetDatatypeProperty(string name, __int64* pCount)
        {
            auto propId = GetPropertyId(name);

            TElem* values = NULL;
            __int64 count = 0;
            auto res = ::GetDatatypeProperty(m_instance, propId, (void**)&values, &count);
            ASSERT(res == 0);

            if (pCount) {
                *pCount = count;
            }

            if (count > 0) {
                return values;
            }
            else {
                return null;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        template<class TInstance> void SetObjectProperty(string name, TInstance* instances, __int64 count)
        {
            auto propId = GetPropertyId(name);
            auto res = ::SetObjectProperty(m_instance, propId, (__int64*)instances, count);
            ASSERT(res == 0);
        }

        /// <summary>
        /// 
        /// </summary>
        template<class TInstance> TInstance* GetObjectProperty(string name, __int64* pCount)
        {
            auto propId = GetPropertyId(name);

            __int64* values = NULL;
            __int64 count = 0;
            auto res = ::GetObjectProperty(m_instance, propId, &values, &count);
            ASSERT(res == 0);

            if (pCount) {
                *pCount = count;
            }

            if (count > 0) {
                return (TInstance*)values;
            }
            else {
                return null;
            }
        }
    };

//## TEMPLATE: BeginWrapperClass

    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class CLASS_NAME
    /// You also can use object of this C++ class instead of __int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    class CLASS_NAME : public /*BASE CLASS*/Instance
    {
    public:
        /// <summary>
        /// Create new instace of OWL class CLASS_NAME and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        static CLASS_NAME Create(__int64 model, string name=null) { return CLASS_NAME(Instance::Create(model, "CLASS_NAME", name), "CLASS_NAME");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="chekClassName">Expected OWL class of the isnatnce, used for diagnostic (optionally)</param>
        CLASS_NAME(__int64 instance = null, string chekClassName = null)
            : /*BASE CLASS*/Instance(instance, (chekClassName != null) ? chekClassName : "CLASS_NAME")
        {}
//## TEMPLATE StartPropertiesBlock

       //
       // Properties with known cardinality restrictions to PROPERTIES_OF_CLASS
       //

//## TEMPLATE: SetDataProperty
        ///<summary>Sets value of PROPERTY_NAME</summary>
        void set_PROPERTY_NAME(double value) { SetDatatypeProperty ("PROPERTY_NAME", &value, 1); }
//## TEMPLATE SetDataArrayProperty
        ///<summary>Sets values of PROPERTY_NAME. OWL cardinality CARDINALITY_MIN..CARDINALITY_MAX</summary>
        void set_PROPERTY_NAME(double* values, __int64 count) { SetDatatypeProperty ("PROPERTY_NAME", values, count); }
//## TEMPLATE GetDataProperty
        ///<summary>Gets value of PROPERTY_NAME, returns null is the property was not set</summary>
        double* get_PROPERTY_NAME() { return GetDatatypeProperty<double>("PROPERTY_NAME", null); }
//## TEMPLATE GetDataArrayProperty
        ///<summary>Gets values of PROPERTY_NAME. OWL cardinality CARDINALITY_MIN..CARDINALITY_MAX</summary>
        double* get_PROPERTY_NAMEasType(__int64* pCount) { return GetDatatypeProperty<double>("PROPERTY_NAME", pCount); }
//## TEMPLATE: SetObjectProperty
        ///<summary>Sets relationship from this instance to an instance of Instance</summary>
        void set_PROPERTY_NAME(Instance& instance) { SetObjectProperty<Instance>("PROPERTY_NAME", &instance, 1); }
//## TEMPLATE SetObjectArrayProperty
        ///<summary>Sets relationships from this instance to an array of Instance. OWL cardinality CARDINALITY_MIN..CARDINALITY_MAX</summary>
        void set_PROPERTY_NAME(Instance* instances, __int64 count) { SetObjectProperty<Instance>("PROPERTY_NAME", instances, count); }
//## TEMPLATE GetObjectProperty
        ///<summary>Get related instance</summary>
        Instance* get_PROPERTY_NAMEasTYPe() { return GetObjectProperty<Instance>("PROPERTY_NAME", null); }
//## TEMPLATE GetObjectArrayProperty
        ///<summary>Get an array of related instances. OWL cardinality CARDINALITY_MIN..CARDINALITY_MAX</summary>
        Instance* get_PROPERTY_NAMEasTYPE(__int64* pCount) { return GetObjectProperty<Instance>("PROPERTY_NAME", pCount); }
//## TEMPLATE GetObjectArrayPropertyInt64
        __int64* get_PROPERTY_NAME_int64(__int64* pCount) { return GetObjectProperty<__int64>("PROPERTY_NAME", pCount); }
//## TEMPLATE: EndWrapperClass
    };
//## TEMPLATE: EndFile template part
}

