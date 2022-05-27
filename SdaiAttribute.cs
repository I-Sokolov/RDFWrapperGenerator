using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDF;

using SdaiInstance = System.Int64;

namespace RDFWrappers
{
    public class SdaiAttribute
    {
        public string name;
        public SdaiInstance definingEntity;
        public bool inverse;
        public enum_express_attr_type attrType;
        public SdaiInstance domainEntity;
        public enum_express_aggr aggrType;
        public bool nestedAggr;
        public Int64 cardinalityMin;
        public Int64 cardinalityMax;
        public bool optional;
        public bool unique;

        public string DefiningEntity { get { return SdaiSchema.GetNameOfEntity(definingEntity); } }
        public string DomainEntity { get { return SdaiSchema.GetNameOfEntity(domainEntity); } }

        override public string ToString()
        {
            string csType = name + ": ";

            if (inverse)
            {
                csType += "inverse ";
            }

            if (nestedAggr)
            {
                System.Diagnostics.Debug.Assert(aggrType != enum_express_aggr.__NONE);
                csType += "NESTED";
            }

            switch (aggrType)
            {
                case enum_express_aggr.__NONE:
                    //scalar
                    break;

                case enum_express_aggr.__ARRAY:
                case enum_express_aggr.__BAG:
                case enum_express_aggr.__LIST:
                case enum_express_aggr.__SET:
                    csType += aggrType.ToString() + "[" + cardinalityMin.ToString() + ".." + cardinalityMax.ToString() + "] OF ";
                    break;

                default:
                    System.Diagnostics.Debug.Assert(false);
                    break;
            }

            switch (attrType)
            {
                case enum_express_attr_type.__NONE: //attribute type is defined by reference domain entity
                    if (domainEntity != 0)
                    {
                        csType += SdaiSchema.GetNameOfEntity(domainEntity);
                    }
                    else
                    {
                        System.Diagnostics.Debug.Assert(false);
                    }
                    break;

                case enum_express_attr_type.__ENUMERATION:
                    if (domainEntity != 0)
                    {
                        csType += "ENUM " + SdaiSchema.GetNameOfEntity(domainEntity);
                    }
                    else
                    {
                        System.Diagnostics.Debug.Assert(false);
                    }
                    break;

                case enum_express_attr_type.__SELECT:
                    if (domainEntity != 0)
                    {
                        csType += "SELECT " + SdaiSchema.GetNameOfEntity(domainEntity);
                    }
                    else
                    {
                        System.Diagnostics.Debug.Assert(false);
                    }
                    break;

                case enum_express_attr_type.__BINARY:
                case enum_express_attr_type.__BINARY_32:
                case enum_express_attr_type.__BOOLEAN:
                case enum_express_attr_type.__INTEGER:
                case enum_express_attr_type.__LOGICAL:
                case enum_express_attr_type.__NUMBER:
                case enum_express_attr_type.__REAL:
                case enum_express_attr_type.__STRING:
                    csType += attrType.ToString();
                    System.Diagnostics.Debug.Assert(domainEntity == 0);
                    break;

                default:
                    System.Diagnostics.Debug.Assert(false);
                    break;
            }

            System.Diagnostics.Debug.Assert(definingEntity != 0);
            csType += " defined by " + DefiningEntity;

            return csType; 
        }
    }
}
