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
        public static void WriteTypes (Generator generator)
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
                        aggr.WriteType();
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="generator"></param>
        /// <param name="attr"></param>
        public static void WriteAttribute (Generator generator, ExpressAttribute attr)
        {
            var aggr = new Aggregation(generator, attr);
            aggr.WriteAttribute();
        }

        /// <summary>
        /// 
        /// </summary>
        private Generator generator;
        private ExpressAttribute attr;

        private Aggregation(Generator generator, ExpressAttribute attr)
        {
            this.generator = generator;
            this.attr = attr;
        }


        private void WriteType ()
        {
            string aggrType;
            int nested;
            string elemType;
            string sdaiType;

            Generator.Template template = GetTypeInfo(out aggrType, out nested, out elemType, out sdaiType);

            if (template != Generator.Template.None)
            {
                for (int nest = 0; nest <= nested; nest++)
                {
                    string aggrName = GetTypeName(aggrType, nest, elemType);

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
                            generator.m_replacements[Generator.KWD_SimpleType] = GetTypeName(aggrType, nest - 1, elemType);
                            generator.WriteByTemplate(Generator.Template.AggregationOfAggregation);
                        }
                    }
                }
            }
        }

        private string GetTypeName (string aggrType, int nesting, string elemType)
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

        private Generator.Template GetTypeInfo(out string aggrType, out int nested, out string elemType, out string sdaiType)
        {
            Generator.Template template = Generator.Template.None;

            aggrType = null;
            elemType = null;
            sdaiType = "";
            nested = 0;

            switch (attr.aggrType)
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
                    Console.WriteLine("unsupported aggrType " + attr.aggrType.ToString());
                    return template;
            }

            string baseType = null;
            ExpressSelect select = null;

            if (attr.IsSimpleType(out elemType, out baseType, out sdaiType))
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
            else if (attr.IsEntityReference(out elemType))
            {
                //WriteEntityReference(attr, expressType);
            }
            else if (attr.IsEnumeration(out elemType))
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

            if (attr.nestedAggr)
            {
                nested = 1;
            }

            return template;
        }
        private void WriteAttribute()
        {
            string aggrType;
            int nesting;
            string elemType;
            string sdaiType;
            Generator.Template template = GetTypeInfo(out aggrType, out nesting, out elemType, out sdaiType);

            if (template != Generator.Template.None)
            {
                string aggrName = GetTypeName(aggrType, nesting, elemType);

                if (generator.m_knownAggregationTypes.Contains(aggrName))
                {
                    generator.m_replacements[Generator.KWD_ATTR_NAME] = attr.name;
                    generator.m_replacements[Generator.KWD_AggregationType] = aggrName;
                    generator.m_replacements[Generator.KWD_SimpleType] = elemType;

                    generator.WriteGetSet(Generator.Template.AttributeAggregationGet, Generator.Template.AttributeAggregationSet, attr.inverse);
                    if (nesting == 0)
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
