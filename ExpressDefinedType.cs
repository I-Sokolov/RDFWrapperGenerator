using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDF;

using ExpressHandle = System.Int64;

namespace RDFWrappers
{
    class ExpressDefinedType
    {
        public string                     name;
        public ExpressHandle              declaration;
        public enum_express_attr_type     type;
        public ExpressHandle              referenced;

        public ExpressDefinedType (ExpressHandle declaration)
        {
            this.declaration = declaration;

            System.Diagnostics.Debug.Assert(ifcengine.engiGetDeclarationType(declaration) == enum_express_declaration.__DEFINED_TYPE);

            name = ExpressSchema.GetNameOfDeclaration(declaration);

            type = ifcengine.engiGetDefinedType(declaration, out referenced);
        }

        public string GetBaseCSType()
        {
            if (referenced != 0)
            { var refType = new ExpressDefinedType(referenced);
                return refType.GetBaseCSType();
            }
            else
            {
                return ExpressSchema.GetCSType(type);
            }
        }

        public string GetSdaiType()
        {
            if (referenced != 0)
            {
                var refType = new ExpressDefinedType(referenced);
                return refType.GetSdaiType();
            }
            else
            {
                return ExpressSchema.GetSdaiType(type);
            }
        }

        public override string ToString()
        {
            var str = new StringBuilder();

            str.Append(string.Format("{0}: {1} {2}", name, type.ToString(), ExpressSchema.GetNameOfDeclaration(referenced)));

            return str.ToString();

        }
    }
}
