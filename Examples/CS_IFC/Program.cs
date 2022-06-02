using System;
using RDF;
using IFC4;
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
            var ifcModel = ifcengine.sdaiOpenModelBN(0, (string)null, "IFC4");

            var ownerHistory = IfcOwnerHistory.Create(ifcModel);

            var wall = IFC4.IfcWall.Create(ifcModel);

            var guid = wall.get_GlobalId();
            var name = wall.get_Name();
            var descr = wall.get_Description();
            var oh = wall.get_OwnerHistory();
            Debug.Assert(descr==null && name==null && guid==null && oh == null);

            wall.set_GlobalId("7-7-7");
            wall.set_Name("Wall name");
            wall.set_Description("My wall description");
            wall.set_OwnerHistory(ownerHistory);

            guid = wall.get_GlobalId();
            name = wall.get_Name();
            descr = wall.get_Description();
            oh = wall.get_OwnerHistory();
            Debug.Assert(descr == "My wall description" && name == "Wall name" && guid == "7-7-7" && oh == ownerHistory);

            var profile = IfcRectangleProfileDef.Create(ifcModel);
            var xdim = profile.get_XDim();
            var ydim = profile.get_YDim();
            Debug.Assert(xdim==null && ydim==null);
            profile.set_XDim(10000);
            profile.set_YDim(80);
            xdim = profile.get_XDim();
            ydim = profile.get_YDim();
            Debug.Assert(xdim.Value == 10000 && ydim.Value == 80);


            IfcTriangulatedFaceSet triangFaceSet = IfcTriangulatedFaceSet.Create(ifcModel);
            var closed = triangFaceSet.get_Closed();
            Debug.Assert(closed==null);
            triangFaceSet.set_Closed(false);
            closed = triangFaceSet.get_Closed();
            Debug.Assert(!closed.Value);

            IfcBSplineCurve curve = IfcBSplineCurveWithKnots.Create(ifcModel);
            Debug.Assert(curve.get_Degree()==null);
            curve.set_Degree(5);
            Debug.Assert(curve.get_Degree().Value == 5);


            ifcengine.sdaiSaveModelBN(ifcModel, "test.cs.ifc");

        }
    }
}
