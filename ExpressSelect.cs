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
                        if (definedType.GetAggregationType() == RDF.enum_express_aggr.__NONE)
                        {
                            var cstype = definedType.GetBaseCSType();
                            if (cstype != null)
                            {
                                switch (cstype)
                                {
                                    case "double": ret.Add(Generator.Template.SelectGetAsDouble); break;
                                    case "Int64": ret.Add(Generator.Template.SelectGetAsInt); break;
                                    case "bool": ret.Add(Generator.Template.SelectGetAsBool); break;
                                    case "string": ret.Add(Generator.Template.SelectGetAsText); break;
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

            generator.m_replacements[Generator.KWD_TYPE_NAME] = name;

            generator.m_replacements[Generator.KWD_GETSET] = "get";
            generator.m_replacements[Generator.KWD_ACCESSOR] = "getter";
            generator.WriteByTemplate(Generator.Template.AttributeSelectAccessor);

            if (!attr.inverse)
            {
                generator.m_replacements[Generator.KWD_GETSET] = "set";
                generator.m_replacements[Generator.KWD_ACCESSOR] = "setter";
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

            generator.m_replacements[Generator.KWD_TYPE_NAME] = name;

            foreach (var bGet in new bool[] { true, false })
            {
                generator.m_replacements[Generator.KWD_ACCESSOR] = bGet ? "getter" : "setter";

                generator.WriteByTemplate(Generator.Template.SelectAccessorBegin);

                foreach (var variant in GetVariants(false))
                {
                    WriteAccessorMethod(generator, variant, bGet);
                }

                if (bGet)
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

        private void WriteAccessorMethod(Generator generator, ExpressHandle selectVariant, bool bGet)
        {
            var type = ifcengine.engiGetDeclarationType(selectVariant);
            switch (type)
            {
                case enum_express_declaration.__DEFINED_TYPE:
                    var definedType = new ExpressDefinedType(selectVariant);
                    if (definedType.GetAggregationType() == enum_express_aggr.__NONE)
                    {
                        WriteAccessorMethod(generator, definedType, bGet);
                    }
                    else
                    {
                        WriteAggrAccessorMethod(generator, definedType, bGet);
                    }
                    break;

                case enum_express_declaration.__SELECT:
                    var selectType = new ExpressSelect(selectVariant);
                    WriteAccessorMethod(generator, selectType);
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
        
        private void WriteAccessorMethod(Generator generator, ExpressEnumeraion enumType, bool bGet)
        {
            generator.m_replacements[Generator.KWD_ENUMERATION_NAME] = enumType.name;
            generator.m_replacements[Generator.KWD_TypeNameUpper] = enumType.name.ToUpper();

            generator.WriteByTemplate(bGet ? Generator.Template.SelectEnumerationGet : Generator.Template.SelectEnumerationSet);

            //var impl = generator.StringByTemplate(bGet ? Generator.Template.SelectGetEntityImplementation : Generator.Template.SelectSetEntityImplementation);
            //generator.m_implementations.Append(impl);

        }


        private void WriteAccessorMethod(Generator generator, ExpressEntity entityType, bool bGet)
        {
            generator.m_replacements[Generator.KWD_REF_ENTITY] = entityType.name;
            generator.m_replacements[Generator.KWD_TypeNameUpper] = entityType.name.ToUpper();

            generator.WriteByTemplate(bGet ? Generator.Template.SelectEntityGet : Generator.Template.SelectEntitySet);

            var impl = generator.StringByTemplate(bGet ? Generator.Template.SelectEntityGetImplementation : Generator.Template.SelectEntitySetImplementation);
            generator.m_implementations.Append(impl);
        }


        private void WriteAccessorMethod(Generator generator, ExpressDefinedType definedType, bool bGet)
        {
            if (definedType.name == "IfcBinary")
                return;
            
            string sdaiType = definedType.GetSdaiType();
            string baseType = definedType.GetBaseCSType();

            generator.m_replacements[Generator.KWD_SimpleType] = definedType.name;
            generator.m_replacements[Generator.KWD_TextType] = definedType.name;
            generator.m_replacements[Generator.KWD_sdaiTYPE] = sdaiType;
            generator.m_replacements[Generator.KWD_TypeNameUpper] = definedType.name.ToUpper();

            var tpl = Generator.Template.None;
            if (bGet)
                tpl = baseType == "string" ? Generator.Template.SelectTextGet : Generator.Template.SelectSimpleGet;
            else
                tpl = baseType == "string" ? Generator.Template.SelectTextSet : Generator.Template.SelectSimpleSet;

            generator.WriteByTemplate (tpl);
        }

        private void WriteAggrAccessorMethod(Generator generator, ExpressDefinedType definedType, bool bGet)
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

            var tpl = bGet ? Generator.Template.SelectAggregationGet : Generator.Template.SelectAggregationSet;
            generator.WriteByTemplate(tpl);

            if (!bGet && !definedType.nestedAggr)
            {
                tpl = baseType == "string" ? Generator.Template.SelectAggregationSetArrayText : Generator.Template.SelectAggregationSetArraySimple;
                generator.WriteByTemplate(tpl);
            }
        }

        private void WriteAccessorMethod(Generator generator, ExpressSelect selectType)
        {
            var saveSelect = generator.m_replacements[Generator.KWD_TYPE_NAME];
            generator.m_replacements[Generator.KWD_TYPE_NAME] = selectType.name;

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
