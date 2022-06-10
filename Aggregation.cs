using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDFWrappers
{
    class Aggregation
    {
        Generator generator;
        ExpressAttribute attr;

        public Aggregation(Generator generator, ExpressAttribute attr)
        {
            this.generator = generator;
            this.attr = attr;
        }

        public void Write()
        {
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
            generator.m_replacements[Generator.KWD_StringType] = generator.m_replacements[Generator.KWD_SimpleType]; //just different words in template
            generator.m_replacements[Generator.KWD_sdaiTYPE] = sdaiType;
            generator.m_replacements[Generator.KWD_AGGR_TYPE] = GetCAggrType();

            //Generator.Template tplGet = (baseType == "string") ? Generator.Template.AggregationTextGet : Generator.Template.AggregationGetSimple;
            //Generator.Template tplSet = (baseType == "string") ? Generator.Template.AggregationSetString : Generator.Template.AggregationSetSimple;

            //generator.WriteGetSet(tplGet, tplSet, attr.inverse);
        }
    }
}
