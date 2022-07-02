﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExpressHandle = System.Int64;

namespace RDFWrappers
{
    public class Generator
    {
        /// <summary>
        /// 
        /// </summary>
        const string KWD_PREPROC = "//##";

        public const string KWD_NAMESPACE = "NAMESPACE_NAME";
        public const string KWD_ENTITY_NAME = "ENTITY_NAME";
        public const string KWD_BASE_CLASS = "/*PARENT_NAME*/Entity";
        public const string KWD_DEFINED_TYPE = "DEFINED_TYPE_NAME";
        public const string KWD_SimpleType = "SimpleType";
        public const string KWD_TextType = "TextType";
        public const string KWD_TypeNameUpper = "TypeNameUpper";
        public const string KWD_ENUMERATION_NAME = "ENUMERATION_NAME";
        public const string KWD_ENUMERATION_VALUES_ARRAY = "ENUMERATION_VALUES_ARRAY";
        public const string KWD_ENUMERATION_ELEMENT = "ENUMERATION_ELEMENT";
        public const string KWD_NUMBER = "1234";
        public const string KWD_ATTR_NAME = "ATTR_NAME";
        public const string KWD_sdaiTYPE = "sdaiTYPE";
        public const string KWD_asType = "asTYPE";
        public const string KWD_REF_ENTITY = "REF_ENTITY";
        public const string KWD_ENUM_TYPE = "ENUMERATION_NAME";
        public const string KWD_ENUM_VALUES = "\"ENUMERATION_STRING_VALUES\"";
        public const string KWD_TYPE_NAME = "GEN_TYPE_NAME";
        public const string KWD_ACCESSOR = "_accessor";
        public const string KWD_nestedSelectAccess = "nestedSelectAccess";
        public const string KWD_GETPUT = "getOrPut";
        public const string KWD_AggregationType = "AggregationType";

        /// <summary>
        /// 
        /// </summary>
        public enum Template
        { 
            None,
            BeginFile,
            TemplateUtilityTypes,
            ClassForwardDeclaration,
            DefinedTypesBegin,
            DefinedType,
            EnumerationsBegin,
            EnumerationBegin,
            EnumerationElement,
            EnumerationEnd,
            AggrgarionTypesBegin,
            AggregationOfSimple,
            AggregationOfText,
            AggregationOfInstance,
            AggregationOfEnum,
            AggregationOfAggregation,
            AggregationOfSelect,
            SelectsBegin,
            SelectAccessorBegin,
            SelectSimpleGet,
            SelectSimplePut,
            SelectTextGet,
            SelectTextPut,
            SelectEntityGet,
            SelectEntityPut,
            SelectEnumerationGet,
            SelectEnumerationPut,
            SelectAggregationGet,
            SelectAggregationPut,
            SelectAggregationPutArraySimple,
            SelectAggregationPutArrayText,
            SelectNested,
            SelectGetAsDouble,
            SelectGetAsInt,
            SelectGetAsBool,
            SelectGetAsText,
            SelectGetAsEntity,
            SelectAccessorEnd,
            EntitiesBegin,
            EntityBegin,
            EntityCreateMethod,
            AttributeSimpleGet,
            AttributeSimplePut,
            AttributeTextGet,
            AttributeTextPut,
            AttributeEntityGet,
            AttributeEntityPut,
            AttributeEnumGet,
            AttributeEnumPut,
            AttributeSelectAccessor,
            AttributeAggregationGet,
            AttributeAggregationPut,
            AttributeAggregationPutArraySimple,
            AttributeAggregationPutArrayText,
            EntityEnd,
            SelectEntityGetImplementation,
            SelectEntityPutImplementation,
            AttributeEntityGetImplementation,
            AttributeEntityPutImplementation,
            EndFile
        }

        //
        public StringBuilder m_implementations = new StringBuilder ();

        /// <summary>
        /// 
        /// </summary>
        public bool m_cs; //cs or cpp
        string m_namespace;

        public ExpressSchema m_schema;

        public StreamWriter m_writer;

        public Dictionary<ExpressHandle, ExpressDefinedType.Foundation> m_writtenDefinedTyes = new Dictionary<ExpressHandle, ExpressDefinedType.Foundation>();

        public HashSet<string> m_knownAggregationTypes = new HashSet<string>();

        Dictionary<Template, string> m_template = new Dictionary<Template, string>();

        public Dictionary<string, string> m_replacements = new Dictionary<string, string>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="cs"></param>
        public Generator (ExpressSchema schema, bool cs, string namespace_)
        {
            m_cs = cs;            
            m_namespace = namespace_;

            m_schema = schema;

            foreach (var template in Enum.GetValues(Template.BeginFile.GetType ()))
            {
                m_template.Add((Template)template, "");
            }

            ParseTemplate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="outputFile"></param>
        public void WriteWrapper(string outputFile)
        {
            using (m_writer = new StreamWriter(outputFile))
            {
                m_replacements[KWD_NAMESPACE] = m_namespace;

                WriteByTemplate(Template.BeginFile);

                WriteForwardDeclarations();

                WriteEnumerations();

                WriteDefinedTypes();

                WriteSelects();

                Aggregation.WriteAttributesTypes(this);

                WriteEntities();

                m_writer.Write (m_implementations);

                WriteByTemplate(Template.EndFile);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        private void WriteForwardDeclarations()
        {
            foreach (var cls in m_schema.m_declarations[RDF.enum_express_declaration.__ENTITY])
            {
                m_replacements[KWD_ENTITY_NAME] = ValidateIdentifier (cls.Key);
                WriteByTemplate(Template.ClassForwardDeclaration);
            }

            m_writer.WriteLine();

            foreach (var cls in m_schema.m_declarations[RDF.enum_express_declaration.__SELECT])
            {
                string name = ValidateIdentifier(cls.Key);

                m_replacements[KWD_ENTITY_NAME] = name;
                WriteByTemplate(Template.ClassForwardDeclaration);

                m_replacements[KWD_ENTITY_NAME] = name + "_get";
                WriteByTemplate(Template.ClassForwardDeclaration);

                m_replacements[KWD_ENTITY_NAME] = name + "_put";
                WriteByTemplate(Template.ClassForwardDeclaration);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        private void WriteDefinedTypes()
        {
            WriteByTemplate(Template.DefinedTypesBegin);

            var visitedTypes = new HashSet<ExpressHandle>();

            foreach (var decl in m_schema.m_declarations[RDF.enum_express_declaration.__DEFINED_TYPE])
            {
                var type = new ExpressDefinedType(decl.Value);
                type.WriteType(this, visitedTypes);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        private void WriteEnumerations()
        {
            WriteByTemplate(Template.EnumerationsBegin);

            foreach (var decl in m_schema.m_declarations[RDF.enum_express_declaration.__ENUM])
            {
                var enumeration = new ExpressEnumeraion(decl.Value);
                WriteEnumeration(enumeration);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="enumeraion"></param>
        private void WriteEnumeration(ExpressEnumeraion enumeraion)
        {
            m_replacements[KWD_ENUMERATION_NAME] = enumeraion.name;

            WriteByTemplate(Template.EnumerationBegin);

            int i = 0;
            var values = new StringBuilder();
            foreach (var _e in enumeraion.GetValues())
            {               
                string e = ValidateIdentifier (_e);

                m_replacements[KWD_ENUMERATION_ELEMENT] = e;
                m_replacements[KWD_NUMBER] = i.ToString();

                WriteByTemplate(Template.EnumerationElement);

                if (i>0)
                    values.Append(", ");
                values.Append("\"");
                values.Append(e);
                values.Append("\"");

                i++;
            }

            m_replacements[KWD_ENUM_VALUES] = values.ToString ();

            WriteByTemplate(Template.EnumerationEnd);
        }

        /// <summary>
        /// 
        /// </summary>
        private void WriteSelects ()
        {
            var visitedSelects = new HashSet<ExpressHandle>();

            foreach (var decl in m_schema.m_declarations[RDF.enum_express_declaration.__SELECT])
            {
                var sel = new ExpressSelect(decl.Value);
                sel.WriteAccessors(this, visitedSelects);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        private void WriteEntities ()
        {
            WriteByTemplate(Template.EntitiesBegin);

            HashSet<ExpressHandle> wroteEntities = new HashSet<ExpressHandle>();

            foreach (var decl in m_schema.m_declarations[RDF.enum_express_declaration.__ENTITY])
            {
                var entity = new ExpressEntity(decl.Value);
                WriteEntity (entity, wroteEntities);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="entity"></param>
        private void WriteEntity(ExpressEntity entity, HashSet<ExpressHandle> wroteEntities)
        {
            if (!wroteEntities.Add(entity.inst))
            {
                return;
            }

            var superTypes = entity.GetSupertypes();

            //
            // Gather base classes and their properties

            if (m_cs) //C# allows one base class only
            {
                if (superTypes.Count > 1)
                {
                    var parentId = superTypes.FirstOrDefault();
                    superTypes.Clear();
                    if (parentId != 0)
                    {
                        superTypes.Add(parentId);
                    }
                }
            }


            string baseClass = "";
            var parentAttributes = new HashSet<string>();

            foreach (var parentId in superTypes)
            {
                var parent = new ExpressEntity(parentId);

                WriteEntity(parent, wroteEntities);

                if (baseClass.Length != 0)
                {
                    baseClass += ", public virtual ";
                }
                baseClass += ValidateIdentifier(parent.name);

                GetAttributeNames(parentAttributes, parent);
            }

            if (baseClass.Length == 0)
            {
                baseClass = "Entity";
            }

            //
            // Write this entity

            string clsName = ValidateIdentifier(entity.name);
            m_replacements[KWD_BASE_CLASS] = baseClass;
            m_replacements[KWD_ENTITY_NAME] = clsName;

            //
            WriteByTemplate(Template.EntityBegin);

            if (!entity.IsAbstract())
            {
                WriteByTemplate(Template.EntityCreateMethod);
            }
                
            WriteAttributes(entity, parentAttributes);

            WriteByTemplate(Template.EntityEnd);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributes"></param>
        /// <param name="entity"></param>
        private void GetAttributeNames(HashSet<string> attributes, ExpressEntity entity)
        {
            var attribs =  entity.GetAttributes();
            foreach (var a in attribs)
            {
                attributes.Add(a.name);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="template"></param>
        public void WriteByTemplate(Template template)
        {
            string str = StringByTemplate(template);
            m_writer.Write(str);
        }

        public string StringByTemplate (Template template)
        {
            string code = m_template[template];

            foreach (var r in m_replacements)
            {
                //if (r.Key.Equals(KWD_asType))
                {
                    code = code.Replace(r.Key, r.Value, true, null); //ignore case
                }
                /*else 
                {
                    code = code.Replace(r.Key, r.Value);
                }*/
            }

            if (m_cs)
            {
                code = code.Replace("string?", "string"); //warning CS8632: The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
            }

            return code;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="entity"></param>
        /// <param name="exportedAttributes"></param>
        private void WriteAttributes(ExpressEntity entity, HashSet<string> exportedAttributes)
        {
            var attribs = entity.GetAttributes();
            foreach (var attr in attribs)
            {
                m_replacements[KWD_ATTR_NAME] =  ValidateIdentifier (attr.name);

                if (exportedAttributes.Add(attr.name))
                {
                    if (!attr.IsAggregation ())
                    {
                        WriteSingeAttribute(attr);
                    }
                    else
                    {
                        Aggregation.WriteAttribute(this, attr);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="attr"></param>
        private void WriteSingeAttribute(ExpressAttribute attr)
        {
            string expressType = null;
            string baseType = null;
            string sdaiType = null;
            if (attr.IsSimpleType(out expressType, out baseType, out sdaiType))
            {
                WriteSimpleAttribute(attr, expressType, baseType, sdaiType);
                return;
            }

            ExpressDefinedType definedType = null;
            ExpressEnumeraion enumeration = null;
            ExpressSelect select = null;
            string referncedEntity = null;

            if ((definedType = attr.IsDefinedType()) != null)
            {
                definedType.WriteAttribute(this, attr);
            }
            else if (attr.IsEntityReference(out referncedEntity))
            {
                WriteEntityReference(attr, referncedEntity);
            }
            else if ((enumeration = attr.IsEnumeration()) != null)
            {
                WriteEnumAttribute(attr, enumeration.name, enumeration.name + "_");
            }
            else if ((select = attr.IsSelect()) != null)
            {
                select.WriteAttribute(this, attr);
            }
            else if (attr.domain == 0 && attr.attrType == RDF.enum_express_attr_type.__LOGICAL)
            {
                WriteEnumAttribute(attr, "LOGICAL", "LOGICAL_VALUE_NAMES");
            }
            else
            {
                Console.WriteLine("Attribute is not supported: " + attr.ToString());
            }
        }

        private void WriteSimpleAttribute(ExpressAttribute attr, string definedType, string baseType, string sdaiType)
        {
            m_replacements[KWD_SimpleType] = (!m_cs && definedType !=null) ? definedType : baseType;
            m_replacements[KWD_TextType] = m_replacements[KWD_SimpleType]; //just different words in template
            m_replacements[KWD_sdaiTYPE] = sdaiType;

            Template tplGet = baseType == "TextData" ? Template.AttributeTextGet : Template.AttributeSimpleGet;
            Template tplPut = baseType == "TextData" ? Template.AttributeTextPut : Template.AttributeSimplePut;

            WriteGetPut(tplGet, tplPut, attr.inverse);
        }

        public void WriteGetPut(Template tplGet, Template tplPut, bool inverse)
        {
            var str = BuildGetPut(tplGet, tplPut, inverse);
            m_writer.Write(str);
        }

        private string BuildGetPut (Template tplGet, Template tplPut, bool inverse)
        {
            StringBuilder str = new StringBuilder();

            string s = StringByTemplate(tplGet);
            str.Append(s);
            
            if (!inverse)
            {
                s = StringByTemplate(tplPut);
                str.Append(s);
            }

            return str.ToString();
        }


        private void WriteEntityReference (ExpressAttribute attr, string domain)
        {
            m_replacements[KWD_REF_ENTITY] = domain;

            WriteGetPut(Template.AttributeEntityGet, Template.AttributeEntityPut, attr.inverse);

            var impl = BuildGetPut(Template.AttributeEntityGetImplementation, Template.AttributeEntityPutImplementation, attr.inverse);
            m_implementations.Append(impl);
        }


        public void WriteEnumAttribute(ExpressAttribute attr, string enumName, string enumValuesArrayName)
        {
            m_replacements[KWD_ENUM_TYPE] = enumName;
            m_replacements[KWD_ENUMERATION_VALUES_ARRAY] = enumValuesArrayName;
            WriteGetPut(Template.AttributeEnumGet, Template.AttributeEnumPut, attr.inverse);
        }

        /// <summary>
        /// 
        /// </summary>
        private void ParseTemplate()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetCallingAssembly();

            var templateName = m_cs ? "EngineEx_Template.cs" : "EngineEx_Template.h";

            string resourceName = FormatResourceName(assembly, templateName);

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                Verify(stream != null, "Failed get resource " + resourceName);
                using (StreamReader reader = new StreamReader(stream)) //to read from file use new StreamReader(templateFile))
                {
                    Verify(reader != null, "Failed create stream reader for " + templateName);
                    Template part = Template.BeginFile;
                    int nline = 0;
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        nline++;

                        if (line.Contains(KWD_PREPROC))
                        {
                            part++;
                            Verify(line.Contains(part.ToString()), string.Format("Expected line {0} contains '{1}' substing while parsing template fle {2}", nline, part.ToString(), templateName));
                        }
                        else
                        {
                            string str = m_template[part];
                            str = str + line + "\r\n";
                            m_template[part] = str;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        private static string FormatResourceName(System.Reflection.Assembly assembly, string resourceName)
        {
            return assembly.GetName().Name + "." + resourceName.Replace(" ", "_").Replace("\\", ".").Replace("/", ".");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="exceptionMsg"></param>
        public static void Verify (bool condition, string exceptionMsg)
        {
            System.Diagnostics.Trace.Assert(condition);
            if (!condition)
            {
                throw new ApplicationException(exceptionMsg);
            }
        }

        public static string ValidateIdentifier(string name)
        {
            string rename = name;

            using (var codeProvider = System.CodeDom.Compiler.CodeDomProvider.CreateProvider("C#"))
            {
                if (!codeProvider.IsValidIdentifier(name))
                {
                    //does not work as expected, var id = codeProvider.CreateValidIdentifier(name);
                    bool first = true;
                    var builder = new System.Text.StringBuilder();
                    foreach (var ch in name)
                    {
                        if (first && char.IsLetter(ch) || !first && char.IsLetterOrDigit(ch))
                        {
                            //fits
                            builder.Append(ch);
                        }
                        else
                        {
                            //replace
                            builder.Append("_");
                            builder.Append(((byte)ch).ToString());
                            builder.Append("_");
                        }

                        first = false;
                    }

                    rename = builder.ToString();

                    if (!codeProvider.IsValidIdentifier(rename))
                    {
                        rename += "_";
                        if (!codeProvider.IsValidIdentifier(rename))
                        {
                            throw new ApplicationException("Can not make a valid identifier from '" + name + "'");
                        }
                    }
                }
                else if (name == "NULL" || name == "union")
                {
                    rename = name + "_";
                }
            }

            if (rename != name)
            {
                //Console.WriteLine("!!!  '{0}' is not a valid identifier, changed to '{1}'", name, rename);
            }

            return rename;
        }
    }
}
