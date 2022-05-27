using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDF;

using SdaiInstance = System.Int64;

namespace RDFWrappers
{
    class ExpressEnumeraion
    {
        string name;
        SdaiInstance inst;
        public ExpressEnumeraion(string name, SdaiInstance inst)
        {
            this.name = name;
            this.inst = inst;
        }

        public List<string> GetValues ()
        {
            var ret = new List<string>();

            int i = 0;
            var ptrValue = IntPtr.Zero;
            while (IntPtr.Zero!=(ptrValue = ifcengine.engiGetEnumerationElement(inst, i++)))
            {
                string value = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(ptrValue);
                ret.Add(value);
            }

            return ret;
        }

        public override string ToString()
        {
            var str = new StringBuilder();

            str.AppendLine(string.Format("{0}:", name));

            var vals = GetValues();
            foreach (var v in vals)
            {
                str.Append("        ");
                str.AppendLine(v);
            }

            return str.ToString();
        }
    }
}
