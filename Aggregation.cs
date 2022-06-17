using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDFWrappers
{
    class Aggregation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="generator"></param>
        public static void WriteAttributesTypes (Generator generator)
        {
            generator.WriteByTemplate(Generator.Template.AggrgarionTypesBegin);

            foreach (var decl in generator.m_schema.m_declarations[RDF.enum_express_declaration.__ENTITY])
            {
                var entity = new ExpressEntity(decl.Value);

                foreach (var attr in entity.GetAttributes ())
                {
                    if (attr.aggrType != RDF.enum_express_aggr.__NONE)
                    {
                        var aggr = new Aggregation(generator, attr);
                        aggr.WriteType(null);
                    }
                }
            }
        }

        public static void WriteDefinedType (Generator generator, ExpressDefinedType definedType)
        {
            var aggr = new Aggregation(generator, definedType);
            aggr.WriteType(definedType.name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="generator"></param>
        /// <param name="attr"></param>
        public static void WriteAttribute (Generator generator, ExpressAttribute attr)
        {
            var aggr = new Aggregation(generator, attr);
            aggr.WriteAttribute(attr.name, attr.inverse);
        }

        /// <summary>
        /// 
        /// </summary>
        private Generator generator;
        private TypeDef typeDef;

        private Aggregation(Generator generator, TypeDef typeDef)
        {
            this.generator = generator;
            this.typeDef = typeDef;
        }


        private void WriteType (string name)
        {
            string aggrType;
            int nested;
            string elemType;
            string sdaiType;

            Generator.Template template = GetAggregationInfo(typeDef, out aggrType, out nested, out elemType, out sdaiType);

            if (template != Generator.Template.None)
            {
                for (int nest = 0; nest <= nested; nest++)
                {
                    string aggrName = name;
                    if (aggrName == null || nest < nested)
                    {
                        aggrName = MakeAggregationTypeName(aggrType, nest, elemType);
                    }

                    if (generator.m_knownAggregationTypes.Add(aggrName))
                    {
                        generator.m_replacements[Generator.KWD_sdaiTYPE] = sdaiType;
                        generator.m_replacements[Generator.KWD_AggregationType] = aggrName;

                        if (nest == 0)
                        {
                            generator.m_replacements[Generator.KWD_SimpleType] = elemType;
                            generator.WriteByTemplate(template);
                        }
                        else
                        {
                            generator.m_replacements[Generator.KWD_SimpleType] = MakeAggregationTypeName(aggrType, nest - 1, elemType);
                            generator.WriteByTemplate(Generator.Template.AggregationOfAggregation);
                        }
                    }
                }
            }
        }

        private string MakeAggregationTypeName (string aggrType, int nesting, string elemType)
        {
            var name = new StringBuilder();

            for (; nesting >= 0; nesting--)
            {
                name.Append(aggrType);
                name.Append("Of");
            }

            var ch = elemType.FirstOrDefault();
            if (Char.IsLower (ch))
            {
                ch = Char.ToUpper(ch);
            }

            name.Append(ch);

            name.Append(elemType.Substring(1));

            return name.ToString();
        }

        private Generator.Template GetAggregationInfo(TypeDef typeDef, out string aggrType, out int nested, out string elemType, out string sdaiType)
        {
            Generator.Template template = Generator.Template.None;

            aggrType = null;
            elemType = null;
            sdaiType = null;
            nested = 0;

            switch (typeDef.aggrType)
            {
                case RDF.enum_express_aggr.__ARRAY:
                    aggrType = "Array";
                    break;

                case RDF.enum_express_aggr.__LIST:
                    aggrType = "List";
                    break;

                case RDF.enum_express_aggr.__SET:
                    aggrType = "Set";
                    break;

                default:
                    Console.WriteLine("unsupported aggregation type " + typeDef.ToString());
                    return template;
            }

            string baseType = null;
            ExpressSelect select = null;

            if (typeDef.IsSimpleType(out elemType, out baseType, out sdaiType))
            {
                if (baseType != null)
                {
                    if (generator.m_cs || elemType == null)
                    {
                        elemType = baseType;
                    }

                    if (baseType == "string")
                    {
                        template = Generator.Template.AggregationOfText;
                    }
                    else
                    {
                        template = Generator.Template.AggregationOfSimple;
                    }
                }
                else
                {
                    Console.WriteLine("Unsupported type in aggregations " + elemType);
                }
            }
            else if (typeDef.IsEntityReference(out elemType))
            {
                //TODO - must be
                return Generator.Template.None;
            }
            /*else if (typeDef.IsEnumeration(out elemType))
            {
                //WriteEnumAttribute(attr, expressType);
            }*/
            else if ((select = typeDef.IsSelect()) != null)
            {
                template = Generator.Template.AggregationOfSelect;
                elemType = select.name;
                sdaiType = null;
            }

            if (typeDef.nestedAggr)
            {
                nested = 1;
            }

            if (template == Generator.Template.None)
            {
                Console.WriteLine("aggregation is not supported: " + typeDef.ToString());
            }

            return template;
        }

        private void WriteAttribute(string attrName, bool isInverse)
        {
            string aggrTypeName = null;
            int nesting = 0;
            string elemType = null;
            string sdaiType = null;
            Generator.Template template = Generator.Template.None;

            if (typeDef.aggrType != RDF.enum_express_aggr.__NONE)
            {
                //unnamed aggregation
                string aggrType;
                template = GetAggregationInfo(typeDef, out aggrType, out nesting, out elemType, out sdaiType);
                if (template != Generator.Template.None)
                    aggrTypeName = MakeAggregationTypeName(aggrType, nesting, elemType);
            }
            else if (RDF.ifcengine.engiGetDeclarationType (typeDef.domain)==RDF.enum_express_declaration.__DEFINED_TYPE)
            {
                //assume defined type 
                var definedType = new ExpressDefinedType(typeDef.domain);
                aggrTypeName = definedType.name;
                string aggrType;
                template = GetAggregationInfo(definedType, out aggrType, out nesting, out elemType, out sdaiType);
            }
            else
            {
                throw new ApplicationException("unsupprorted aggregation " + typeDef.ToString());
            }

            if (template != Generator.Template.None)
            {
                if (generator.m_knownAggregationTypes.Contains(aggrTypeName))
                {
                    generator.m_replacements[Generator.KWD_ATTR_NAME] = attrName;
                    generator.m_replacements[Generator.KWD_AggregationType] = aggrTypeName;
                    generator.m_replacements[Generator.KWD_SimpleType] = elemType;

                    generator.WriteGetSet(Generator.Template.AttributeAggregationGet, Generator.Template.AttributeAggregationSet, isInverse);
                    if (nesting == 0 && sdaiType != null)
                    {
                        if (sdaiType == "sdaiSTRING")
                            generator.WriteByTemplate(Generator.Template.AttributeAggregationSetArrayText);
                        else
                            generator.WriteByTemplate(Generator.Template.AttributeAggregationSetArraySimple);
                    }
                }
            }
        }

#if not_now
            if (attr.nestedAggr)
            {
                //TODO
            }
            else
            {
                string expressType = null;
                string baseType = null;
                string sdaiType = null;
                ExpressSelect select = null;

                if (attr.IsSimpleType(out expressType, out baseType, out sdaiType))
                {
                    WriteSimpleType(expressType, baseType, sdaiType);
                }
                else if (attr.IsEntityReference(out expressType))
                {
                    //WriteEntityReference(attr, expressType);
                }
                else if (attr.IsEnumeration(out expressType))
                {
                    //WriteEnumAttribute(attr, expressType);
                }
                else if ((select = attr.IsSelect()) != null)
                {
                    //select.WriteAttribute(this, attr);
                }
                else
                {
                    Console.WriteLine(attr.name + " not supported");
                }
            }
        }

        private string GetCAggrType()
        {
            switch (attr.aggrType)
            {
                case RDF.enum_express_aggr.__ARRAY: return "list";
                case RDF.enum_express_aggr.__LIST: return "list";
                case RDF.enum_express_aggr.__SET: return "set";
            }
            throw new ApplicationException ("unsupported aggrType " + attr.aggrType.ToString());
        }

        private void WriteSimpleType(string expressType, string baseType, string sdaiType)
        {
            generator.m_replacements[Generator.KWD_SimpleType] = (!generator.m_cs && expressType != null) ? expressType : baseType;
            generator.m_replacements[Generator.KWD_TextType] = generator.m_replacements[Generator.KWD_SimpleType]; //just different words in template
            generator.m_replacements[Generator.KWD_sdaiTYPE] = sdaiType;
            generator.m_replacements[Generator.KWD_AGGR_TYPE] = GetCAggrType();

            //Generator.Template tplGet = (baseType == "string") ? Generator.Template.AggregationTextGet : Generator.Template.AggregationGetSimple;
            //Generator.Template tplSet = (baseType == "string") ? Generator.Template.AggregationSetString : Generator.Template.AggregationSetSimple;

            //generator.WriteGetSet(tplGet, tplSet, attr.inverse);
        }
#endif
    }
}
