//
// Early-binding C# API for SDAI (CE wrappers)
//
using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using RDF;

namespace RDF
{
    partial class ifcengine
    {
        public const int sdaiTYPE = 0;
    };
}

namespace NAMESPACE_NAME
{
    using SdaiModel = Int64;
    using SdaiInstance = Int64;
    using SdaiAggr = Int64;

    using TextValue = String;
    using StringValue = String;
    using IntValue = Int64;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
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
        public static string FromValue(TEnum value, TextValue[] allStrings)
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

        protected Select(SdaiInstance instance = 0, TextValue attrName = null, IntValue adb = 0)
        {
            Init(instance, attrName, adb);
        }

        protected Select(Select outer)
        {
            Debug.Assert(outer != null);
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

        private bool CheckADBType (IntValue adb, TextValue typeName)
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
            if (inst == 0 || ifcengine.sdaiIsKindOfBN(inst, typeName)!=0)
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
            if (adb !=0)
            {
                SdaiInstance inst = 0;
                if (ifcengine.sdaiGetADBValue(adb, ifcengine.sdaiINSTANCE, out inst)!=0)
                {
                    if (ifcengine.sdaiIsKindOfBN(inst, typeName)!=0)
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
        public abstract void FromSdaiAggrObj(IList lst, SdaiInstance inst, SdaiAggr aggr);
        public abstract SdaiAggr ToSdaiAggrObj(IEnumerable lst, SdaiInstance instance, TextValue attrName);

    }

    public abstract class AggrSerializer<TElem> : IAggrSerializerObj
    {
        //
        public abstract void FromSdaiAggr(IList<TElem> lst, SdaiInstance inst, SdaiAggr aggr);
        public abstract SdaiAggr ToSdaiAggr(IEnumerable<TElem> lst, SdaiInstance instance, TextValue attrName);

        //
        public void FromAttr(IList<TElem> lst, SdaiInstance instance, TextValue attrName)
        {
            SdaiAggr aggr = 0;
            ifcengine.sdaiGetAttrBN(instance, attrName, ifcengine.sdaiAGGR, out aggr);
            if (aggr!=0)
            {
                FromSdaiAggr(lst, instance, aggr);
            }
        }

        void IAggrSerializerObj.FromSdaiAggrObj(IList lst, SdaiInstance inst, SdaiAggr aggr)
        {
            var lst2 = new List<TElem>();
            FromSdaiAggr(lst2, inst, aggr);
            foreach (var elem in lst2)
            {
                lst.Add(elem);
            }
        }

        SdaiAggr IAggrSerializerObj.ToSdaiAggrObj(IEnumerable lst, long instance, string attrName)
        {
            var lst2 = new List<TElem>();
            foreach (var elem in lst)
            {
                lst2.Add((TElem)elem);
            }
            return ToSdaiAggr(lst2, instance, attrName);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class AggrSerializer_IntVal : AggrSerializer<IntValue>
    {
        //
        public override void FromSdaiAggr(IList<IntValue> lst, SdaiInstance unused, SdaiAggr aggr)
        {
            IntValue cnt = ifcengine.sdaiGetMemberCount(aggr);
            for (IntValue i = 0; i < cnt; i++) {
                IntValue val;
                ifcengine.engiGetAggrElement(aggr, i, ifcengine.sdaiINTEGER, out val);
                lst.Add(val);
            }
        }

        //
        public override SdaiAggr ToSdaiAggr(IEnumerable<IntValue> lst, SdaiInstance instance, TextValue attrName)
        {
            SdaiAggr aggr = ifcengine.sdaiCreateAggrBN(instance, attrName);
            foreach (var v in lst)
            {
                IntValue val = v;
                ifcengine.sdaiAppend(aggr, ifcengine.sdaiINTEGER, ref val);
            }
            return aggr;
        }
    };

    /// <summary>
    /// 
    /// </summary>
    public class AggrSerializer_double : AggrSerializer<double>
    {
        //
        public override void FromSdaiAggr(IList<double> lst, SdaiInstance unused, SdaiAggr aggr)
        {
            IntValue cnt = ifcengine.sdaiGetMemberCount(aggr);
            for (IntValue i = 0; i < cnt; i++)
            {
                double val;
                ifcengine.engiGetAggrElement(aggr, i, ifcengine.sdaiREAL, out val);
                lst.Add(val);
            }
        }

        //
        public override SdaiAggr ToSdaiAggr(IEnumerable<double> lst, SdaiInstance instance, TextValue attrName)
        {
            SdaiAggr aggr = ifcengine.sdaiCreateAggrBN(instance, attrName);
            foreach (var v in lst)
            {
                double val = v;
                ifcengine.sdaiAppend(aggr, ifcengine.sdaiREAL, ref val);
            }
            return aggr;
        }
    };

    public class AggrSerializerText : AggrSerializer<TextValue>
{
        private IntValue m_sdaiType;

        public AggrSerializerText(IntValue sdaiType) 
        {
            Debug.Assert(sdaiType == ifcengine.sdaiSTRING || sdaiType == ifcengine.sdaiBINARY);
            m_sdaiType = sdaiType;
        }

        public override void FromSdaiAggr(IList<TextValue> lst, SdaiInstance unused, SdaiAggr aggr)
        {
            var cnt = ifcengine.sdaiGetMemberCount(aggr);
            for (IntValue i = 0; i < cnt; i++)
            {
                IntPtr ptr = IntPtr.Zero;
                ifcengine.engiGetAggrElement(aggr, i, m_sdaiType, out ptr);
                var value = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(ptr);
                if (value != null)
                    lst.Add(value);
            }
        }

        public override SdaiAggr ToSdaiAggr(IEnumerable<TextValue> lst, SdaiInstance instance, TextValue attrName)
        {
            SdaiAggr aggr = ifcengine.sdaiCreateAggrBN(instance, attrName);
            foreach (var val in lst)
            {
                ifcengine.sdaiAppend((IntValue)aggr, m_sdaiType, val);
            }
            return aggr;
        }

    };

    public class AggrSerializerInstance : AggrSerializer<SdaiInstance>
    {
        //
        public override void FromSdaiAggr(IList<SdaiInstance> lst, SdaiInstance unused, SdaiAggr aggr)
        {
            var cnt = ifcengine.sdaiGetMemberCount(aggr);
            for (IntValue i = 0; i < cnt; i++)
            {
                SdaiInstance val = 0;
                ifcengine.engiGetAggrElement(aggr, i, ifcengine.sdaiINSTANCE, out val);
                //TElem elem(val);
                if (val != 0)
                {
                    lst.Add(val);
                }
            }
        }

        //
        public override SdaiAggr ToSdaiAggr(IEnumerable<SdaiInstance> lst, SdaiInstance instance, TextValue attrName)
        {
            var aggr = ifcengine.sdaiCreateAggrBN(instance, attrName);
            foreach (var val in lst)
            {
                SdaiInstance v = val;
                ifcengine.sdaiAppend((IntValue)aggr, ifcengine.sdaiINSTANCE, v);
            }
            return aggr;
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
        public override void FromSdaiAggr(IList<TEnum> lst, SdaiInstance unused, SdaiAggr aggr)
        {
            var cnt = ifcengine.sdaiGetMemberCount(aggr);
            for (IntValue i = 0; i < cnt; i++)
            {
                IntPtr ptr = IntPtr.Zero;
                ifcengine.engiGetAggrElement(aggr, i, m_sdaiType, out ptr);
                var value = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(ptr);
                var ind = EnumIndex.FromString(value, m_EnumValues);
                var val = EnumValue<TEnum>.FromIndex(ind);
                if (val.HasValue)
                {
                    lst.Add(val.Value);
                }
            }
        }

        //
        public override SdaiAggr ToSdaiAggr(IEnumerable<TEnum> lst, SdaiInstance instance, TextValue attrName)
        {
            SdaiAggr aggr = ifcengine.sdaiCreateAggrBN(instance, attrName);
            foreach (var val in lst) {
                var value = EnumString<TEnum>.FromValue (val, m_EnumValues);
                ifcengine.sdaiAppend((IntValue)aggr, m_sdaiType, value);
            }
            return aggr;
        }

        /// <summary>
        /// 
        /// </summary>
        public class AggrSerializerAggr<TNestedAggr, TNestedSerializer> : AggrSerializer<TNestedAggr>
                        where TNestedAggr : IList, new ()
                        where TNestedSerializer : IAggrSerializerObj, new()
        {
            public override void FromSdaiAggr(IList<TNestedAggr> lst, SdaiInstance instance, SdaiAggr aggr)
            {
                var cnt = ifcengine.sdaiGetMemberCount(aggr);
                for (IntValue i = 0; i < cnt; i++)
                {
                    SdaiAggr nested = 0;
                    ifcengine.engiGetAggrElement(aggr, i, ifcengine.sdaiAGGR, out nested);
                    if (nested != 0)
                    {
                        var nestedAggr = new TNestedAggr();
                        lst.Add(nestedAggr);

                        var nestedSerializer = new TNestedSerializer();
                        nestedSerializer.FromSdaiAggrObj(nestedAggr, instance, nested);
                    }
                }
            }

            //
            public override SdaiAggr ToSdaiAggr(IEnumerable<TNestedAggr> lst, SdaiInstance instance, TextValue attrName)
            {
                SdaiAggr aggr = ifcengine.sdaiCreateAggrBN(instance, attrName);
                foreach (var val in lst)
                {
                    var nestedSerializer = new TNestedSerializer();
                    SdaiAggr nested = nestedSerializer.ToSdaiAggrObj(val, instance, null);
                    ifcengine.sdaiAppend(aggr, ifcengine.sdaiAGGR, nested);
                }
                return aggr;
            }
        };

        public class AggrSerializerSelect<TSelect> : AggrSerializer<TSelect> where TSelect : Select, new()
        {
            public override void FromSdaiAggr(IList<TSelect> lst, SdaiInstance instance, SdaiAggr aggr)
            {
                var cnt = ifcengine.sdaiGetMemberCount(aggr);
                for (IntValue i = 0; i < cnt; i++)
                {
                    IntValue adb = 0;
                    ifcengine.engiGetAggrElement(aggr, i, ifcengine.sdaiADB, out adb);
                    if (adb != 0)
                    {
                        var select = new TSelect();
                        select.Init(instance, null, adb);
                        lst.Add(select);
                    }
                }
            }

            //
            public override SdaiAggr ToSdaiAggr(IEnumerable<TSelect> lst, SdaiInstance instance, TextValue attrName)
            {
                SdaiAggr aggr = ifcengine.sdaiCreateAggrBN(instance, attrName);
                foreach (var val in lst)
                {
                    var adb = val.ADB();
                    if (adb != 0)
                    {
                        ifcengine.sdaiAppend((IntValue)aggr, ifcengine.sdaiADB, adb);
                    }
                }
                return aggr;
            }
        };
//## TEMPLATE: TemplateUtilityTypes

public class SimpleType : List<object> {};
public class SImpleType : Select {};
public class SimpleTypeSerializer : AggrSerializer_double {};
        public struct REF_ENTITY 
        { 
            REF_ENTITY(SdaiInstance inst) { }
            public static implicit operator REF_ENTITY(SdaiInstance value) { return new REF_ENTITY(value); }
            public static implicit operator SdaiInstance(REF_ENTITY value) { return 0; }
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
            public static string[] ENUMERATION_NAME_ = { "ENUMERATION_STRING_VALUES" };
//## TEMPLATE: AggregationTypesBegin
        }

    //
    // Unnamed aggregations
    //
//## AggregationOfSimple
    public class AggregationType : List<SimpleType> { };
    public class AggregationTypeSerializer : AggrSerializer_double {};
//## AggregationOfText
    public class Aggregationtype : List<TextValue> {};
    public class AggregationtypeSerializer : AggrSerializerText { public AggregationtypeSerializer() : base(ifcengine.sdaiTYPE) {} };
//## AggregationOfInstance
    public class AggregationTYpe : List<SimpleType> {};
    public class AggregationTYpeSerializer : AggrSerializerInstance {};
//## AggregationOfEnum
    public class AggregationTyPe : List<Enums.ENUMERATION_NAME> {};
    public class AggregationTyPeSerializer : AggrSerializerEnum<Enums.ENUMERATION_NAME> { public AggregationTyPeSerializer() : base(Enums.ENUMERATION_NAME_, ifcengine.sdaiTYPE) { } };
//## AggregationOfAggregation
    public class AggregationTYPe : List<SimpleType> {};
    public class AggregationTYPeSerializer : AggrSerializerAggr<SimpleType, SimpleTypeSerializer> {};
//## AggregationOfSelect
    public class AggregationTYPE : List<SimpleType> {};
    public class AggregationTYPESerializer : AggrSerializerSelect<SImpleType> {};
        //## SelectsBegin

        //
        // SELECT TYPES
        // 
        //## TEMPLATE: SelectAccessorBegin

        public class GEN_TYPE_NAME_accessor : Select
    {
        public GEN_TYPE_NAME_accessor(SdaiInstance instance, TextValue attrName = null, IntValue adb = 0) : base(instance, attrName, adb) { }
        public GEN_TYPE_NAME_accessor(Select outer) : base(outer) { }
        //## SelectSimpleGet
        public bool is_SimpleType() { return IsADBType("TypeNameUpper"); }
        public double? get_double() { return get_double("TypeNameUpper", ifcengine.sdaiTYPE); }
        //## SelectSimplePut
        public void put_double(double value) { put_double("TypeNameUpper", ifcengine.sdaiTYPE, value); }
        //## SelectTextGet
        public bool is_TextType() { return IsADBType("TypeNameUpper"); }
        public TextValue get_TextType() { return getTextValue("TypeNameUpper", ifcengine.sdaiTYPE); }
        //## SelectTextPut
        public void put_TextType(TextValue value) { putTextValue("TypeNameUpper", ifcengine.sdaiTYPE, value); }
        //## SelectEntityGet
        public bool is_REF_ENTITY() { return IsADBEntity("REF_ENTITY"); }
        public REF_ENTITY get_REF_ENTITY() { return getEntityInstance("TypeNameUpper"); }
            //## SelectEntityPut
            public void put_REF_ENTITY(REF_ENTITY inst) { putEntityInstance("TypeNameUpper", inst); }
            //## SelectEnumerationGet
            public bool is_ENUMERATION_NAME() { return IsADBType("TypeNameUpper"); }
        public Enums.ENUMERATION_NAME? get_ENUMERATION_NAME() { int ind = getEnumerationIndex("TypeNameUpper", Enums.ENUMERATION_VALUES_ARRAY); return EnumValue<Enums.ENUMERATION_NAME>.FromIndex(ind); }
        //## SelectEnumerationPut
        public void put_ENUMERATION_NAME(Enums.ENUMERATION_NAME value) { TextValue val = EnumString<Enums.ENUMERATION_NAME>.FromValue(value, Enums.ENUMERATION_VALUES_ARRAY); putEnumerationValue("TypeNameUpper", val); }
        //## SelectAggregationGet
        public bool is_AggregationType() { return IsADBType("TypeNameUpper"); }
        public IEnumerable<SimpleType> get_AggregationType() { SdaiAggr aggr = getAggrValue("TypeNameUpper"); AggregationTypeSerializer sr; sr.FromSdaiAggr(lst, m_instance, aggr); }
        //## SelectAggregationPut
        public void put_AggregationType(IEnumerable<SimpleType> lst) { AggregationTypeSerializer<TList> sr; SdaiAggr aggr = sr.ToSdaiAggr(lst, m_instance, NULL); putAggrValue("TypeNameUpper", aggr); }
        //## SelectAggregationPutArray

        //TArrayElem[] may be SimpleType[] or array of convertible elements
        template<typename TArrayElem> void put_AggregationType(TArrayElem arr[], size_t n) { AggregationType lst; ArrayToList(arr, n, lst); put_AggregationType(lst); }
        //## SelectNested
        GEN_TYPE_NAME_accessor nestedSelectAccess_GEN_TYPE_NAME() { return GEN_TYPE_NAME_accessor(this); }
        //## SelectGetAsDouble
        Nullable<double> as_double() { double val = 0; if (sdaiGetAttrBN(m_instance, m_attrName, sdaiREAL, &val)) return val; else return Nullable<double>(); }
        //## SelectGetAsInt
        Nullable<IntValue> as_int() { IntValue val = 0; if (sdaiGetAttrBN(m_instance, m_attrName, sdaiINTEGER, &val)) return val; else return Nullable<IntValue>(); }
        //## SelectGetAsBool
        Nullable<bool> as_bool() { bool val = 0; if (sdaiGetAttrBN(m_instance, m_attrName, sdaiBOOLEAN, &val)) return val; else return Nullable<bool>(); }
        //## SelectGetAsText
        TextValue as_text() { TextValue val = NULL; sdaiGetAttrBN(m_instance, m_attrName, sdaiSTRING, &val); return val; }
        //## SelectGetAsEntity
        SdaiInstance as_instance() { return getEntityInstance(NULL); }
        //## SelectAccessorEnd
    };

    //## TEMPLATE: BeginEntities

    //
    // Entities
    //
    //## TEMPLATE: BeginEntity

    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class ENTITY_NAME
    /// You also can use object of this C# class instead of int64_t handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class ENTITY_NAME : /*PARENT_NAME*/Entity
    {
        /// <summary>
        /// Constructs object of this C# class that wraps existing instance
        /// </summary>
        public ENTITY_NAME(SdaiInstance instance, string entityName = null)
            : base(instance, entityName != null ? entityName : "ENTITY_NAME")
        {
        }

//## EntityCreateMethod
        /// <summary>
        /// Create new instace of ENTITY_NAME and returns object of this C++ class to interact with
        /// </summary>
        public new static ENTITY_NAME Create(SdaiModel model) { SdaiInstance inst = ifcengine.sdaiCreateInstanceBN(model, "ENTITY_NAME"); Debug.Assert(inst!=0); return new ENTITY_NAME(inst); }

//## GetSimpleAttribute
        
        //public SimpleType? get_ATTR_NAME() { SimpleType value; if (0 != ifcengine.sdaiGetAttrBN(m_instance, "ATTR_NAME", ifcengine.sdaiTYPE, out value)) return value; else return null; } 
//## SetSimpleAttribute
        //public void set_ATTR_NAME(SimpleType value) { ifcengine.sdaiPutAttrBN (m_instance, "ATTR_NAME", ifcengine.sdaiTYPE, ref value); }
//## GetSimpleAttributeString
        
        public string get_attr_NAME() { return getString("ATTR_NAME"); } 
//## SetSimpleAttributeString
        public void set_ATTR_NAME(string value) { ifcengine.sdaiPutAttrBN (m_instance, "ATTR_NAME", ifcengine.sdaiSTRING, value); }
//## GetEntityAttribute

        public REF_ENTITY get_Attr_NAME() { SdaiInstance inst = 0; ifcengine.sdaiGetAttrBN(m_instance, "ATTR_NAME", ifcengine.sdaiINSTANCE, out inst); return inst != 0 ? new REF_ENTITY(inst) : null; } 
//## SetEntityAttribute
        public void set_Attr_NAME(REF_ENTITY inst) { SdaiInstance i = inst;  ifcengine.sdaiPutAttrBN(m_instance, "ATTR_NAME", ifcengine.sdaiINSTANCE, i); }
//## GetEnumAttribute

        public Enums.ENUMERATION_NAME? get_ATtr_NAME() { int v = getENUM("ATTR_NAME", Enums.ENUMERATION_NAME_); if (v >= 0) return (Enums.ENUMERATION_NAME) v; else return null; }
//## SetEnumAttribute
        public void set_ATTR_NAME(Enums.ENUMERATION_NAME value) { string val = Enums.ENUMERATION_NAME_[(int)value]; ifcengine.sdaiPutAttrBN(m_instance, "ATTR_NAME", ifcengine.sdaiENUM, val); }
//## EndEntity
    }

//## GetEntityAttributeImplementation
//## SetEntityAttributeImplementation
//## TEMPLATE: EndFile 
    /// <summary>
    /// Provides utility methods to interact with a generic entity instnace
    /// You also can use object of this class instead of int64_t handle of the instance in any place where the handle is required
    /// </summary>
    public class Entity : IEquatable<Entity>, IComparable, IComparable<Entity>
    {
        /// <summary>
        /// underlyed instance handle
        /// </summary>
        protected SdaiInstance m_instance = 0;

        /// <summary>
        /// Constructs object that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        protected Entity(SdaiInstance instance, string entityName)
        {
            m_instance = instance;
            //System.Diagnostics.Debug.Assert(entityName == null/*do not check*/ || ifcengine.sdaiIsKindOf(instance, entityName));
        }


        /// <summary>
        /// Conversion to instance handle, so the object of the class can be used anywhere where a handle required
        /// </summary>
        public static implicit operator SdaiInstance(Entity instance) => instance.m_instance;

        public static Entity Create(SdaiModel model) { System.Diagnostics.Debug.Assert(false); return null; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attrName"></param>
        /// <returns></returns>
        protected string getString (string attrName, Int64 valueType = ifcengine.sdaiSTRING)
        {
            IntPtr ptr = IntPtr.Zero;
            if (0!=ifcengine.sdaiGetAttrBN(m_instance, attrName, valueType, out ptr))
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
        /// <param name="attrName"></param>
        /// <param name="rEnumValues"></param>
        /// <returns></returns>
        protected int getENUM(string attrName, string[] rEnumValues)
        {
            string value = getString(attrName, ifcengine.sdaiENUM);

            if (value != null)
            {

                for (int i = 0; i < rEnumValues.Length; i++)
                {
                    if (value == rEnumValues[i])
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool operator ==(Entity i1, Entity i2) => (Equals(i1, i2));

        /// <summary>
        /// 
        /// </summary>
        public static bool operator !=(Entity i1, Entity i2) => (!(i1 == i2));

        /// <summary>
        /// 
        /// </summary>
        public override bool Equals(Object obj)
        {
            return Equals(obj as Entity);
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Equals(Entity other)
        {
            return (other == null) ? false : (other.m_instance == m_instance);
        }

        /// <summary>
        /// 
        /// </summary>
        public int CompareTo(object obj)
        {
            return CompareTo(obj as Entity);
        }

        /// <summary>
        /// 
        /// </summary>
        public int CompareTo(Entity other)
        {
            return (other == null) ? 1 : m_instance.CompareTo(other.m_instance);
        }

        /// <summary>
        /// 
        /// </summary>
        public override int GetHashCode()
        {
            return m_instance.GetHashCode();
        }
    }
}

