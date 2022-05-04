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
        const string PREPROC = "//##"; 

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

        public void Run(Schema schema)
        {

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

                    if (line.StartsWith(PREPROC))
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
