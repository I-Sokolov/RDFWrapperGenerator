using System;
using Engine;

namespace RDFWrappers
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                //
                //
                string fileName = null;
                if (args.Length > 0)
                {
                    fileName = args[0];
                    Console.WriteLine("Generate wrapper classes to work with RDF model {0}", fileName);
                }
                else
                {
                    Console.WriteLine("Generate wrapper classes to work with RDF Geometry Kernel model");
                    Console.WriteLine("(specify file name in command line if you want genrate for custom model)");
                }

                //
                //
                var model = x86_64.OpenModel(fileName);

                var schema = new Schema(model);

                x86_64.CloseModel(model);
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return 13;
            }
        }
    }
}
