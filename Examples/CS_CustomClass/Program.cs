using System;

namespace CustomClass
{

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string modelPathName = "CustomModel.bin";

                var model = RDF.engine.OpenModel(modelPathName);

                if (!System.IO.File.Exists(modelPathName))
                {
                    Console.WriteLine("Model file {0} does not exist. Creating new and defining custom class and properties...");
                    DefineCustomClass(model);
                    RDF.engine.SaveModel(model, modelPathName);
                    Console.WriteLine("Initial model cetaed sucessfully. Generate wrapper and restart this application");
                }
                else
                {
                    CreateCustomClassInstance(model);
                }

                RDF.engine.CloseModel(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


        private static void DefineCustomClass(Int64 model)
        {
            var classCustomCylinder = RDF.engine.CreateClass(model, "MyCustomCylinder");
            var classCylinder = RDF.engine.GetClassByName(model, "Cylinder");
            RDF.engine.SetClassParent(classCustomCylinder, classCylinder, 1);

            var customStringProperty = RDF.engine.CreateProperty(model, RDF.engine.DATATYPEPROPERTY_TYPE_CHAR, "MyCustomString");
            var customBoolProperty = RDF.engine.CreateProperty(model, RDF.engine.DATATYPEPROPERTY_TYPE_BOOLEAN, "MyCustomBool");

            RDF.engine.SetClassPropertyCardinalityRestriction(classCustomCylinder, customStringProperty, 1, 10);
            RDF.engine.SetClassPropertyCardinalityRestriction(classCustomCylinder, customBoolProperty, 1, 10);
        }

        static void CreateCustomClassInstance(Int64 model)
        {
            var cylinder = CustomModel.MyCustomCylinder.Create(model);

            string[] str = cylinder.get_MyCustomString();
            System.Diagnostics.Trace.Assert(str == null);

            bool[] flags = cylinder.get_MyCustomBool();
            System.Diagnostics.Trace.Assert(flags == null);

            var strset = new string[] { "S1", "S2" };
            cylinder.set_MyCustomString(strset);

            var flagset = new bool[] { false, true, false };
            cylinder.set_MyCustomBool(flagset);

            str = cylinder.get_MyCustomString();
            flags = cylinder.get_MyCustomBool();
            
            System.Diagnostics.Trace.Assert(str.Length == 2 && str[0]=="S1" && str[1]=="S2");   
            System.Diagnostics.Trace.Assert(flags.Length == 3 && !flags[0] && flags[1] && !flags[2]);
        }


    }
}
