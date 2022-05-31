﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDF;

using ExpressHandle = System.Int64;

namespace RDFWrappers
{
    public class ExpressAttribute
    {
        public string name;
        public ExpressHandle definingEntity;
        public bool inverse;
        public enum_express_attr_type attrType;
        public ExpressHandle domain;
        public enum_express_aggr aggrType;
        public bool nestedAggr;
        public Int64 cardinalityMin;
        public Int64 cardinalityMax;
        public bool optional;
        public bool unique;

        public bool AsSimpleType(bool downgradeDefinedType, out string csType, out string sdaiType)
        {
            return AsSimpleType(downgradeDefinedType, attrType, domain, out csType, out sdaiType);
        }

        private bool AsSimpleType (bool downgradeDefinedType, enum_express_attr_type attrType, ExpressHandle domainEntity, out string csType, out string sdaiType)
        {
            csType = null;
            sdaiType = null;

            switch (attrType)
            {
                case enum_express_attr_type.__NONE: //attribute type is defined by reference domain entity
                    if (enum_express_declaration.__DEFINED_TYPE == ifcengine.engiGetDeclarationType(domainEntity))
                    {
                        var definedType = new ExpressDefinedType(domainEntity);
                        bool ret = AsSimpleType(downgradeDefinedType, definedType.type, definedType.referenced, out csType, out sdaiType);
                        if (ret && !downgradeDefinedType)
                        {
                            csType = definedType.name;
                        }
                        return ret;
                    }
                    else
                    {
                        return false;
                    }

                case enum_express_attr_type.__ENUMERATION:
                case enum_express_attr_type.__SELECT:
                case enum_express_attr_type.__BINARY:
                case enum_express_attr_type.__BINARY_32:
                    return false;

                case enum_express_attr_type.__BOOLEAN:
                    csType = "bool";
                    sdaiType = "sdaiBOOLEAN";
                    return true;

                case enum_express_attr_type.__INTEGER:
                    csType = "Int64";
                    sdaiType = "sdaiINTEGER";
                    return true;

                case enum_express_attr_type.__LOGICAL:
                    csType = "Int64";
                    sdaiType = "sdaiLOGICAL";
                    return true;

                case enum_express_attr_type.__REAL:
                case enum_express_attr_type.__NUMBER:
                    csType = "double";
                    sdaiType = "sdaiREAL";
                    return true;

                case enum_express_attr_type.__STRING:
                    csType = "string";
                    sdaiType = "sdaiSTRING";
                    return true;

                default:
                    System.Diagnostics.Debug.Assert(false);
                    return false;
            }
        }

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
