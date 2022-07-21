//
// Early-binding C# API for SDAI (CE wrappers)
//
using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using RDF;

namespace NAMESPACE_NAME
{
    using SdaiModel = Int64;
    using SdaiInstance = Int64;
    using SdaiAggr = Int64;

    using IntValue = Int64;
    using TextValue = String;

    /// <summary>
    /// 
    /// </summary>
    class EnumValue<TEnum> where TEnum : struct, Enum
    {
        static public TEnum? FromIndex(int index)
        {
            var values = System.Enum.GetValues<TEnum>();
            if (index >= 0 && index < values.Length)
            {
                return values[index];
            }
            else
            {
                return null;
            }
        }
    }

    class EnumIndex
    {
        static public int FromString(TextValue value, TextValue[] allStrings)
        {
            for (int i = 0; i < allStrings.Length; i++)
            {
                if (value == allStrings[i])
                    return i;
            }
            return -1;
        }
    }

    class EnumString<TEnum> where TEnum : struct, Enum, IComparable
    {
        public static TextValue FromValue(TEnum value, TextValue[] allStrings)
        {
            var values = System.Enum.GetValues<TEnum>();

            for (int i = 0; i < values.Length; i++)
            {
                if (values[i].Equals(value))
                {
                    if (i < allStrings.Length)
                    {
                        return allStrings[i];
                    }
                    else
                    {
                        Debug.Assert(false);
                        return null;
                    }
                }
            }

            Debug.Assert(false);
            return null;
        }
    }

    /// <summary>
    /// Helper class to handle and access SELECT instance data
    /// </summary>
    public class Select
    {
        protected SdaiInstance m_instance;
        protected TextValue m_attrName;

        private IntValue m_adb;
        private Select m_outerSelect;

        public IntValue ADB()
        {
            if (m_outerSelect != null)
            {
                return m_outerSelect.ADB();
            }

            if (m_adb == 0 && m_instance != 0 && m_attrName != null)
            {
                m_adb = ifcengine.sdaiCreateEmptyADB();
                if (0 != ifcengine.sdaiGetAttrBN(m_instance, m_attrName, ifcengine.sdaiADB, out m_adb))
                {
                    ifcengine.sdaiDeleteADB(m_adb);
                    m_adb = 0;
                }
            }

            return m_adb;
        }

        protected Select(SdaiInstance instance, TextValue attrName = null, IntValue adb = 0)
        {
            Init(instance, attrName, adb);
        }

        protected Select(Select outer)
        {
            m_instance = 0;
            m_attrName = null;
            m_adb = 0;
            m_outerSelect = outer;
            if (m_outerSelect != null)
            {
                m_instance = m_outerSelect.m_instance;
            }
        }

        public void Init(SdaiInstance instance, TextValue attrName = null, IntValue adb = 0)
        {
            Debug.Assert(instance != 0);
            m_instance = instance;
            m_attrName = attrName;
            m_adb = adb;
            m_outerSelect = null;
        }

        protected void SetADB(IntValue adb)
        {
            if (m_outerSelect != null)
            {
                m_outerSelect.SetADB(adb);
            }
            else
            {
                //???sdaiDeleteADB(m_adb);
                m_adb = adb;

                if (m_instance != 0 && m_attrName != null)
                {
                    ifcengine.sdaiPutAttrBN(m_instance, m_attrName, ifcengine.sdaiADB, m_adb);
                }
            }
        }

        private bool CheckADBType(IntValue adb, TextValue typeName)
        {
            if (adb == 0)
            {
                return false;
            }

            if (typeName == null)
            {
                return true; //any suitable
            }

            var pPath = ifcengine.sdaiGetADBTypePath(adb, 0);
            var path = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(pPath);

            return path != null && path == typeName;
        }

        //
        //
        protected IntValue? get_IntValue(TextValue typeName, IntValue sdaiType)
        {
            IntValue? ret = null;
            var adb = ADB();
            if (CheckADBType(adb, typeName))
            {
                IntValue val = 0;
                if (ifcengine.sdaiGetADBValue(adb, sdaiType, out val) != 0)
                {
                    ret = val;
                }
            }
            return ret;
        }

        protected void put_IntValue(TextValue typeName, IntValue sdaiType, IntValue value)
        {
            var adb = ifcengine.sdaiCreateADB(sdaiType, ref value);
            ifcengine.sdaiPutADBTypePath(adb, 1, typeName);
            SetADB(adb);
        }

        //
        protected double? get_double(TextValue typeName, IntValue sdaiType)
        {
            double? ret = null;
            var adb = ADB();
            if (CheckADBType(adb, typeName))
            {
                double val = 0;
                if (ifcengine.sdaiGetADBValue(adb, sdaiType, out val) != 0)
                {
                    ret = val;
                }
            }
            return ret;
        }

        //
        protected void put_double(TextValue typeName, IntValue sdaiType, double value)
        {
            var adb = ifcengine.sdaiCreateADB(sdaiType, ref value);
            ifcengine.sdaiPutADBTypePath(adb, 1, typeName);
            SetADB(adb);
        }

        //
        protected TextValue getTextValue(TextValue typeName, IntValue sdaiType)
        {
            TextValue ret = null;
            var adb = ADB();
            if (CheckADBType(adb, typeName))
            {
                IntPtr ptr = IntPtr.Zero;
                if (ifcengine.sdaiGetADBValue(adb, sdaiType, out ptr) != 0)
                {
                    ret = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(ptr);
                }
            }
            return ret;
        }

        //
        protected void putTextValue(TextValue typeName, IntValue sdaiType, TextValue value)
        {
            var adb = ifcengine.sdaiCreateADB(sdaiType, value);
            ifcengine.sdaiPutADBTypePath(adb, 1, typeName);
            SetADB(adb);
        }

        //
        protected int getEnumerationIndex(TextValue typeName, TextValue[] rEnumValues)
        {
            int ret = -1;
            var adb = ADB();
            if (CheckADBType(adb, typeName))
            {
                IntPtr ptr = IntPtr.Zero;
                if (0 != ifcengine.sdaiGetADBValue(adb, ifcengine.sdaiENUM, out ptr))
                {
                    var value = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(ptr);
                    ret = EnumIndex.FromString(value, rEnumValues);
                }
            }
            return ret;
        }

        //
        protected void putEnumerationValue(TextValue typeName, TextValue value)
        {
            var adb = ifcengine.sdaiCreateADB(ifcengine.sdaiENUM, value);
            ifcengine.sdaiPutADBTypePath(adb, 1, typeName);
            SetADB(adb);
        }

        //
        protected SdaiInstance getEntityInstance(TextValue typeName)
        {
            SdaiInstance ret = 0;
            var adb = ADB();
            if (adb != 0)
            {
                SdaiInstance inst = 0;
                if (ifcengine.sdaiGetADBValue(adb, ifcengine.sdaiINSTANCE, out inst) != 0)
                {
                    if (typeName == null || ifcengine.sdaiIsKindOfBN(inst, typeName) != 0)
                    {
                        ret = inst;
                    }
                }
            }
            return ret;
        }

        //
        protected void putEntityInstance(TextValue typeName, SdaiInstance inst)
        {
            if (inst == 0 || ifcengine.sdaiIsKindOfBN(inst, typeName) != 0)
            {
                var adb = ifcengine.sdaiCreateADB(ifcengine.sdaiINSTANCE, inst);
                SetADB(adb);
            }
            else
            {
                Debug.Assert(false);
            }
        }

        //
        protected SdaiAggr getAggrValue(TextValue typeName)
        {
            SdaiAggr ret = 0;
            var adb = ADB();
            if (CheckADBType(adb, typeName))
            {
                if (ifcengine.sdaiGetADBValue(adb, ifcengine.sdaiAGGR, out ret) == 0)
                {
                    ret = 0;
                }
            }
            return ret;
        }

        //
        protected void putAggrValue(TextValue typeName, SdaiAggr value)
        {
            var adb = ifcengine.sdaiCreateADB(ifcengine.sdaiAGGR, value);
            ifcengine.sdaiPutADBTypePath(adb, 1, typeName);
            SetADB(adb);
        }

        //
        protected bool IsADBType(TextValue typeName)
        {
            var adb = ADB();
            return CheckADBType(adb, typeName);
        }

        protected bool IsADBEntity(TextValue typeName)
        {
            var adb = ADB();
            if (adb != 0)
            {
                SdaiInstance inst = 0;
                if (ifcengine.sdaiGetADBValue(adb, ifcengine.sdaiINSTANCE, out inst) != 0)
                {
                    if (ifcengine.sdaiIsKindOfBN(inst, typeName) != 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    };

    /// <summary>
    /// Aggregations templates
    /// </summary>
    /// 
    public interface IAggrSerializerObj
    {
        public abstract IList FromSdaiAggrUntyped(SdaiInstance inst, SdaiAggr aggr);
        public abstract SdaiAggr ToSdaiAggr(IEnumerable lst, SdaiInstance instance, TextValue attrName);

    }

    public abstract class AggrSerializer<TElem> : IAggrSerializerObj //TODO do we need these classes public?
    {
        //
        public List<TElem> FromAttr(SdaiInstance instance, TextValue attrName)
        {
            SdaiAggr aggr = 0;
            ifcengine.sdaiGetAttrBN(instance, attrName, ifcengine.sdaiAGGR, out aggr);
            return FromSdaiAggr(instance, aggr);
        }

        //
        public List<TElem> FromSdaiAggr(SdaiInstance inst, SdaiAggr aggr)
        {
            var ret = new List<TElem>();
            IntValue cnt = ifcengine.sdaiGetMemberCount(aggr);
            for (IntValue i = 0; i < cnt; i++)
            {
                TElem elem;
                if (GetAggrElement(inst, aggr, i, out elem))
                {
                    ret.Add(elem);
                }
            }
            return ret;
        }

        public SdaiAggr ToSdaiAggr(IEnumerable<TElem> lst, SdaiInstance instance, TextValue attrName)
        {
            SdaiAggr aggr = ifcengine.sdaiCreateAggrBN(instance, attrName);
            foreach (var v in lst)
            {
                AppendAggrElement(instance, aggr, v);
            }
            return aggr;
        }

        public SdaiAggr ToSdaiAggr(IEnumerable lst, SdaiInstance instance, TextValue attrName)
        {
            SdaiAggr aggr = ifcengine.sdaiCreateAggrBN(instance, attrName);
            foreach (var v in lst)
            {
                AppendAggrElement(instance, aggr, (TElem)v);
            }
            return aggr;
        }

        protected abstract bool GetAggrElement(SdaiInstance inst, SdaiAggr aggr, IntValue i, out TElem elem);
        protected abstract void AppendAggrElement(SdaiInstance inst, SdaiAggr aggr, TElem elem);

        IList IAggrSerializerObj.FromSdaiAggrUntyped(SdaiInstance inst, SdaiAggr aggr)
        {
            return FromSdaiAggr(inst, aggr);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class AggrSerializer_IntValue<TElem> : AggrSerializer<IntValue>
    {
        protected override bool GetAggrElement(SdaiInstance inst, SdaiAggr aggr, IntValue i, out IntValue elem)
        {
            ifcengine.engiGetAggrElement(aggr, i, ifcengine.sdaiINTEGER, out elem);
            return true;
        }
        protected override void AppendAggrElement(SdaiInstance inst, SdaiAggr aggr, IntValue elem)
        {
            ifcengine.sdaiAppend(aggr, ifcengine.sdaiINTEGER, ref elem);
        }
    };

    /// <summary>
    /// 
    /// </summary>
    public class AggrSerializer_double<TElem> : AggrSerializer<double>
    {
        protected override bool GetAggrElement(SdaiInstance inst, SdaiAggr aggr, IntValue i, out double elem)
        {
            ifcengine.engiGetAggrElement(aggr, i, ifcengine.sdaiREAL, out elem);
            return true;
        }
        protected override void AppendAggrElement(SdaiInstance inst, SdaiAggr aggr, double elem)
        {
            ifcengine.sdaiAppend(aggr, ifcengine.sdaiREAL, ref elem);
        }
    };

    public class AggrSerializerText<TElem> : AggrSerializer<TextValue>
    {
        private IntValue m_sdaiType;

        public AggrSerializerText(IntValue sdaiType)
        {
            Debug.Assert(sdaiType == ifcengine.sdaiSTRING || sdaiType == ifcengine.sdaiBINARY);
            m_sdaiType = sdaiType;
        }
        protected override bool GetAggrElement(SdaiInstance inst, SdaiAggr aggr, IntValue i, out TextValue elem)
        {
            IntPtr ptr = IntPtr.Zero;
            ifcengine.engiGetAggrElement(aggr, i, m_sdaiType, out ptr);
            elem = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(ptr);
            return (elem != null);
        }
        protected override void AppendAggrElement(SdaiInstance inst, SdaiAggr aggr, TextValue elem)
        {
            ifcengine.sdaiAppend(aggr, m_sdaiType, elem);
        }
    };

    public class AggrSerializerInstance<TElem> : AggrSerializer<TElem> where TElem : Entity, new()
    {
        protected override bool GetAggrElement(SdaiInstance inst, SdaiAggr aggr, IntValue i, out TElem elem)
        {
            SdaiInstance val = 0;
            ifcengine.engiGetAggrElement(aggr, i, ifcengine.sdaiINSTANCE, out val);
            elem = new TElem();
            elem.Set(val);
            return (elem != 0);
        }
        protected override void AppendAggrElement(SdaiInstance inst, SdaiAggr aggr, TElem elem)
        {
            SdaiInstance v = elem;
            ifcengine.sdaiAppend(aggr, ifcengine.sdaiINSTANCE, v);
        }
    };

    /// <summary>
    /// 
    /// </summary>
    public class AggrSerializerEnum<TEnum> : AggrSerializer<TEnum> where TEnum : struct, Enum
    {
        private IntValue m_sdaiType;
        private TextValue[] m_EnumValues;

        public AggrSerializerEnum(TextValue[] enumValues, IntValue sdaiType)
        {
            Debug.Assert(sdaiType == ifcengine.sdaiENUM || sdaiType == ifcengine.sdaiLOGICAL);
            m_EnumValues = enumValues;
            m_sdaiType = sdaiType;
        }

        //
        protected override bool GetAggrElement(SdaiInstance inst, SdaiAggr aggr, IntValue i, out TEnum elem)
        {
            IntPtr ptr = IntPtr.Zero;
            ifcengine.engiGetAggrElement(aggr, i, m_sdaiType, out ptr);
            var value = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(ptr);
            var ind = EnumIndex.FromString(value, m_EnumValues);
            var val = EnumValue<TEnum>.FromIndex(ind);
            if (val.HasValue)
            {
                elem = val.Value;
                return true;
            }
            else
            {
                elem = EnumValue<TEnum>.FromIndex(0).Value;
                return false;
            }
        }
        protected override void AppendAggrElement(SdaiInstance inst, SdaiAggr aggr, TEnum elem)
        {
            var value = EnumString<TEnum>.FromValue(elem, m_EnumValues);
            ifcengine.sdaiAppend(aggr, m_sdaiType, value);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class AggrSerializerAggr<TNestedAggr, TNestedSerializer> : AggrSerializer<TNestedAggr>
                    where TNestedAggr : IEnumerable
                    where TNestedSerializer : IAggrSerializerObj, new()
    {
        protected override bool GetAggrElement(SdaiInstance inst, SdaiAggr aggr, IntValue i, out TNestedAggr elem)
        {
            SdaiAggr nested = 0;
            ifcengine.engiGetAggrElement(aggr, i, ifcengine.sdaiAGGR, out nested);
            if (nested != 0)
            {
                var nestedSerializer = new TNestedSerializer();
                elem = (TNestedAggr)nestedSerializer.FromSdaiAggrUntyped(inst, nested);
                return true;
            }
            else
            {
                elem = default(TNestedAggr);
                return false;
            }
        }
        protected override void AppendAggrElement(SdaiInstance inst, SdaiAggr aggr, TNestedAggr elem)
        {
            var nestedSerializer = new TNestedSerializer();
            SdaiAggr nested = nestedSerializer.ToSdaiAggr(elem, inst, null);
            ifcengine.sdaiAppend(aggr, ifcengine.sdaiAGGR, nested);
        }
    };

    public class AggrSerializerSelect<TSelect> : AggrSerializer<TSelect> where TSelect : Select, new()
    {
        protected override bool GetAggrElement(SdaiInstance inst, SdaiAggr aggr, IntValue i, out TSelect elem)
        {
            IntValue adb = 0;
            ifcengine.engiGetAggrElement(aggr, i, ifcengine.sdaiADB, out adb);
            if (adb != 0)
            {
                elem = new TSelect();
                elem.Init(inst, null, adb);
                return true;
            }
            else
            {
                elem = null;
                return false;
            }
        }
        protected override void AppendAggrElement(SdaiInstance inst, SdaiAggr aggr, TSelect elem)
        {
            var adb = elem.ADB();
            if (adb != 0)
            {
                ifcengine.sdaiAppend((IntValue)aggr, ifcengine.sdaiADB, adb);
            }
        }
    };

    /// <summary>
    /// Provides utility methods to interact with a generic SDAI instnace
    /// You also can use object of this class instead of SdaiInstance handle in any place where the handle is required
    /// </summary>
    public abstract class Entity : IEquatable<Entity>, IComparable, IComparable<Entity>
    {
        public static Entity Create(SdaiModel model) { System.Diagnostics.Debug.Assert(false); return null; }

        //
        protected SdaiInstance m_instance = 0;

        public Entity(SdaiInstance instance)
        {
            Set(instance);
        }

        public void Set (SdaiInstance instance)
        {
            m_instance = instance;

            if (m_instance != 0)
            {
                if (ifcengine.sdaiIsKindOfBN(m_instance, EntityName()) == 0)
                {
                    m_instance = 0;
                }
            }
        }

        protected abstract TextValue EntityName();

        /// <summary>
        /// Conversion to instance handle, so the object of the class can be used anywhere where a handle required
        /// </summary>
        public static implicit operator SdaiInstance(Entity instance) => instance.m_instance;

        protected TextValue getString(TextValue attrName, Int64 sdaiType)
        {
            IntPtr ptr = IntPtr.Zero;
            if (0 != ifcengine.sdaiGetAttrBN(m_instance, attrName, sdaiType, out ptr))
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
        public static bool operator ==(Entity i1, Entity i2) => (Equals(i1, i2));
        public static bool operator !=(Entity i1, Entity i2) => (!(i1 == i2));
        public override bool Equals(Object obj)
        {
            return Equals(obj as Entity);
        }
        public bool Equals(Entity other)
        {
            return (other == null) ? false : (other.m_instance == m_instance);
        }
        public int CompareTo(object obj)
        {
            return CompareTo(obj as Entity);
        }
        public int CompareTo(Entity other)
        {
            return (other == null) ? 1 : m_instance.CompareTo(other.m_instance);
        }
        public override int GetHashCode()
        {
            return m_instance.GetHashCode();
        }
    }

    //## TEMPLATE: TemplateUtilityTypes

    public class SimpleType : List<object> { };
    public class SImpleType : Select { public SImpleType() : base(0) {} };
    public class SimpleTypeSerializer : AggrSerializer_double<double> { };
    public class REF_ENTITY : Entity
    {
        public REF_ENTITY() : base(0) { }
        public REF_ENTITY(SdaiInstance inst) : base (inst) { } 
        public static implicit operator REF_ENTITY(SdaiInstance value) { return new REF_ENTITY(value); }
        public static implicit operator SdaiInstance(REF_ENTITY value) { return 0; }
        protected override TextValue EntityName () { return "unknown, this class is not for usage"; } 
    };
    //## TEMPLATE: ClassForwardDeclaration
    //## TEMPLATE: DefinedTypesBegin
    //## TEMPLATE: DefinedType
    //## TEMPLATE: EnumerationsBegin

    //
    // Enumerations
    //
    public class Enums
    {
        public static TextValue[] ENUMERATION_VALUES_ARRAY = null;

        public enum LOGICAL_VALUE { False = 0, True = 1, Unknown = 2 };
        public static TextValue[] LOGICAL_VALUE_ = { "F", "T", "U" };
        //## TEMPLATE: EnumerationBegin

        public enum ENUMERATION_NAME
        {
            //## EnumerationElement
            ENUMERATION_ELEMENT = 1234,
            //## EnumerationEnd
        };
        public static TextValue[] ENUMERATION_NAME_ = { "ENUMERATION_STRING_VALUES" };
        //## TEMPLATE: EnumerationsEnd
    }

    //## TEMPLATE: AggregationTypesBegin
    //
    // Unnamed aggregations
    //
    public class AggrSerializer_SimpleType<SimpleType> { }
    //## AggregationOfSimple
    public class AggregationTYpe : List<SimpleType> { }
    public class AggregationTYpeSerializer : AggrSerializer_SimpleType<SimpleType> { }
    //## AggregationOfText
    public class Aggregationtype : List<TextValue> { }
    public class AggregationtypeSerializer : AggrSerializerText<TextValue> { public AggregationtypeSerializer() : base(ifcengine.sdaiTYPE) { } }
    //## AggregationOfInstance
    public class AggregationType : List<SimpleType> { }
    public class AggregationTypeSerializer : AggrSerializerInstance<REF_ENTITY> { }
    //## AggregationOfEnum
    public class AggregationTyPe : List<Enums.ENUMERATION_NAME> { }
    //TODO public class AggregationTyPeSerializer : AggrSerializerEnum<Enums.ENUMERATION_NAME> { public AggregationTyPeSerializer() : base(Enums.ENUMERATION_NAME_, ifcengine.sdaiTYPE) { } };
    public class AggregationTyPeSerializer : AggrSerializerEnum<Enums.ENUMERATION_NAME> { public AggregationTyPeSerializer() : base(Enums.ENUMERATION_NAME_, ifcengine.sdaiENUM) { } };
    //## AggregationOfAggregation
    public class AggregationTYPe : List<SimpleType> { }
    public class AggregationTYPeSerializer : AggrSerializerAggr<SimpleType, SimpleTypeSerializer> { }
    //## AggregationOfSelect
    public class AggregationTYPE : List<SimpleType> { }
    public class AggregationTYPESerializer : AggrSerializerSelect<SImpleType> { }
    //## SelectsBegin

    //
    // SELECT TYPES
    // 
    //## TEMPLATE: SelectAccessorBegin

    public class GEN_TYPE_NAME_accessor : Select
    {
        public GEN_TYPE_NAME_accessor(SdaiInstance instance, TextValue attrName = null, IntValue adb = 0) : base(instance, attrName, adb) { }

        /// do not use this constructor, it is for internal workflow
        public GEN_TYPE_NAME_accessor() : base (null) { }

        /// do not use this constructor, it is for internal workflow
        public GEN_TYPE_NAME_accessor(Select outer) : base(outer) { }

        //## SelectSimpleGet
        public bool is_TypeNameIfc() { return IsADBType("TypeNameUpper"); }
        public double? get_TypeNameifc() { return get_double("TypeNameUpper", ifcengine.sdaiTYPE); }
        //## SelectSimplePut
        public void put_TypeNameIfc (double value) { put_double("TypeNameUpper", ifcengine.sdaiTYPE, value); }
        //## SelectTextGet
        public bool is_TypeNameIFC() { return IsADBType("TypeNameUpper"); }
        public TextValue get_TypeNameIFC() { return getTextValue("TypeNameUpper", ifcengine.sdaiTYPE); }
        //## SelectTextPut
        public void put_TypeNameIFC(TextValue value) { putTextValue("TypeNameUpper", ifcengine.sdaiTYPE, value); }
        //## SelectEntityGet
        public bool is_REF_ENTITY() { return IsADBEntity("REF_ENTITY"); }
        public REF_ENTITY get_REF_ENTITY() { return getEntityInstance("TypeNameUpper"); }
        //## SelectEntityPut
        public void put_REF_ENTITY(REF_ENTITY inst) { putEntityInstance("TypeNameUpper", inst); }
        //## SelectEnumerationGet
        public bool is_TypeNAmeIFC() { return IsADBType("TypeNameUpper"); }
        public Enums.ENUMERATION_NAME? get_TypeNAmeIFC() { int ind = getEnumerationIndex("TypeNameUpper", Enums.ENUMERATION_VALUES_ARRAY); return EnumValue<Enums.ENUMERATION_NAME>.FromIndex(ind); }
        //## SelectEnumerationPut
        public void put_TypeNAmeIFC(Enums.ENUMERATION_NAME value) { TextValue val = EnumString<Enums.ENUMERATION_NAME>.FromValue(value, Enums.ENUMERATION_VALUES_ARRAY); putEnumerationValue("TypeNameUpper", val); }
        //## SelectAggregationGet
        public bool is_AggregationType() { return IsADBType("TypeNameUpper"); }
        public List<REF_ENTITY> get_AggregationType() { SdaiAggr aggr = getAggrValue("TypeNameUpper"); return (new AggregationTypeSerializer()).FromSdaiAggr(m_instance, aggr); }
        //## SelectAggregationPut
        public void put_AggregationType(IEnumerable<REF_ENTITY> lst) { SdaiAggr aggr = (new AggregationTypeSerializer()).ToSdaiAggr(lst, m_instance, null); putAggrValue("TypeNameUpper", aggr); }
        public void put_AggregationType(IEnumerable lst) { SdaiAggr aggr = (new AggregationTypeSerializer()).ToSdaiAggr(lst, m_instance, null); putAggrValue("TypeNameUpper", aggr); }
        //## SelectAggregationPutArray
        //## SelectNested
        GEN_TYPE_NAME_accessor nestedSelectAccess_GEN_TYPE_NAME() { return new GEN_TYPE_NAME_accessor(this); }
        //## SelectGetAsDouble
        double? as_double() { double val = 0; if (ifcengine.sdaiGetAttrBN(m_instance, m_attrName, ifcengine.sdaiREAL, out val) != 0) return val; else return null; }
        //## SelectGetAsInt
        IntValue? as_int() { IntValue val = 0; if (ifcengine.sdaiGetAttrBN(m_instance, m_attrName, ifcengine.sdaiINTEGER, out val) != 0) return val; else return null; }
        //## SelectGetAsBool
        bool? as_bool() { bool val = false; if (ifcengine.sdaiGetAttrBN(m_instance, m_attrName, ifcengine.sdaiBOOLEAN, out val) != 0) return val; else return null; }
        //## SelectGetAsText
        TextValue as_text() { IntPtr ptr = IntPtr.Zero; ifcengine.sdaiGetAttrBN(m_instance, m_attrName, ifcengine.sdaiSTRING, out ptr); return System.Runtime.InteropServices.Marshal.PtrToStringAnsi(ptr); }
        //## SelectGetAsEntity
        SdaiInstance as_instance() { return getEntityInstance(null); }
        //## SelectAccessorEnd
    };

    //## TEMPLATE: EntitiesBegin

    //
    // Entities
    // 

    //## TEMPLATE: EntityBegin

    /// <summary>
    /// Provides utility methods to interact with an instnace of ENTITY_NAME
    /// You also can use object of this C++ class instead of IntValue handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class ENTITY_NAME : /*PARENT_NAME*/Entity
    {
        /// <summary>
        /// Constructs object of this C++ class that wraps existing SdaiInstance of ENTITY_NAME
        /// </summary>
        /// <param name="instance">An instance to interact with</param>
        public ENTITY_NAME(SdaiInstance instance) : base(instance) { }
        public ENTITY_NAME() : base(0) { }

        public static implicit operator ENTITY_NAME(SdaiInstance instance) => new ENTITY_NAME(instance);

        protected override TextValue EntityName () { return "ENTITY_NAME"; }

        //## EntityCreateMethod
        /// <summary>
        /// Create new instace of ENTITY_NAME and returns object of this class to interact with
        /// </summary>
        public static new ENTITY_NAME Create(SdaiModel model) { SdaiInstance inst = ifcengine.sdaiCreateInstanceBN(model, "ENTITY_NAME"); Debug.Assert(inst != 0); return inst; }
        //## AttributeSimpleGet

        public double? get_ATTR_NAME() { double val = 0; if (ifcengine.sdaiGetAttrBN(m_instance, "ATTR_NAME", ifcengine.sdaiTYPE, out val) != 0) return val; else return null; }
        //## AttributeSimplePut
        public void put_ATTR_NAME(double value) { ifcengine.sdaiPutAttrBN(m_instance, "ATTR_NAME", ifcengine.sdaiTYPE, ref value); }
        //## AttributeTextGet

        public TextValue get_attr_NAME() { return getString("ATTR_NAME", ifcengine.sdaiTYPE); }
        //## AttributeTextPut
        public void put_ATTR_NAME(TextValue value) { ifcengine.sdaiPutAttrBN(m_instance, "ATTR_NAME", ifcengine.sdaiTYPE, value); }
        //## AttributeEntityGet

        public REF_ENTITY get_Attr_NAME() { SdaiInstance inst = 0; ifcengine.sdaiGetAttrBN(m_instance, "ATTR_NAME", ifcengine.sdaiINSTANCE, out inst); return inst; }
        //## AttributeEntityPut
        public void put_Attr_NAME(REF_ENTITY inst) { SdaiInstance i = inst; ifcengine.sdaiPutAttrBN(m_instance, "ATTR_NAME", ifcengine.sdaiINSTANCE, i); }
        //## AttributeEnumGet

        public Enums.ENUMERATION_NAME? get_ATtr_NAME() { var str = getString("ATTR_NAME", ifcengine.sdaiSTRING); var ind = EnumIndex.FromString(str, Enums.ENUMERATION_VALUES_ARRAY); return EnumValue<Enums.ENUMERATION_NAME>.FromIndex(ind); }
        //## AttributeEnumPut
        public void put_ATTR_NAME(Enums.ENUMERATION_NAME value) { var str = EnumString<Enums.ENUMERATION_NAME>.FromValue(value, Enums.ENUMERATION_VALUES_ARRAY); ifcengine.sdaiPutAttrBN(m_instance, "ATTR_NAME", ifcengine.sdaiENUM, str); }
        //## AttributeSelectAccessor
        
        public GEN_TYPE_NAME_accessor getOrPut_ATTR_NAME() { return new GEN_TYPE_NAME_accessor(m_instance, "ATTR_NAME", 0); }
        //## AttributeAggregationGet

        public List<REF_ENTITY> get_ATTr_NAME() { return (new AggregationTypeSerializer()).FromAttr(m_instance, "ATTR_NAME"); }
        //## AttributeAggregationPut
        public void put_ATTr_NAME(IEnumerable<SimpleType> lst) { (new AggregationTypeSerializer()).ToSdaiAggr(lst, m_instance, "ATTR_NAME"); }
        public void put_ATTr_NAME(IEnumerable lst) { (new AggregationTypeSerializer()).ToSdaiAggr(lst, m_instance, "ATTR_NAME"); }
        //## AttributeAggregationPutArray
        //## EntityEnd
    };

    //## SelectEntityGetImplementation
    //## SelectEntityPutImplementation
    //## AttributeEntityGetImplementation
    //## AttributeEntityPutImplementation
    //## TEMPLATE: EndFile template part
}

