using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExpressHandle = System.Int64;

namespace RDFWrappers
{
    class Generator
    {
        /// <summary>
        /// 
        /// </summary>
        const string KWD_PREPROC = "//##";

        const string KWD_NAMESPACE = "NAMESPACE_NAME";
        const string KWD_ENTITY_NAME = "ENTITY_NAME";
        const string KWD_BASE_CLASS = "/*PARENT_NAME*/Entity";
        const string KWD_DEFINED_TYPE = "DEFINED_TYPE_NAME";
        const string KWD_CS_DATATYPE = "double";
        const string KWD_StringType = "StringType";
        const string KWD_ENUMERATION_NAME = "ENUMERATION_NAME";
        const string KWD_ENUMERATION_ELEMENT = "ENUMERATION_ELEMENT";
        const string KWD_NUMBER = "1234";
        const string KWD_ATTR_NAME = "ATTR_NAME";
        const string KWD_sdai_DATATYPE = "sdaiREAL";
        const string KWD_asType = "asTYPE";

        /// <summary>
        /// 
        /// </summary>
        enum Template
        { 
            None,
            BeginFile,
            TemplateUtilityTypes,
            ClassForwardDeclaration,
            BeginDefinedTypes,
            DefinedType,
            BeginEnumerations,
            BeginEnumeration,
            EnumerationElement,
            EndEnumeration,
            BeginEntities,
            BeginEntity,
            EntityCreateMethod,
            GetSimpleAttribute,
            SetSimpleAttribute,
            GetSimpleAttributeString,
            SetSimpleAttributeString,
            EndEntity,
            EndFile
        }

        /// <summary>
        /// 
        /// </summary>
        bool m_cs; //cs or cpp
        string m_TInt64;
        string m_namespace;

        ExpressSchema m_schema;

        Dictionary<Template, string> m_template = new Dictionary<Template, string>();

        HashSet<ExpressHandle> m_wroteDefinedTyes = new HashSet<ExpressHandle>();

        HashSet<ExpressHandle> m_wroteEntities = new HashSet<ExpressHandle>();

        Dictionary<string, string> m_replacements = new Dictionary<string, string>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="cs"></param>
        public Generator (ExpressSchema schema, bool cs, string namespace_)
        {
            m_cs = cs;            
            m_TInt64 = m_cs ? "Int64" : "int64_t";
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
            using (var writer = new StreamWriter(outputFile))
            {
                m_replacements[KWD_NAMESPACE] = m_namespace;

                WriteByTemplate(writer, Template.BeginFile);

                WriteForwardDeclarations(writer);

                WriteDefinedTypes(writer);

                WriteEnumerations(writer);

                WriteEntities(writer);

                WriteByTemplate(writer, Template.EndFile);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        private void WriteForwardDeclarations(StreamWriter writer)
        {
            foreach (var cls in m_schema.m_declarations[RDF.enum_express_declaration.__ENTITY])
            {
                m_replacements[KWD_ENTITY_NAME] = cls.Key;
                WriteByTemplate(writer, Template.ClassForwardDeclaration);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        private void WriteDefinedTypes(StreamWriter writer)
        {
            WriteByTemplate(writer, Template.BeginDefinedTypes);

            foreach (var decl in m_schema.m_declarations[RDF.enum_express_declaration.__DEFINED_TYPE])
            {
                var type = new ExpressDefinedType(decl.Value);
                WriteDefinedType(writer, type);
            }
        }

        private bool WriteDefinedType(StreamWriter writer, ExpressDefinedType definedType)
        {
            if (!m_wroteDefinedTyes.Add (definedType.declaration))
            {
                return true;
            }

            if (definedType.referenced != 0)
            {
                var referencedType = new ExpressDefinedType(definedType.referenced);
                if (!WriteDefinedType(writer, referencedType))
                {
                    Console.WriteLine("Defineded type {0} is not supported, because referenced type {1} is not supported", definedType.name, referencedType.name);
                    return false;
                }

                m_replacements[KWD_CS_DATATYPE] = referencedType.name;
            }
            else
            {
                var csType = ExpressSchema.GetCSType(definedType.type);
                if (csType==null)
                {
                    Console.WriteLine("Defined type {0} is not supproted, because primitive type is {1}", definedType.name, definedType.type.ToString());
                    return false;
                }

                m_replacements[KWD_CS_DATATYPE] = csType;
            }

            m_replacements[KWD_DEFINED_TYPE] = definedType.name;

            WriteByTemplate(writer, Template.DefinedType);

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        private void WriteEnumerations(StreamWriter writer)
        {
            WriteByTemplate(writer, Template.BeginEnumerations);

            foreach (var decl in m_schema.m_declarations[RDF.enum_express_declaration.__ENUM])
            {
                var enumeration = new ExpressEnumeraion(decl.Key, decl.Value);
                WriteEnumeration(writer, enumeration);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="enumeraion"></param>
        private void WriteEnumeration(StreamWriter writer, ExpressEnumeraion enumeraion)
        {
            m_replacements[KWD_ENUMERATION_NAME] = enumeraion.name;

            WriteByTemplate(writer, Template.BeginEnumeration);

            int i = 0;
            foreach (var e in enumeraion.GetValues())
            {
                m_replacements[KWD_ENUMERATION_ELEMENT] = e;
                m_replacements[KWD_NUMBER] = i.ToString();

                WriteByTemplate(writer, Template.EnumerationElement);

                i++;
            }

            WriteByTemplate(writer, Template.EndEnumeration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        private void WriteEntities (StreamWriter writer)
        {
            WriteByTemplate(writer, Template.BeginEntities);

            foreach (var decl in m_schema.m_declarations[RDF.enum_express_declaration.__ENTITY])
            {
                var entity = new ExpressEntity(decl.Value);
                WriteEntity (writer, entity);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="entity"></param>
        private void WriteEntity(StreamWriter writer, ExpressEntity entity)
        {
            if (!m_wroteEntities.Add(entity.inst))
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

                WriteEntity(writer, parent);

                if (baseClass.Length != 0)
                {
                    baseClass += ", ";
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
            WriteByTemplate(writer, Template.BeginEntity);

            if (!entity.IsAbstract())
            {
                WriteByTemplate(writer, Template.EntityCreateMethod);
            }
                
            WriteAttributes(writer, entity, parentAttributes);

            WriteByTemplate(writer, Template.EndEntity);
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
        private void WriteByTemplate(StreamWriter writer, Template template)
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
            else
            {
                code = code.Replace("string", "const char* const");
                code = code.Replace("const const", "const");
                code = code.Replace("Int64", "int64_t");
            }

            writer.Write(code);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="entity"></param>
        /// <param name="exportedAttributes"></param>
        private void WriteAttributes(StreamWriter writer, ExpressEntity entity, HashSet<string> exportedAttributes)
        {
            var attribs = entity.GetAttributes();
            foreach (var attr in attribs)
            {
                if (exportedAttributes.Add(attr.name))
                {
                    switch (attr.aggrType)
                    {
                        case RDF.enum_express_aggr.__NONE:
                            WriteSingeAttribute(writer, attr);
                            break;

                        case RDF.enum_express_aggr.__ARRAY:
                        case RDF.enum_express_aggr.__LIST:
                            WriteListAggregation(writer, attr);
                            break;

                        case RDF.enum_express_aggr.__SET:
                            WriteSetAggregation(writer, attr);
                            break;

                        case RDF.enum_express_aggr.__BAG:
                            Console.WriteLine("Unsupported aggregation type: " + attr.aggrType.ToString());
                            break;

                        default:
                            System.Diagnostics.Debug.Assert(false);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="attr"></param>
        private void WriteSingeAttribute(StreamWriter writer, ExpressAttribute attr)
        {
            string definedType;
            string baseType;
            string sdaiType;
            if (attr.AsSimpleType (out definedType, out baseType, out sdaiType))
            {
                WriteSimpleAttribute(writer, attr, definedType, baseType, sdaiType);
            }
/*
            switch (attr.attrType)
            {
                case RDF.enum_express_attr_type.__NONE:
                    //TODO
                    break;

                case RDF.enum_express_attr_type.__BINARY:
                case RDF.enum_express_attr_type.__BINARY_32:
                    Console.WriteLine("Unsupported attribute type: " + attr.aggrType.ToString());
                    break;

                case RDF.enum_express_attr_type.__BOOLEAN:
                case RDF.enum_express_attr_type.__INTEGER:
                case RDF.enum_express_attr_type.__LOGICAL:
                case RDF.enum_express_attr_type.__NUMBER:
                case RDF.enum_express_attr_type.__REAL:
                case RDF.enum_express_attr_type.__STRING:
                    WriteSimpleAttribute(writer, attr);
                    break;

                case RDF.enum_express_attr_type.__SELECT:
                    //TODO
                    break;

                case RDF.enum_express_attr_type.__ENUMERATION:
                    //TODO
                    break;

                default:
                    System.Diagnostics.Debug.Assert(false);
                    break;
            }*/
        }


        private void WriteSimpleAttribute(StreamWriter writer, ExpressAttribute attr, string definedType, string baseType, string sdaiType)
        {
            m_replacements[KWD_ATTR_NAME] = attr.name;
            m_replacements[KWD_CS_DATATYPE] = baseType;
            m_replacements[KWD_StringType] = (baseType == "string" && definedType != null) ? definedType : "const char*";
            m_replacements[KWD_sdai_DATATYPE] = sdaiType;

            WriteByTemplate(writer, baseType=="string" ? Template.GetSimpleAttributeString : Template.GetSimpleAttribute);
            
            if (!attr.inverse)
            {
                WriteByTemplate(writer, baseType == "string" ? Template.SetSimpleAttributeString : Template.SetSimpleAttribute);
            }
        }

        private void WriteListAggregation(StreamWriter writer, ExpressAttribute attr)
        { 
            //TODO
        }

        private void WriteSetAggregation(StreamWriter writer, ExpressAttribute attr)
        {
            //TODO
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="prop"></param>
        /// <param name="template"></param>
        private void WriteSetObjectProperty(StreamWriter writer, Schema.ClassProperty prop, Template template)
        {
            if (prop.Restrictions().Count > 0)
            {
                foreach (var restr in prop.Restrictions())
                {
                    string instClass = ExpressSchema.GetNameOfDeclaration(restr);
                    WriteAccessObjectProperty(writer, instClass, "", template);
                }
            }
            else
            {
                WriteAccessObjectProperty(writer, "Instance", "", template);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="prop"></param>
        /// <param name="template"></param>
        private void WriteGetObjectProperty(StreamWriter writer, Schema.ClassProperty prop, Template template)
        {
            if (prop.Restrictions().Count > 0)
            {
                bool first = true;
                foreach (var restr in prop.Restrictions())
                {
                    Verify(first, "This case was not tested yet: more then one restriction");

                    string instClass = ExpressSchema.GetNameOfDeclaration(restr);
                    string asType = first ? "" : instClass;
                    WriteAccessObjectProperty(writer, instClass, asType, template);

                    first = false;
                }
            }
            else
            {
                WriteAccessObjectProperty(writer, "Instance", "", template);
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="objectType"></param>
        /// <param name="asType"></param>
        /// <param name="template"></param>
        private void WriteAccessObjectProperty(StreamWriter writer, string objectType, string asType, Template template)
        {
/*
            m_replacements[KWD_OBJECT_TYPE] = objectType;
            m_replacements[KWD_asType] = asType;
            WriteByTemplate(writer, template);
*/
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

        public static string ValidateIdentifier (string name)
        {
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
                            builder.Append("_R");
                            builder.Append(((byte)ch).ToString());
                            builder.Append("R_");
                        }

                        first = false;
                    }

                    var id = builder.ToString();

                    Console.WriteLine("!!!  {0} is not a valid identifier, changed to {1}", name, id);
                    return id;
                }
                else
                {
                    return name;
                }
            }
        }
    }
}
