using System;
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

        public Int64 aggregation;

        public bool IsAggregation()
        {
            if (aggregation != 0)
            {
                return true;
            }

            if (domain != 0)
            {
                if (ifcengine.engiGetDeclarationType (domain) == enum_express_declaration.__DEFINED_TYPE)
                {
                    ExpressHandle refer;
                    Int64 aggr;
                    ifcengine.engiGetDefinedType(domain, out refer, out aggr);
                    if (aggr != 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        
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

            baseType = ExpressSchema.GetPrimitiveType(attrType);

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
                    sdaiType = "sdaiBOOLEAN";
                    return true;

                case enum_express_attr_type.__INTEGER:
                    sdaiType = "sdaiINTEGER";
                    return true;

                case enum_express_attr_type.__REAL:
                case enum_express_attr_type.__NUMBER:
                    sdaiType = "sdaiREAL";
                    return true;

                case enum_express_attr_type.__STRING:
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

        public ExpressDefinedType IsDefinedType ()
        {
            if (domain != 0)
            {
                if (enum_express_declaration.__DEFINED_TYPE == ifcengine.engiGetDeclarationType(domain))
                {
                    var dt = new ExpressDefinedType(domain);
                    return dt;
                }
            }
            return null;
        }

        public ExpressEnumeraion IsEnumeration()
        {
            if (attrType == enum_express_attr_type.__NONE)
            {
                if (enum_express_declaration.__ENUM == ifcengine.engiGetDeclarationType(domain))
                {
                    var en = new ExpressEnumeraion(domain);
                    return en;
                }
            }

            return null;
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

        public override string ToString()
        {
            var str = new StringBuilder();

            while (aggregation != 0)
            {
                enum_express_aggr aggrType;
                Int64 cardMin, cardMax;
                ifcengine.engiGetAggregation(aggregation, out aggrType, out cardMin, out cardMax, out aggregation);

                str.Append(aggrType.ToString() + "[" + cardMin.ToString() + ".." + cardMax.ToString() + "] OF ");
            }

            switch (attrType)
            {
                case enum_express_attr_type.__NONE: //attribute type is defined by reference domain entity
                    if (domain != 0)
                    {
                        str.Append (ExpressSchema.GetNameOfDeclaration(domain));
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
                        str.Append ("ENUM " + ExpressSchema.GetNameOfDeclaration(domain));
                    }
                    else
                    {
                        System.Diagnostics.Debug.Assert(false);
                    }
                    break;

                case enum_express_attr_type.__SELECT:
                    if (domain != 0)
                    {
                        str.Append ("SELECT " + ExpressSchema.GetNameOfDeclaration(domain));
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
                    str.Append (attrType.ToString());
                    if(domain != 0)
                    {
                        str.Append("<-");
                        str.Append(ExpressSchema.GetNameOfDeclaration(domain));
                    }
                    break;

                default:
                    System.Diagnostics.Debug.Assert(false);
                    break;
            }

            return str.ToString();
        }

    }
}
