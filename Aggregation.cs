using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDF;

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

            foreach (var decl in generator.m_schema.m_declarations[enum_express_declaration.__ENTITY])
            {
                var entity = new ExpressEntity(decl.Value);

                var attrs = entity.GetAttributes();
                foreach (var attr in attrs)
                {
                    if (attr.aggregation != 0) //unnamed aggregation
                    {
                        var aggr = new Aggregation(generator, attr);
                        aggr.WriteType(null);
                    }
                }
            }
        }

        public static enum_express_aggr WriteDefinedType (Generator generator, ExpressDefinedType definedType)
        {
            var aggr = new Aggregation(generator, definedType);
            return aggr.WriteType(definedType.name);
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


        enum_express_aggr WriteType (string name)
        {
            string elemType;
            string sdaiType;

            Generator.Template template = GetAggregatedType(typeDef, out elemType, out sdaiType);

            if (template != Generator.Template.None)
            {
                enum_express_aggr aggrType = enum_express_aggr.__NONE;
                var aggregation = typeDef.aggregation;
                while (aggregation!=0)
                {
                    Int64 crdMin, crdMax;
                    ifcengine.engiGetAggregation(aggregation, out aggrType, out crdMin, out crdMax, out aggregation);
                    System.Diagnostics.Debug.Assert(!(aggregation != 0 && name != null), "test nested aggregation for definded type"); 

                    string aggrName = (aggregation == 0) ? name : null; //use given name for outer aggregation
                    if (aggrName == null)
                    {
                        aggrName = MakeAggregationTypeName(aggrType, elemType);
                    }
                    if (aggrName == null)
                    {
                        return enum_express_aggr.__NONE;
                    }

                    if (generator.m_knownAggregationTypes.Add(aggrName))
                    {
                        generator.m_replacements[Generator.KWD_sdaiTYPE] = sdaiType;
                        generator.m_replacements[Generator.KWD_AggregationType] = aggrName;
                        generator.m_replacements[Generator.KWD_SimpleType] = elemType;
                        generator.m_replacements[Generator.KWD_ENUM_TYPE] = elemType;
                        generator.WriteByTemplate(template);
                    }

                    //for outer aggregarion
                    elemType = aggrName;
                    name = null;
                    template = Generator.Template.AggregationOfAggregation;
                }

                return aggrType;
            }

            return enum_express_aggr.__NONE;
        }

        private string MakeAggregationTypeName (enum_express_aggr aggrType, string elemType)
        {
            System.Diagnostics.Debug.Assert(elemType != null);
            bool camelCase = Char.IsUpper(elemType.First());

            var name = new StringBuilder();

            switch (aggrType)
            {
                case enum_express_aggr.__ARRAY:
                    name.Append(camelCase ? "Array" : "array");
                    break;

                case enum_express_aggr.__LIST:
                    name.Append(camelCase ? "List" : "list");
                    break;

                case RDF.enum_express_aggr.__SET:
                    name.Append(camelCase ? "Set" : "set");
                    break;

                case RDF.enum_express_aggr.__BAG:
                    name.Append(camelCase ? "Bag" : "bag");
                    break;

                default:
                    Console.WriteLine("unsupported aggregation type " + typeDef.ToString());
                    return null;
            }

            if (camelCase)
                name.Append("Of");
            else
                name.Append("_of_");
            
            name.Append(elemType);

            return name.ToString();
        }

        private Generator.Template GetAggregatedType(TypeDef typeDef, out string elemType, out string sdaiType)
        {
            Generator.Template template = Generator.Template.None;

            elemType = null;
            sdaiType = null;

            string baseType = null;
            ExpressSelect select = null;
            ExpressEnumeraion enumeration = null;
            ExpressDefinedType definedType = null;

            if (typeDef.IsSimpleType(out elemType, out baseType, out sdaiType))
            {
                if (baseType != null)
                {
                    if (generator.m_cs || elemType == null)
                    {
                        elemType = baseType;
                    }

                    if (baseType == "TextData")
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
                template = Generator.Template.AggregationOfInstance;
                sdaiType = "sdaiINSTANCE";
            }
            else if ((enumeration = typeDef.IsEnumeration()) != null)
            {
                elemType = enumeration.name;
                sdaiType = "sdaiENUM";
                template = Generator.Template.AggregationOfEnum;
            }
            else if (typeDef.domain == 0 && typeDef.attrType == RDF.enum_express_attr_type.__LOGICAL)
            {
                elemType = "LOGICAL_VALUE";
                sdaiType = "sdaiLOGICAL";
                template = Generator.Template.AggregationOfEnum;
            }
            else if ((select = typeDef.IsSelect()) != null)
            {
                template = Generator.Template.AggregationOfSelect;
                elemType = select.name;
                sdaiType = null;
            }
            else if ((definedType = typeDef.IsDefinedType()) != null)
            {
                ExpressDefinedType.Foundation foundation = null;
                if (generator.m_writtenDefinedTyes.TryGetValue(definedType.declaration, out foundation))
                {
                    switch (foundation.declarationType)
                    {
                        case enum_express_declaration.__ENTITY:
                            template = Generator.Template.AggregationOfInstance;
                            System.Diagnostics.Debug.Assert(false, "not tested");
                            break;
                        case enum_express_declaration.__ENUM:
                            template = Generator.Template.AggregationOfEnum;
                            System.Diagnostics.Debug.Assert(false, "not tested");
                            break;
                        case enum_express_declaration.__SELECT:
                            template = Generator.Template.AggregationOfSelect;
                            break;
                        default:
                            Console.WriteLine("Unexpected foundation type " + foundation.declarationType.ToString() + " in aggregation of " + typeDef.ToString());
                            System.Diagnostics.Debug.Assert(false);
                            break;
                    }

                    elemType = definedType.name;
                    sdaiType = definedType.GetSdaiType();
                }
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
            bool nested = false;
            string elemType = null;
            string sdaiType = null;
            Generator.Template template = Generator.Template.None;

            if (typeDef.aggregation != 0)
            {
                //unnamed aggregation
                template = GetAggregatedType(typeDef, out elemType, out sdaiType);
                if (template != Generator.Template.None)
                {
                    aggrTypeName = elemType;
                    for (var aggregation = typeDef.aggregation; aggregation != 0;)
                    {
                        enum_express_aggr aggrType;
                        Int64 crdMin, crdMax;
                        ifcengine.engiGetAggregation(aggregation, out aggrType, out crdMin, out crdMax, out aggregation);
                        aggrTypeName = MakeAggregationTypeName(aggrType, aggrTypeName);
                        if (aggregation != 0)
                        {
                            nested = true;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Aggregated attribute is not supported " + attrName + ": " + typeDef.ToString());
                }
            }
            else if (RDF.ifcengine.engiGetDeclarationType (typeDef.domain)==RDF.enum_express_declaration.__DEFINED_TYPE)
            {
                if (generator.m_writtenDefinedTyes.ContainsKey(typeDef.domain))
                {
                    var definedType = new ExpressDefinedType(typeDef.domain);
                    aggrTypeName = definedType.name;
                    template = GetAggregatedType(definedType, out elemType, out sdaiType);
                }
                else
                {
                    Console.WriteLine("Aggregated attribute is not supported (defined type is not supported) " + attrName + ": " + typeDef.ToString());
                }
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

                    generator.WriteGetPut(Generator.Template.AttributeAggregationGet, Generator.Template.AttributeAggregationPut, isInverse);
                    if (!nested && sdaiType != null)
                    {
                        if (sdaiType == "sdaiSTRING")
                            generator.WriteByTemplate(Generator.Template.AttributeAggregationPutArrayText);
                        else
                            generator.WriteByTemplate(Generator.Template.AttributeAggregationPutArraySimple);
                    }
                }
            }
        }
    }
}
