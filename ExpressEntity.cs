using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDF;

using ExpressHandle = System.Int64;

namespace RDFWrappers
{
    class ExpressEntity
    {
        public string name;
        public ExpressHandle inst;

        public ExpressEntity(ExpressHandle inst)
        {
            this.name = ExpressSchema.GetNameOfDeclaration (inst);
            this.inst = inst;
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ExpressAttribute> GetAttributes()
        {
            var ret = new List<ExpressAttribute>();

            var nattr = ifcengine.engiGetEntityNoArguments(inst);
            for (int i = 0; i < nattr; i++)
            {
                IntPtr ptrName = IntPtr.Zero;
                Int64 definingEntity, domainEntity;
                enum_express_attr_type attrType;
                enum_express_aggr aggrType;
                byte inverse, nestedAggr, optional, unique;
                Int64 cardinalityMin, cardinalityMax;

                byte ok = ifcengine.engiGetEntityProperty
                                (inst, i,
                                out ptrName,
                                out definingEntity, out inverse,
                                out attrType, out domainEntity,
                                out aggrType, out nestedAggr,
                                out cardinalityMin, out cardinalityMax,
                                out optional, out unique
                                );
                System.Diagnostics.Debug.Assert(ok != 0);

                if (ok != 0)
                {
                    var prop = new ExpressAttribute
                    {
                        name = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(ptrName),
                        definingEntity = definingEntity,
                        inverse = inverse != 0 ? true : false,
                        attrType = attrType,
                        domain = domainEntity,
                        aggrType = aggrType,
                        nestedAggr = nestedAggr != 0 ? true : false,
                        cardinalityMin = cardinalityMin,
                        cardinalityMax = cardinalityMax,
                        optional = optional != 0 ? true : false,
                        unique = unique != 0 ? true : false
                    };

                    //check duplications
                    bool add = true;
                    foreach (var a in ret)
                    { 
                        if (a.name == prop.name)
                        {
                            //Console.WriteLine("Duplicated attribute {0} for entity {1}", a.name, name);
                            add = false;
                            break;
                        }
                    }

                    //
                    if (add)
                        ret.Add(prop);
                }
            }

            return ret;
        }

        public List<ExpressHandle> GetSupertypes ()
        {
            var ret = new List<ExpressHandle>();

            int ind = 0;
            while (true)
            {
                var parentId = ifcengine.engiGetEntityParentEx(inst, ind++);
                if (parentId == 0)
                    break;
                ret.Add(parentId);
            }

            return ret;
        }

        public override string ToString()
        {
            var str = new StringBuilder();

            str.Append(string.Format("{0}:", name));

            foreach (var parent in GetSupertypes())
            {
                str.Append (string.Format(" {0}", ExpressSchema.GetNameOfDeclaration(parent)));
            }
            str.AppendLine() ;

            foreach (var attr in GetAttributes())
            {
                str.AppendLine(string.Format ("    {0}", attr.ToString()));
            }

            return str.ToString();
        }

        public bool IsAbstract()
        {
            return 0!=ifcengine.engiGetEntityIsAbstract(inst);
        }
    }
}
