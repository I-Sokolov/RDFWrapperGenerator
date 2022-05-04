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
        const string KWD_PREPROC = "//##";
        const string KWD_INSTANCE_CLASS = "INSTANCE_CLASS";

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

        Dictionary<TemplatePart, string> m_templateParts = new Dictionary<TemplatePart, string>();

        public CSGenerator (string templateFile)
        {
            foreach (var part in Enum.GetValues(TemplatePart.BeginFile.GetType ()))
            {
                m_templateParts.Add((TemplatePart)part, "");
            }

            ReadTemplate(templateFile);
        }

        public void WriteWrapper(Schema schema, string outputFile)
        {
            using (var writer = new StreamWriter(outputFile))
            {
                writer.WriteLine(m_templateParts[TemplatePart.BeginFile]);

                foreach (var cls in schema.m_classes)
                {
                    WriteWrapper(writer, cls.Key, cls.Value);
                }

                writer.WriteLine(m_templateParts[TemplatePart.BeginFactoryClass]);

                foreach (var cls in schema.m_classes)
                {
                    WriteFactory(writer, cls.Key, cls.Value);
                }

                writer.WriteLine(m_templateParts[TemplatePart.EndFile]);
            }
        }

        private void WriteWrapper (StreamWriter writer, string clsName, Schema.Class cls)
        {
            string text = m_templateParts[TemplatePart.BeginWrapperClass];
            text = text.Replace(KWD_INSTANCE_CLASS, clsName);
            writer.WriteLine(text);

            //TODO - base classes

            foreach (var prop in cls.properties)
            {
                //TODO - properties
            }

            writer.WriteLine(m_templateParts[TemplatePart.EndWrapperClass]);
        }

        private void WriteFactory(StreamWriter writer, string clsName, Schema.Class cls)
        {
            var text = m_templateParts[TemplatePart.FactoryMethod];
            text = text.Replace(KWD_INSTANCE_CLASS, clsName);
            writer.WriteLine(text);
        }

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
                        if (!line.Contains(part.ToString()))
                        {
                            var msg = string.Format("Expected line {0} contains '{1}' substing while parsing template fle {2}",
                                nline, part.ToString(), templateFile);
                            throw new ApplicationException(msg);
                        }
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
    }
}
