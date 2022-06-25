using System;
using CommandLine;


namespace RDFWrappers
{
    class Program
    {
        public class Options
        {
            [CommandLine.Option("schema", HelpText = "Pathname of EXPRESS file or name of embedded schema.")]
            public string schema { get; set; }

            [CommandLine.Option("cs", HelpText = "Pathnamte of c# file to be generated. Default is model name.")]
            public string csFile { get; set; }

            [CommandLine.Option('h', HelpText = "Pathname of c++ header file to be generated. Default is model name.")]
            public string hFile { get; set; }

            [CommandLine.Option("namespace", HelpText = "Namespase. Default is schema name.")]
            public string Namespace { get; set; }

            [CommandLine.Option("printSchema", Default = false, HelpText = "Print schema declarations.")]
            public bool printSchema { get; set; }
        }

        static int Main(string[] args)
        {
            try
            {
                /*
                var sdai = new ExpressSchema("IFC2x3");
                sdai.ToConsole();
                */

                Options options = null;
                Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(o => { options = o; });
                if (options == null)
                {
                    return -1;//--help option
                }

                //
                Console.WriteLine("This application generates C# and C++ header files with helper classes to work with EXPRESS models.");
                Console.WriteLine("Use --help to ptint more information and visit http://www.rdf.bg");
                Console.WriteLine("");

                //
                // Update and validate options
                //
                if (string.IsNullOrWhiteSpace (options.schema))
                {
                    //options.schema = "ap242ed2_mim_lf_v1.101.exp";
                    //options.schema = "..\\..\\..\\EXPRESS\\structural_frame_schema.exp";
                    //options.schema = "..\\..\\..\\EXPRESS\\IFC2X3_TC1.exp";                    
                    //options.schema = "..\\..\\..\\EXPRESS\\IFC4_ADD2_TC1.exp";
                    //options.schema = "..\\..\\..\\EXPRESS\\IFC4x1.exp";
                    //options.schema = "..\\..\\..\\EXPRESS\\IFC4x2.exp";
                    //options.schema = "..\\..\\..\\EXPRESS\\IFC4x3.exp";
                    //options.schema = "AP242";
                    options.schema = "IFC4";
                    //options.schema = "IFC2x3";
                    //options.schema = "IFC4x3";
                    //options.schema = "CIS2";
                }

                string baseName = System.IO.Path.GetFileNameWithoutExtension (options.schema);

                if (string.IsNullOrWhiteSpace (options.csFile))
                {
                    options.csFile = baseName + ".cs";
                }
                if (string.IsNullOrWhiteSpace (options.hFile))
                {
                    options.hFile = baseName + ".h";
                }
                if (string.IsNullOrWhiteSpace (options.Namespace))
                {
                    options.Namespace = baseName;
                }
                options.Namespace = Generator.ValidateIdentifier(options.Namespace);

                //
                // Main course
                //
                Console.WriteLine("Generating classes for " + baseName);

                var schema = new ExpressSchema(options.schema);

                Console.WriteLine("....");

                if (options.printSchema)
                {
                    schema.ToConsole();
                    Console.WriteLine();
                }

                //
                /*/
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
                System.Console.WriteLine();*/

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
