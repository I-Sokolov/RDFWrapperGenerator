// CPP_IFC.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>

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

    IfcGloballyUniqueId guid = wall.get_GlobalId();
    IfcLabel name = wall.get_Name();
    IfcText descr = wall.get_Description();
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
           && oh == ownerHistory
           && predType.Value() == IfcWallTypeEnum_POLYGONAL
    );



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


    //
    // SELECT tests
    //

    //
    // various data type and nested selects
    //
    IfcMeasureWithUnit measureWithUnit = IfcMeasureWithUnit::Create(ifcModel);

    //numeric value (sequence notation)
    Nullable<IfcAbsorbedDoseMeasure> dval = measureWithUnit.get_ValueComponent().get_IfcMeasureValue().get_IfcRatioMeasure();
    //Nullable<double> works also good
    ASSERT(dval.IsNull());

    //shortcuts methods
    auto as_double = measureWithUnit.get_ValueComponent().as_double();
    auto as_text = measureWithUnit.get_ValueComponent().as_text();
    auto as_int = measureWithUnit.get_ValueComponent().as_int();
    auto as_bool = measureWithUnit.get_ValueComponent().as_bool();
    ASSERT(as_double.IsNull() && as_text == NULL && as_int.IsNull() && as_bool.IsNull());

    //numeric value (alteranative notation)
    IfcMeasureValue_getter getMeasureValue(measureWithUnit, "ValueComponent");
    dval = getMeasureValue.get_IfcRatioMeasure();
    ASSERT(dval.IsNull());

    //can do even this... but it may be tricky - 
    // !detached select will not follows instance changes, but changing it will change instance - gets a copy, in futire it works like setter
    // see below detached select behaviour
    IfcMeasureValue measureValue_detachedSelect (measureWithUnit, "ValueComponent");
    dval = measureValue_detachedSelect.get_IfcRatioMeasure();
    ASSERT(dval.IsNull());

    //text based value
    IfcDescriptiveMeasure sval = getMeasureValue.get_IfcDescriptiveMeasure();
    ASSERT(sval == NULL);

    IfcText txt = measureWithUnit.get_ValueComponent().get_IfcSimpleValue().get_IfcText();
    ASSERT(txt == NULL);

    //set to numeric
    measureWithUnit.set_ValueComponent().set_IfcMeasureValue().set_IfcRatioMeasure(0.5);

    dval = measureWithUnit.get_ValueComponent().get_IfcMeasureValue().get_IfcRatioMeasure();
    ASSERT(dval.Value() == 0.5);

    sval = measureWithUnit.get_ValueComponent().get_IfcMeasureValue().get_IfcDescriptiveMeasure();
    ASSERT(sval == NULL);

    txt = measureWithUnit.get_ValueComponent().get_IfcSimpleValue().get_IfcText();
    ASSERT(txt == NULL);

    //shortcuts methods
    as_double = measureWithUnit.get_ValueComponent().as_double();
    as_text = measureWithUnit.get_ValueComponent().as_text();
    as_int = measureWithUnit.get_ValueComponent().as_int();
    as_bool = measureWithUnit.get_ValueComponent().as_bool();
    ASSERT(as_double.Value() == 0.5 && 0 == strcmp(as_text, "0.500000") && as_int.Value() == 0 && as_bool.IsNull());

    //detached select behaviour
    //detached select was not changed when instance changed
    dval = measureValue_detachedSelect.get_IfcRatioMeasure();
    ASSERT(dval.IsNull());
    //but changing the detached select will change host instance
    measureValue_detachedSelect.set_IfcAreaMeasure(2.7);
    dval = measureValue_detachedSelect.get_IfcAreaMeasure();
    ASSERT(dval.Value() == 2.7);
    //instance was changed
    dval = measureWithUnit.get_ValueComponent().get_IfcMeasureValue().get_IfcRatioMeasure();
    ASSERT(dval.IsNull());
    dval = measureWithUnit.get_ValueComponent().get_IfcMeasureValue().get_IfcAreaMeasure();
    ASSERT(dval.Value()==2.7);

    //set DescriptiveMeasure
    measureWithUnit.set_ValueComponent().set_IfcMeasureValue().set_IfcDescriptiveMeasure("my descreptive measure");

    dval = measureWithUnit.get_ValueComponent().get_IfcMeasureValue().get_IfcRatioMeasure();
    ASSERT(dval.IsNull());

    sval = measureWithUnit.get_ValueComponent().get_IfcMeasureValue().get_IfcDescriptiveMeasure();
    ASSERT(0 == strcmp(sval, "my descreptive measure"));

    txt = measureWithUnit.get_ValueComponent().get_IfcSimpleValue().get_IfcText();
    ASSERT(txt == NULL);

    as_double = measureWithUnit.get_ValueComponent().as_double();
    as_text = measureWithUnit.get_ValueComponent().as_text();
    as_int = measureWithUnit.get_ValueComponent().as_int();
    as_bool = measureWithUnit.get_ValueComponent().as_bool();
    ASSERT(as_double.IsNull() && 0 == strcmp(as_text, "my descreptive measure") && as_int.IsNull() && as_bool.IsNull());

    //set text
    measureWithUnit.set_ValueComponent().set_IfcSimpleValue().set_IfcText("my text");

    dval = measureWithUnit.get_ValueComponent().get_IfcMeasureValue().get_IfcRatioMeasure();
    ASSERT(dval.IsNull());

    sval = measureWithUnit.get_ValueComponent().get_IfcMeasureValue().get_IfcDescriptiveMeasure();
    ASSERT(sval == NULL);

    txt = measureWithUnit.get_ValueComponent().get_IfcSimpleValue().get_IfcText();
    ASSERT(0 == strcmp(txt, "my text"));

    IfcComplexNumber complexVal;
    measureWithUnit.get_ValueComponent().get_IfcMeasureValue().get_IfcComplexNumber(complexVal);
    ASSERT(complexVal.empty());

    as_double = measureWithUnit.get_ValueComponent().as_double();
    as_text = measureWithUnit.get_ValueComponent().as_text();
    as_int = measureWithUnit.get_ValueComponent().as_int();
    as_bool = measureWithUnit.get_ValueComponent().as_bool();
    ASSERT(as_double.IsNull() && 0 == strcmp(as_text, "my text") && as_int.IsNull() && as_bool.IsNull());

    //
    // entities select
    //
    auto actor = IfcActor::Create(ifcModel);

    auto person = actor.get_TheActor().get_IfcPerson();
    auto organization = actor.get_TheActor().get_IfcOrganization();
    ASSERT(person == 0 && organization == 0);

    SdaiInstance instance = actor.get_TheActor().as_instance();
    ASSERT(instance == 0);

    auto setPerson = IfcPerson::Create(ifcModel);
    setPerson.set_Identification("justApeson");

    actor.set_TheActor().set_IfcPerson(setPerson);
    person = actor.get_TheActor().get_IfcPerson();
    ASSERT(setPerson == person);

    organization = actor.get_TheActor().get_IfcOrganization();
    ASSERT(organization == 0);

    instance = actor.get_TheActor().as_instance();
    ASSERT(instance == person);

    //
    // Aggregations
    //

    //as defined type
    auto site = IfcSite::Create(ifcModel);

    IfcCompoundPlaneAngleMeasure longitude;
    site.get_RefLongitude(longitude);
    ASSERT(longitude.size() == 0);

    longitude.push_back(54);
    site.set_RefLongitude(longitude);

    longitude.clear();
    site.get_RefLongitude(longitude);
    ASSERT(longitude.size() == 1 && longitude.front() == 54);

    int64_t rint[] = {3,4};
    site.set_RefLongitude(rint, 2);

    longitude.clear();
    site.get_RefLongitude(longitude);
    ASSERT(longitude.size() == 2 && longitude.front() == 3 && longitude.back() == 4);

    //double unnamed
    auto point = IfcCartesianPoint::Create(ifcModel);

    ListOfIfcLengthMeasure coords;
    point.get_Coordinates(coords);
    ASSERT(coords.empty());

    double my2DPoint[] = {1.0,2.0}; //can use array to set
    point.set_Coordinates(my2DPoint, 2);

    coords.clear();
    point.get_Coordinates(coords);
    ASSERT(coords.size() == 2 && coords.front() == 1 && coords.back() == 2);

    coords.push_back(3);
    point.set_Coordinates(coords); //can use sdt::list to set
    ASSERT(coords.size() == 3 && coords.front() == 1 && coords.back() == 3);

    //string
    ListOfIfcLabel middleNames;
    person.get_MiddleNames(middleNames);
    ASSERT(middleNames.empty());

    const char* DaliMiddleNames[] = {"Domingo", "Felipe", "Jacinto"};
    person.set_MiddleNames(DaliMiddleNames, 3);

    person.get_MiddleNames(middleNames);
    ASSERT(middleNames.size() == 3);
    int i = 0;
    for (auto m : middleNames) {
        ASSERT(0 == strcmp(m.c_str(), DaliMiddleNames[i++]));
    }

    //
    // LIST of LIST
    //
    auto pointList = IfcCartesianPointList3D::Create(ifcModel);

    ListOfListOfIfcLengthMeasure coordList;

    pointList.get_CoordList(coordList);
    ASSERT(coordList.empty());

    //point (1,0.1)
    coordList.push_back(ListOfIfcLengthMeasure());
    coordList.back().push_back(1);
    coordList.back().push_back(0);
    coordList.back().push_back(1);

    //point (0,1,0)
    coordList.push_back(ListOfIfcLengthMeasure());
    coordList.back().push_back(0);
    coordList.back().push_back(1);
    coordList.back().push_back(0);

    pointList.set_CoordList(coordList);

    ListOfListOfIfcLengthMeasure coordListCheck;
    pointList.get_CoordList(coordListCheck);
    ASSERT(coordList == coordListCheck);

    //
    // Aggregation in select 
    // 
    auto prop = IfcPropertySingleValue::Create(ifcModel);

    IfcComplexNumber cplxNum;
    prop.get_NominalValue().get_IfcMeasureValue().get_IfcComplexNumber(cplxNum);
    ASSERT(cplxNum.size() == 0);

    double cplx[] = {2.1, 1.5};
    prop.set_NominalValue().set_IfcMeasureValue().set_IfcComplexNumber(cplx, 2);

    prop.get_NominalValue().get_IfcMeasureValue().get_IfcComplexNumber(cplxNum);
    ASSERT(cplxNum.size() == 2 && cplxNum.front() == 2.1 && cplxNum.back() == 1.5);


    //
    //IndexedPolyCurve
    //
#if 0
    auto poly = IfcIndexedPolyCurve::Create(ifcModel);

    ASSERT(poly.get_Points() == 0);

    ListOfIfcSegmentIndexSelect segments;
    poly.get_Segments(segments);
    ASSERT(segments.empty());

    //2D points
    double rpt[]={
        0,0,
        1,0,
        1,1,
        0,1
    };

    //indexes of line and arc;
    IfcPositiveInteger line[] = {0,1};
    IfcPositiveInteger arc[] = {1,2,3};

    //create points list
    //
    auto points = IfcCartesianPointList2D::Create(ifcModel);

    ListOfListOfIfcLengthMeasure lstCoords; //TODO: helper method ListOfListOfT::FromArray (T* r, int NumRow, int NumCol);
    for (int i = 0; i < 4; i++) {
        lstCoords.push_back(ListOfIfcLengthMeasure());
        for (int j = 0; j < 2; j++) {
            lstCoords.back().push_back(rpt[2 * i + j]);
        }
    }   

    points.set_CoordList(lstCoords);

    //create segments list
    //
    segments.clear();
    
    IfcSegmentIndexSelect segment (poly);//select needs to know entity
    segment.set_IfcLineIndex(line, 2);
    segments.push_back(segment);
    
    segment.set_IfcArcIndex(arc,3);
    segments.push_back(segment);

    //
    //
    poly.set_Segments(segments);
    poly.set_Points(points);
    poly.set_SelfIntersect(false);

    //
    // get and check
    //
    points = 0;
    coordList.clear();
    segments.clear();

    points = IfcCartesianPointList2D(poly.get_Points()); //TODO isInstanceOf!
    ASSERT(points != 0);

    points.get_CoordList(coordList);
    ASSERT(coordList.size() == 4);
    i = 0;
    for (auto& coord : coordList) {
        ASSERT(coord.size() == 2);
        ASSERT(coord.front() == rpt[2 * i] && coord.back() == rpt[2 * i + 1]);
        i++;
    }

    poly.get_Segments(segments);
    ASSERT(segments.size() == 2);
    
    IfcArcIndex arcInd;
    IfcLineIndex lineInd;
    segments.front().get_IfcArcIndex(arcInd);
    segments.front().get_IfcLineIndex(lineInd);

    ASSERT(arcInd.empty());
    ASSERT(lineInd.size()==2 && lineInd.front()==0 && lineInd.back()==1);

    arcInd.clear();
    lineInd.clear();
    segments.back().get_IfcArcIndex(arcInd);
    segments.back().get_IfcLineIndex(lineInd);

    ASSERT(arcInd.size()==3 && arcInd.front()==1 && arcInd.back()==3);
    ASSERT(lineInd.empty());

    //append line
    lineInd.push_back(3);
    lineInd.push_back(0);
    segment.set_IfcLineIndex(lineInd);
    segments.push_back(segment);

    poly.set_Segments(segments);

    //check now
    segments.clear();
    poly.get_Segments(segments);
    ASSERT(segments.size() == 3);

    segment = segments.back();
    arcInd.clear();
    lineInd.clear();
    segment.get_IfcArcIndex(arcInd);
    segment.get_IfcLineIndex(lineInd);

    ASSERT(arcInd.empty());
    ASSERT(lineInd.size() == 2 && lineInd.front() == 3 && lineInd.back() == 0);
#endif
    sdaiSaveModelBN(ifcModel, "Test.ifc");

#if 0
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