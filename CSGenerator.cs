using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDFWrappers
{
    class CSGenerator
    {
        /// <summary>
        /// 
        /// </summary>
        const string KWD_PREPROC = "//##";
        const string KWD_BASE_CLASS = "/*BASE CLASS*/Instance";
        const string KWD_CLASS_NAME = "CLASS_NAME";
        const string KWD_PROPERTY_NAME = "PROPERTY_NAME";
        const string KWD_DATA_TYPE = "double";

        /// <summary>
        /// 
        /// </summary>
        enum TemplatePart
        { 
            BeginFile,
            BeginWrapperClass,
            StartPropertiesBlock,
            SetDataProperty,
            SetDataArrayProperty,
            GetDataProperty,
            SetObjectProperty,
            SetObjectArrayProperty,
            GetObjectProperty,
            EndWrapperClass,
            BeginFactoryClass,
            FactoryMethod,
            EndFile
        }

        /// <summary>
        /// 
        /// </summary>
        Schema m_schema;

        Dictionary<TemplatePart, string> m_templateParts = new Dictionary<TemplatePart, string>();

        HashSet<string> m_addedProperties;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="templateFile"></param>
        public CSGenerator (Schema schema, string templateFile)
        {
            m_schema = schema;

            foreach (var part in Enum.GetValues(TemplatePart.BeginFile.GetType ()))
            {
                m_templateParts.Add((TemplatePart)part, "");
            }

            ReadTemplate(templateFile);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="outputFile"></param>
        public void WriteWrapper(string outputFile)
        {
            using (var writer = new StreamWriter(outputFile))
            {
                writer.Write(m_templateParts[TemplatePart.BeginFile]);

                //
                foreach (var cls in m_schema.m_classes)
                {
                    WriteClassWrapper(writer, cls.Key, cls.Value);
                }

                //
                writer.Write(m_templateParts[TemplatePart.BeginFactoryClass]);

                //
                foreach (var cls in m_schema.m_classes)
                {
                    WriteClassFactory(writer, cls.Key, cls.Value);
                }

                //
                writer.Write(m_templateParts[TemplatePart.EndFile]);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="clsName"></param>
        /// <param name="cls"></param>
        private void WriteClassWrapper (StreamWriter writer, string clsName, Schema.Class cls)
        {
            m_addedProperties = new HashSet<string>();

            AssertIdentifier(clsName);

            //
            //
            string textBeginWrapper = m_templateParts[TemplatePart.BeginWrapperClass];
            textBeginWrapper = textBeginWrapper.Replace(KWD_CLASS_NAME, clsName);

            //first parent is the base class
            var parentName = "Instance";
            var itParent = cls.parents.GetEnumerator();
            if (itParent.MoveNext())
            {
                var parentId = itParent.Current;
                parentName = m_schema.GetNameOfClass(parentId);
                AssertIdentifier(parentName);
            }
            textBeginWrapper = textBeginWrapper.Replace(KWD_BASE_CLASS, parentName);

            writer.Write(textBeginWrapper);

            //
            //
            AddProperties(writer, clsName, cls.properties);

            //
            // only single inheritance is suppirted, dirrectly take properties from othe parents
            while (itParent.MoveNext())
            {
                AddParentProperties(writer, itParent.Current);
            }

            //
            //
            writer.Write(m_templateParts[TemplatePart.EndWrapperClass]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentClassId"></param>
        private void AddParentProperties (StreamWriter writer, Int64 parentClassId)
        {
            string parentName = m_schema.GetNameOfClass(parentClassId);
            var parentClass = m_schema.m_classes[parentName];

            AddProperties(writer, parentName, parentClass.properties);
            
            foreach (var nextParent in parentClass.parents)
            {
                AddParentProperties(writer, nextParent);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="properiesOfClass"></param>
        /// <param name="properties"></param>
        private void AddProperties (StreamWriter writer, string properiesOfClass, List<Schema.ClassProperty> properties)
        {
            bool first = true;
            foreach (var prop in properties)
            {
                if (m_addedProperties.Add (prop.name))
                {
                    if (first)
                    {
                        var text = m_templateParts[TemplatePart.StartPropertiesBlock];
                        text = text.Replace(KWD_CLASS_NAME, properiesOfClass);
                        writer.Write(text);
                        first = false;
                    }

                    AddProperty(writer, prop);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="classProp"></param>
        private void AddProperty (StreamWriter writer, Schema.ClassProperty classProp)
        {
            var prop = m_schema.m_properties[classProp.name];

            if (classProp.max < 2)
            {
                if (prop.IsObject())
                {
                    AddSingleObjectProperty(writer, classProp.name, prop);
                }
                else
                {
                    AddSingleDataProperty(writer, classProp.name, prop);
                }
            }
            else
            {
                writer.WriteLine("//TODO array " + classProp.name);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="prop"></param>
        private void AddSingleObjectProperty(StreamWriter writer, string name, Schema.Property prop)
        {
            var text = m_templateParts[TemplatePart.SetObjectProperty];
            text = text.Replace(KWD_PROPERTY_NAME, name);
            writer.Write(text);
        }

        private void AddSingleDataProperty(StreamWriter writer, string name, Schema.Property prop)
        {
            var text = m_templateParts[TemplatePart.SetDataProperty];
            text = text.Replace(KWD_PROPERTY_NAME, name);
            text = text.Replace(KWD_DATA_TYPE, prop.DataType());
            writer.Write(text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="clsName"></param>
        /// <param name="cls"></param>
        private void WriteClassFactory(StreamWriter writer, string clsName, Schema.Class cls)
        {
            var text = m_templateParts[TemplatePart.FactoryMethod];
            text = text.Replace(KWD_CLASS_NAME, clsName);
            writer.Write(text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="templateFile"></param>
        private void ReadTemplate(string templateFile)
        {
            TemplatePart part = TemplatePart.BeginFile;

            using (var reader = new StreamReader(templateFile))
            {
                int nline = 0;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    nline++;

                    if (line.Contains(KWD_PREPROC))
                    {
                        part++;
                        Verify(line.Contains(part.ToString()), string.Format("Expected line {0} contains '{1}' substing while parsing template fle {2}", nline, part.ToString(), templateFile));
                    }
                    else
                    {
                        string str = m_templateParts[part];
                        str = str + line + "\r\n";
                        m_templateParts[part] = str;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="exceptionMsg"></param>
        private void Verify (bool condition, string exceptionMsg)
        {
            System.Diagnostics.Trace.Assert(condition);
            if (!condition)
            {
                throw new ApplicationException(exceptionMsg);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        private void AssertIdentifier (string name)
        {
            using (var codeProvider = System.CodeDom.Compiler.CodeDomProvider.CreateProvider("C#"))
            {
                Verify(codeProvider.IsValidIdentifier(name), name + " is invalid identifier");
            }
        }
    }
}
