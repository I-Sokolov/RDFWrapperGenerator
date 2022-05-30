using System;
using RDF;
using System.Diagnostics;

namespace CS_GeometryKernel
{
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var model = ifcengine.sdaiOpenModelBN(0, (string)null, "IFC4");

            var wall = IFC4.IfcWall.Create(model);

            ifcengine.sdaiSaveModelBN(model, "test.ifc");

        }
    }
}
