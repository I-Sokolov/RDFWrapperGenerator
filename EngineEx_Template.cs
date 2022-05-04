//
// Helper classes (C# wrappers)
//
using System;
using System.Runtime.InteropServices;

namespace Engine
{
    /// <summary>
    /// Wrapper class for generic instance
    /// </summary>
    public class Instance
    {
        /// <summary>
        /// 
        /// </summary>
        private Int64 m_instance = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="cls"></param>
        public Instance (Int64 instance, string cls)
        {
            m_instance = instance;
#if DEBUG
            if (m_instance != 0 && cls != null)
            {
                var clsid1 = x86_64.GetInstanceClass(m_instance);
                var model = x86_64.GetModel(m_instance);
                var clsid2 = x86_64.GetClassByName(model, cls);
                System.Diagnostics.Trace.Assert(clsid1 != clsid2);
            }
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        public static implicit operator Int64(Instance instance) => instance.m_instance;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Int64 GetPropertyId(string name)
        {
            var model = x86_64.GetModel(m_instance);
            System.Diagnostics.Debug.Assert(model != 0);

            var propId = x86_64.GetPropertyByName(model, name);
            System.Diagnostics.Debug.Assert(propId != 0);

            return propId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void SetDatatypeProperty(string name, double value)
        {
            var propId = GetPropertyId(name);
            var res = x86_64.SetDataTypeProperty(m_instance, propId, ref value, 1);
            System.Diagnostics.Debug.Assert(res == 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void SetDatatypeProperty(string name, long value)
        {
            var propId = GetPropertyId(name);
            var res = x86_64.SetDataTypeProperty(m_instance, propId, ref value, 1);
            System.Diagnostics.Debug.Assert(res == 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void SetDatatypeProperty(string name, bool value)
        {
            var propId = GetPropertyId(name);
            byte v = (byte)(value?1:0);
            var res = x86_64.SetDataTypeProperty(m_instance, propId, ref v, 1);
            System.Diagnostics.Debug.Assert(res == 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void SetDatatypeProperty(string name, string value)
        {
            var propId = GetPropertyId(name);
            var res = x86_64.SetDataTypeProperty(m_instance, propId, ref value, 1);
            System.Diagnostics.Debug.Assert(res == 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="instance"></param>
        public void SetObjectProperty(string name, Int64 instance)
        {
            var propId = GetPropertyId(name);
            var res = x86_64.SetObjectProperty(m_instance, propId, ref instance, 1);
            System.Diagnostics.Debug.Assert(res == 0);
        }

    }

//## TEMPLATE: BeginWrapperClass

/// <summary>
/// 
/// </summary>
public class INSTANCE_CLASS : Instance
    {
        public INSTANCE_CLASS(Int64 instance) : base (instance, "INSTANCE_CLASS")  {}

//## TEMPLATE: SetDataProperty
        public void set_PROPERTY_NAME (double value) { SetDatatypeProperty ("ROPERTY_NAME", value); }

//## TEMPLATE: SetObjectProperty
        public void set_RPOPERTY_NAME (Instance instance) { SetObjectProperty("PROPERTY_NAME", instance); }
//## TEMPLATE: EndWrapperClass

    }

//## TEMPLATE - BeginFactoryClass

    /// <summary>
    /// Factory class to create instances
    /// </summary>
    public class Create
    {
//## FactoryMethod template part
        /// <summary> 
        /// Create instance of INSTANCE_CLASS
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">Name of the instance (optional)</param>
        /// <returns>Returns a handle to created instance</returns>
        public static INSTANCE_CLASS INSTANCE_CLASS(Int64 model, string name=null) { return new INSTANCE_CLASS (CreateInstance(model, "INSTANCE_CLASS", name));}

//## EndFile template part
        private static Int64 CreateInstance(Int64 model, string className, string instanseName)
        {
            Int64 clsid = x86_64.GetClassByName(model, className);
            System.Diagnostics.Debug.Assert(clsid != 0);

            Int64 instance = x86_64.CreateInstance(clsid, instanseName);
            System.Diagnostics.Debug.Assert(instance != 0);

            return instance;
        }
    }
}

