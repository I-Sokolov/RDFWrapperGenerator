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

            generator.m_replacements[Generator.KWD_GETPUT] = "get";
            generator.m_replacements[Generator.KWD_ACCESSOR] = "_get";
            generator.WriteByTemplate(Generator.Template.AttributeSelectAccessor);

            if (!attr.inverse)
            {
                generator.m_replacements[Generator.KWD_GETPUT] = "put";
                generator.m_replacements[Generator.KWD_ACCESSOR] = "_put";
                generator.WriteByTemplate(Generator.Template.AttributeSelectAccessor);
            }

            generator.m_replacements.Remove(Generator.KWD_GETPUT);
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
                generator.m_replacements[Generator.KWD_ACCESSOR] = bGet.HasValue ? (bGet.Value ? "_get" : "_put") : "";

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
                    var definedType = new ExpressDefinedType(selectVariant);
                    WriteAccessorMethod(generator, definedType, bGet);
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
                generator.WriteByTemplate(bGet.Value ? Generator.Template.SelectEnumerationGet : Generator.Template.SelectEnumerationPut);
            }
            else
            {
                generator.WriteByTemplate(Generator.Template.SelectEnumerationGet);
                generator.WriteByTemplate(Generator.Template.SelectEnumerationPut);
            }
        }


        private void WriteAccessorMethod(Generator generator, ExpressEntity entityType, bool? bGet)
        {
            generator.m_replacements[Generator.KWD_REF_ENTITY] = Generator.ValidateIdentifier (entityType.name);
            generator.m_replacements[Generator.KWD_TypeNameUpper] = entityType.name.ToUpper();

            if (bGet.HasValue)
            {
                generator.WriteByTemplate(bGet.Value ? Generator.Template.SelectEntityGet : Generator.Template.SelectEntityPut);

                var impl = generator.StringByTemplate(bGet.Value ? Generator.Template.SelectEntityGetImplementation : Generator.Template.SelectEntityPutImplementation);
                generator.m_implementations.Append(impl);
            }
            else
            {
                generator.WriteByTemplate(Generator.Template.SelectEntityGet);
                generator.WriteByTemplate(Generator.Template.SelectEntityPut);

                var impl = generator.StringByTemplate(Generator.Template.SelectEntityGetImplementation);
                generator.m_implementations.Append(impl);

                impl = generator.StringByTemplate(Generator.Template.SelectEntityPutImplementation);
                generator.m_implementations.Append(impl);
            }
        }


        private void WriteAccessorMethod(Generator generator, ExpressDefinedType definedType, bool? bGet)
        {
            ExpressDefinedType.Foundation foundation = null;
            if (!generator.m_writtenDefinedTyes.TryGetValue(definedType.declaration, out foundation))
                foundation = null;
            if (foundation == null)
            {
                Console.WriteLine("SLECT " + name + " - DefinedType is not supported: " + definedType.name);
                return; //>>>>>>>>>>>>>>>>>>
            }


            if (foundation.aggrType != enum_express_aggr.__NONE)
            {
                WriteAggrAccessorMethod(generator, definedType, foundation, bGet);
            }
            else
            {
                switch (foundation.domainType)
                {
                    case enum_express_declaration.__UNDEF: //based on primitive
                        switch (foundation.attrType)
                        {
                            case enum_express_attr_type.__LOGICAL:
                                WriteAccessorEnumMethod(generator, definedType.name, "LOGICAL_VALUE_NAMES", bGet);
                                break;

                            default:
                                WriteSimpleAccessorMethod(generator, definedType, bGet);
                                break;
                        }
                        break;

                    default:
                        Console.WriteLine("SLECT " + name + " - DefinedType " + definedType.name + " is " + foundation.domainType.ToString()); ;
                        break;
                }
            }
        }

        private void WriteSimpleAccessorMethod(Generator generator, ExpressDefinedType definedType, bool? bGet)
        {
            string sdaiType = definedType.GetSdaiType();
            string baPutype = definedType.GetBaseCSType();

            generator.m_replacements[Generator.KWD_SimpleType] = definedType.name;
            generator.m_replacements[Generator.KWD_TextType] = definedType.name;
            generator.m_replacements[Generator.KWD_sdaiTYPE] = sdaiType;
            generator.m_replacements[Generator.KWD_TypeNameUpper] = definedType.name.ToUpper();

            Generator.Template tplGet;
            Generator.Template tplPut;
            if (baPutype == "TextData")
            {
                tplGet = Generator.Template.SelectTextGet;
                tplPut = Generator.Template.SelectTextPut;
            }
            else 
            {
                tplGet = Generator.Template.SelectSimpleGet;
                tplPut = Generator.Template.SelectSimplePut;
            }

            if (bGet.HasValue)
            {
                generator.WriteByTemplate(bGet.Value ? tplGet : tplPut);
            }
            else
            {
                generator.WriteByTemplate(tplGet);
                generator.WriteByTemplate(tplPut);
            }
        }

        private void WriteAggrAccessorMethod(Generator generator, ExpressDefinedType definedType, ExpressDefinedType.Foundation foundation, bool? bGet)
        {                
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
                var tpl = bGet.Value ? Generator.Template.SelectAggregationGet : Generator.Template.SelectAggregationPut;
                generator.WriteByTemplate(tpl);
            }
            else
            {
                generator.WriteByTemplate(Generator.Template.SelectAggregationGet);
                generator.WriteByTemplate(Generator.Template.SelectAggregationPut);
            }

            //Put array methods
            if (!(bGet.HasValue && bGet.Value))
            {
                enum_express_aggr aggr;
                Int64 crdMin, crdMax, nestedAggr;
                ifcengine.engiGetAggregation(definedType.aggregation, out aggr, out crdMin, out crdMax, out nestedAggr);
                System.Diagnostics.Debug.Assert(nestedAggr == 0); //to test
                if (nestedAggr == 0 && sdaiType != null)
                {
                    var tpl = baseType == "TextData" ? Generator.Template.SelectAggregationPutArrayText : Generator.Template.SelectAggregationPutArraySimple;
                    generator.WriteByTemplate(tpl);

                    if (foundation.domainType == enum_express_declaration.__ENTITY)
                    {
                        generator.m_replacements[Generator.KWD_SimpleType] = "IntData";
                        generator.WriteByTemplate(Generator.Template.SelectAggregationPutArraySimple);
                    }
                }
            }
        }

        private void WriteAccessorMethod(Generator generator, ExpressSelect selectType, bool? bGet)
        {
            var saveSelect = generator.m_replacements[Generator.KWD_TYPE_NAME];
            generator.m_replacements[Generator.KWD_TYPE_NAME] = selectType.name;

            generator.m_replacements[Generator.KWD_nestedSelectAccess] = bGet.HasValue ? (bGet.Value ? "get" : "put") : "";

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
