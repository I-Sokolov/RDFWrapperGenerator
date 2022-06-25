using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using RDF;

using ExpressHandle = System.Int64;

namespace RDFWrappers
{
    public class ExpressSelect
    {
        public string name { get { return ExpressSchema.GetNameOfDeclaration(inst); } }
        public ExpressHandle inst;

        public ExpressSelect(ExpressHandle inst)
        {
            this.inst = inst;
        }

        private HashSet<ExpressHandle> GetVariants(bool resolveNestedSelects)
        {
            var ret = new HashSet<ExpressHandle>();

            int i = 0;
            ExpressHandle variant;
            while (0 != (variant = ifcengine.engiGetSelectElement(inst, i++)))
            {
                if (resolveNestedSelects && ifcengine.engiGetDeclarationType(variant) == enum_express_declaration.__SELECT)
                {
                    var nestedSelect = new ExpressSelect(variant);
                    var nestedVariants = nestedSelect.GetVariants(resolveNestedSelects);
                    foreach (var v in nestedVariants)
                    {
                        if (!ret.Add(v))
                        {
                            throw new ApplicationException(string.Format("duplicated type {0} in SELECT {1}", ExpressSchema.GetNameOfDeclaration(v), name));
                        }
                    }
                }
                else
                {
                    if (!ret.Add(variant))
                    {
                        throw new ApplicationException(string.Format("duplicated type {0} in SELECT {1}", ExpressSchema.GetNameOfDeclaration(variant), name));
                    }
                }
            }

            return ret;
        }

        private List<ExpressHandle> GetNestedSelects()
        {
            var ret = new List<ExpressHandle>();

            int i = 0;
            ExpressHandle variant;
            while (0 != (variant = ifcengine.engiGetSelectElement(inst, i++)))
            {
                if (ifcengine.engiGetDeclarationType(variant) == enum_express_declaration.__SELECT)
                {
                    ret.Add(variant);
                }
            }

            return ret;
        }

        private HashSet<Generator.Template> CollectAsTypes ()
        {
            var ret = new HashSet<Generator.Template>();

            int i = 0;
            ExpressHandle variant;
            while (0 != (variant = ifcengine.engiGetSelectElement(inst, i++)))
            {
                enum_express_declaration declType = ifcengine.engiGetDeclarationType(variant);

                switch (declType)
                {
                    case enum_express_declaration.__ENTITY:
                        ret.Add(Generator.Template.SelectGetAsEntity);
                        break;

                    case enum_express_declaration.__ENUM:
                        //ret.Add(Generator.Template.SelectGetAsString);
                        break;

                    case enum_express_declaration.__SELECT:
                        var nestedSelect = new ExpressSelect(variant);
                        foreach (var nestedType in nestedSelect.CollectAsTypes())
                        {
                            ret.Add(nestedType);
                        }
                        break;

                    case enum_express_declaration.__DEFINED_TYPE:
                        var definedType = new ExpressDefinedType(variant);
                        if (!definedType.IsAggregation () && definedType.attrType != enum_express_attr_type.__LOGICAL)
                        {
                            var cstype = definedType.GetBaseCSType();
                            if (cstype != null)
                            {
                                switch (cstype)
                                {
                                    case "double": ret.Add(Generator.Template.SelectGetAsDouble); break;
                                    case "IntData": ret.Add(Generator.Template.SelectGetAsInt); break;
                                    case "bool": ret.Add(Generator.Template.SelectGetAsBool); break;
                                    case "TextData": ret.Add(Generator.Template.SelectGetAsText); break;
                                    default: throw new ApplicationException("unexpected cs type " + cstype);
                                }
                            }
                        }
                        break;
                }
            }

            return ret;

        }

        public void WriteAttribute(Generator generator, ExpressAttribute attr)
        {
            generator.m_writer.WriteLine();

            generator.m_replacements[Generator.KWD_TYPE_NAME] = Generator.ValidateIdentifier (name);

            generator.m_replacements[Generator.KWD_GETSET] = "get";
            generator.m_replacements[Generator.KWD_ACCESSOR] = "_getter";
            generator.WriteByTemplate(Generator.Template.AttributeSelectAccessor);

            if (!attr.inverse)
            {
                generator.m_replacements[Generator.KWD_GETSET] = "set";
                generator.m_replacements[Generator.KWD_ACCESSOR] = "_setter";
                generator.WriteByTemplate(Generator.Template.AttributeSelectAccessor);
            }

            generator.m_replacements.Remove(Generator.KWD_GETSET);
            generator.m_replacements.Remove(Generator.KWD_ACCESSOR);
        }

        public void WriteAccessors(Generator generator, HashSet<ExpressHandle> wroteSelects)
        {
            if (!wroteSelects.Add(inst))
            {
                return;
            }

            foreach (var nested in GetNestedSelects())
            {
                (new ExpressSelect(nested)).WriteAccessors(generator, wroteSelects);
            }

            generator.m_replacements[Generator.KWD_TYPE_NAME] = Generator.ValidateIdentifier (name);

            foreach (var bGet in new bool?[] { null, true, false })
            {
                generator.m_replacements[Generator.KWD_ACCESSOR] = bGet.HasValue ? (bGet.Value ? "_getter" : "_setter") : "";

                generator.WriteByTemplate(Generator.Template.SelectAccessorBegin);

                foreach (var variant in GetVariants(false))
                {
                    if (!bGet.HasValue)
                        generator.m_writer.WriteLine();

                    WriteAccessorMethod(generator, variant, bGet);
                }

                if (bGet.HasValue && bGet.Value) //to implement for detached selects it needs implementation with m_adb
                {
                    var astypes = CollectAsTypes();
                    if (astypes.Count > 0)
                    {
                        generator.m_writer.WriteLine();
                        foreach (var astype in astypes)
                        {
                            generator.WriteByTemplate(astype);
                        }
                    }
                }

                generator.WriteByTemplate(Generator.Template.SelectAccessorEnd);
            }

        }

        private void WriteAccessorMethod(Generator generator, ExpressHandle selectVariant, bool? bGet)
        {
            var type = ifcengine.engiGetDeclarationType(selectVariant);
            switch (type)
            {
                case enum_express_declaration.__DEFINED_TYPE:
                    {
                        enum_express_aggr aggrType;
                        if (generator.m_knownDefinedTyes.TryGetValue(selectVariant, out aggrType))
                        {
                            var definedType = new ExpressDefinedType(selectVariant);
                            if (aggrType == enum_express_aggr.__NONE)
                            {
                                System.Diagnostics.Trace.Assert(!definedType.IsAggregation ());
                                WriteAccessorMethod(generator, definedType, bGet);
                            }
                            else
                            {
                                WriteAggrAccessorMethod(generator, definedType, bGet);
                            }
                        }
                        else
                        {
                            Console.WriteLine("SLECT " + name + " - DefinedType is not supported: " + ExpressSchema.GetNameOfDeclaration(selectVariant));
                        }
                    }
                    break;

                case enum_express_declaration.__SELECT:
                    var selectType = new ExpressSelect(selectVariant);
                    WriteAccessorMethod(generator, selectType, bGet);
                    break;

                case enum_express_declaration.__ENTITY:
                    var entityType = new ExpressEntity(selectVariant);
                    WriteAccessorMethod(generator, entityType, bGet);
                    break;

                case enum_express_declaration.__ENUM:
                    var enumType = new ExpressEnumeraion(selectVariant);
                    WriteAccessorMethod(generator, enumType, bGet);
                    break;

                default:
                    System.Diagnostics.Debug.Assert(false);
                    break;
            }
        }

        private void WriteAccessorMethod(Generator generator, ExpressEnumeraion enumType, bool? bGet)
        {
            var name = Generator.ValidateIdentifier(enumType.name);
            WriteAccessorEnumMethod(generator, name, name + "_", bGet);
        }

        private void WriteAccessorEnumMethod(Generator generator, string enumName, string enumValuesArray, bool? bGet)
        { 
            generator.m_replacements[Generator.KWD_ENUMERATION_NAME] = enumName;
            generator.m_replacements[Generator.KWD_TypeNameUpper] = enumName.ToUpper();
            generator.m_replacements[Generator.KWD_ENUMERATION_VALUES_ARRAY] = enumValuesArray;

            if (bGet.HasValue)
            {
                generator.WriteByTemplate(bGet.Value ? Generator.Template.SelectEnumerationGet : Generator.Template.SelectEnumerationSet);
            }
            else
            {
                generator.WriteByTemplate(Generator.Template.SelectEnumerationGet);
                generator.WriteByTemplate(Generator.Template.SelectEnumerationSet);
            }
        }


        private void WriteAccessorMethod(Generator generator, ExpressEntity entityType, bool? bGet)
        {
            generator.m_replacements[Generator.KWD_REF_ENTITY] = Generator.ValidateIdentifier (entityType.name);
            generator.m_replacements[Generator.KWD_TypeNameUpper] = entityType.name.ToUpper();

            if (bGet.HasValue)
            {
                generator.WriteByTemplate(bGet.Value ? Generator.Template.SelectEntityGet : Generator.Template.SelectEntitySet);

                var impl = generator.StringByTemplate(bGet.Value ? Generator.Template.SelectEntityGetImplementation : Generator.Template.SelectEntitySetImplementation);
                generator.m_implementations.Append(impl);
            }
            else
            {
                generator.WriteByTemplate(Generator.Template.SelectEntityGet);
                generator.WriteByTemplate(Generator.Template.SelectEntitySet);

                var impl = generator.StringByTemplate(Generator.Template.SelectEntityGetImplementation);
                generator.m_implementations.Append(impl);

                impl = generator.StringByTemplate(Generator.Template.SelectEntitySetImplementation);
                generator.m_implementations.Append(impl);
            }
        }


        private void WriteAccessorMethod(Generator generator, ExpressDefinedType definedType, bool? bGet)
        {
            switch (definedType.attrType)
            {
                case enum_express_attr_type.__BINARY:
                case enum_express_attr_type.__BINARY_32:
                    return;

                case enum_express_attr_type.__LOGICAL:
                    WriteAccessorEnumMethod(generator, definedType.name, "LOGICAL_VALUE_NAMES", bGet);
                    return;
            }

            string sdaiType = definedType.GetSdaiType();
            string baseType = definedType.GetBaseCSType();

            generator.m_replacements[Generator.KWD_SimpleType] = definedType.name;
            generator.m_replacements[Generator.KWD_TextType] = definedType.name;
            generator.m_replacements[Generator.KWD_sdaiTYPE] = sdaiType;
            generator.m_replacements[Generator.KWD_TypeNameUpper] = definedType.name.ToUpper();

            Generator.Template tplGet;
            Generator.Template tplSet;
            if (baseType == "TextData")
            {
                tplGet = Generator.Template.SelectTextGet;
                tplSet = Generator.Template.SelectTextSet;
            }
            else 
            {
                tplGet = Generator.Template.SelectSimpleGet;
                tplSet = Generator.Template.SelectSimpleSet;
            }

            if (bGet.HasValue)
            {
                generator.WriteByTemplate(bGet.Value ? tplGet : tplSet);
            }
            else
            {
                generator.WriteByTemplate(tplGet);
                generator.WriteByTemplate(tplSet);
            }
        }

        private void WriteAggrAccessorMethod(Generator generator, ExpressDefinedType definedType, bool? bGet)
        {
            if (definedType.name == "IfcBinary")
                return;

            string sdaiType = definedType.GetSdaiType();
            string baseType = definedType.GetBaseCSType();

            string elemType = baseType;
            if (definedType.domain != 0 && !generator.m_cs)
            {
                elemType = ExpressSchema.GetNameOfDeclaration(definedType.domain);
            }

            generator.m_replacements[Generator.KWD_AggregationType] = definedType.name;
            generator.m_replacements[Generator.KWD_SimpleType] = elemType;
            generator.m_replacements[Generator.KWD_TextType] = elemType;
            generator.m_replacements[Generator.KWD_sdaiTYPE] = sdaiType;
            generator.m_replacements[Generator.KWD_TypeNameUpper] = definedType.name.ToUpper();

            if (bGet.HasValue)
            {
                var tpl = bGet.Value ? Generator.Template.SelectAggregationGet : Generator.Template.SelectAggregationSet;
                generator.WriteByTemplate(tpl);
            }
            else
            {
                generator.WriteByTemplate(Generator.Template.SelectAggregationGet);
                generator.WriteByTemplate(Generator.Template.SelectAggregationSet);
            }

            if (!(bGet.HasValue && bGet.Value))
            {
                enum_express_aggr aggr;
                Int64 crdMin, crdMax, nestedAggr;
                ifcengine.engiGetAggregation(definedType.aggregation, out aggr, out crdMin, out crdMax, out nestedAggr);
                if (nestedAggr == 0)
                {
                    var tpl = baseType == "TextData" ? Generator.Template.SelectAggregationSetArrayText : Generator.Template.SelectAggregationSetArraySimple;
                    generator.WriteByTemplate(tpl);
                }
            }
        }

        private void WriteAccessorMethod(Generator generator, ExpressSelect selectType, bool? bGet)
        {
            var saveSelect = generator.m_replacements[Generator.KWD_TYPE_NAME];
            generator.m_replacements[Generator.KWD_TYPE_NAME] = selectType.name;

            generator.m_replacements[Generator.KWD_nestedSelectAccess] = bGet.HasValue ? (bGet.Value ? "get" : "set") : "";

            generator.WriteByTemplate(Generator.Template.SelectNested);

            generator.m_replacements[Generator.KWD_TYPE_NAME] = saveSelect;
        }


        public override string ToString()
        {
            var str = new StringBuilder();

            str.AppendLine(string.Format("{0}:", name));

            foreach (var variant in GetVariants(false))
            {
                var name = ExpressSchema.GetNameOfDeclaration(variant);
                var type = ifcengine.engiGetDeclarationType(variant);

                str.AppendLine(string.Format("        {0} {1}", name, type.ToString()));
            }

            return str.ToString();

        }

    }
}
