﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDF;

using ExpressHandle = System.Int64;

namespace RDFWrappers
{
    public class TypeDef
    {
        public enum_express_attr_type attrType;
        public ExpressHandle domain;
        public enum_express_aggr aggrType;
        public bool nestedAggr;
        public Int64 cardinalityMin;
        public Int64 cardinalityMax;

        public bool IsSimpleType(out string domainType, out string baseType, out string sdaiType)
        {
            return IsSimpleType(attrType, domain, out domainType, out baseType, out sdaiType);
        }

        private bool IsSimpleType(enum_express_attr_type attrType, ExpressHandle domainEntity, out string domainTypeName, out string baseType, out string sdaiType)
        {
            domainTypeName = null;
            baseType = null;
            sdaiType = null;

            if (domainEntity != 0)
            {
                domainTypeName = ExpressSchema.GetNameOfDeclaration(domainEntity);
            }

            switch (attrType)
            {
                case enum_express_attr_type.__NONE: //attribute type is defined by reference domain entity
                    if (enum_express_declaration.__DEFINED_TYPE == ifcengine.engiGetDeclarationType(domainEntity))
                    {
                        var domainType = new ExpressDefinedType(domainEntity);
                        string skip;
                        bool ret = IsSimpleType(domainType.attrType, domainType.domain, out skip, out baseType, out sdaiType);
                        return ret;
                    }
                    else
                    {
                        return false;
                    }

                case enum_express_attr_type.__LOGICAL:
                case enum_express_attr_type.__ENUMERATION:
                case enum_express_attr_type.__SELECT:
                case enum_express_attr_type.__BINARY:
                case enum_express_attr_type.__BINARY_32:
                    return false;

                case enum_express_attr_type.__BOOLEAN:
                    baseType = "bool";
                    sdaiType = "sdaiBOOLEAN";
                    return true;

                case enum_express_attr_type.__INTEGER:
                    baseType = "Int64";
                    sdaiType = "sdaiINTEGER";
                    return true;

                case enum_express_attr_type.__REAL:
                case enum_express_attr_type.__NUMBER:
                    baseType = "double";
                    sdaiType = "sdaiREAL";
                    return true;

                case enum_express_attr_type.__STRING:
                    baseType = "string";
                    sdaiType = "sdaiSTRING";
                    return true;

                default:
                    System.Diagnostics.Debug.Assert(false);
                    return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public bool IsEntityReference(out string entityName)
        {
            entityName = null;

            if (attrType == enum_express_attr_type.__NONE)
            {
                if (enum_express_declaration.__ENTITY == ifcengine.engiGetDeclarationType(domain))
                {
                    entityName = ExpressSchema.GetNameOfDeclaration(domain);
                    return true;
                }
            }
            return false;
        }

        public bool IsEnumeration(out string enumerationName)
        {
            enumerationName = null;

            if (attrType == enum_express_attr_type.__NONE)
            {
                if (enum_express_declaration.__ENUM == ifcengine.engiGetDeclarationType(domain))
                {
                    enumerationName = ExpressSchema.GetNameOfDeclaration(domain);
                    return true;
                }
            }

            return false;
        }

        public ExpressSelect IsSelect()
        {
            if (attrType == enum_express_attr_type.__NONE)
            {
                if (enum_express_declaration.__SELECT == ifcengine.engiGetDeclarationType(domain))
                {
                    var sel = new ExpressSelect(domain);
                    return sel;
                }
            }

            return null;
        }

    }
}
