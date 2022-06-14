using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDF;

using ExpressHandle = System.Int64;

namespace RDFWrappers
{
    public class ExpressDefinedType :  TypeDef
    {
        public string                     name;
        public ExpressHandle              declaration;

        public ExpressDefinedType (ExpressHandle declaration)
        {
            this.declaration = declaration;

            System.Diagnostics.Debug.Assert(ifcengine.engiGetDeclarationType(declaration) == enum_express_declaration.__DEFINED_TYPE);

            name = ExpressSchema.GetNameOfDeclaration(declaration);

            attrType = ifcengine.engiGetDefinedType(declaration, out domain, out aggrType, out nestedAggr, out cardinalityMin, out cardinalityMax);
        }

        public string GetBaseCSType()
        {
            if (domain != 0)
            { var refType = new ExpressDefinedType(domain);
                return refType.GetBaseCSType();
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
                var refType = new ExpressDefinedType(domain);
                return refType.GetSdaiType();
            }
            else
            {
                return ExpressSchema.GetSdaiType(attrType);
            }
        }

        public override string ToString()
        {
            var str = new StringBuilder();

            str.Append(string.Format("{0}: {1}", name, base.ToString ()));

            return str.ToString();

        }
    }
}
