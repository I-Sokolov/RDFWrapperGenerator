using System;
using CommandLine;

namespace RDFWrappers
{
    class Program
    {
        public class Options
        {
            [CommandLine.Option("model", Default = "", HelpText = "Pathname of the model containing the ontology to generate wrapper classes for. If no model is given, it will generate wrapper classes for geometry kernel.")]
            public string modelFile { get; set; }

            [CommandLine.Option("cs", Default="EngineEx.cs", HelpText = "Pathnamte of c# file to be generated.")]
            public string csFile { get; set; }

            [CommandLine.Option('h', Default = "EngineEx.h", HelpText = "Pathname of c++ header file to be generated.")]
            public string hFile { get; set; }

            [CommandLine.Option("namespace", Default = "engine", HelpText = "Namespcase, default is model name or Engine if no model file was specified.")]
            public string Namespace { get; set; }

            [CommandLine.Option("printClasses", Default = false, HelpText = "Print model classes with properties.")]
            public bool printClasses { get; set; }
        }

        static int Main(string[] args)
        {
            try
            {
                Options options = null;
                Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(o => { options = o; });
                if (options == null)
                {
                    return -1;//--help option
                }

                //
                Console.WriteLine("This application generates C# and C++ header files with helper classes to work with RDF models.");
                Console.WriteLine("Use --help to ptint more information and visit http://www.rdf.bg");
                Console.WriteLine("");

                //
                //
                if (string.IsNullOrWhiteSpace (options.modelFile))
                {
                    options.modelFile = null;
                }

                Console.WriteLine("Generating classes for " + (options.modelFile == null ? "Geometry Kernel" : options.modelFile));
                var model = Engine.x86_64.OpenModel(options.modelFile);
                Console.WriteLine();

                var schema = new Schema(model);
                if (options.printClasses)
                {
                    schema.ToConsole();
                    Console.WriteLine();
                }

                //
                //
                if (!string.IsNullOrWhiteSpace(options.csFile))
                {
                    Console.WriteLine("Generate C# file " + options.csFile);
                    Generator csgen = new Generator(schema, true, options.Namespace);
                    csgen.WriteWrapper(options.csFile);
                }
                else
                {
                    Console.WriteLine("Do not create C# file");
                }
                System.Console.WriteLine();

                if (!string.IsNullOrWhiteSpace(options.hFile))
                {
                    Console.WriteLine("Generate C++ header file " + options.hFile);
                    Generator cppgen = new Generator(schema, false, options.Namespace);
                    cppgen.WriteWrapper(options.hFile);
                }
                else
                {
                    Console.WriteLine("Do not create C++ header file");
                }
                System.Console.WriteLine();

                Engine.x86_64.CloseModel(model);
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine("ERROR:");
                Console.WriteLine(e.ToString());
                return 13;
            }
        }
    }
}
