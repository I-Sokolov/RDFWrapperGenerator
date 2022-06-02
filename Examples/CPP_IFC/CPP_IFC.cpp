// CPP_IFC.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>

#include "ifcengine.h"
#include "O:\DevArea\RDF\RDFWrappers\EngineEx_Template.h"
#include "O:\DevArea\RDF\RDFWrappers\bin\Debug\net5.0\IFC4.h"

using namespace IFC4;

#ifndef ASSERT
#define ASSERT assert
#endif



int main()
{

    int_t  ifcModel = sdaiCreateModelBN(0, NULL, "IFC4");
    ASSERT(ifcModel);

    auto ownerHistory = IfcOwnerHistory::Create(ifcModel);

    auto wall = IfcWall::Create(ifcModel);

    auto guid = wall.get_GlobalId();
    auto name = wall.get_Name();
    auto descr = wall.get_Description();
    IfcOwnerHistory oh = wall.get_OwnerHistory();
    auto predType = wall.get_PredefinedType();
    ASSERT(!descr && !name && !guid && !oh && predType.IsNull());

    wall.set_GlobalId("7-7-7");
    wall.set_Name("Wall name");
    wall.set_Description("My wall description");
    wall.set_OwnerHistory(ownerHistory);
    wall.set_PredefinedType(IfcWallTypeEnum_POLYGONAL);

    guid = wall.get_GlobalId();
    name = wall.get_Name();
    descr = wall.get_Description();
    oh = wall.get_OwnerHistory();
    predType = wall.get_PredefinedType();
    ASSERT(!strcmp(descr, "My wall description") 
           && !strcmp(name, "Wall name") 
           && !strcmp(guid, "7-7-7") 
           && oh==ownerHistory
           && predType.Value() == IfcWallTypeEnum_POLYGONAL
    ) ;



    auto profile = IfcRectangleProfileDef::Create(ifcModel);
    auto xdim = profile.get_XDim();
    auto ydim = profile.get_YDim();
    ASSERT(xdim.IsNull() && ydim.IsNull());
    profile.set_XDim(10000);
    profile.set_YDim(80);
    xdim = profile.get_XDim();
    ydim = profile.get_YDim();
    ASSERT(xdim.Value() == 10000 && ydim.Value() == 80);


    IfcTriangulatedFaceSet triangFaceSet = IfcTriangulatedFaceSet::Create(ifcModel);
    auto closed = triangFaceSet.get_Closed();
    ASSERT(closed.IsNull());
    triangFaceSet.set_Closed(false);
    closed = triangFaceSet.get_Closed();
    ASSERT(!closed.Value());

    IfcBSplineCurve curve = IfcBSplineCurveWithKnots::Create(ifcModel);
    ASSERT(curve.get_Degree().IsNull());
    curve.set_Degree(5);
    ASSERT(curve.get_Degree().Value() == 5);

    IfcCartesianPointList3D pointList = IfcCartesianPointList3D::Create(ifcModel);
    int_t* coordList = nullptr;
    void* ret = sdaiGetAttrBN(pointList, "CoordList", sdaiAGGR, &coordList);
    if (!coordList) {
        coordList = sdaiCreateAggrBN(pointList, "CoordList");
        coordList = 0;
        ret = sdaiGetAttrBN(pointList, "CoordList", sdaiAGGR, &coordList);
    }

    for (double v = 1; v < 5; v = v + 1) {
        int_t* coords = sdaiCreateAggrBN(pointList, nullptr);
        //int_t* coords = sdaiCreateAggr(pointList, 0);

        sdaiAppend((int_t) coords, sdaiREAL, (void*) &v);
        sdaiAppend((int_t) coords, sdaiREAL, (void*) &v);
        sdaiAppend((int_t) coords, sdaiREAL, (void*) &v);
        sdaiAppend((int_t) coordList, sdaiAGGR, (void*) coords);
    }

    sdaiSaveModelBN(ifcModel, "Test.ifc");

#if 0
    //
    //            IFCINDEXEDPOLYCURVE(#1, (IfcLineIndex((1, 2)), IfcArcIndex((2, 3, 4))), $);
    //
    int_t  ifcCartesianPointList3DInstance = sdaiCreateInstanceBN(ifcModel, "IFCCARTESIANPOINTLIST3D"),
        ifcIndexedPolyCurveInstance = sdaiCreateInstanceBN(ifcModel, "IFCINDEXEDPOLYCURVE");

    sdaiPutAttrBN(ifcIndexedPolyCurveInstance, "Points", sdaiINSTANCE, (void*) ifcCartesianPointList3DInstance);

    int_t  valueOne = 1, valueTwo = 2, valueThree = 3, valueFour = 4;

    int_t* ifcLineIndexADB_01_aggr = sdaiCreateAggr(ifcIndexedPolyCurveInstance, 0);
    sdaiAppend((int_t) ifcLineIndexADB_01_aggr, sdaiINTEGER, &valueOne);
    sdaiAppend((int_t) ifcLineIndexADB_01_aggr, sdaiINTEGER, &valueTwo);

    void    ifcLineIndexADB_01 = sdaiCreateADB(sdaiAGGR, (void) ifcLineIndexADB_01_aggr);
    sdaiPutADBTypePath(ifcLineIndexADB_01, 1, "IfcLineIndex");    //     Note: I expect it is better to use IFCLINEINDEX Has uppercase

    int_t* ifcArcIndexADB_02_aggr = sdaiCreateAggr(ifcIndexedPolyCurveInstance, 0);
    sdaiAppend((int_t) ifcArcIndexADB_02_aggr, sdaiINTEGER, &valueTwo);
    sdaiAppend((int_t) ifcArcIndexADB_02_aggr, sdaiINTEGER, &valueThree);
    sdaiAppend((int_t) ifcArcIndexADB_02_aggr, sdaiINTEGER, &valueFour);

    void    ifcArcIndexADB_02 = sdaiCreateADB(sdaiAGGR, (void) ifcArcIndexADB_02_aggr);
    sdaiPutADBTypePath(ifcArcIndexADB_02, 1, "IfcArcIndex");      //     Note: I expect it is better to use IFCARCINDEX Has uppercase

    int_t* segmentsAggr = sdaiCreateAggrBN(ifcIndexedPolyCurveInstance, "Segments");
    sdaiAppend((int_t) segmentsAggr, sdaiADB, (void*) ifcLineIndexADB_01);
    sdaiAppend((int_t) segmentsAggr, sdaiADB, (void*) ifcArcIndexADB_02);

    sdaiSaveModelBNUnicode(ifcModel, ifcFileName);

    Example code for reading:
    int_t  ifcModel = sdaiOpenModelBNUnicode(0, ifcFileName, ifcSchemaName_IFC4x1);
    if (ifcModel) {
        int_t* ifcIndexedPolyCurveInstances = sdaiGetEntityExtentBN(ifcModel, "IFCINDEXEDPOLYCURVE"),
            ifcIndexedPolyCurveInstance = 0;
        engiGetAggrElement(ifcIndexedPolyCurveInstances, 0, sdaiINSTANCE, &ifcIndexedPolyCurveInstance);

        int_t* segmentsAggr = nullptr;
        sdaiGetAttrBN(ifcIndexedPolyCurveInstance, "Segments", sdaiAGGR, &segmentsAggr);
        int_t  segmentsAggrCnt = sdaiGetMemberCount(segmentsAggr);
        for (int_t i = 0; i < segmentsAggrCnt; i++) {
            int_t* segmentsADB = 0;
            engiGetAggrElement(segmentsAggr, i, sdaiADB, &segmentsADB);

            char* path = sdaiGetADBTypePath(segmentsADB, 0);
            int_t* ifcLineArcIndex = nullptr;
            sdaiGetADBValue(segmentsADB, sdaiAGGR, &ifcLineArcIndex);
            int_t  ifcLineArcIndexCnt = sdaiGetMemberCount(ifcLineArcIndex);
            for (int_t j = 0; j < ifcLineArcIndexCnt; j++) {
                int64_t       integerValue = 0;
                engiGetAggrElement(ifcLineArcIndex, j, sdaiINTEGER, &integerValue);

                //
                //     integer values in two arrays (1, 2) and (2, 3, 4) as expected
                //
            }
        }
    }
#endif    
}