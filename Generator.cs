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
        const string KWD_DATA_TYPE = "double";
        const string KWD_ENUMERATION_NAME = "ENUMERATION_NAME";
        const string KWD_ENUMERATION_ELEMENT = "ENUMERATION_ELEMENT";
        const string KWD_NUMBER = "1234";

        const string KWD_PROPERTIES_OF = "PROPERTIES_OF_CLASS";
        const string KWD_PROPERTY_NAME = "PROPERTY_NAME";
        const string KWD_CARDINALITY_MIN = "CARDINALITY_MIN";
        const string KWD_CARDINALITY_MAX = "CARDINALITY_MAX";
        const string KWD_OBJECT_TYPE = "Instance";
        const string KWD_asType = "asTYPE";

        /// <summary>
        /// 
        /// </summary>
        enum Template
        { 
            None,
            BeginFile,
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
            StartPropertiesBlock,
            SetDataProperty,
            SetDataArrayProperty,
            GetDataProperty,
            GetDataArrayProperty,
            SetObjectProperty,
            SetObjectArrayProperty,
            GetObjectProperty,
            GetObjectArrayProperty,
            GetObjectArrayPropertyInt64,
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

                m_replacements[KWD_DATA_TYPE] = referencedType.name;
            }
            else
            {
                var csType = ExpressSchema.GetCSType(definedType.type);
                if (csType==null)
                {
                    Console.WriteLine("Defined type {0} is not supproted, because primitive type is {1}", definedType.name, definedType.type.ToString());
                    return false;
                }

                m_replacements[KWD_DATA_TYPE] = csType;
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
                var parentId = superTypes.FirstOrDefault();
                superTypes.Clear();
                if (parentId != 0)
                {
                    superTypes.Add(parentId);
                }
            }


            string baseClass = "";
            var parentProperties = new HashSet<string>();

            foreach (var parentId in superTypes)
            {
                var parent = new ExpressEntity(parentId);

                WriteEntity(writer, parent);

                if (baseClass.Length != 0)
                {
                    baseClass += ", ";
                }
                baseClass += ValidateIdentifier(parent.name);

                AddPropertiesNames(parentProperties, parent);
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
                
            WriteProperties(writer, entity, parentProperties);

            WriteByTemplate(writer, Template.EndEntity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="entity"></param>
        private void AddPropertiesNames(HashSet<string> properties, ExpressEntity entity)
        {
#if NOT_NOW
            string parentName = m_schema.GetNameOfClass(parentId);
            var parentClass = m_schema.m_classes[parentName];

            foreach (var cp in parentClass.properties)
            {
                m_addedProperties.Add(cp.Name());
            }

            foreach (var nextParent in parentClass.parents)
            {
                CollectParentProperties(nextParent);
            }
#endif
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
                if (r.Key.Equals(KWD_asType))
                {
                    code = code.Replace(r.Key, r.Value, true, null); //ignore case
                }
                else 
                {
                    code = code.Replace(r.Key, r.Value);
                }
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

        private void WriteProperties (StreamWriter writer, ExpressEntity entity, HashSet<string> exportedProperties)
        {
#if NOT_NOW
            m_replacements[KWD_PROPERTIES_OF] = properiesOfClass;

            bool first = true;
            foreach (var prop in properties)
            {
                if (m_addedProperties.Add (prop.Name()))
                {
                    if (first)
                    {
                        WriteByTemplate(writer, Template.StartPropertiesBlock);
                        first = false;
                    }

                    WritePropertyMethods(writer, prop);
                }
            }
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="classProp"></param>
        private void WritePropertyMethods (StreamWriter writer, Schema.ClassProperty classProp)
        {
            if (classProp.CSDataType() == null)
            {
                return;
            }

            m_replacements[KWD_PROPERTY_NAME] = classProp.Name();
            m_replacements[KWD_DATA_TYPE] = classProp.CSDataType();
            m_replacements[KWD_CARDINALITY_MIN] = classProp.CardinalityMin().ToString();
            m_replacements[KWD_CARDINALITY_MAX] = classProp.CardinalityMax().ToString();
            m_replacements[KWD_asType] = "";

            if (!classProp.IsObject())
            {
                if (classProp.CardinalityMax() == 1)
                {
                    WriteByTemplate(writer, Template.SetDataProperty);
                    WriteByTemplate(writer, Template.GetDataProperty);
                }
                else
                {
                    WriteByTemplate(writer, Template.SetDataArrayProperty);
                    WriteByTemplate(writer, Template.GetDataArrayProperty);
                }

            }
            else
            {
                if (classProp.CardinalityMax() == 1)
                {
                    WriteSetObjectProperty(writer, classProp, Template.SetObjectProperty);
                    //do we need this? we lose restrictions control! WriteAccessObjectProperty(writer, classProp.name, TInt64, "", Template.SetObjectProperty);
                    WriteGetObjectProperty(writer, classProp, Template.GetObjectProperty);
                }
                else
                {
                    WriteSetObjectProperty(writer, classProp, Template.SetObjectArrayProperty);
                    WriteAccessObjectProperty(writer, m_TInt64, "", Template.SetObjectArrayProperty);

                    WriteGetObjectProperty(writer, classProp, Template.GetObjectArrayProperty);
                    WriteAccessObjectProperty(writer, m_TInt64, "", Template.GetObjectArrayPropertyInt64);
                }

            }
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
            m_replacements[KWD_OBJECT_TYPE] = objectType;
            m_replacements[KWD_asType] = asType;
            WriteByTemplate(writer, template);
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

                    Console.Write("!!!  {0} is not a valid identifier, changed to {1}", name, id);
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
