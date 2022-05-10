using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CustomClass
{
    class ModelCreator
    {
        public static void Run(string pathName)
        {
            var model = RDF.engine.OpenModel(pathName);

            DefineCustomClass(model);

            RDF.engine.SaveModel(model, pathName);
            RDF.engine.CloseModel(model);
        }

        private static void DefineCustomClass (Int64 model)
        {
            var classCustomCylinder = RDF.engine.CreateClass(model, "MyCustomCylinder");
            var classCylinder = RDF.engine.GetClassByName(model, "Cylinder");
            RDF.engine.SetClassParent(classCustomCylinder, classCylinder, 1);

            var customStringProperty = RDF.engine.CreateProperty(model, RDF.engine.DATATYPEPROPERTY_TYPE_CHAR, "MyCustomString");

            RDF.engine.SetClassPropertyCardinalityRestriction(classCustomCylinder, customStringProperty, 1, 10);
        }
    }
}
