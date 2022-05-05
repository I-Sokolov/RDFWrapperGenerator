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
        const string KWD_INSTANCE_CLASS = "INSTANCE_CLASS";
        const string KWD_BASE_CLASS = "/*BASE CLASS*/Instance";

        /// <summary>
        /// 
        /// </summary>
        enum TemplatePart
        { 
            BeginFile,
            BeginWrapperClass,
            SetDataProperty,
            SetObjectProperty,
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
                writer.WriteLine(m_templateParts[TemplatePart.BeginFile]);

                //
                foreach (var cls in m_schema.m_classes)
                {
                    WriteClassWrapper(writer, cls.Key, cls.Value);
                }

                //
                writer.WriteLine(m_templateParts[TemplatePart.BeginFactoryClass]);

                //
                foreach (var cls in m_schema.m_classes)
                {
                    WriteClassFactory(writer, cls.Key, cls.Value);
                }

                //
                writer.WriteLine(m_templateParts[TemplatePart.EndFile]);
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
            AssertIdentifier(clsName);

            //
            //
            string textBeginWrapper = m_templateParts[TemplatePart.BeginWrapperClass];
            textBeginWrapper = textBeginWrapper.Replace(KWD_INSTANCE_CLASS, clsName);

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

            writer.WriteLine(textBeginWrapper);

            //
            // only single inheritance is suppirted, dirrectly take properties from othe parents
            while (itParent.MoveNext ())
            {
                AddParentProperties(itParent.Current);
            }

            //
            //
            AddProperties(clsName, cls.properties);

            //
            //
            writer.WriteLine(m_templateParts[TemplatePart.EndWrapperClass]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentClassId"></param>
        private void AddParentProperties (Int64 parentClassId)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="properiesOfClass"></param>
        /// <param name="properties"></param>
        private void AddProperties (string properiesOfClass, List<Schema.ClassProperty> properties)
        {

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
            text = text.Replace(KWD_INSTANCE_CLASS, clsName);
            writer.WriteLine(text);
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

                    if (line.StartsWith(KWD_PREPROC))
                    {
                        part++;
                        Verify(line.Contains(part.ToString()), string.Format("Expected line {0} contains '{1}' substing while parsing template fle {2}", nline, part.ToString(), templateFile));
                    }
                    else
                    {
                        string str = m_templateParts[part];
                        str = str + "\r\n" + line;
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
