//
// Helper classes (C# wrappers)
//
using System;
using System.Runtime.InteropServices;

namespace Engine
{
//## TEMPLATE: BeginWrapperClass
    /// <summary>
    /// 
    /// </summary>
    public class CLASS_NAME : /*BASE CLASS*/Instance
    {
        public CLASS_NAME(Int64 instance, string chekClassName = null) 
            : base (instance, (chekClassName!=null) ? chekClassName : "CLASS_NAME") 
        {
        }
//## TEMPLATE StartPropertiesBlock
       //
       // Properties with assigned cardinality to CLASS_NAME
       //
//## TEMPLATE: SetDataProperty
        public void set_PROPERTY_NAME(double value) { SetDatatypeProperty ("PROPERTY_NAME", value); }
//## TEMPLATE SetDataArrayProperty
        public void set_PROPERTY_NAME(double[] values) { SetDatatypeProperty ("PROPERTY_NAME", values); }
//## TEMPLATE GetDataProperty
        public double? get_PROPERTY_NAME() { var arr = GetDatatypeProperty_double("PROPERTY_NAME"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
//## TEMPLATE GetDataArrayProperty
        public double[] get_PROPERTY_NAMEasType() { return GetDatatypeProperty_double("PROPERTY_NAME"); }
//## TEMPLATE: SetObjectProperty
        public void set_PROPERTY_NAME(Instance instance) { SetObjectProperty("PROPERTY_NAME", instance); }
//## TEMPLATE SetObjectArrayProperty
        public void set_PROPERTY_NAME(Instance[] instances) { SetObjectProperty("PROPERTY_NAME", instances); }
//## TEMPLATE GetObjectProperty
        public Instance get_PROPERTY_NAMEasTYPe() 
        {
            var propId = GetPropertyId("PROPERTY_NAME");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = Engine.x86_64.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Instance(values[0], null);
            }
            else
            {
                return null;
            }
        }
//## TEMPLATE GetObjectArrayProperty
        public Instance[] get_PROPERTY_NAMEasTYPE() 
        {
            var propId = GetPropertyId("PROPERTY_NAME");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = Engine.x86_64.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                var ret = new Instance[card];
                for (int i = 0; i < card; i++)
                {
                    ret[i] = new Instance(values[i], null);
                }

                return ret;
            }
            else
            {
                return null;
            }
        }
//## TEMPLATE GetObjectArrayPropertyInt64
        public Int64[] get_PROPERTY_NAME_Int64()  //TODO - do we need this variant ?
        {
            var propId = GetPropertyId("PROPERTY_NAME");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = Engine.x86_64.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
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
//## TEMPLATE: EndWrapperClass
    }

//## TEMPLATE - BeginFactoryClass
    /// <summary>
    /// Factory class to create instances
    /// </summary>
    public class Create
    {
//## TEMPLATE: FactoryMethod template part
        /// <summary>Create instance of CLASS_NAME</summary>
        public static CLASS_NAME CLASS_NAME(Int64 model, string name=null) { return new CLASS_NAME (CreateInstance(model, "CLASS_NAME", name));}
//## TEMPLATE: EndFile template part

        private static Int64 CreateInstance(Int64 model, string className, string instanseName)
        {
            Int64 clsid = x86_64.GetClassByName(model, className);
            System.Diagnostics.Debug.Assert(clsid != 0);

            Int64 instance = x86_64.CreateInstance(clsid, instanseName);
            System.Diagnostics.Debug.Assert(instance != 0);

            return instance;
        }
    }

    /// <summary>
    /// Generic instance
    /// </summary>
    public class Instance
    {
        /// <summary>
        /// 
        /// </summary>
        protected Int64 m_instance = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="cls"></param>
        public Instance(Int64 instance, string cls)
        {
            m_instance = instance;
#if DEBUG
            if (m_instance != 0 && cls != null)
            {
                var clsid1 = x86_64.GetInstanceClass(m_instance);
                var model = x86_64.GetModel(m_instance);
                var clsid2 = x86_64.GetClassByName(model, cls);
                System.Diagnostics.Trace.Assert(clsid1 == clsid2);
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
        public void SetDatatypeProperty(string name, double[] values)
        {
            var propId = GetPropertyId(name);
            var res = x86_64.SetDataTypeProperty(m_instance, propId, values, values.Length);
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
        /// <param name="values"></param>
        public void SetDatatypeProperty(string name, long[] values)
        {
            var propId = GetPropertyId(name);
            var res = x86_64.SetDataTypeProperty(m_instance, propId, values, values.Length);
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
            byte v = (byte)(value ? 1 : 0);
            var res = x86_64.SetDataTypeProperty(m_instance, propId, ref v, 1);
            System.Diagnostics.Debug.Assert(res == 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="values"></param>
        public void SetDatatypeProperty(string name, bool[] values)
        {
            byte[] bytes = new byte[values.Length];
            for (int i = 0; i < values.Length; i++)
                bytes[i] = values[i] ? (byte)1 : (byte)0;

            var propId = GetPropertyId(name);
            var res = x86_64.SetDataTypeProperty(m_instance, propId, bytes, values.Length);
            System.Diagnostics.Debug.Assert(res == 0);
        }

        /// <summary>
        /// 1111
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
        /// <param name="value"></param>
        public void SetDatatypeProperty(string name, string[] values)
        {
            var propId = GetPropertyId(name);
            var res = x86_64.SetDataTypeProperty(m_instance, propId, values, values.Length);
            System.Diagnostics.Debug.Assert(res == 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public double[] GetDatatypeProperty_double(string name)
        {
            var propId = GetPropertyId(name);

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = Engine.x86_64.GetDatatypeProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                System.Diagnostics.Debug.Assert(card == 1);

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
        /// <param name="name"></param>
        /// <returns></returns>
        public long[] GetDatatypeProperty_long(string name)
        {
            var propId = GetPropertyId(name);

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = Engine.x86_64.GetDatatypeProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                System.Diagnostics.Debug.Assert(card == 1);

                var values = new long[card];
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
        /// <param name="name"></param>
        /// <returns></returns>
        public bool[] GetDatatypeProperty_bool(string name)
        {
            var propId = GetPropertyId(name);

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = Engine.x86_64.GetDatatypeProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                System.Diagnostics.Debug.Assert(card == 1);

                var values = new long[card];
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
        /// <param name="name"></param>
        /// <returns></returns>
        public string[] GetDatatypeProperty_string(string name)
        {
            var propId = GetPropertyId(name);

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = Engine.x86_64.GetDatatypeProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                System.Diagnostics.Debug.Assert(card == 1);

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
        /// <param name="name"></param>
        /// <param name="instance"></param>
        public void SetObjectProperty(string name, Int64 instance)
        {
            var propId = GetPropertyId(name);
            var res = x86_64.SetObjectProperty(m_instance, propId, ref instance, 1);
            System.Diagnostics.Debug.Assert(res == 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="instances"></param>
        public void SetObjectProperty(string name, Instance[] instances)
        {
            var inst = new Int64[instances.Length];
            for (int i = 0; i < instances.Length; i++)
                inst[i] = instances[i];

            SetObjectProperty(name, inst);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="instances"></param>
        public void SetObjectProperty(string name, Int64[] instances)
        {
            var propId = GetPropertyId(name);

            var inst = new Int64[instances.Length];
            for (int i = 0; i < instances.Length; i++)
                inst[i] = instances[i];

            var res = x86_64.SetObjectProperty(m_instance, propId, ref inst[0], inst.Length);
            System.Diagnostics.Debug.Assert(res == 0);
        }

    }
}

