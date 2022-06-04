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
        string name { get { return ExpressSchema.GetNameOfDeclaration(inst); } }
        ExpressHandle inst;

        public ExpressSelect(ExpressHandle inst)
        {
            this.inst = inst;
        }

        public HashSet<ExpressHandle> GetVariants(bool resolveNestedSelects)
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

        public void WriteGetSetMethods (Generator generator, ExpressAttribute attr)
        {
            var variants = GetVariants(true);
            foreach (var variant in variants)
            {
                WriteGetSetMethods(generator, attr, variant);
            }
        }


        private void WriteGetSetMethods(Generator generator, ExpressAttribute attr, ExpressHandle selectVariant)
        {
            switch (ifcengine.engiGetDeclarationType (selectVariant))
            {
                case enum_express_declaration.__DEFINED_TYPE:
                    var definedType = new ExpressDefinedType(selectVariant);
                    WriteGetSetMethods(generator, attr, definedType);
                    break;

                case enum_express_declaration.__ENTITY:
                    break;

                default:
                    System.Diagnostics.Debug.Assert(false);
                    break;
            }
        }

        private void WriteGetSetMethods(Generator generator, ExpressAttribute attr, ExpressDefinedType definedType)
        {
            if (definedType.name == "IfcBinary")
                return;

            //TODO - methods that return defined typed does not use defined type
            string sdaiType = definedType.GetSdaiType();

            generator.m_replacements[Generator.KWD_CS_DATATYPE] = definedType.name;
            generator.m_replacements[Generator.KWD_TYPE_NAME] = definedType.name;
            //generator.m_replacements[Generator.KWD_StringType] = (baseType == "string" && definedType != null) ? definedType : "const char*";
            generator.m_replacements[Generator.KWD_sdai_DATATYPE] = sdaiType;

            //Template tplGet = baseType == "string" ? Template.GetSimpleAttributeString : Template.GetSimpleAttribute;
            //Template tplSet = baseType == "string" ? Template.SetSimpleAttributeString : Template.SetSimpleAttribute;

            generator.WriteGetSet(Generator.Template.GetSelectSimpleAttribute, Generator.Template.SetSelectSimpleAttribute, attr.inverse);
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
