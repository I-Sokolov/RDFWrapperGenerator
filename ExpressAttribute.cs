using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDF;

using ExpressHandle = System.Int64;

namespace RDFWrappers
{
    public class ExpressAttribute : TypeDef
    {
        public string name;
        public ExpressHandle definingEntity;
        public bool inverse;
        public bool optional;
        public bool unique;


        private string DefiningEntity { get { return ExpressSchema.GetNameOfDeclaration(definingEntity); } }
        //private string Domain { get { return ExpressSchema.GetNameOfDeclaration(domain); } }

        /*
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetSdaiType()
        {
            switch (attrType)
            {
                case enum_express_attr_type.__NONE: //attribute type is defined by reference domain entity
                    return "sdaiINSTANCE";

                case enum_express_attr_type.__ENUMERATION:
                    return "sdaiENUM";

                case enum_express_attr_type.__SELECT:
                case enum_express_attr_type.__BINARY:
                case enum_express_attr_type.__BINARY_32:
                    System.Diagnostics.Debug.Assert(false);
                    return null;

                case enum_express_attr_type.__BOOLEAN:
                    return "sdaiBOOLEAN";

                case enum_express_attr_type.__INTEGER:
                    return "sdaiINTEGER";

                case enum_express_attr_type.__LOGICAL:
                    return "sdaiLOGICAL";

                case enum_express_attr_type.__REAL:
                case enum_express_attr_type.__NUMBER:
                    return "sdaiREAL";

                case enum_express_attr_type.__STRING:
                    return "sdaiSTRING";

                default:
                    System.Diagnostics.Debug.Assert(false);
                    return null;
            }
        }
        */

    override public string ToString()
        {
            string str = name + ": ";

            if (inverse)
            {
                str += "inverse ";
            }

            if (nestedAggr)
            {
                System.Diagnostics.Debug.Assert(aggrType != enum_express_aggr.__NONE);
                str += "NESTED";
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
                    str += aggrType.ToString() + "[" + cardinalityMin.ToString() + ".." + cardinalityMax.ToString() + "] OF ";
                    break;

                default:
                    System.Diagnostics.Debug.Assert(false);
                    break;
            }

            switch (attrType)
            {
                case enum_express_attr_type.__NONE: //attribute type is defined by reference domain entity
                    if (domain != 0)
                    {
                        str += ExpressSchema.GetNameOfDeclaration(domain);
                    }
                    else
                    {
                        System.Diagnostics.Debug.Assert(false);
                    }
                    break;

                case enum_express_attr_type.__ENUMERATION:
                    System.Diagnostics.Debug.Assert(false); //never happens
                    if (domain != 0)
                    {
                        str += "ENUM " + ExpressSchema.GetNameOfDeclaration(domain);
                    }
                    else
                    {
                        System.Diagnostics.Debug.Assert(false);
                    }
                    break;

                case enum_express_attr_type.__SELECT:
                    System.Diagnostics.Debug.Assert(false); //never happens
                    if (domain != 0)
                    {
                        str += "SELECT " + ExpressSchema.GetNameOfDeclaration(domain);
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
                    str += attrType.ToString();
                    System.Diagnostics.Debug.Assert(domain == 0);
                    break;

                default:
                    System.Diagnostics.Debug.Assert(false);
                    break;
            }

            System.Diagnostics.Debug.Assert(definingEntity != 0);
            str += " defined by " + DefiningEntity;

            return str; 
        }

    }
}
