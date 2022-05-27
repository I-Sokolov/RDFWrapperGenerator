using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDF;

using SdaiInstance = System.Int64;

namespace RDFWrappers
{
    class ExpressDefinedType
    {
        string                            name;
        SdaiInstance                      inst;
        enum_express_attr_type            type;
        SdaiInstance                      referenced;

        public ExpressDefinedType (string name, SdaiInstance inst)
        {
            this.name = name;
            this.inst = inst;

            type = ifcengine.engiGetDefinedType(inst, out referenced);
        }


        public override string ToString()
        {
            var str = new StringBuilder();

            str.Append(string.Format("{0}: {1} {2}", name, type.ToString(), ExpressSchema.GetNameOfEntity(referenced)));

            return str.ToString();

        }
    }
}
