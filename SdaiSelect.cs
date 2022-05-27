using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDF;

using SdaiInstance = System.Int64;

namespace RDFWrappers
{
    class SdaiSelect
    {
        string name;
        SdaiInstance inst;

        public SdaiSelect(string name, SdaiInstance inst)
        {
            this.name = name;
            this.inst = inst;
        }

        public List<SdaiInstance> GetVariants()
        {
            var ret = new List<SdaiInstance>();

            int i = 0;
            SdaiInstance variant;
            while (0 != ifcengine.engiGetSelectDefinitionVariant(inst, i++, out variant))
            {
                ret.Add(variant);
            }

            return ret;
        }


        public override string ToString()
        {
            var str = new StringBuilder();

            str.AppendLine(string.Format("{0}:", name));

            foreach (var variant in GetVariants())
            {
                var name = SdaiSchema.GetNameOfEntity(variant);
                var type = ifcengine.engiGetDefinitionType(variant);

                str.AppendLine(string.Format("        {0} {1}", name, type.ToString()));
            }

            return str.ToString();

        }

    }
}
