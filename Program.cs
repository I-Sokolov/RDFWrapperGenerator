using System;
using CommandLine;


namespace RDFWrappers
{
    class Program
    {
        public class Options
        {
            [CommandLine.Option("model", HelpText = "Pathname of the model containing the ontology to generate wrapper classes for. If no model is given, it will generate classes for GeometryKernel (GEOM).")]
            public string modelFile { get; set; }

            [CommandLine.Option("cs", HelpText = "Pathnamte of c# file to be generated. Default is model name.")]
            public string csFile { get; set; }

            [CommandLine.Option('h', HelpText = "Pathname of c++ header file to be generated. Default is model name.")]
            public string hFile { get; set; }

            [CommandLine.Option("namespace", HelpText = "Namespase. Default is model name.")]
            public string Namespace { get; set; }

            [CommandLine.Option("printClasses", Default = false, HelpText = "Print model classes with properties.")]
            public bool printClasses { get; set; }
        }

        static int Main(string[] args)
        {
            try
            {
                var sdai = new SdaiSchema("ap242ed2_mim_lf_v1.101.exp");
                sdai.ToConsole();
                Console.WriteLine("Generate C# file IFC4.cs");
                var gen = new Generator(sdai, true, "IFC4");
                gen.WriteWrapper("IFC4.cs");
                Console.WriteLine("Generate C++ file IFC4.h");
                gen = new Generator(sdai, false, "IFC4");
                gen.WriteWrapper("IFC4.h");

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
                // Update and validate options
                //
                if (string.IsNullOrWhiteSpace (options.modelFile))
                {
                    options.modelFile = null;
                }
                else
                {
                    Generator.Verify(System.IO.File.Exists(options.modelFile), "File does not exist: " + options.modelFile);
                }

                string baseNameSmall = (options.modelFile == null ? "geom" : System.IO.Path.GetFileNameWithoutExtension (options.modelFile));
                string baseNameCapital = (options.modelFile == null ? "GEOM" : System.IO.Path.GetFileNameWithoutExtension(options.modelFile));

                if (string.IsNullOrWhiteSpace (options.csFile))
                {
                    options.csFile = baseNameSmall + ".cs";
                }
                if (string.IsNullOrWhiteSpace (options.hFile))
                {
                    options.hFile = baseNameSmall + ".h";
                }
                if (string.IsNullOrWhiteSpace (options.Namespace))
                {
                    options.Namespace = baseNameCapital;
                }
                options.Namespace = Generator.ValidateIdentifier(options.Namespace);

                //
                // Main course
                //
                Console.WriteLine("Generating classes for " + baseNameCapital);
                var model = RDF.engine.OpenModel(options.modelFile);
                Console.WriteLine();

                var schema = new RdfSchema(model);
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

                RDF.engine.CloseModel(model);
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
