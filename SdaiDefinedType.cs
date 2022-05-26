using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SdaiInstance = System.Int64;

namespace RDFWrappers
{
    class SdaiDefinedType
    {
        string       name;
        SdaiInstance inst;
        public SdaiDefinedType (string name, SdaiInstance inst)
        {
            this.name = name;
            this.inst = inst;
        }

        public override string ToString()
        {
            var str = new StringBuilder();

            str.Append(string.Format("{0}:", name));

            return str.ToString();

        }
    }
}
