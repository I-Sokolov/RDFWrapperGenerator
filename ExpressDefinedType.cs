using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDF;

using ExpressHandle = System.Int64;

namespace RDFWrappers
{
    public class ExpressDefinedType : TypeDef
    {
        public class Foundation
            {
            public enum_express_declaration domainType = enum_express_declaration.__UNDEF; //Undef for primitive types, see attrType 
            public enum_express_attr_type attrType = enum_express_attr_type.__NONE;        
            public enum_express_aggr aggrType = enum_express_aggr.__NONE;
            };

        public string                     name;
        public ExpressHandle              declaration;

        public ExpressDefinedType (ExpressHandle declaration)
        {
            this.declaration = declaration;

            System.Diagnostics.Debug.Assert(ifcengine.engiGetDeclarationType(declaration) == enum_express_declaration.__DEFINED_TYPE);

            name = ExpressSchema.GetNameOfDeclaration(declaration);

            attrType = ifcengine.engiGetDefinedType(declaration, out domain, out aggregation);
        }

        public string GetBaseCSType()
        {
            if (domain != 0)
            {
                var domainType = ifcengine.engiGetDeclarationType(domain);
                switch (domainType)
                {
                    case enum_express_declaration.__DEFINED_TYPE:
                        var refType = new ExpressDefinedType(domain);
                        return refType.GetBaseCSType();

                    case enum_express_declaration.__ENTITY:
                        return "IntData";

                    default:
                        return null;
                }
            }
            else
            {
                return ExpressSchema.GetPrimitiveType(attrType);
            }
        }

        public string GetSdaiType()
        {
            if (domain != 0)
            {
                var domainType = ifcengine.engiGetDeclarationType(domain);
                switch (domainType)
                {
                    case enum_express_declaration.__ENTITY:
                        return "sdaiINSTANCE";

                    case enum_express_declaration.__ENUM:
                        return "sdaiENUM";

                    case enum_express_declaration.__SELECT:
                        return null;

                    case enum_express_declaration.__DEFINED_TYPE:
                        var refType = new ExpressDefinedType(domain);
                        return refType.GetSdaiType();

                    default:
                        throw new ApplicationException("unknonw express type " + domainType.ToString());
                }
            }
            else
            {
                return ExpressSchema.GetSdaiType(attrType);
            }
        }

        public Foundation WriteType(Generator generator, HashSet<ExpressHandle> visitedTypes)
        {
            if (!visitedTypes.Add(declaration))
            {
                Foundation f = null;
                generator.m_writtenDefinedTyes.TryGetValue(declaration, out f);
                return f; //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            }

            var foundation = new Foundation();
            foundation.attrType = attrType;

            if (domain != 0)
            {
                var referTypeName = ExpressSchema.GetNameOfDeclaration(domain);
                generator.m_replacements[Generator.KWD_SimpleType] = referTypeName;

                var referType = RDF.ifcengine.engiGetDeclarationType(domain);
                foundation.domainType = referType;

                switch (referType)
                {
                    case RDF.enum_express_declaration.__ENUM:
                        foundation = null;
                        break;

                    case RDF.enum_express_declaration.__SELECT:
                        foundation = null;
                        break;

                    case RDF.enum_express_declaration.__ENTITY:
                        break;

                    case RDF.enum_express_declaration.__DEFINED_TYPE:
                        var referencedType = new ExpressDefinedType(domain);
                        var baseFoundation = referencedType.WriteType(generator, visitedTypes);
                        foundation.aggrType = baseFoundation.aggrType;
                        foundation.attrType = baseFoundation.attrType;
                        foundation.domainType = baseFoundation.domainType;
                        break;

                    default:
                        throw new ApplicationException("Unexpexted defined type domain " + referType.ToString());
                }

                if (foundation == null)
                {
                    Console.WriteLine("Can not write dfineded type {0} referenced to {1} {2}, unsupported foundation", name, referType.ToString(), referTypeName);
                    return null;
                }
            }
            else if (attrType == RDF.enum_express_attr_type.__LOGICAL)
            {
                generator.m_replacements[Generator.KWD_SimpleType] = "LOGICAL";
            }
            else
            {
                var csType = ExpressSchema.GetPrimitiveType(attrType);
                if (csType == null)
                {
                    Console.WriteLine("Defined type {0} is not supproted (primitive type is {1})", name, attrType.ToString());
                    return null;
                }

                generator.m_replacements[Generator.KWD_SimpleType] = csType;
            }

            //
            //
            if (aggregation == 0)
            {
                generator.m_replacements[Generator.KWD_DEFINED_TYPE] = name;
                generator.WriteByTemplate(Generator.Template.DefinedType);
            }
            else
            {
                foundation.aggrType = Aggregation.WriteDefinedType(generator, this);
            }

            generator.m_writtenDefinedTyes.Add(declaration, foundation);

            return foundation;
        }

        public void WriteAttribute (Generator generator, ExpressAttribute attr)
        {
            switch (attrType)
            {
                case RDF.enum_express_attr_type.__BINARY:
                case RDF.enum_express_attr_type.__BINARY_32:
                    return;

                case RDF.enum_express_attr_type.__LOGICAL:
                    generator.WriteEnumAttribute(attr, name, "LOGICAL_VALUE_NAMES");
                    return;
            }

            Console.WriteLine("Attribute '" + attr.name + "' is not supporrted, defined type: " + ToString() + ", defining entity " + ExpressSchema.GetNameOfDeclaration(attr.definingEntity));
        }

        public override string ToString()
        {
            var str = new StringBuilder();

            str.Append(string.Format("{0}: {1}", name, base.ToString ()));

            return str.ToString();

        }
    }
}
