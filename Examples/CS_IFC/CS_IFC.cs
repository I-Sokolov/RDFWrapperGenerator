using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using RDF;
using IFC4;

namespace CS_IFC
{
    class CS_IFC
    {
        public static void IFC4_test()
        {
            var ifcModel = ifcengine.sdaiCreateModelBN(0, (string)null, "IFC4");
            ASSERT(ifcModel!=0);
            ifcengine.SetSPFFHeaderItem(ifcModel, 9, 0, ifcengine.sdaiSTRING, "IFC4");
            ifcengine.SetSPFFHeaderItem(ifcModel, 9, 1, ifcengine.sdaiSTRING, (string)null);

            var ownerHistory = IfcOwnerHistory.Create(ifcModel);

            var wall = IfcWall.Create(ifcModel);

            var guid = wall.get_GlobalId();
            var name = wall.get_Name();
            var descr = wall.get_Description();
            IfcOwnerHistory oh = wall.get_OwnerHistory();
            var predType = wall.get_PredefinedType();
            ASSERT(descr==null && name==null && guid==null && oh==0 && predType==null);

            wall.put_GlobalId("7-7-7");
            wall.put_Name("Wall name");
            wall.put_Description("My wall description");
            wall.put_OwnerHistory(ownerHistory);
            wall.put_PredefinedType(IfcWallTypeEnum.POLYGONAL);

            guid = wall.get_GlobalId();
            name = wall.get_Name();
            descr = wall.get_Description();
            oh = wall.get_OwnerHistory();
            predType = wall.get_PredefinedType();
            ASSERT(descr == "My wall description"
                   && name == "Wall name"
                   && guid == "7-7-7"
                   && oh == ownerHistory
                   && predType.Value == IfcWallTypeEnum.POLYGONAL
            );

            var profile = IfcRectangleProfileDef.Create(ifcModel);
            var xdim = profile.get_XDim();
            var ydim = profile.get_YDim();
            ASSERT(xdim == null && ydim == null);
            profile.put_XDim(10000);
            profile.put_YDim(80);
            xdim = profile.get_XDim();
            ydim = profile.get_YDim();
            ASSERT(xdim.Value == 10000 && ydim.Value == 80);


            IfcTriangulatedFaceSet triangFaceSet = IfcTriangulatedFaceSet.Create(ifcModel);
            var closed = triangFaceSet.get_Closed();
            ASSERT(closed==null);
            triangFaceSet.put_Closed(false);
            closed = triangFaceSet.get_Closed();
            ASSERT(!closed.Value);

            IfcBSplineCurve curve = IfcBSplineCurveWithKnots.Create(ifcModel);
            ASSERT(!curve.get_Degree().HasValue);
            curve.put_Degree(5);
            ASSERT(curve.get_Degree().Value == 5);

            //type casting check
            var product = new IfcProduct (wall);
            ASSERT(product != 0);
            name = product.get_Name();
            ASSERT(name == "Wall name");

            IfcBuilding building = new IfcBuilding(wall);
            ASSERT(building == 0);

            //
            // SELECT tests
            //

            //
            // various data type and nested selects
            //
            IfcMeasureWithUnit measureWithUnit = IfcMeasureWithUnit.Create(ifcModel);

            //numeric value (sequence notation)
            double? dval = measureWithUnit.get_ValueComponent().get_IfcMeasureValue().get_IfcRatioMeasure();
            //Nullable<double> works also good
            ASSERT(!dval.HasValue);

            //shortcuts methods
            var as_double = measureWithUnit.get_ValueComponent().as_double();
            var as_text = measureWithUnit.get_ValueComponent().as_text();
            var as_int = measureWithUnit.get_ValueComponent().as_int();
            var as_bool = measureWithUnit.get_ValueComponent().as_bool();
            ASSERT(!as_double.HasValue && as_text == null && as_int == null && as_bool == null);

            //numeric value (alteranative notation)
            var getMeasureValue = new IfcMeasureValue_get(measureWithUnit, "ValueComponent");
            dval = getMeasureValue.get_IfcRatioMeasure();
            ASSERT(dval == null);

            // see below detached select test
            var measureValue_detachedSelect = new IfcMeasureValue(measureWithUnit, "ValueComponent");
            dval = measureValue_detachedSelect.get_IfcRatioMeasure();
            ASSERT(!dval.HasValue);

            //text based value
            string sval = getMeasureValue.get_IfcDescriptiveMeasure();
            ASSERT(sval == null);

            string txt = measureWithUnit.get_ValueComponent().get_IfcSimpleValue().get_IfcText();
            ASSERT(txt == null);

            //set to numeric
            measureWithUnit.put_ValueComponent().put_IfcMeasureValue().put_IfcRatioMeasure(0.5);

            dval = measureWithUnit.get_ValueComponent().get_IfcMeasureValue().get_IfcRatioMeasure();
            ASSERT(dval.Value == 0.5);

            sval = measureWithUnit.get_ValueComponent().get_IfcMeasureValue().get_IfcDescriptiveMeasure();
            ASSERT(sval == null);

            txt = measureWithUnit.get_ValueComponent().get_IfcSimpleValue().get_IfcText();
            ASSERT(txt == null);

            //check type methodt
            if (measureWithUnit.get_ValueComponent().get_IfcMeasureValue().is_IfcAreaMeasure())
            {
                ASSERT(false);
            }
            else if (!measureWithUnit.get_ValueComponent().get_IfcMeasureValue().is_IfcRatioMeasure())
            {
                ASSERT(false);
            }
            else if (measureWithUnit.get_ValueComponent().get_IfcSimpleValue().is_IfcText())
            {
                ASSERT(false);
            }
            else if (measureWithUnit.get_ValueComponent().get_IfcMeasureValue().is_IfcComplexNumber())
            {
                ASSERT(false);
            }

            //shortcuts methods
            as_double = measureWithUnit.get_ValueComponent().as_double();
            as_text = measureWithUnit.get_ValueComponent().as_text();
            as_int = measureWithUnit.get_ValueComponent().as_int();
            as_bool = measureWithUnit.get_ValueComponent().as_bool();
            ASSERT(as_double.Value == 0.5 && as_text == "0.500000" && as_int.Value == 0 && !as_bool.HasValue);

            //detached select behaviour
            //detached select is also changed when instance changed
            dval = measureValue_detachedSelect.get_IfcRatioMeasure();
            ASSERT(dval! == 0.5);
            //but changing the detached select will change host instance
            measureValue_detachedSelect.put_IfcAreaMeasure(2.7);
            dval = measureValue_detachedSelect.get_IfcAreaMeasure();
            ASSERT(dval.Value == 2.7);
            //instance was changed
            dval = measureWithUnit.get_ValueComponent().get_IfcMeasureValue().get_IfcRatioMeasure();
            ASSERT(dval == null);
            dval = measureWithUnit.get_ValueComponent().get_IfcMeasureValue().get_IfcAreaMeasure();
            ASSERT(dval! == 2.7);

            //set DescriptiveMeasure
            measureWithUnit.put_ValueComponent().put_IfcMeasureValue().put_IfcDescriptiveMeasure("my descreptive measure");

            dval = measureWithUnit.get_ValueComponent().get_IfcMeasureValue().get_IfcRatioMeasure();
            ASSERT(dval==null);

            sval = measureWithUnit.get_ValueComponent().get_IfcMeasureValue().get_IfcDescriptiveMeasure();
            ASSERT(sval == "my descreptive measure");

            txt = measureWithUnit.get_ValueComponent().get_IfcSimpleValue().get_IfcText();
            ASSERT(txt == null);

            as_double = measureWithUnit.get_ValueComponent().as_double();
            as_text = measureWithUnit.get_ValueComponent().as_text();
            as_int = measureWithUnit.get_ValueComponent().as_int();
            as_bool = measureWithUnit.get_ValueComponent().as_bool();
            ASSERT(as_double == null && (as_text == "my descreptive measure") && as_int == null && as_bool==null) ;

            //set text
            measureWithUnit.put_ValueComponent().put_IfcSimpleValue().put_IfcText("my text");

            ASSERT(measureWithUnit.get_ValueComponent().get_IfcSimpleValue().is_IfcText());

            dval = measureWithUnit.get_ValueComponent().get_IfcMeasureValue().get_IfcRatioMeasure();
            ASSERT(dval==null);

            sval = measureWithUnit.get_ValueComponent().get_IfcMeasureValue().get_IfcDescriptiveMeasure();
            ASSERT(sval == null);

            txt = measureWithUnit.get_ValueComponent().get_IfcSimpleValue().get_IfcText();
            ASSERT(txt== "my text");

            IfcComplexNumber complexVal = measureWithUnit.get_ValueComponent().get_IfcMeasureValue().get_IfcComplexNumber();
            ASSERT(complexVal.Count==0);

            as_double = measureWithUnit.get_ValueComponent().as_double();
            as_text = measureWithUnit.get_ValueComponent().as_text();
            as_int = measureWithUnit.get_ValueComponent().as_int();
            as_bool = measureWithUnit.get_ValueComponent().as_bool();
            ASSERT(as_double==null && (as_text == "my text") && as_int==null && as_bool==null);

            //
            // simple aggrgations in select
            //
            double[] arrDouble = { 2, 5 };
            measureWithUnit.put_ValueComponent().put_IfcMeasureValue().put_IfcComplexNumber(arrDouble);
            complexVal = measureWithUnit.get_ValueComponent().get_IfcMeasureValue().get_IfcComplexNumber();
            ASSERT(complexVal.Count==2 && complexVal[0]==2 && complexVal[1]==5);

            //
            // entities select
            //
            var actor = IfcActor.Create(ifcModel);

            var person = actor.get_TheActor().get_IfcPerson();
            var organization = actor.get_TheActor().get_IfcOrganization();
            ASSERT(person == 0 && organization == 0);

            Int64 instance = actor.get_TheActor().as_instance();
            ASSERT(instance == 0);

            var setPerson = IfcPerson.Create(ifcModel);
            setPerson.put_Identification("justApeson");

            actor.put_TheActor().put_IfcPerson(setPerson);
            person = actor.get_TheActor().get_IfcPerson();
            ASSERT(setPerson == person);

            organization = actor.get_TheActor().get_IfcOrganization();
            ASSERT(organization == 0);

            instance = actor.get_TheActor().as_instance();
            ASSERT(instance == person);

            ASSERT(person.get_Identification() == "justApeson");

            ASSERT(actor.get_TheActor().is_IfcPerson());
            ASSERT(!actor.get_TheActor().is_IfcOrganization());

            //
            // LOGICAL VALUES
            //
            ASSERT(curve.get_ClosedCurve()==null);
            curve.put_ClosedCurve(LOGICAL_VALUE.Unknown);
            ASSERT(curve.get_ClosedCurve().Value == LOGICAL_VALUE.Unknown);

            var ifcLogical = measureWithUnit.get_ValueComponent().get_IfcSimpleValue().get_IfcLogical();
            ASSERT(ifcLogical==null);
            measureWithUnit.put_ValueComponent().put_IfcSimpleValue().put_IfcLogical(LOGICAL_VALUE.True);
            ifcLogical = measureWithUnit.get_ValueComponent().get_IfcSimpleValue().get_IfcLogical();
            ASSERT(ifcLogical.Value == LOGICAL_VALUE.True);

            var relIntersect = IfcRelInterferesElements.Create(ifcModel);
            ifcLogical = relIntersect.get_ImpliedOrder();
            ASSERT(ifcLogical==null);
            relIntersect.put_ImpliedOrder(LOGICAL_VALUE.False);
            ifcLogical = relIntersect.get_ImpliedOrder();
            ASSERT(ifcLogical.Value == LOGICAL_VALUE.False);

            //
            // Aggregations
            //

            //as defined type
            var site = IfcSite.Create(ifcModel);

            IfcCompoundPlaneAngleMeasure longitude;
            longitude = site.get_RefLongitude();
            ASSERT(longitude.Count == 0);

            longitude = new IfcCompoundPlaneAngleMeasure();
            longitude.Add(54);
            site.put_RefLongitude(longitude);

            longitude = site.get_RefLongitude();
            ASSERT(longitude.Count == 1 && longitude[0] == 54);


            Int64[] rint = { 3, 4 };
            site.put_RefLongitude(rint);

            longitude = site.get_RefLongitude();
            ASSERT(longitude.Count == 2 && longitude[0] == 3 && longitude[1] == 4);

            //double unnamed
            var point = IfcCartesianPoint.Create(ifcModel);

            ListOfIfcLengthMeasure coords = point.get_Coordinates();
            ASSERT(coords.Count==0);

            double[] my2DPoint = { 1.0, 2.0 }; //can use array to set
            point.put_Coordinates(my2DPoint);

            coords = point.get_Coordinates();
            ASSERT(coords.Count == 2 && coords[0] == 1 && coords[1] == 2);

            coords.Add(3);
            point.put_Coordinates(coords); //can use sdt.list to set
            ASSERT(coords.Count == 3 && coords[0] == 1 && coords[2] == 3);

            //string
            ListOfIfcLabel middleNames = person.get_MiddleNames();
            ASSERT(middleNames.Count==0);

            string[] DaliMiddleNames = { "Domingo", "Felipe", "Jacinto" };
            person.put_MiddleNames(DaliMiddleNames);

            middleNames = person.get_MiddleNames();
            ASSERT(middleNames.Count == 3);
            int i = 0;
            foreach (var m in middleNames)
            {
                ASSERT(m == DaliMiddleNames[i++]);
            }

            //
            // LIST of LIST
            //
            var pointList = IfcCartesianPointList3D.Create(ifcModel);

            ListOfListOfIfcLengthMeasure coordList = pointList.get_CoordList();
            ASSERT(coordList.Count==0);

            //point (1,0.1)
            coordList.Add(new ListOfIfcLengthMeasure());
            coordList.Last().Add(1);
            coordList.Last().Add(0);
            coordList.Last().Add(1);

            //point (0,1,0)
            coordList.Add(new ListOfIfcLengthMeasure());
            coordList.Last().Add(0);
            coordList.Last().Add(1);
            coordList.Last().Add(0);

            pointList.put_CoordList(coordList);

            ListOfListOfIfcLengthMeasure coordListCheck = pointList.get_CoordList();
            ASSERT_EQ(coordList, coordListCheck);
#if NOT_NOW

            //
            // Aggregation in select 
            // 
            var prop = IfcPropertySingleValue.Create(ifcModel);

            IfcComplexNumber cplxNum;
            prop.get_NominalValue().get_IfcMeasureValue().get_IfcComplexNumber(cplxNum);
            ASSERT(cplxNum.size() == 0);

            double cplx[] = { 2.1, 1.5 };
            prop.put_NominalValue().put_IfcMeasureValue().put_IfcComplexNumber(cplx, 2);

            prop.get_NominalValue().get_IfcMeasureValue().get_IfcComplexNumber(cplxNum);
            ASSERT(cplxNum.size() == 2 && cplxNum.front() == 2.1 && cplxNum.back() == 1.5);


            //
            //IndexedPolyCurve
            //
            var poly = IfcIndexedPolyCurve.Create(ifcModel);

            ASSERT(poly.get_Points() == 0);

            ListOfIfcSegmentIndexSelect segments;
            poly.get_Segments(segments);
            ASSERT(segments.empty());

            //2D points
            double rpt[] ={
        0,0,
        1,0,
        1,1,
        0,1
    };

            //indexes of line and arc;
            IfcPositiveInteger line[] = { 0, 1 };
            IfcPositiveInteger arc[] = { 1, 2, 3 };

            //create points list
            //
            var points = IfcCartesianPointList2D.Create(ifcModel);

            ListOfListOfIfcLengthMeasure lstCoords; //TODO: helper method ListOfListOfT.FromArray (T* r, int NumRow, int NumCol);
            for (int i = 0; i < 4; i++)
            {
                lstCoords.push_back(ListOfIfcLengthMeasure());
                for (int j = 0; j < 2; j++)
                {
                    lstCoords.back().push_back(rpt[2 * i + j]);
                }
            }

            points.put_CoordList(lstCoords);

            //create segments list
            //
            segments.clear();

            IfcSegmentIndexSelect segment(poly);
            segment.put_IfcLineIndex(line, 2);
            segments.push_back(segment);

            segment.put_IfcArcIndex(arc, 3);
            segments.push_back(segment);

            //
            //
            poly.put_Segments(segments);
            poly.put_Points(points);
            poly.put_SelfIntersect(false);

            //
            // get and check
            //
            points = 0;
            coordList.clear();
            segments.clear();

            var pts = poly.get_Points();
            points = IfcCartesianPointList2D(pts); //TODO isInstanceOf!
            ASSERT(points != 0);

            points.get_CoordList(coordList);
            ASSERT(coordList.size() == 4);
            i = 0;
            for (var & coord : coordList)
            {
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
            ASSERT(lineInd.size() == 2 && lineInd.front() == 0 && lineInd.back() == 1);

            arcInd.clear();
            lineInd.clear();
            segments.back().get_IfcArcIndex(arcInd);
            segments.back().get_IfcLineIndex(lineInd);

            ASSERT(arcInd.size() == 3 && arcInd.front() == 1 && arcInd.back() == 3);
            ASSERT(lineInd.empty());

            //append line
            lineInd.push_back(3);
            lineInd.push_back(0);
            segment.put_IfcLineIndex(lineInd);
            segments.push_back(segment);

            poly.put_Segments(segments);

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

            ///
            /// Aggregation of instances
            /// 
            var prodRepr = wall.get_Representation();
            ASSERT(prodRepr == 0);

            prodRepr = IfcProductDefinitionShape.Create(ifcModel);
            wall.put_Representation(prodRepr);
            ASSERT(wall.get_Representation() == prodRepr);

            ListOfIfcRepresentation lstRep;
            prodRepr.get_Representations(lstRep);
            ASSERT(lstRep.empty());

            var repr = IfcShapeRepresentation.Create(ifcModel);
            lstRep.push_back(repr);
            prodRepr.put_Representations(lstRep);

            lstRep.clear();
            prodRepr.get_Representations(lstRep);
            ASSERT(lstRep.size() == 1 && lstRep.front() == repr);

            SetOfIfcRepresentationItem lstItems;
            repr.get_Items(lstItems);
            ASSERT(lstItems.size() == 0);

            lstItems.push_back(poly);
            lstItems.push_back(triangFaceSet);
            lstItems.push_back(curve);

            repr.put_Items(lstItems);

            lstItems.clear();
            repr.get_Items(lstItems);
            ASSERT(lstItems.size() == 3 && lstItems.front() == poly && lstItems.back() == curve);

            ///
            /// Defined type aggregation of instance
            var relProps = IfcRelDefinesByProperties.Create(ifcModel);

            SetOfIfcObjectDefinition relObj;
            relProps.get_RelatedObjects(relObj);
            ASSERT(relObj.empty());

            relObj.push_back(wall);

            relProps.put_RelatedObjects(relObj);

            relObj.clear();
            relProps.get_RelatedObjects(relObj);
            ASSERT(relObj.size() == 1 && relObj.front() == wall);

            IfcPropertySetDefinitionSet psSet;
            relProps.get_RelatingPropertyDefinition().get_IfcPropertySetDefinitionSet(psSet);
            ASSERT(psSet.size() == 0);

            ASSERT(relProps.get_RelatingPropertyDefinition().get_IfcPropertySetDefinition() == 0);

            var emptyPset = IfcPropertySet.Create(ifcModel);
            emptyPset.put_Name("Empty property set");

            relProps.put_RelatingPropertyDefinition().put_IfcPropertySetDefinition(emptyPset);
            ASSERT(relProps.get_RelatingPropertyDefinition().get_IfcPropertySetDefinition() == emptyPset);
            relProps.get_RelatingPropertyDefinition().get_IfcPropertySetDefinitionSet(psSet);
            ASSERT(psSet.size() == 0);

            psSet.push_back(emptyPset);
            relProps.put_RelatingPropertyDefinition().put_IfcPropertySetDefinitionSet(psSet);
            ASSERT(relProps.get_RelatingPropertyDefinition().get_IfcPropertySetDefinition() == 0);
            psSet.clear();
            relProps.get_RelatingPropertyDefinition().get_IfcPropertySetDefinitionSet(psSet);
            ASSERT(psSet.size() == 1 && psSet.front() == emptyPset);
#endif
            /// 
            /// 
            ifcengine.sdaiSaveModelBN(ifcModel, "Test.ifc");
            ifcengine.sdaiCloseModel(ifcModel);
#if NOT_NOW
            ifcModel = sdaiOpenModelBN(NULL, "Test.ifc", "IFC4");

            var entityIfcRelDefinesByProperties = sdaiGetEntity(ifcModel, "IfcRelDefinesByProperties");
            ASSERT(entityIfcRelDefinesByProperties);

            int_t* rels = sdaiGetEntityExtent(ifcModel, entityIfcRelDefinesByProperties);
            var N_rels = sdaiGetMemberCount(rels);
            ASSERT(N_rels == 1);
            for (int_t i = 0; i < N_rels; i++)
            {

                int_t rel = 0;
                engiGetAggrElement(rels, i, sdaiINSTANCE, &rel);

                var get = IfcRelDefinesByProperties(rel).get_RelatingPropertyDefinition();
                ASSERT(get.get_IfcPropertySetDefinition() == 0);
                psSet.clear();
                get.get_IfcPropertySetDefinitionSet(psSet);
                ASSERT(psSet.size() == 1);
                name = psSet.front().get_Name();
                ASSERT(!strcmp(name, "Empty property set"));
            }

            sdaiCloseModel(ifcModel);
#endif
        }

        private static void ASSERT(bool c)
        {
            Debug.Assert(c);
        }

        private static void ASSERT_EQ(IEnumerable lst1, IEnumerable lst2)
        {
            var it1 = lst1.GetEnumerator();
            var it2 = lst2.GetEnumerator();

            bool m1 = it1.MoveNext();
            bool m2 = it2.MoveNext();
            while (m1 && m2)
            {
                var cmp1 = it1.Current as IComparable;
                var cmp2 = it2.Current as IComparable;
                if (cmp1 != null && cmp2 != null)
                {
                    ASSERT(cmp1.Equals(cmp2));
                }
                else
                {
                    var lst11 = it1.Current as IEnumerable;
                    var lst22 = it2.Current as IEnumerable;
                    if (lst11 != null && lst22 != null)
                    {
                        ASSERT_EQ(lst11, lst22);
                    }
                    else
                    {
                        ASSERT(it1.Current == null && it2.Current == null);
                    }
                }

                m1 = it1.MoveNext();
                m2 = it2.MoveNext();
            }

            ASSERT(!m1 && !m2);
        }

    }
}
