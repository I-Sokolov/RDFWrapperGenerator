//
// Helper classes (C# wrappers)
//
using System;
using RDF;

namespace GEOM
{
// Classes list:
//     AdvancedFace
//     AdvancedFace2D
//     AdvancedFace3D
//     AdvancedFaceMapped
//     AlignedSegment
//     AlignedSegments
//     Alignment
//     AmbientLight
//     Appearance
//     Arc3D
//     BezierCurve
//     BezierSurface
//     BiQuadraticParabola
//     Blend
//     BlossCurve
//     BooleanOperation
//     BooleanOperation2D
//     BoundaryRepresentation
//     Box
//     BSplineCurve
//     BSplineSurface
//     Circle
//     CircleByPoints
//     ClippedPyramid
//     Clipping
//     Clothoid
//     ClothoidCurve
//     Collection
//     Color
//     ColorComponent
//     Cone
//     ConicalCurve
//     ConicalSurface
//     Copy
//     CosineCurve
//     Cube
//     CubicParabola
//     Cuboid
//     Curve
//     CurvesFromSurface
//     Cylinder
//     CylindricalSurface
//     DegenerateToroidalSurface
//     Deviation
//     DirectionalLight
//     DirectLight
//     Dummy
//     Ellipse
//     EllipticCone
//     Environment
//     ExtrudedPolygon
//     ExtrudedPolygonTapered
//     ExtrusionAreaSolid
//     ExtrusionAreaSolidSet
//     Face
//     Face2D
//     Face2DSet
//     FiniteSurface
//     FrustumCone
//     GeometricItem
//     HornTorus
//     Hyperbola
//     InfiniteSurface
//     InverseMatrix
//     InvertedCurve
//     InvertedSurface
//     Light
//     Line3D
//     Line3Dn
//     Line3DSet
//     LineByFace
//     Material
//     Mathematics
//     Matrix
//     MatrixMultiplication
//     Mesh
//     NURBSCurve
//     NURBSSurface
//     Parabola
//     Plane
//     PlaneSurface
//     Point
//     Point3D
//     Point3DSet
//     Point3DSetByGeometricItem
//     PointLight
//     PointLoop
//     Polygon2D
//     Polygon3D
//     PolyLine3D
//     Prism
//     Profile
//     Projection
//     Pyramid
//     RationalBezierCurve
//     RationalBezierSurface
//     RectangleCurve
//     Repetition
//     RingTorus
//     Shadow
//     SineCurve
//     SkewedCone
//     SkewedFrustumCone
//     Solid
//     SolidBySurface
//     SolidLine
//     Sphere
//     SphericalSurface
//     SpindleTorus
//     Spiral
//     SplineCurve
//     SplineSurface
//     SpotLight
//     Surface
//     SurfaceBySweptCurve
//     SurfaceOfLinearExtrusion
//     SurfaceOfRevolution
//     SweptAreaSolid
//     SweptAreaSolidSet
//     SweptAreaSolidTapered
//     SweptBlend
//     Texture
//     ToroidalSurface
//     Torus
//     Transformation
//     TransitionalCurve
//     TriangleCurve
//     TriangleReduction
//     TriangleSet
//     Vector
//     Vector3
//     View
//     World

    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class AdvancedFace
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class AdvancedFace : GeometricItem
    {
        /// <summary>
        /// Create new instace of OWL class AdvancedFace and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new AdvancedFace Create(Int64 model, string name=null) { return new AdvancedFace(Instance.Create(model, "AdvancedFace", name), "AdvancedFace");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public AdvancedFace(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "AdvancedFace") 
        {            
        }

       //
       // Properties with known cardinality restrictions to AdvancedFace
       //

        ///<summary>Sets relationships from this instance to an array of Curve. OWL cardinality 0..-1</summary>
        public void set_bounds(Curve[] instances) { SetObjectProperty("bounds", instances); }
        ///<summary>Sets relationships from this instance to an array of Int64. OWL cardinality 0..-1</summary>
        public void set_bounds(Int64[] instances) { SetObjectProperty("bounds", instances); }
        ///<summary>Get an array of related instances. OWL cardinality 0..-1</summary>
        public Curve[] get_bounds() 
        {
            var propId = GetPropertyId("bounds");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                var ret = new Curve[card];
                for (int i = 0; i < card; i++)
                {
                    ret[i] = new Curve(values[i], null);
                }

                return ret;
            }
            else
            {
                return null;
            }
        }
        ///<summary>Get an array of handles of related instances. OWL cardinality 0..-1</summary>
        public Int64[] get_bounds_Int64()  
        {
            var propId = GetPropertyId("bounds");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                return values;
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets relationship from this instance to an instance of Surface</summary>
        public void set_surface(Surface instance) { SetObjectProperty("surface", instance); }
        ///<summary>Get related instance</summary>
        public Surface get_surface() 
        {
            var propId = GetPropertyId("surface");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Surface(values[0], null);
            }
            else
            {
                return null;
            }
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class AdvancedFace2D
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class AdvancedFace2D : AdvancedFace
    {
        /// <summary>
        /// Create new instace of OWL class AdvancedFace2D and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new AdvancedFace2D Create(Int64 model, string name=null) { return new AdvancedFace2D(Instance.Create(model, "AdvancedFace2D", name), "AdvancedFace2D");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public AdvancedFace2D(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "AdvancedFace2D") 
        {            
        }

       //
       // Properties with known cardinality restrictions to AdvancedFace2D
       //

        ///<summary>Sets value of clipped</summary>
        public void set_clipped(bool value) { SetDatatypeProperty ("clipped", value); }
        ///<summary>Gets value of clipped, returns null is the property was not set</summary>
        public bool? get_clipped() { var arr = GetDatatypeProperty_bool("clipped"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of scalingX</summary>
        public void set_scalingX(double value) { SetDatatypeProperty ("scalingX", value); }
        ///<summary>Gets value of scalingX, returns null is the property was not set</summary>
        public double? get_scalingX() { var arr = GetDatatypeProperty_double("scalingX"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of scalingY</summary>
        public void set_scalingY(double value) { SetDatatypeProperty ("scalingY", value); }
        ///<summary>Gets value of scalingY, returns null is the property was not set</summary>
        public double? get_scalingY() { var arr = GetDatatypeProperty_double("scalingY"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class AdvancedFace3D
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class AdvancedFace3D : AdvancedFace
    {
        /// <summary>
        /// Create new instace of OWL class AdvancedFace3D and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new AdvancedFace3D Create(Int64 model, string name=null) { return new AdvancedFace3D(Instance.Create(model, "AdvancedFace3D", name), "AdvancedFace3D");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public AdvancedFace3D(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "AdvancedFace3D") 
        {            
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class AdvancedFaceMapped
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class AdvancedFaceMapped : AdvancedFace
    {
        /// <summary>
        /// Create new instace of OWL class AdvancedFaceMapped and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new AdvancedFaceMapped Create(Int64 model, string name=null) { return new AdvancedFaceMapped(Instance.Create(model, "AdvancedFaceMapped", name), "AdvancedFaceMapped");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public AdvancedFaceMapped(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "AdvancedFaceMapped") 
        {            
        }

       //
       // Properties with known cardinality restrictions to AdvancedFaceMapped
       //

        ///<summary>Sets value of scalingX</summary>
        public void set_scalingX(double value) { SetDatatypeProperty ("scalingX", value); }
        ///<summary>Gets value of scalingX, returns null is the property was not set</summary>
        public double? get_scalingX() { var arr = GetDatatypeProperty_double("scalingX"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of scalingY</summary>
        public void set_scalingY(double value) { SetDatatypeProperty ("scalingY", value); }
        ///<summary>Gets value of scalingY, returns null is the property was not set</summary>
        public double? get_scalingY() { var arr = GetDatatypeProperty_double("scalingY"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class AlignedSegment
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class AlignedSegment : Curve
    {
        /// <summary>
        /// Create new instace of OWL class AlignedSegment and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new AlignedSegment Create(Int64 model, string name=null) { return new AlignedSegment(Instance.Create(model, "AlignedSegment", name), "AlignedSegment");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public AlignedSegment(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "AlignedSegment") 
        {            
        }

       //
       // Properties with known cardinality restrictions to AlignedSegment
       //

        ///<summary>Sets value of length</summary>
        public void set_length(double value) { SetDatatypeProperty ("length", value); }
        ///<summary>Gets value of length, returns null is the property was not set</summary>
        public double? get_length() { var arr = GetDatatypeProperty_double("length"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of offsetX</summary>
        public void set_offsetX(double value) { SetDatatypeProperty ("offsetX", value); }
        ///<summary>Gets value of offsetX, returns null is the property was not set</summary>
        public double? get_offsetX() { var arr = GetDatatypeProperty_double("offsetX"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of offsetY</summary>
        public void set_offsetY(double value) { SetDatatypeProperty ("offsetY", value); }
        ///<summary>Gets value of offsetY, returns null is the property was not set</summary>
        public double? get_offsetY() { var arr = GetDatatypeProperty_double("offsetY"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets relationship from this instance to an instance of Curve</summary>
        public void set_segment(Curve instance) { SetObjectProperty("segment", instance); }
        ///<summary>Get related instance</summary>
        public Curve get_segment() 
        {
            var propId = GetPropertyId("segment");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Curve(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets values of tangentDirectionStart. OWL cardinality 3..3</summary>
        public void set_tangentDirectionStart(double[] values) { SetDatatypeProperty ("tangentDirectionStart", values); }
        ///<summary>Gets values of tangentDirectionStart. OWL cardinality 3..3</summary>
        public double[] get_tangentDirectionStart() { return GetDatatypeProperty_double("tangentDirectionStart"); }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class AlignedSegments
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class AlignedSegments : Curve
    {
        /// <summary>
        /// Create new instace of OWL class AlignedSegments and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new AlignedSegments Create(Int64 model, string name=null) { return new AlignedSegments(Instance.Create(model, "AlignedSegments", name), "AlignedSegments");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public AlignedSegments(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "AlignedSegments") 
        {            
        }

       //
       // Properties with known cardinality restrictions to AlignedSegments
       //

        ///<summary>Sets relationships from this instance to an array of AlignedSegment. OWL cardinality 0..-1</summary>
        public void set_segments(AlignedSegment[] instances) { SetObjectProperty("segments", instances); }
        ///<summary>Sets relationships from this instance to an array of Int64. OWL cardinality 0..-1</summary>
        public void set_segments(Int64[] instances) { SetObjectProperty("segments", instances); }
        ///<summary>Get an array of related instances. OWL cardinality 0..-1</summary>
        public AlignedSegment[] get_segments() 
        {
            var propId = GetPropertyId("segments");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                var ret = new AlignedSegment[card];
                for (int i = 0; i < card; i++)
                {
                    ret[i] = new AlignedSegment(values[i], null);
                }

                return ret;
            }
            else
            {
                return null;
            }
        }
        ///<summary>Get an array of handles of related instances. OWL cardinality 0..-1</summary>
        public Int64[] get_segments_Int64()  
        {
            var propId = GetPropertyId("segments");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                return values;
            }
            else
            {
                return null;
            }
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Alignment
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Alignment : Curve
    {
        /// <summary>
        /// Create new instace of OWL class Alignment and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Alignment Create(Int64 model, string name=null) { return new Alignment(Instance.Create(model, "Alignment", name), "Alignment");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Alignment(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Alignment") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Alignment
       //

        ///<summary>Sets relationship from this instance to an instance of AlignedSegments</summary>
        public void set_horizontal(AlignedSegments instance) { SetObjectProperty("horizontal", instance); }
        ///<summary>Get related instance</summary>
        public AlignedSegments get_horizontal() 
        {
            var propId = GetPropertyId("horizontal");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new AlignedSegments(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets value of offsetX</summary>
        public void set_offsetX(double value) { SetDatatypeProperty ("offsetX", value); }
        ///<summary>Gets value of offsetX, returns null is the property was not set</summary>
        public double? get_offsetX() { var arr = GetDatatypeProperty_double("offsetX"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of type</summary>
        public void set_type(long value) { SetDatatypeProperty ("type", value); }
        ///<summary>Gets value of type, returns null is the property was not set</summary>
        public long? get_type() { var arr = GetDatatypeProperty_long("type"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets relationship from this instance to an instance of AlignedSegments</summary>
        public void set_vertical(AlignedSegments instance) { SetObjectProperty("vertical", instance); }
        ///<summary>Get related instance</summary>
        public AlignedSegments get_vertical() 
        {
            var propId = GetPropertyId("vertical");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new AlignedSegments(values[0], null);
            }
            else
            {
                return null;
            }
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class AmbientLight
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class AmbientLight : Light
    {
        /// <summary>
        /// Create new instace of OWL class AmbientLight and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new AmbientLight Create(Int64 model, string name=null) { return new AmbientLight(Instance.Create(model, "AmbientLight", name), "AmbientLight");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public AmbientLight(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "AmbientLight") 
        {            
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Appearance
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Appearance : Instance
    {
        /// <summary>
        /// Create new instace of OWL class Appearance and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Appearance Create(Int64 model, string name=null) { return new Appearance(Instance.Create(model, "Appearance", name), "Appearance");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Appearance(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Appearance") 
        {            
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Arc3D
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Arc3D : Curve
    {
        /// <summary>
        /// Create new instace of OWL class Arc3D and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Arc3D Create(Int64 model, string name=null) { return new Arc3D(Instance.Create(model, "Arc3D", name), "Arc3D");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Arc3D(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Arc3D") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Arc3D
       //

        ///<summary>Sets value of hasNormals</summary>
        public void set_hasNormals(bool value) { SetDatatypeProperty ("hasNormals", value); }
        ///<summary>Gets value of hasNormals, returns null is the property was not set</summary>
        public bool? get_hasNormals() { var arr = GetDatatypeProperty_bool("hasNormals"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of radius</summary>
        public void set_radius(double value) { SetDatatypeProperty ("radius", value); }
        ///<summary>Gets value of radius, returns null is the property was not set</summary>
        public double? get_radius() { var arr = GetDatatypeProperty_double("radius"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of segmentationParts</summary>
        public void set_segmentationParts(long value) { SetDatatypeProperty ("segmentationParts", value); }
        ///<summary>Gets value of segmentationParts, returns null is the property was not set</summary>
        public long? get_segmentationParts() { var arr = GetDatatypeProperty_long("segmentationParts"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of size</summary>
        public void set_size(double value) { SetDatatypeProperty ("size", value); }
        ///<summary>Gets value of size, returns null is the property was not set</summary>
        public double? get_size() { var arr = GetDatatypeProperty_double("size"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of start</summary>
        public void set_start(double value) { SetDatatypeProperty ("start", value); }
        ///<summary>Gets value of start, returns null is the property was not set</summary>
        public double? get_start() { var arr = GetDatatypeProperty_double("start"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class BezierCurve
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class BezierCurve : SplineCurve
    {
        /// <summary>
        /// Create new instace of OWL class BezierCurve and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new BezierCurve Create(Int64 model, string name=null) { return new BezierCurve(Instance.Create(model, "BezierCurve", name), "BezierCurve");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public BezierCurve(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "BezierCurve") 
        {            
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class BezierSurface
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class BezierSurface : SplineSurface
    {
        /// <summary>
        /// Create new instace of OWL class BezierSurface and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new BezierSurface Create(Int64 model, string name=null) { return new BezierSurface(Instance.Create(model, "BezierSurface", name), "BezierSurface");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public BezierSurface(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "BezierSurface") 
        {            
        }

       //
       // Properties with known cardinality restrictions to BezierSurface
       //

        ///<summary>Sets value of setting</summary>
        public void set_setting(long value) { SetDatatypeProperty ("setting", value); }
        ///<summary>Gets value of setting, returns null is the property was not set</summary>
        public long? get_setting() { var arr = GetDatatypeProperty_long("setting"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class BiQuadraticParabola
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class BiQuadraticParabola : TransitionalCurve
    {
        /// <summary>
        /// Create new instace of OWL class BiQuadraticParabola and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new BiQuadraticParabola Create(Int64 model, string name=null) { return new BiQuadraticParabola(Instance.Create(model, "BiQuadraticParabola", name), "BiQuadraticParabola");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public BiQuadraticParabola(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "BiQuadraticParabola") 
        {            
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Blend
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Blend : Solid
    {
        /// <summary>
        /// Create new instace of OWL class Blend and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Blend Create(Int64 model, string name=null) { return new Blend(Instance.Create(model, "Blend", name), "Blend");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Blend(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Blend") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Blend
       //

        ///<summary>Sets relationship from this instance to an instance of Curve</summary>
        public void set_bottomPolygon(Curve instance) { SetObjectProperty("bottomPolygon", instance); }
        ///<summary>Get related instance</summary>
        public Curve get_bottomPolygon() 
        {
            var propId = GetPropertyId("bottomPolygon");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Curve(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets value of forceSolid</summary>
        public void set_forceSolid(bool value) { SetDatatypeProperty ("forceSolid", value); }
        ///<summary>Gets value of forceSolid, returns null is the property was not set</summary>
        public bool? get_forceSolid() { var arr = GetDatatypeProperty_bool("forceSolid"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of fraction</summary>
        public void set_fraction(double value) { SetDatatypeProperty ("fraction", value); }
        ///<summary>Gets value of fraction, returns null is the property was not set</summary>
        public double? get_fraction() { var arr = GetDatatypeProperty_double("fraction"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of hasBottom</summary>
        public void set_hasBottom(bool value) { SetDatatypeProperty ("hasBottom", value); }
        ///<summary>Gets value of hasBottom, returns null is the property was not set</summary>
        public bool? get_hasBottom() { var arr = GetDatatypeProperty_bool("hasBottom"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of hasTop</summary>
        public void set_hasTop(bool value) { SetDatatypeProperty ("hasTop", value); }
        ///<summary>Gets value of hasTop, returns null is the property was not set</summary>
        public bool? get_hasTop() { var arr = GetDatatypeProperty_bool("hasTop"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of height</summary>
        public void set_height(double value) { SetDatatypeProperty ("height", value); }
        ///<summary>Gets value of height, returns null is the property was not set</summary>
        public double? get_height() { var arr = GetDatatypeProperty_double("height"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets relationship from this instance to an instance of Curve</summary>
        public void set_topPolygon(Curve instance) { SetObjectProperty("topPolygon", instance); }
        ///<summary>Get related instance</summary>
        public Curve get_topPolygon() 
        {
            var propId = GetPropertyId("topPolygon");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Curve(values[0], null);
            }
            else
            {
                return null;
            }
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class BlossCurve
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class BlossCurve : TransitionalCurve
    {
        /// <summary>
        /// Create new instace of OWL class BlossCurve and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new BlossCurve Create(Int64 model, string name=null) { return new BlossCurve(Instance.Create(model, "BlossCurve", name), "BlossCurve");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public BlossCurve(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "BlossCurve") 
        {            
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class BooleanOperation
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class BooleanOperation : Solid
    {
        /// <summary>
        /// Create new instace of OWL class BooleanOperation and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new BooleanOperation Create(Int64 model, string name=null) { return new BooleanOperation(Instance.Create(model, "BooleanOperation", name), "BooleanOperation");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public BooleanOperation(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "BooleanOperation") 
        {            
        }

       //
       // Properties with known cardinality restrictions to BooleanOperation
       //

        ///<summary>Sets relationship from this instance to an instance of Deviation</summary>
        public void set_deviation(Deviation instance) { SetObjectProperty("deviation", instance); }
        ///<summary>Get related instance</summary>
        public Deviation get_deviation() 
        {
            var propId = GetPropertyId("deviation");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Deviation(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets relationship from this instance to an instance of GeometricItem</summary>
        public void set_firstObject(GeometricItem instance) { SetObjectProperty("firstObject", instance); }
        ///<summary>Get related instance</summary>
        public GeometricItem get_firstObject() 
        {
            var propId = GetPropertyId("firstObject");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new GeometricItem(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets relationship from this instance to an instance of GeometricItem</summary>
        public void set_secondObject(GeometricItem instance) { SetObjectProperty("secondObject", instance); }
        ///<summary>Get related instance</summary>
        public GeometricItem get_secondObject() 
        {
            var propId = GetPropertyId("secondObject");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new GeometricItem(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets value of setting</summary>
        public void set_setting(long value) { SetDatatypeProperty ("setting", value); }
        ///<summary>Gets value of setting, returns null is the property was not set</summary>
        public long? get_setting() { var arr = GetDatatypeProperty_long("setting"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of type</summary>
        public void set_type(long value) { SetDatatypeProperty ("type", value); }
        ///<summary>Gets value of type, returns null is the property was not set</summary>
        public long? get_type() { var arr = GetDatatypeProperty_long("type"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class BooleanOperation2D
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class BooleanOperation2D : Face
    {
        /// <summary>
        /// Create new instace of OWL class BooleanOperation2D and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new BooleanOperation2D Create(Int64 model, string name=null) { return new BooleanOperation2D(Instance.Create(model, "BooleanOperation2D", name), "BooleanOperation2D");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public BooleanOperation2D(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "BooleanOperation2D") 
        {            
        }

       //
       // Properties with known cardinality restrictions to BooleanOperation2D
       //

        ///<summary>Sets relationship from this instance to an instance of Deviation</summary>
        public void set_deviation(Deviation instance) { SetObjectProperty("deviation", instance); }
        ///<summary>Get related instance</summary>
        public Deviation get_deviation() 
        {
            var propId = GetPropertyId("deviation");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Deviation(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets relationship from this instance to an instance of GeometricItem</summary>
        public void set_firstObject(GeometricItem instance) { SetObjectProperty("firstObject", instance); }
        ///<summary>Get related instance</summary>
        public GeometricItem get_firstObject() 
        {
            var propId = GetPropertyId("firstObject");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new GeometricItem(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets relationship from this instance to an instance of GeometricItem</summary>
        public void set_secondObject(GeometricItem instance) { SetObjectProperty("secondObject", instance); }
        ///<summary>Get related instance</summary>
        public GeometricItem get_secondObject() 
        {
            var propId = GetPropertyId("secondObject");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new GeometricItem(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets value of type</summary>
        public void set_type(long value) { SetDatatypeProperty ("type", value); }
        ///<summary>Gets value of type, returns null is the property was not set</summary>
        public long? get_type() { var arr = GetDatatypeProperty_long("type"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class BoundaryRepresentation
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class BoundaryRepresentation : Solid
    {
        /// <summary>
        /// Create new instace of OWL class BoundaryRepresentation and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new BoundaryRepresentation Create(Int64 model, string name=null) { return new BoundaryRepresentation(Instance.Create(model, "BoundaryRepresentation", name), "BoundaryRepresentation");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public BoundaryRepresentation(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "BoundaryRepresentation") 
        {            
        }

       //
       // Properties with known cardinality restrictions to BoundaryRepresentation
       //

        ///<summary>Sets value of consistencyCheck</summary>
        public void set_consistencyCheck(long value) { SetDatatypeProperty ("consistencyCheck", value); }
        ///<summary>Gets value of consistencyCheck, returns null is the property was not set</summary>
        public long? get_consistencyCheck() { var arr = GetDatatypeProperty_long("consistencyCheck"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of epsilon</summary>
        public void set_epsilon(double value) { SetDatatypeProperty ("epsilon", value); }
        ///<summary>Gets value of epsilon, returns null is the property was not set</summary>
        public double? get_epsilon() { var arr = GetDatatypeProperty_double("epsilon"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets relationships from this instance to an array of Face. OWL cardinality 0..-1</summary>
        public void set_faces(Face[] instances) { SetObjectProperty("faces", instances); }
        ///<summary>Sets relationships from this instance to an array of Int64. OWL cardinality 0..-1</summary>
        public void set_faces(Int64[] instances) { SetObjectProperty("faces", instances); }
        ///<summary>Get an array of related instances. OWL cardinality 0..-1</summary>
        public Face[] get_faces() 
        {
            var propId = GetPropertyId("faces");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                var ret = new Face[card];
                for (int i = 0; i < card; i++)
                {
                    ret[i] = new Face(values[i], null);
                }

                return ret;
            }
            else
            {
                return null;
            }
        }
        ///<summary>Get an array of handles of related instances. OWL cardinality 0..-1</summary>
        public Int64[] get_faces_Int64()  
        {
            var propId = GetPropertyId("faces");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                return values;
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets values of flags. OWL cardinality 0..-1</summary>
        public void set_flags(long[] values) { SetDatatypeProperty ("flags", values); }
        ///<summary>Gets values of flags. OWL cardinality 0..-1</summary>
        public long[] get_flags() { return GetDatatypeProperty_long("flags"); }
        ///<summary>Sets value of fraction</summary>
        public void set_fraction(double value) { SetDatatypeProperty ("fraction", value); }
        ///<summary>Gets value of fraction, returns null is the property was not set</summary>
        public double? get_fraction() { var arr = GetDatatypeProperty_double("fraction"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets values of indices. OWL cardinality 0..-1</summary>
        public void set_indices(long[] values) { SetDatatypeProperty ("indices", values); }
        ///<summary>Gets values of indices. OWL cardinality 0..-1</summary>
        public long[] get_indices() { return GetDatatypeProperty_long("indices"); }
        ///<summary>Sets values of normalCoordinates. OWL cardinality 0..-1</summary>
        public void set_normalCoordinates(double[] values) { SetDatatypeProperty ("normalCoordinates", values); }
        ///<summary>Gets values of normalCoordinates. OWL cardinality 0..-1</summary>
        public double[] get_normalCoordinates() { return GetDatatypeProperty_double("normalCoordinates"); }
        ///<summary>Sets values of normalIndices. OWL cardinality 0..-1</summary>
        public void set_normalIndices(long[] values) { SetDatatypeProperty ("normalIndices", values); }
        ///<summary>Gets values of normalIndices. OWL cardinality 0..-1</summary>
        public long[] get_normalIndices() { return GetDatatypeProperty_long("normalIndices"); }
        ///<summary>Sets value of relativeEpsilon</summary>
        public void set_relativeEpsilon(double value) { SetDatatypeProperty ("relativeEpsilon", value); }
        ///<summary>Gets value of relativeEpsilon, returns null is the property was not set</summary>
        public double? get_relativeEpsilon() { var arr = GetDatatypeProperty_double("relativeEpsilon"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets values of textureCoordinates. OWL cardinality 0..-1</summary>
        public void set_textureCoordinates(double[] values) { SetDatatypeProperty ("textureCoordinates", values); }
        ///<summary>Gets values of textureCoordinates. OWL cardinality 0..-1</summary>
        public double[] get_textureCoordinates() { return GetDatatypeProperty_double("textureCoordinates"); }
        ///<summary>Sets values of textureIndices. OWL cardinality 0..-1</summary>
        public void set_textureIndices(long[] values) { SetDatatypeProperty ("textureIndices", values); }
        ///<summary>Gets values of textureIndices. OWL cardinality 0..-1</summary>
        public long[] get_textureIndices() { return GetDatatypeProperty_long("textureIndices"); }
        ///<summary>Sets values of vertices. OWL cardinality 0..-1</summary>
        public void set_vertices(double[] values) { SetDatatypeProperty ("vertices", values); }
        ///<summary>Gets values of vertices. OWL cardinality 0..-1</summary>
        public double[] get_vertices() { return GetDatatypeProperty_double("vertices"); }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Box
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Box : Solid
    {
        /// <summary>
        /// Create new instace of OWL class Box and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Box Create(Int64 model, string name=null) { return new Box(Instance.Create(model, "Box", name), "Box");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Box(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Box") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Box
       //

        ///<summary>Sets value of height</summary>
        public void set_height(double value) { SetDatatypeProperty ("height", value); }
        ///<summary>Gets value of height, returns null is the property was not set</summary>
        public double? get_height() { var arr = GetDatatypeProperty_double("height"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of length</summary>
        public void set_length(double value) { SetDatatypeProperty ("length", value); }
        ///<summary>Gets value of length, returns null is the property was not set</summary>
        public double? get_length() { var arr = GetDatatypeProperty_double("length"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of width</summary>
        public void set_width(double value) { SetDatatypeProperty ("width", value); }
        ///<summary>Gets value of width, returns null is the property was not set</summary>
        public double? get_width() { var arr = GetDatatypeProperty_double("width"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class BSplineCurve
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class BSplineCurve : SplineCurve
    {
        /// <summary>
        /// Create new instace of OWL class BSplineCurve and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new BSplineCurve Create(Int64 model, string name=null) { return new BSplineCurve(Instance.Create(model, "BSplineCurve", name), "BSplineCurve");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public BSplineCurve(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "BSplineCurve") 
        {            
        }

       //
       // Properties with known cardinality restrictions to BSplineCurve
       //

        ///<summary>Sets values of knotMultiplicities. OWL cardinality 0..-1</summary>
        public void set_knotMultiplicities(long[] values) { SetDatatypeProperty ("knotMultiplicities", values); }
        ///<summary>Gets values of knotMultiplicities. OWL cardinality 0..-1</summary>
        public long[] get_knotMultiplicities() { return GetDatatypeProperty_long("knotMultiplicities"); }
        ///<summary>Sets values of knots. OWL cardinality 2..-1</summary>
        public void set_knots(double[] values) { SetDatatypeProperty ("knots", values); }
        ///<summary>Gets values of knots. OWL cardinality 2..-1</summary>
        public double[] get_knots() { return GetDatatypeProperty_double("knots"); }
        ///<summary>Sets value of setting</summary>
        public void set_setting(long value) { SetDatatypeProperty ("setting", value); }
        ///<summary>Gets value of setting, returns null is the property was not set</summary>
        public long? get_setting() { var arr = GetDatatypeProperty_long("setting"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class BSplineSurface
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class BSplineSurface : SplineSurface
    {
        /// <summary>
        /// Create new instace of OWL class BSplineSurface and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new BSplineSurface Create(Int64 model, string name=null) { return new BSplineSurface(Instance.Create(model, "BSplineSurface", name), "BSplineSurface");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public BSplineSurface(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "BSplineSurface") 
        {            
        }

       //
       // Properties with known cardinality restrictions to BSplineSurface
       //

        ///<summary>Sets value of setting</summary>
        public void set_setting(long value) { SetDatatypeProperty ("setting", value); }
        ///<summary>Gets value of setting, returns null is the property was not set</summary>
        public long? get_setting() { var arr = GetDatatypeProperty_long("setting"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets values of uKnotMultiplicities. OWL cardinality 0..-1</summary>
        public void set_uKnotMultiplicities(long[] values) { SetDatatypeProperty ("uKnotMultiplicities", values); }
        ///<summary>Gets values of uKnotMultiplicities. OWL cardinality 0..-1</summary>
        public long[] get_uKnotMultiplicities() { return GetDatatypeProperty_long("uKnotMultiplicities"); }
        ///<summary>Sets values of uKnots. OWL cardinality 2..-1</summary>
        public void set_uKnots(double[] values) { SetDatatypeProperty ("uKnots", values); }
        ///<summary>Gets values of uKnots. OWL cardinality 2..-1</summary>
        public double[] get_uKnots() { return GetDatatypeProperty_double("uKnots"); }
        ///<summary>Sets values of vKnotMultiplicities. OWL cardinality 0..-1</summary>
        public void set_vKnotMultiplicities(long[] values) { SetDatatypeProperty ("vKnotMultiplicities", values); }
        ///<summary>Gets values of vKnotMultiplicities. OWL cardinality 0..-1</summary>
        public long[] get_vKnotMultiplicities() { return GetDatatypeProperty_long("vKnotMultiplicities"); }
        ///<summary>Sets values of vKnots. OWL cardinality 2..-1</summary>
        public void set_vKnots(double[] values) { SetDatatypeProperty ("vKnots", values); }
        ///<summary>Gets values of vKnots. OWL cardinality 2..-1</summary>
        public double[] get_vKnots() { return GetDatatypeProperty_double("vKnots"); }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Circle
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Circle : ConicalCurve
    {
        /// <summary>
        /// Create new instace of OWL class Circle and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Circle Create(Int64 model, string name=null) { return new Circle(Instance.Create(model, "Circle", name), "Circle");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Circle(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Circle") 
        {            
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class CircleByPoints
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class CircleByPoints : Circle
    {
        /// <summary>
        /// Create new instace of OWL class CircleByPoints and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new CircleByPoints Create(Int64 model, string name=null) { return new CircleByPoints(Instance.Create(model, "CircleByPoints", name), "CircleByPoints");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public CircleByPoints(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "CircleByPoints") 
        {            
        }

       //
       // Properties with known cardinality restrictions to CircleByPoints
       //

        ///<summary>Sets relationships from this instance to an array of Point3D. OWL cardinality 3..3</summary>
        public void set_pointReferences(Point3D[] instances) { SetObjectProperty("pointReferences", instances); }
        ///<summary>Sets relationships from this instance to an array of Int64. OWL cardinality 3..3</summary>
        public void set_pointReferences(Int64[] instances) { SetObjectProperty("pointReferences", instances); }
        ///<summary>Get an array of related instances. OWL cardinality 3..3</summary>
        public Point3D[] get_pointReferences() 
        {
            var propId = GetPropertyId("pointReferences");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                var ret = new Point3D[card];
                for (int i = 0; i < card; i++)
                {
                    ret[i] = new Point3D(values[i], null);
                }

                return ret;
            }
            else
            {
                return null;
            }
        }
        ///<summary>Get an array of handles of related instances. OWL cardinality 3..3</summary>
        public Int64[] get_pointReferences_Int64()  
        {
            var propId = GetPropertyId("pointReferences");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                return values;
            }
            else
            {
                return null;
            }
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class ClippedPyramid
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class ClippedPyramid : Solid
    {
        /// <summary>
        /// Create new instace of OWL class ClippedPyramid and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new ClippedPyramid Create(Int64 model, string name=null) { return new ClippedPyramid(Instance.Create(model, "ClippedPyramid", name), "ClippedPyramid");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public ClippedPyramid(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "ClippedPyramid") 
        {            
        }

       //
       // Properties with known cardinality restrictions to ClippedPyramid
       //

        ///<summary>Sets values of coordinates. OWL cardinality 12..12</summary>
        public void set_coordinates(double[] values) { SetDatatypeProperty ("coordinates", values); }
        ///<summary>Gets values of coordinates. OWL cardinality 12..12</summary>
        public double[] get_coordinates() { return GetDatatypeProperty_double("coordinates"); }
        ///<summary>Sets values of points. OWL cardinality 0..12</summary>
        public void set_points(double[] values) { SetDatatypeProperty ("points", values); }
        ///<summary>Gets values of points. OWL cardinality 0..12</summary>
        public double[] get_points() { return GetDatatypeProperty_double("points"); }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Clipping
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Clipping : Solid
    {
        /// <summary>
        /// Create new instace of OWL class Clipping and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Clipping Create(Int64 model, string name=null) { return new Clipping(Instance.Create(model, "Clipping", name), "Clipping");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Clipping(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Clipping") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Clipping
       //

        ///<summary>Sets relationship from this instance to an instance of Deviation</summary>
        public void set_deviation(Deviation instance) { SetObjectProperty("deviation", instance); }
        ///<summary>Get related instance</summary>
        public Deviation get_deviation() 
        {
            var propId = GetPropertyId("deviation");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Deviation(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets relationship from this instance to an instance of GeometricItem</summary>
        public void set_object(GeometricItem instance) { SetObjectProperty("object", instance); }
        ///<summary>Get related instance</summary>
        public GeometricItem get_object() 
        {
            var propId = GetPropertyId("object");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new GeometricItem(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets relationship from this instance to an instance of Plane</summary>
        public void set_plane(Plane instance) { SetObjectProperty("plane", instance); }
        ///<summary>Get related instance</summary>
        public Plane get_plane() 
        {
            var propId = GetPropertyId("plane");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Plane(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets value of type</summary>
        public void set_type(long value) { SetDatatypeProperty ("type", value); }
        ///<summary>Gets value of type, returns null is the property was not set</summary>
        public long? get_type() { var arr = GetDatatypeProperty_long("type"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Clothoid
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Clothoid : Curve
    {
        /// <summary>
        /// Create new instace of OWL class Clothoid and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Clothoid Create(Int64 model, string name=null) { return new Clothoid(Instance.Create(model, "Clothoid", name), "Clothoid");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Clothoid(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Clothoid") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Clothoid
       //

        ///<summary>Sets value of A</summary>
        public void set_A(double value) { SetDatatypeProperty ("A", value); }
        ///<summary>Gets value of A, returns null is the property was not set</summary>
        public double? get_A() { var arr = GetDatatypeProperty_double("A"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets relationship from this instance to an instance of Vector</summary>
        public void set_direction(Vector instance) { SetObjectProperty("direction", instance); }
        ///<summary>Get related instance</summary>
        public Vector get_direction() 
        {
            var propId = GetPropertyId("direction");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Vector(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets value of length</summary>
        public void set_length(double value) { SetDatatypeProperty ("length", value); }
        ///<summary>Gets value of length, returns null is the property was not set</summary>
        public double? get_length() { var arr = GetDatatypeProperty_double("length"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of orientation</summary>
        public void set_orientation(long value) { SetDatatypeProperty ("orientation", value); }
        ///<summary>Gets value of orientation, returns null is the property was not set</summary>
        public long? get_orientation() { var arr = GetDatatypeProperty_long("orientation"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of radius</summary>
        public void set_radius(double value) { SetDatatypeProperty ("radius", value); }
        ///<summary>Gets value of radius, returns null is the property was not set</summary>
        public double? get_radius() { var arr = GetDatatypeProperty_double("radius"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of segmentationParts</summary>
        public void set_segmentationParts(long value) { SetDatatypeProperty ("segmentationParts", value); }
        ///<summary>Gets value of segmentationParts, returns null is the property was not set</summary>
        public long? get_segmentationParts() { var arr = GetDatatypeProperty_long("segmentationParts"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class ClothoidCurve
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class ClothoidCurve : TransitionalCurve
    {
        /// <summary>
        /// Create new instace of OWL class ClothoidCurve and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new ClothoidCurve Create(Int64 model, string name=null) { return new ClothoidCurve(Instance.Create(model, "ClothoidCurve", name), "ClothoidCurve");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public ClothoidCurve(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "ClothoidCurve") 
        {            
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Collection
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Collection : GeometricItem
    {
        /// <summary>
        /// Create new instace of OWL class Collection and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Collection Create(Int64 model, string name=null) { return new Collection(Instance.Create(model, "Collection", name), "Collection");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Collection(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Collection") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Collection
       //

        ///<summary>Sets value of consistencyCheck</summary>
        public void set_consistencyCheck(long value) { SetDatatypeProperty ("consistencyCheck", value); }
        ///<summary>Gets value of consistencyCheck, returns null is the property was not set</summary>
        public long? get_consistencyCheck() { var arr = GetDatatypeProperty_long("consistencyCheck"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of epsilon</summary>
        public void set_epsilon(double value) { SetDatatypeProperty ("epsilon", value); }
        ///<summary>Gets value of epsilon, returns null is the property was not set</summary>
        public double? get_epsilon() { var arr = GetDatatypeProperty_double("epsilon"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of forceSolid</summary>
        public void set_forceSolid(bool value) { SetDatatypeProperty ("forceSolid", value); }
        ///<summary>Gets value of forceSolid, returns null is the property was not set</summary>
        public bool? get_forceSolid() { var arr = GetDatatypeProperty_bool("forceSolid"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of fraction</summary>
        public void set_fraction(double value) { SetDatatypeProperty ("fraction", value); }
        ///<summary>Gets value of fraction, returns null is the property was not set</summary>
        public double? get_fraction() { var arr = GetDatatypeProperty_double("fraction"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets relationships from this instance to an array of GeometricItem. OWL cardinality 0..-1</summary>
        public void set_objects(GeometricItem[] instances) { SetObjectProperty("objects", instances); }
        ///<summary>Sets relationships from this instance to an array of Int64. OWL cardinality 0..-1</summary>
        public void set_objects(Int64[] instances) { SetObjectProperty("objects", instances); }
        ///<summary>Get an array of related instances. OWL cardinality 0..-1</summary>
        public GeometricItem[] get_objects() 
        {
            var propId = GetPropertyId("objects");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                var ret = new GeometricItem[card];
                for (int i = 0; i < card; i++)
                {
                    ret[i] = new GeometricItem(values[i], null);
                }

                return ret;
            }
            else
            {
                return null;
            }
        }
        ///<summary>Get an array of handles of related instances. OWL cardinality 0..-1</summary>
        public Int64[] get_objects_Int64()  
        {
            var propId = GetPropertyId("objects");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                return values;
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets value of relativeEpsilon</summary>
        public void set_relativeEpsilon(double value) { SetDatatypeProperty ("relativeEpsilon", value); }
        ///<summary>Gets value of relativeEpsilon, returns null is the property was not set</summary>
        public double? get_relativeEpsilon() { var arr = GetDatatypeProperty_double("relativeEpsilon"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of representsSolid</summary>
        public void set_representsSolid(bool value) { SetDatatypeProperty ("representsSolid", value); }
        ///<summary>Gets value of representsSolid, returns null is the property was not set</summary>
        public bool? get_representsSolid() { var arr = GetDatatypeProperty_bool("representsSolid"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Color
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Color : Appearance
    {
        /// <summary>
        /// Create new instace of OWL class Color and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Color Create(Int64 model, string name=null) { return new Color(Instance.Create(model, "Color", name), "Color");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Color(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Color") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Color
       //

        ///<summary>Sets relationship from this instance to an instance of ColorComponent</summary>
        public void set_ambient(ColorComponent instance) { SetObjectProperty("ambient", instance); }
        ///<summary>Get related instance</summary>
        public ColorComponent get_ambient() 
        {
            var propId = GetPropertyId("ambient");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new ColorComponent(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets value of ambientReflectance</summary>
        public void set_ambientReflectance(double value) { SetDatatypeProperty ("ambientReflectance", value); }
        ///<summary>Gets value of ambientReflectance, returns null is the property was not set</summary>
        public double? get_ambientReflectance() { var arr = GetDatatypeProperty_double("ambientReflectance"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets relationship from this instance to an instance of ColorComponent</summary>
        public void set_diffuse(ColorComponent instance) { SetObjectProperty("diffuse", instance); }
        ///<summary>Get related instance</summary>
        public ColorComponent get_diffuse() 
        {
            var propId = GetPropertyId("diffuse");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new ColorComponent(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets relationship from this instance to an instance of ColorComponent</summary>
        public void set_emissive(ColorComponent instance) { SetObjectProperty("emissive", instance); }
        ///<summary>Get related instance</summary>
        public ColorComponent get_emissive() 
        {
            var propId = GetPropertyId("emissive");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new ColorComponent(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets relationship from this instance to an instance of ColorComponent</summary>
        public void set_specular(ColorComponent instance) { SetObjectProperty("specular", instance); }
        ///<summary>Get related instance</summary>
        public ColorComponent get_specular() 
        {
            var propId = GetPropertyId("specular");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new ColorComponent(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets value of transparency</summary>
        public void set_transparency(double value) { SetDatatypeProperty ("transparency", value); }
        ///<summary>Gets value of transparency, returns null is the property was not set</summary>
        public double? get_transparency() { var arr = GetDatatypeProperty_double("transparency"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class ColorComponent
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class ColorComponent : Appearance
    {
        /// <summary>
        /// Create new instace of OWL class ColorComponent and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new ColorComponent Create(Int64 model, string name=null) { return new ColorComponent(Instance.Create(model, "ColorComponent", name), "ColorComponent");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public ColorComponent(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "ColorComponent") 
        {            
        }

       //
       // Properties with known cardinality restrictions to ColorComponent
       //

        ///<summary>Sets value of B</summary>
        public void set_B(double value) { SetDatatypeProperty ("B", value); }
        ///<summary>Gets value of B, returns null is the property was not set</summary>
        public double? get_B() { var arr = GetDatatypeProperty_double("B"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of G</summary>
        public void set_G(double value) { SetDatatypeProperty ("G", value); }
        ///<summary>Gets value of G, returns null is the property was not set</summary>
        public double? get_G() { var arr = GetDatatypeProperty_double("G"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of R</summary>
        public void set_R(double value) { SetDatatypeProperty ("R", value); }
        ///<summary>Gets value of R, returns null is the property was not set</summary>
        public double? get_R() { var arr = GetDatatypeProperty_double("R"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of W</summary>
        public void set_W(double value) { SetDatatypeProperty ("W", value); }
        ///<summary>Gets value of W, returns null is the property was not set</summary>
        public double? get_W() { var arr = GetDatatypeProperty_double("W"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Cone
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Cone : Solid
    {
        /// <summary>
        /// Create new instace of OWL class Cone and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Cone Create(Int64 model, string name=null) { return new Cone(Instance.Create(model, "Cone", name), "Cone");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Cone(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Cone") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Cone
       //

        ///<summary>Sets value of height</summary>
        public void set_height(double value) { SetDatatypeProperty ("height", value); }
        ///<summary>Gets value of height, returns null is the property was not set</summary>
        public double? get_height() { var arr = GetDatatypeProperty_double("height"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of radius</summary>
        public void set_radius(double value) { SetDatatypeProperty ("radius", value); }
        ///<summary>Gets value of radius, returns null is the property was not set</summary>
        public double? get_radius() { var arr = GetDatatypeProperty_double("radius"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of segmentationParts</summary>
        public void set_segmentationParts(long value) { SetDatatypeProperty ("segmentationParts", value); }
        ///<summary>Gets value of segmentationParts, returns null is the property was not set</summary>
        public long? get_segmentationParts() { var arr = GetDatatypeProperty_long("segmentationParts"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class ConicalCurve
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class ConicalCurve : Curve
    {
        /// <summary>
        /// Create new instace of OWL class ConicalCurve and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new ConicalCurve Create(Int64 model, string name=null) { return new ConicalCurve(Instance.Create(model, "ConicalCurve", name), "ConicalCurve");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public ConicalCurve(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "ConicalCurve") 
        {            
        }

       //
       // Properties with known cardinality restrictions to ConicalCurve
       //

        ///<summary>Sets value of a</summary>
        public void set_a(double value) { SetDatatypeProperty ("a", value); }
        ///<summary>Gets value of a, returns null is the property was not set</summary>
        public double? get_a() { var arr = GetDatatypeProperty_double("a"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of segmentationParts</summary>
        public void set_segmentationParts(long value) { SetDatatypeProperty ("segmentationParts", value); }
        ///<summary>Gets value of segmentationParts, returns null is the property was not set</summary>
        public long? get_segmentationParts() { var arr = GetDatatypeProperty_long("segmentationParts"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of size</summary>
        public void set_size(double value) { SetDatatypeProperty ("size", value); }
        ///<summary>Gets value of size, returns null is the property was not set</summary>
        public double? get_size() { var arr = GetDatatypeProperty_double("size"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of start</summary>
        public void set_start(double value) { SetDatatypeProperty ("start", value); }
        ///<summary>Gets value of start, returns null is the property was not set</summary>
        public double? get_start() { var arr = GetDatatypeProperty_double("start"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class ConicalSurface
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class ConicalSurface : InfiniteSurface
    {
        /// <summary>
        /// Create new instace of OWL class ConicalSurface and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new ConicalSurface Create(Int64 model, string name=null) { return new ConicalSurface(Instance.Create(model, "ConicalSurface", name), "ConicalSurface");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public ConicalSurface(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "ConicalSurface") 
        {            
        }

       //
       // Properties with known cardinality restrictions to ConicalSurface
       //

        ///<summary>Sets value of height</summary>
        public void set_height(double value) { SetDatatypeProperty ("height", value); }
        ///<summary>Gets value of height, returns null is the property was not set</summary>
        public double? get_height() { var arr = GetDatatypeProperty_double("height"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of radius</summary>
        public void set_radius(double value) { SetDatatypeProperty ("radius", value); }
        ///<summary>Gets value of radius, returns null is the property was not set</summary>
        public double? get_radius() { var arr = GetDatatypeProperty_double("radius"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of segmentationParts</summary>
        public void set_segmentationParts(long value) { SetDatatypeProperty ("segmentationParts", value); }
        ///<summary>Gets value of segmentationParts, returns null is the property was not set</summary>
        public long? get_segmentationParts() { var arr = GetDatatypeProperty_long("segmentationParts"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of semiVerticalAngle</summary>
        public void set_semiVerticalAngle(double value) { SetDatatypeProperty ("semiVerticalAngle", value); }
        ///<summary>Gets value of semiVerticalAngle, returns null is the property was not set</summary>
        public double? get_semiVerticalAngle() { var arr = GetDatatypeProperty_double("semiVerticalAngle"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Copy
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Copy : GeometricItem
    {
        /// <summary>
        /// Create new instace of OWL class Copy and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Copy Create(Int64 model, string name=null) { return new Copy(Instance.Create(model, "Copy", name), "Copy");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Copy(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Copy") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Copy
       //

        ///<summary>Sets value of instanceReference</summary>
        public void set_instanceReference(long value) { SetDatatypeProperty ("instanceReference", value); }
        ///<summary>Gets value of instanceReference, returns null is the property was not set</summary>
        public long? get_instanceReference() { var arr = GetDatatypeProperty_long("instanceReference"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class CosineCurve
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class CosineCurve : TransitionalCurve
    {
        /// <summary>
        /// Create new instace of OWL class CosineCurve and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new CosineCurve Create(Int64 model, string name=null) { return new CosineCurve(Instance.Create(model, "CosineCurve", name), "CosineCurve");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public CosineCurve(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "CosineCurve") 
        {            
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Cube
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Cube : Solid
    {
        /// <summary>
        /// Create new instace of OWL class Cube and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Cube Create(Int64 model, string name=null) { return new Cube(Instance.Create(model, "Cube", name), "Cube");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Cube(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Cube") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Cube
       //

        ///<summary>Sets value of length</summary>
        public void set_length(double value) { SetDatatypeProperty ("length", value); }
        ///<summary>Gets value of length, returns null is the property was not set</summary>
        public double? get_length() { var arr = GetDatatypeProperty_double("length"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class CubicParabola
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class CubicParabola : TransitionalCurve
    {
        /// <summary>
        /// Create new instace of OWL class CubicParabola and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new CubicParabola Create(Int64 model, string name=null) { return new CubicParabola(Instance.Create(model, "CubicParabola", name), "CubicParabola");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public CubicParabola(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "CubicParabola") 
        {            
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Cuboid
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Cuboid : Solid
    {
        /// <summary>
        /// Create new instace of OWL class Cuboid and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Cuboid Create(Int64 model, string name=null) { return new Cuboid(Instance.Create(model, "Cuboid", name), "Cuboid");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Cuboid(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Cuboid") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Cuboid
       //

        ///<summary>Sets value of height</summary>
        public void set_height(double value) { SetDatatypeProperty ("height", value); }
        ///<summary>Gets value of height, returns null is the property was not set</summary>
        public double? get_height() { var arr = GetDatatypeProperty_double("height"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of length</summary>
        public void set_length(double value) { SetDatatypeProperty ("length", value); }
        ///<summary>Gets value of length, returns null is the property was not set</summary>
        public double? get_length() { var arr = GetDatatypeProperty_double("length"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of width</summary>
        public void set_width(double value) { SetDatatypeProperty ("width", value); }
        ///<summary>Gets value of width, returns null is the property was not set</summary>
        public double? get_width() { var arr = GetDatatypeProperty_double("width"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Curve
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Curve : GeometricItem
    {
        /// <summary>
        /// Create new instace of OWL class Curve and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Curve Create(Int64 model, string name=null) { return new Curve(Instance.Create(model, "Curve", name), "Curve");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Curve(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Curve") 
        {            
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class CurvesFromSurface
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class CurvesFromSurface : Curve
    {
        /// <summary>
        /// Create new instace of OWL class CurvesFromSurface and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new CurvesFromSurface Create(Int64 model, string name=null) { return new CurvesFromSurface(Instance.Create(model, "CurvesFromSurface", name), "CurvesFromSurface");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public CurvesFromSurface(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "CurvesFromSurface") 
        {            
        }

       //
       // Properties with known cardinality restrictions to CurvesFromSurface
       //

        ///<summary>Sets relationships from this instance to an array of Face. OWL cardinality 1..-1</summary>
        public void set_faces(Face[] instances) { SetObjectProperty("faces", instances); }
        ///<summary>Sets relationships from this instance to an array of Int64. OWL cardinality 1..-1</summary>
        public void set_faces(Int64[] instances) { SetObjectProperty("faces", instances); }
        ///<summary>Get an array of related instances. OWL cardinality 1..-1</summary>
        public Face[] get_faces() 
        {
            var propId = GetPropertyId("faces");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                var ret = new Face[card];
                for (int i = 0; i < card; i++)
                {
                    ret[i] = new Face(values[i], null);
                }

                return ret;
            }
            else
            {
                return null;
            }
        }
        ///<summary>Get an array of handles of related instances. OWL cardinality 1..-1</summary>
        public Int64[] get_faces_Int64()  
        {
            var propId = GetPropertyId("faces");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                return values;
            }
            else
            {
                return null;
            }
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Cylinder
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Cylinder : Solid
    {
        /// <summary>
        /// Create new instace of OWL class Cylinder and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Cylinder Create(Int64 model, string name=null) { return new Cylinder(Instance.Create(model, "Cylinder", name), "Cylinder");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Cylinder(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Cylinder") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Cylinder
       //

        ///<summary>Sets value of length</summary>
        public void set_length(double value) { SetDatatypeProperty ("length", value); }
        ///<summary>Gets value of length, returns null is the property was not set</summary>
        public double? get_length() { var arr = GetDatatypeProperty_double("length"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of radius</summary>
        public void set_radius(double value) { SetDatatypeProperty ("radius", value); }
        ///<summary>Gets value of radius, returns null is the property was not set</summary>
        public double? get_radius() { var arr = GetDatatypeProperty_double("radius"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of segmentationParts</summary>
        public void set_segmentationParts(long value) { SetDatatypeProperty ("segmentationParts", value); }
        ///<summary>Gets value of segmentationParts, returns null is the property was not set</summary>
        public long? get_segmentationParts() { var arr = GetDatatypeProperty_long("segmentationParts"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class CylindricalSurface
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class CylindricalSurface : InfiniteSurface
    {
        /// <summary>
        /// Create new instace of OWL class CylindricalSurface and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new CylindricalSurface Create(Int64 model, string name=null) { return new CylindricalSurface(Instance.Create(model, "CylindricalSurface", name), "CylindricalSurface");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public CylindricalSurface(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "CylindricalSurface") 
        {            
        }

       //
       // Properties with known cardinality restrictions to CylindricalSurface
       //

        ///<summary>Sets value of radius</summary>
        public void set_radius(double value) { SetDatatypeProperty ("radius", value); }
        ///<summary>Gets value of radius, returns null is the property was not set</summary>
        public double? get_radius() { var arr = GetDatatypeProperty_double("radius"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of segmentationParts</summary>
        public void set_segmentationParts(long value) { SetDatatypeProperty ("segmentationParts", value); }
        ///<summary>Gets value of segmentationParts, returns null is the property was not set</summary>
        public long? get_segmentationParts() { var arr = GetDatatypeProperty_long("segmentationParts"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class DegenerateToroidalSurface
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class DegenerateToroidalSurface : FiniteSurface
    {
        /// <summary>
        /// Create new instace of OWL class DegenerateToroidalSurface and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new DegenerateToroidalSurface Create(Int64 model, string name=null) { return new DegenerateToroidalSurface(Instance.Create(model, "DegenerateToroidalSurface", name), "DegenerateToroidalSurface");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public DegenerateToroidalSurface(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "DegenerateToroidalSurface") 
        {            
        }

       //
       // Properties with known cardinality restrictions to DegenerateToroidalSurface
       //

        ///<summary>Sets value of majorRadius</summary>
        public void set_majorRadius(double value) { SetDatatypeProperty ("majorRadius", value); }
        ///<summary>Gets value of majorRadius, returns null is the property was not set</summary>
        public double? get_majorRadius() { var arr = GetDatatypeProperty_double("majorRadius"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of minorRadius</summary>
        public void set_minorRadius(double value) { SetDatatypeProperty ("minorRadius", value); }
        ///<summary>Gets value of minorRadius, returns null is the property was not set</summary>
        public double? get_minorRadius() { var arr = GetDatatypeProperty_double("minorRadius"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of segmentationParts</summary>
        public void set_segmentationParts(long value) { SetDatatypeProperty ("segmentationParts", value); }
        ///<summary>Gets value of segmentationParts, returns null is the property was not set</summary>
        public long? get_segmentationParts() { var arr = GetDatatypeProperty_long("segmentationParts"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Deviation
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Deviation : Instance
    {
        /// <summary>
        /// Create new instace of OWL class Deviation and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Deviation Create(Int64 model, string name=null) { return new Deviation(Instance.Create(model, "Deviation", name), "Deviation");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Deviation(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Deviation") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Deviation
       //

        ///<summary>Sets value of absoluteEpsilon</summary>
        public void set_absoluteEpsilon(double value) { SetDatatypeProperty ("absoluteEpsilon", value); }
        ///<summary>Gets value of absoluteEpsilon, returns null is the property was not set</summary>
        public double? get_absoluteEpsilon() { var arr = GetDatatypeProperty_double("absoluteEpsilon"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of relativeEpsilon</summary>
        public void set_relativeEpsilon(double value) { SetDatatypeProperty ("relativeEpsilon", value); }
        ///<summary>Gets value of relativeEpsilon, returns null is the property was not set</summary>
        public double? get_relativeEpsilon() { var arr = GetDatatypeProperty_double("relativeEpsilon"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class DirectionalLight
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class DirectionalLight : DirectLight
    {
        /// <summary>
        /// Create new instace of OWL class DirectionalLight and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new DirectionalLight Create(Int64 model, string name=null) { return new DirectionalLight(Instance.Create(model, "DirectionalLight", name), "DirectionalLight");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public DirectionalLight(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "DirectionalLight") 
        {            
        }

       //
       // Properties with known cardinality restrictions to DirectionalLight
       //

        ///<summary>Sets relationship from this instance to an instance of Vector</summary>
        public void set_direction(Vector instance) { SetObjectProperty("direction", instance); }
        ///<summary>Get related instance</summary>
        public Vector get_direction() 
        {
            var propId = GetPropertyId("direction");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Vector(values[0], null);
            }
            else
            {
                return null;
            }
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class DirectLight
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class DirectLight : Light
    {
        /// <summary>
        /// Create new instace of OWL class DirectLight and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new DirectLight Create(Int64 model, string name=null) { return new DirectLight(Instance.Create(model, "DirectLight", name), "DirectLight");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public DirectLight(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "DirectLight") 
        {            
        }

       //
       // Properties with known cardinality restrictions to DirectLight
       //

        ///<summary>Sets value of attenuation</summary>
        public void set_attenuation(double value) { SetDatatypeProperty ("attenuation", value); }
        ///<summary>Gets value of attenuation, returns null is the property was not set</summary>
        public double? get_attenuation() { var arr = GetDatatypeProperty_double("attenuation"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of range</summary>
        public void set_range(double value) { SetDatatypeProperty ("range", value); }
        ///<summary>Gets value of range, returns null is the property was not set</summary>
        public double? get_range() { var arr = GetDatatypeProperty_double("range"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Dummy
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Dummy : GeometricItem
    {
        /// <summary>
        /// Create new instace of OWL class Dummy and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Dummy Create(Int64 model, string name=null) { return new Dummy(Instance.Create(model, "Dummy", name), "Dummy");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Dummy(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Dummy") 
        {            
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Ellipse
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Ellipse : ConicalCurve
    {
        /// <summary>
        /// Create new instace of OWL class Ellipse and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Ellipse Create(Int64 model, string name=null) { return new Ellipse(Instance.Create(model, "Ellipse", name), "Ellipse");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Ellipse(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Ellipse") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Ellipse
       //

        ///<summary>Sets value of b</summary>
        public void set_b(double value) { SetDatatypeProperty ("b", value); }
        ///<summary>Gets value of b, returns null is the property was not set</summary>
        public double? get_b() { var arr = GetDatatypeProperty_double("b"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of radiusI</summary>
        public void set_radiusI(double value) { SetDatatypeProperty ("radiusI", value); }
        ///<summary>Gets value of radiusI, returns null is the property was not set</summary>
        public double? get_radiusI() { var arr = GetDatatypeProperty_double("radiusI"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of radiusII</summary>
        public void set_radiusII(double value) { SetDatatypeProperty ("radiusII", value); }
        ///<summary>Gets value of radiusII, returns null is the property was not set</summary>
        public double? get_radiusII() { var arr = GetDatatypeProperty_double("radiusII"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class EllipticCone
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class EllipticCone : Solid
    {
        /// <summary>
        /// Create new instace of OWL class EllipticCone and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new EllipticCone Create(Int64 model, string name=null) { return new EllipticCone(Instance.Create(model, "EllipticCone", name), "EllipticCone");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public EllipticCone(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "EllipticCone") 
        {            
        }

       //
       // Properties with known cardinality restrictions to EllipticCone
       //

        ///<summary>Sets value of height</summary>
        public void set_height(double value) { SetDatatypeProperty ("height", value); }
        ///<summary>Gets value of height, returns null is the property was not set</summary>
        public double? get_height() { var arr = GetDatatypeProperty_double("height"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of radiusI</summary>
        public void set_radiusI(double value) { SetDatatypeProperty ("radiusI", value); }
        ///<summary>Gets value of radiusI, returns null is the property was not set</summary>
        public double? get_radiusI() { var arr = GetDatatypeProperty_double("radiusI"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of radiusII</summary>
        public void set_radiusII(double value) { SetDatatypeProperty ("radiusII", value); }
        ///<summary>Gets value of radiusII, returns null is the property was not set</summary>
        public double? get_radiusII() { var arr = GetDatatypeProperty_double("radiusII"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of segmentationParts</summary>
        public void set_segmentationParts(long value) { SetDatatypeProperty ("segmentationParts", value); }
        ///<summary>Gets value of segmentationParts, returns null is the property was not set</summary>
        public long? get_segmentationParts() { var arr = GetDatatypeProperty_long("segmentationParts"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Environment
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Environment : Instance
    {
        /// <summary>
        /// Create new instace of OWL class Environment and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Environment Create(Int64 model, string name=null) { return new Environment(Instance.Create(model, "Environment", name), "Environment");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Environment(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Environment") 
        {            
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class ExtrudedPolygon
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class ExtrudedPolygon : Solid
    {
        /// <summary>
        /// Create new instace of OWL class ExtrudedPolygon and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new ExtrudedPolygon Create(Int64 model, string name=null) { return new ExtrudedPolygon(Instance.Create(model, "ExtrudedPolygon", name), "ExtrudedPolygon");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public ExtrudedPolygon(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "ExtrudedPolygon") 
        {            
        }

       //
       // Properties with known cardinality restrictions to ExtrudedPolygon
       //

        ///<summary>Sets value of extrusionLength</summary>
        public void set_extrusionLength(double value) { SetDatatypeProperty ("extrusionLength", value); }
        ///<summary>Gets value of extrusionLength, returns null is the property was not set</summary>
        public double? get_extrusionLength() { var arr = GetDatatypeProperty_double("extrusionLength"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of fraction</summary>
        public void set_fraction(double value) { SetDatatypeProperty ("fraction", value); }
        ///<summary>Gets value of fraction, returns null is the property was not set</summary>
        public double? get_fraction() { var arr = GetDatatypeProperty_double("fraction"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets values of openingPoints. OWL cardinality 0..-1</summary>
        public void set_openingPoints(double[] values) { SetDatatypeProperty ("openingPoints", values); }
        ///<summary>Gets values of openingPoints. OWL cardinality 0..-1</summary>
        public double[] get_openingPoints() { return GetDatatypeProperty_double("openingPoints"); }
        ///<summary>Sets values of openingSizes. OWL cardinality 0..-1</summary>
        public void set_openingSizes(long[] values) { SetDatatypeProperty ("openingSizes", values); }
        ///<summary>Gets values of openingSizes. OWL cardinality 0..-1</summary>
        public long[] get_openingSizes() { return GetDatatypeProperty_long("openingSizes"); }
        ///<summary>Sets values of points. OWL cardinality 6..-1</summary>
        public void set_points(double[] values) { SetDatatypeProperty ("points", values); }
        ///<summary>Gets values of points. OWL cardinality 6..-1</summary>
        public double[] get_points() { return GetDatatypeProperty_double("points"); }
        ///<summary>Sets values of polygonDirection. OWL cardinality 0..3</summary>
        public void set_polygonDirection(double[] values) { SetDatatypeProperty ("polygonDirection", values); }
        ///<summary>Gets values of polygonDirection. OWL cardinality 0..3</summary>
        public double[] get_polygonDirection() { return GetDatatypeProperty_double("polygonDirection"); }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class ExtrudedPolygonTapered
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class ExtrudedPolygonTapered : Solid
    {
        /// <summary>
        /// Create new instace of OWL class ExtrudedPolygonTapered and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new ExtrudedPolygonTapered Create(Int64 model, string name=null) { return new ExtrudedPolygonTapered(Instance.Create(model, "ExtrudedPolygonTapered", name), "ExtrudedPolygonTapered");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public ExtrudedPolygonTapered(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "ExtrudedPolygonTapered") 
        {            
        }

       //
       // Properties with known cardinality restrictions to ExtrudedPolygonTapered
       //

        ///<summary>Sets value of extrusionLength</summary>
        public void set_extrusionLength(double value) { SetDatatypeProperty ("extrusionLength", value); }
        ///<summary>Gets value of extrusionLength, returns null is the property was not set</summary>
        public double? get_extrusionLength() { var arr = GetDatatypeProperty_double("extrusionLength"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of fraction</summary>
        public void set_fraction(double value) { SetDatatypeProperty ("fraction", value); }
        ///<summary>Gets value of fraction, returns null is the property was not set</summary>
        public double? get_fraction() { var arr = GetDatatypeProperty_double("fraction"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets values of openingPoints. OWL cardinality 0..-1</summary>
        public void set_openingPoints(double[] values) { SetDatatypeProperty ("openingPoints", values); }
        ///<summary>Gets values of openingPoints. OWL cardinality 0..-1</summary>
        public double[] get_openingPoints() { return GetDatatypeProperty_double("openingPoints"); }
        ///<summary>Sets values of openingPointsEnd. OWL cardinality 0..-1</summary>
        public void set_openingPointsEnd(double[] values) { SetDatatypeProperty ("openingPointsEnd", values); }
        ///<summary>Gets values of openingPointsEnd. OWL cardinality 0..-1</summary>
        public double[] get_openingPointsEnd() { return GetDatatypeProperty_double("openingPointsEnd"); }
        ///<summary>Sets values of openingSizes. OWL cardinality 0..-1</summary>
        public void set_openingSizes(long[] values) { SetDatatypeProperty ("openingSizes", values); }
        ///<summary>Gets values of openingSizes. OWL cardinality 0..-1</summary>
        public long[] get_openingSizes() { return GetDatatypeProperty_long("openingSizes"); }
        ///<summary>Sets values of openingSizesEnd. OWL cardinality 0..-1</summary>
        public void set_openingSizesEnd(long[] values) { SetDatatypeProperty ("openingSizesEnd", values); }
        ///<summary>Gets values of openingSizesEnd. OWL cardinality 0..-1</summary>
        public long[] get_openingSizesEnd() { return GetDatatypeProperty_long("openingSizesEnd"); }
        ///<summary>Sets values of points. OWL cardinality 6..-1</summary>
        public void set_points(double[] values) { SetDatatypeProperty ("points", values); }
        ///<summary>Gets values of points. OWL cardinality 6..-1</summary>
        public double[] get_points() { return GetDatatypeProperty_double("points"); }
        ///<summary>Sets values of pointsEnd. OWL cardinality 6..-1</summary>
        public void set_pointsEnd(double[] values) { SetDatatypeProperty ("pointsEnd", values); }
        ///<summary>Gets values of pointsEnd. OWL cardinality 6..-1</summary>
        public double[] get_pointsEnd() { return GetDatatypeProperty_double("pointsEnd"); }
        ///<summary>Sets values of polygonDirection. OWL cardinality 0..3</summary>
        public void set_polygonDirection(double[] values) { SetDatatypeProperty ("polygonDirection", values); }
        ///<summary>Gets values of polygonDirection. OWL cardinality 0..3</summary>
        public double[] get_polygonDirection() { return GetDatatypeProperty_double("polygonDirection"); }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class ExtrusionAreaSolid
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class ExtrusionAreaSolid : Solid
    {
        /// <summary>
        /// Create new instace of OWL class ExtrusionAreaSolid and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new ExtrusionAreaSolid Create(Int64 model, string name=null) { return new ExtrusionAreaSolid(Instance.Create(model, "ExtrusionAreaSolid", name), "ExtrusionAreaSolid");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public ExtrusionAreaSolid(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "ExtrusionAreaSolid") 
        {            
        }

       //
       // Properties with known cardinality restrictions to ExtrusionAreaSolid
       //

        ///<summary>Sets relationship from this instance to an instance of Curve</summary>
        public void set_extrusionArea(Curve instance) { SetObjectProperty("extrusionArea", instance); }
        ///<summary>Get related instance</summary>
        public Curve get_extrusionArea() 
        {
            var propId = GetPropertyId("extrusionArea");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Curve(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets relationships from this instance to an array of Curve. OWL cardinality 0..-1</summary>
        public void set_extrusionAreaOpenings(Curve[] instances) { SetObjectProperty("extrusionAreaOpenings", instances); }
        ///<summary>Sets relationships from this instance to an array of Int64. OWL cardinality 0..-1</summary>
        public void set_extrusionAreaOpenings(Int64[] instances) { SetObjectProperty("extrusionAreaOpenings", instances); }
        ///<summary>Get an array of related instances. OWL cardinality 0..-1</summary>
        public Curve[] get_extrusionAreaOpenings() 
        {
            var propId = GetPropertyId("extrusionAreaOpenings");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                var ret = new Curve[card];
                for (int i = 0; i < card; i++)
                {
                    ret[i] = new Curve(values[i], null);
                }

                return ret;
            }
            else
            {
                return null;
            }
        }
        ///<summary>Get an array of handles of related instances. OWL cardinality 0..-1</summary>
        public Int64[] get_extrusionAreaOpenings_Int64()  
        {
            var propId = GetPropertyId("extrusionAreaOpenings");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                return values;
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets values of extrusionDirection. OWL cardinality 0..3</summary>
        public void set_extrusionDirection(double[] values) { SetDatatypeProperty ("extrusionDirection", values); }
        ///<summary>Gets values of extrusionDirection. OWL cardinality 0..3</summary>
        public double[] get_extrusionDirection() { return GetDatatypeProperty_double("extrusionDirection"); }
        ///<summary>Sets value of extrusionLength</summary>
        public void set_extrusionLength(double value) { SetDatatypeProperty ("extrusionLength", value); }
        ///<summary>Gets value of extrusionLength, returns null is the property was not set</summary>
        public double? get_extrusionLength() { var arr = GetDatatypeProperty_double("extrusionLength"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of fraction</summary>
        public void set_fraction(double value) { SetDatatypeProperty ("fraction", value); }
        ///<summary>Gets value of fraction, returns null is the property was not set</summary>
        public double? get_fraction() { var arr = GetDatatypeProperty_double("fraction"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class ExtrusionAreaSolidSet
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class ExtrusionAreaSolidSet : Solid
    {
        /// <summary>
        /// Create new instace of OWL class ExtrusionAreaSolidSet and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new ExtrusionAreaSolidSet Create(Int64 model, string name=null) { return new ExtrusionAreaSolidSet(Instance.Create(model, "ExtrusionAreaSolidSet", name), "ExtrusionAreaSolidSet");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public ExtrusionAreaSolidSet(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "ExtrusionAreaSolidSet") 
        {            
        }

       //
       // Properties with known cardinality restrictions to ExtrusionAreaSolidSet
       //

        ///<summary>Sets relationships from this instance to an array of Curve. OWL cardinality 1..-1</summary>
        public void set_extrusionAreaSet(Curve[] instances) { SetObjectProperty("extrusionAreaSet", instances); }
        ///<summary>Sets relationships from this instance to an array of Int64. OWL cardinality 1..-1</summary>
        public void set_extrusionAreaSet(Int64[] instances) { SetObjectProperty("extrusionAreaSet", instances); }
        ///<summary>Get an array of related instances. OWL cardinality 1..-1</summary>
        public Curve[] get_extrusionAreaSet() 
        {
            var propId = GetPropertyId("extrusionAreaSet");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                var ret = new Curve[card];
                for (int i = 0; i < card; i++)
                {
                    ret[i] = new Curve(values[i], null);
                }

                return ret;
            }
            else
            {
                return null;
            }
        }
        ///<summary>Get an array of handles of related instances. OWL cardinality 1..-1</summary>
        public Int64[] get_extrusionAreaSet_Int64()  
        {
            var propId = GetPropertyId("extrusionAreaSet");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                return values;
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets values of extrusionDirection. OWL cardinality 0..3</summary>
        public void set_extrusionDirection(double[] values) { SetDatatypeProperty ("extrusionDirection", values); }
        ///<summary>Gets values of extrusionDirection. OWL cardinality 0..3</summary>
        public double[] get_extrusionDirection() { return GetDatatypeProperty_double("extrusionDirection"); }
        ///<summary>Sets value of extrusionLength</summary>
        public void set_extrusionLength(double value) { SetDatatypeProperty ("extrusionLength", value); }
        ///<summary>Gets value of extrusionLength, returns null is the property was not set</summary>
        public double? get_extrusionLength() { var arr = GetDatatypeProperty_double("extrusionLength"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of fraction</summary>
        public void set_fraction(double value) { SetDatatypeProperty ("fraction", value); }
        ///<summary>Gets value of fraction, returns null is the property was not set</summary>
        public double? get_fraction() { var arr = GetDatatypeProperty_double("fraction"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Face
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Face : GeometricItem
    {
        /// <summary>
        /// Create new instace of OWL class Face and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Face Create(Int64 model, string name=null) { return new Face(Instance.Create(model, "Face", name), "Face");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Face(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Face") 
        {            
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Face2D
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Face2D : Face
    {
        /// <summary>
        /// Create new instace of OWL class Face2D and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Face2D Create(Int64 model, string name=null) { return new Face2D(Instance.Create(model, "Face2D", name), "Face2D");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Face2D(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Face2D") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Face2D
       //

        ///<summary>Sets relationships from this instance to an array of Curve. OWL cardinality 0..-1</summary>
        public void set_innerPolygons(Curve[] instances) { SetObjectProperty("innerPolygons", instances); }
        ///<summary>Sets relationships from this instance to an array of Int64. OWL cardinality 0..-1</summary>
        public void set_innerPolygons(Int64[] instances) { SetObjectProperty("innerPolygons", instances); }
        ///<summary>Get an array of related instances. OWL cardinality 0..-1</summary>
        public Curve[] get_innerPolygons() 
        {
            var propId = GetPropertyId("innerPolygons");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                var ret = new Curve[card];
                for (int i = 0; i < card; i++)
                {
                    ret[i] = new Curve(values[i], null);
                }

                return ret;
            }
            else
            {
                return null;
            }
        }
        ///<summary>Get an array of handles of related instances. OWL cardinality 0..-1</summary>
        public Int64[] get_innerPolygons_Int64()  
        {
            var propId = GetPropertyId("innerPolygons");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                return values;
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets relationship from this instance to an instance of Curve</summary>
        public void set_outerPolygon(Curve instance) { SetObjectProperty("outerPolygon", instance); }
        ///<summary>Get related instance</summary>
        public Curve get_outerPolygon() 
        {
            var propId = GetPropertyId("outerPolygon");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Curve(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets value of setting</summary>
        public void set_setting(long value) { SetDatatypeProperty ("setting", value); }
        ///<summary>Gets value of setting, returns null is the property was not set</summary>
        public long? get_setting() { var arr = GetDatatypeProperty_long("setting"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Face2DSet
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Face2DSet : Face
    {
        /// <summary>
        /// Create new instace of OWL class Face2DSet and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Face2DSet Create(Int64 model, string name=null) { return new Face2DSet(Instance.Create(model, "Face2DSet", name), "Face2DSet");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Face2DSet(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Face2DSet") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Face2DSet
       //

        ///<summary>Sets relationships from this instance to an array of Curve. OWL cardinality 0..-1</summary>
        public void set_polygons(Curve[] instances) { SetObjectProperty("polygons", instances); }
        ///<summary>Sets relationships from this instance to an array of Int64. OWL cardinality 0..-1</summary>
        public void set_polygons(Int64[] instances) { SetObjectProperty("polygons", instances); }
        ///<summary>Get an array of related instances. OWL cardinality 0..-1</summary>
        public Curve[] get_polygons() 
        {
            var propId = GetPropertyId("polygons");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                var ret = new Curve[card];
                for (int i = 0; i < card; i++)
                {
                    ret[i] = new Curve(values[i], null);
                }

                return ret;
            }
            else
            {
                return null;
            }
        }
        ///<summary>Get an array of handles of related instances. OWL cardinality 0..-1</summary>
        public Int64[] get_polygons_Int64()  
        {
            var propId = GetPropertyId("polygons");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                return values;
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets value of setting</summary>
        public void set_setting(long value) { SetDatatypeProperty ("setting", value); }
        ///<summary>Gets value of setting, returns null is the property was not set</summary>
        public long? get_setting() { var arr = GetDatatypeProperty_long("setting"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class FiniteSurface
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class FiniteSurface : Surface
    {
        /// <summary>
        /// Create new instace of OWL class FiniteSurface and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new FiniteSurface Create(Int64 model, string name=null) { return new FiniteSurface(Instance.Create(model, "FiniteSurface", name), "FiniteSurface");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public FiniteSurface(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "FiniteSurface") 
        {            
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class FrustumCone
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class FrustumCone : Solid
    {
        /// <summary>
        /// Create new instace of OWL class FrustumCone and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new FrustumCone Create(Int64 model, string name=null) { return new FrustumCone(Instance.Create(model, "FrustumCone", name), "FrustumCone");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public FrustumCone(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "FrustumCone") 
        {            
        }

       //
       // Properties with known cardinality restrictions to FrustumCone
       //

        ///<summary>Sets value of factor</summary>
        public void set_factor(double value) { SetDatatypeProperty ("factor", value); }
        ///<summary>Gets value of factor, returns null is the property was not set</summary>
        public double? get_factor() { var arr = GetDatatypeProperty_double("factor"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of height</summary>
        public void set_height(double value) { SetDatatypeProperty ("height", value); }
        ///<summary>Gets value of height, returns null is the property was not set</summary>
        public double? get_height() { var arr = GetDatatypeProperty_double("height"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of radius</summary>
        public void set_radius(double value) { SetDatatypeProperty ("radius", value); }
        ///<summary>Gets value of radius, returns null is the property was not set</summary>
        public double? get_radius() { var arr = GetDatatypeProperty_double("radius"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of segmentationParts</summary>
        public void set_segmentationParts(long value) { SetDatatypeProperty ("segmentationParts", value); }
        ///<summary>Gets value of segmentationParts, returns null is the property was not set</summary>
        public long? get_segmentationParts() { var arr = GetDatatypeProperty_long("segmentationParts"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class GeometricItem
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class GeometricItem : Instance
    {
        /// <summary>
        /// Create new instace of OWL class GeometricItem and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new GeometricItem Create(Int64 model, string name=null) { return new GeometricItem(Instance.Create(model, "GeometricItem", name), "GeometricItem");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public GeometricItem(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "GeometricItem") 
        {            
        }

       //
       // Properties with known cardinality restrictions to GeometricItem
       //

        ///<summary>Sets relationship from this instance to an instance of Material</summary>
        public void set_material(Material instance) { SetObjectProperty("material", instance); }
        ///<summary>Get related instance</summary>
        public Material get_material() 
        {
            var propId = GetPropertyId("material");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Material(values[0], null);
            }
            else
            {
                return null;
            }
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class HornTorus
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class HornTorus : Torus
    {
        /// <summary>
        /// Create new instace of OWL class HornTorus and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new HornTorus Create(Int64 model, string name=null) { return new HornTorus(Instance.Create(model, "HornTorus", name), "HornTorus");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public HornTorus(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "HornTorus") 
        {            
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Hyperbola
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Hyperbola : ConicalCurve
    {
        /// <summary>
        /// Create new instace of OWL class Hyperbola and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Hyperbola Create(Int64 model, string name=null) { return new Hyperbola(Instance.Create(model, "Hyperbola", name), "Hyperbola");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Hyperbola(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Hyperbola") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Hyperbola
       //

        ///<summary>Sets value of b</summary>
        public void set_b(double value) { SetDatatypeProperty ("b", value); }
        ///<summary>Gets value of b, returns null is the property was not set</summary>
        public double? get_b() { var arr = GetDatatypeProperty_double("b"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class InfiniteSurface
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class InfiniteSurface : Surface
    {
        /// <summary>
        /// Create new instace of OWL class InfiniteSurface and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new InfiniteSurface Create(Int64 model, string name=null) { return new InfiniteSurface(Instance.Create(model, "InfiniteSurface", name), "InfiniteSurface");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public InfiniteSurface(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "InfiniteSurface") 
        {            
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class InverseMatrix
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class InverseMatrix : Matrix
    {
        /// <summary>
        /// Create new instace of OWL class InverseMatrix and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new InverseMatrix Create(Int64 model, string name=null) { return new InverseMatrix(Instance.Create(model, "InverseMatrix", name), "InverseMatrix");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public InverseMatrix(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "InverseMatrix") 
        {            
        }

       //
       // Properties with known cardinality restrictions to InverseMatrix
       //

        ///<summary>Sets relationship from this instance to an instance of Matrix</summary>
        public void set_matrix(Matrix instance) { SetObjectProperty("matrix", instance); }
        ///<summary>Get related instance</summary>
        public Matrix get_matrix() 
        {
            var propId = GetPropertyId("matrix");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Matrix(values[0], null);
            }
            else
            {
                return null;
            }
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class InvertedCurve
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class InvertedCurve : Curve
    {
        /// <summary>
        /// Create new instace of OWL class InvertedCurve and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new InvertedCurve Create(Int64 model, string name=null) { return new InvertedCurve(Instance.Create(model, "InvertedCurve", name), "InvertedCurve");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public InvertedCurve(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "InvertedCurve") 
        {            
        }

       //
       // Properties with known cardinality restrictions to InvertedCurve
       //

        ///<summary>Sets relationship from this instance to an instance of Curve</summary>
        public void set_curve(Curve instance) { SetObjectProperty("curve", instance); }
        ///<summary>Get related instance</summary>
        public Curve get_curve() 
        {
            var propId = GetPropertyId("curve");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Curve(values[0], null);
            }
            else
            {
                return null;
            }
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class InvertedSurface
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class InvertedSurface : Surface
    {
        /// <summary>
        /// Create new instace of OWL class InvertedSurface and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new InvertedSurface Create(Int64 model, string name=null) { return new InvertedSurface(Instance.Create(model, "InvertedSurface", name), "InvertedSurface");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public InvertedSurface(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "InvertedSurface") 
        {            
        }

       //
       // Properties with known cardinality restrictions to InvertedSurface
       //

        ///<summary>Sets relationship from this instance to an instance of Surface</summary>
        public void set_surface(Surface instance) { SetObjectProperty("surface", instance); }
        ///<summary>Get related instance</summary>
        public Surface get_surface() 
        {
            var propId = GetPropertyId("surface");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Surface(values[0], null);
            }
            else
            {
                return null;
            }
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Light
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Light : Environment
    {
        /// <summary>
        /// Create new instace of OWL class Light and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Light Create(Int64 model, string name=null) { return new Light(Instance.Create(model, "Light", name), "Light");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Light(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Light") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Light
       //

        ///<summary>Sets relationship from this instance to an instance of Color</summary>
        public void set_color(Color instance) { SetObjectProperty("color", instance); }
        ///<summary>Get related instance</summary>
        public Color get_color() 
        {
            var propId = GetPropertyId("color");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Color(values[0], null);
            }
            else
            {
                return null;
            }
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Line3D
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Line3D : Curve
    {
        /// <summary>
        /// Create new instace of OWL class Line3D and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Line3D Create(Int64 model, string name=null) { return new Line3D(Instance.Create(model, "Line3D", name), "Line3D");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Line3D(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Line3D") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Line3D
       //

        ///<summary>Sets value of asOpenGL</summary>
        public void set_asOpenGL(bool value) { SetDatatypeProperty ("asOpenGL", value); }
        ///<summary>Gets value of asOpenGL, returns null is the property was not set</summary>
        public bool? get_asOpenGL() { var arr = GetDatatypeProperty_bool("asOpenGL"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets values of points. OWL cardinality 6..6</summary>
        public void set_points(double[] values) { SetDatatypeProperty ("points", values); }
        ///<summary>Gets values of points. OWL cardinality 6..6</summary>
        public double[] get_points() { return GetDatatypeProperty_double("points"); }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Line3Dn
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Line3Dn : Curve
    {
        /// <summary>
        /// Create new instace of OWL class Line3Dn and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Line3Dn Create(Int64 model, string name=null) { return new Line3Dn(Instance.Create(model, "Line3Dn", name), "Line3Dn");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Line3Dn(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Line3Dn") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Line3Dn
       //

        ///<summary>Sets value of asOpenGL</summary>
        public void set_asOpenGL(bool value) { SetDatatypeProperty ("asOpenGL", value); }
        ///<summary>Gets value of asOpenGL, returns null is the property was not set</summary>
        public bool? get_asOpenGL() { var arr = GetDatatypeProperty_bool("asOpenGL"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets values of endDirection. OWL cardinality 0..3</summary>
        public void set_endDirection(double[] values) { SetDatatypeProperty ("endDirection", values); }
        ///<summary>Gets values of endDirection. OWL cardinality 0..3</summary>
        public double[] get_endDirection() { return GetDatatypeProperty_double("endDirection"); }
        ///<summary>Sets values of points. OWL cardinality 0..6</summary>
        public void set_points(double[] values) { SetDatatypeProperty ("points", values); }
        ///<summary>Gets values of points. OWL cardinality 0..6</summary>
        public double[] get_points() { return GetDatatypeProperty_double("points"); }
        ///<summary>Sets values of startDirection. OWL cardinality 0..3</summary>
        public void set_startDirection(double[] values) { SetDatatypeProperty ("startDirection", values); }
        ///<summary>Gets values of startDirection. OWL cardinality 0..3</summary>
        public double[] get_startDirection() { return GetDatatypeProperty_double("startDirection"); }
        ///<summary>Sets value of x0</summary>
        public void set_x0(double value) { SetDatatypeProperty ("x0", value); }
        ///<summary>Gets value of x0, returns null is the property was not set</summary>
        public double? get_x0() { var arr = GetDatatypeProperty_double("x0"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of x1</summary>
        public void set_x1(double value) { SetDatatypeProperty ("x1", value); }
        ///<summary>Gets value of x1, returns null is the property was not set</summary>
        public double? get_x1() { var arr = GetDatatypeProperty_double("x1"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of y0</summary>
        public void set_y0(double value) { SetDatatypeProperty ("y0", value); }
        ///<summary>Gets value of y0, returns null is the property was not set</summary>
        public double? get_y0() { var arr = GetDatatypeProperty_double("y0"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of y1</summary>
        public void set_y1(double value) { SetDatatypeProperty ("y1", value); }
        ///<summary>Gets value of y1, returns null is the property was not set</summary>
        public double? get_y1() { var arr = GetDatatypeProperty_double("y1"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of z0</summary>
        public void set_z0(double value) { SetDatatypeProperty ("z0", value); }
        ///<summary>Gets value of z0, returns null is the property was not set</summary>
        public double? get_z0() { var arr = GetDatatypeProperty_double("z0"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of z1</summary>
        public void set_z1(double value) { SetDatatypeProperty ("z1", value); }
        ///<summary>Gets value of z1, returns null is the property was not set</summary>
        public double? get_z1() { var arr = GetDatatypeProperty_double("z1"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Line3DSet
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Line3DSet : Curve
    {
        /// <summary>
        /// Create new instace of OWL class Line3DSet and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Line3DSet Create(Int64 model, string name=null) { return new Line3DSet(Instance.Create(model, "Line3DSet", name), "Line3DSet");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Line3DSet(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Line3DSet") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Line3DSet
       //

        ///<summary>Sets value of asOpenGL</summary>
        public void set_asOpenGL(bool value) { SetDatatypeProperty ("asOpenGL", value); }
        ///<summary>Gets value of asOpenGL, returns null is the property was not set</summary>
        public bool? get_asOpenGL() { var arr = GetDatatypeProperty_bool("asOpenGL"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets values of points. OWL cardinality 0..-1</summary>
        public void set_points(double[] values) { SetDatatypeProperty ("points", values); }
        ///<summary>Gets values of points. OWL cardinality 0..-1</summary>
        public double[] get_points() { return GetDatatypeProperty_double("points"); }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class LineByFace
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class LineByFace : Curve
    {
        /// <summary>
        /// Create new instace of OWL class LineByFace and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new LineByFace Create(Int64 model, string name=null) { return new LineByFace(Instance.Create(model, "LineByFace", name), "LineByFace");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public LineByFace(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "LineByFace") 
        {            
        }

       //
       // Properties with known cardinality restrictions to LineByFace
       //

        ///<summary>Sets relationships from this instance to an array of Face. OWL cardinality 1..-1</summary>
        public void set_faces(Face[] instances) { SetObjectProperty("faces", instances); }
        ///<summary>Sets relationships from this instance to an array of Int64. OWL cardinality 1..-1</summary>
        public void set_faces(Int64[] instances) { SetObjectProperty("faces", instances); }
        ///<summary>Get an array of related instances. OWL cardinality 1..-1</summary>
        public Face[] get_faces() 
        {
            var propId = GetPropertyId("faces");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                var ret = new Face[card];
                for (int i = 0; i < card; i++)
                {
                    ret[i] = new Face(values[i], null);
                }

                return ret;
            }
            else
            {
                return null;
            }
        }
        ///<summary>Get an array of handles of related instances. OWL cardinality 1..-1</summary>
        public Int64[] get_faces_Int64()  
        {
            var propId = GetPropertyId("faces");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                return values;
            }
            else
            {
                return null;
            }
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Material
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Material : Appearance
    {
        /// <summary>
        /// Create new instace of OWL class Material and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Material Create(Int64 model, string name=null) { return new Material(Instance.Create(model, "Material", name), "Material");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Material(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Material") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Material
       //

        ///<summary>Sets relationship from this instance to an instance of Color</summary>
        public void set_color(Color instance) { SetObjectProperty("color", instance); }
        ///<summary>Get related instance</summary>
        public Color get_color() 
        {
            var propId = GetPropertyId("color");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Color(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets relationships from this instance to an array of Texture. OWL cardinality 0..2</summary>
        public void set_textures(Texture[] instances) { SetObjectProperty("textures", instances); }
        ///<summary>Sets relationships from this instance to an array of Int64. OWL cardinality 0..2</summary>
        public void set_textures(Int64[] instances) { SetObjectProperty("textures", instances); }
        ///<summary>Get an array of related instances. OWL cardinality 0..2</summary>
        public Texture[] get_textures() 
        {
            var propId = GetPropertyId("textures");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                var ret = new Texture[card];
                for (int i = 0; i < card; i++)
                {
                    ret[i] = new Texture(values[i], null);
                }

                return ret;
            }
            else
            {
                return null;
            }
        }
        ///<summary>Get an array of handles of related instances. OWL cardinality 0..2</summary>
        public Int64[] get_textures_Int64()  
        {
            var propId = GetPropertyId("textures");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                return values;
            }
            else
            {
                return null;
            }
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Mathematics
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Mathematics : Instance
    {
        /// <summary>
        /// Create new instace of OWL class Mathematics and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Mathematics Create(Int64 model, string name=null) { return new Mathematics(Instance.Create(model, "Mathematics", name), "Mathematics");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Mathematics(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Mathematics") 
        {            
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Matrix
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Matrix : Mathematics
    {
        /// <summary>
        /// Create new instace of OWL class Matrix and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Matrix Create(Int64 model, string name=null) { return new Matrix(Instance.Create(model, "Matrix", name), "Matrix");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Matrix(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Matrix") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Matrix
       //

        ///<summary>Sets value of _11</summary>
        public void set__11(double value) { SetDatatypeProperty ("_11", value); }
        ///<summary>Gets value of _11, returns null is the property was not set</summary>
        public double? get__11() { var arr = GetDatatypeProperty_double("_11"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of _12</summary>
        public void set__12(double value) { SetDatatypeProperty ("_12", value); }
        ///<summary>Gets value of _12, returns null is the property was not set</summary>
        public double? get__12() { var arr = GetDatatypeProperty_double("_12"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of _13</summary>
        public void set__13(double value) { SetDatatypeProperty ("_13", value); }
        ///<summary>Gets value of _13, returns null is the property was not set</summary>
        public double? get__13() { var arr = GetDatatypeProperty_double("_13"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of _21</summary>
        public void set__21(double value) { SetDatatypeProperty ("_21", value); }
        ///<summary>Gets value of _21, returns null is the property was not set</summary>
        public double? get__21() { var arr = GetDatatypeProperty_double("_21"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of _22</summary>
        public void set__22(double value) { SetDatatypeProperty ("_22", value); }
        ///<summary>Gets value of _22, returns null is the property was not set</summary>
        public double? get__22() { var arr = GetDatatypeProperty_double("_22"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of _23</summary>
        public void set__23(double value) { SetDatatypeProperty ("_23", value); }
        ///<summary>Gets value of _23, returns null is the property was not set</summary>
        public double? get__23() { var arr = GetDatatypeProperty_double("_23"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of _31</summary>
        public void set__31(double value) { SetDatatypeProperty ("_31", value); }
        ///<summary>Gets value of _31, returns null is the property was not set</summary>
        public double? get__31() { var arr = GetDatatypeProperty_double("_31"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of _32</summary>
        public void set__32(double value) { SetDatatypeProperty ("_32", value); }
        ///<summary>Gets value of _32, returns null is the property was not set</summary>
        public double? get__32() { var arr = GetDatatypeProperty_double("_32"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of _33</summary>
        public void set__33(double value) { SetDatatypeProperty ("_33", value); }
        ///<summary>Gets value of _33, returns null is the property was not set</summary>
        public double? get__33() { var arr = GetDatatypeProperty_double("_33"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of _41</summary>
        public void set__41(double value) { SetDatatypeProperty ("_41", value); }
        ///<summary>Gets value of _41, returns null is the property was not set</summary>
        public double? get__41() { var arr = GetDatatypeProperty_double("_41"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of _42</summary>
        public void set__42(double value) { SetDatatypeProperty ("_42", value); }
        ///<summary>Gets value of _42, returns null is the property was not set</summary>
        public double? get__42() { var arr = GetDatatypeProperty_double("_42"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of _43</summary>
        public void set__43(double value) { SetDatatypeProperty ("_43", value); }
        ///<summary>Gets value of _43, returns null is the property was not set</summary>
        public double? get__43() { var arr = GetDatatypeProperty_double("_43"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets values of coordinates. OWL cardinality 0..12</summary>
        public void set_coordinates(double[] values) { SetDatatypeProperty ("coordinates", values); }
        ///<summary>Gets values of coordinates. OWL cardinality 0..12</summary>
        public double[] get_coordinates() { return GetDatatypeProperty_double("coordinates"); }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class MatrixMultiplication
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class MatrixMultiplication : Matrix
    {
        /// <summary>
        /// Create new instace of OWL class MatrixMultiplication and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new MatrixMultiplication Create(Int64 model, string name=null) { return new MatrixMultiplication(Instance.Create(model, "MatrixMultiplication", name), "MatrixMultiplication");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public MatrixMultiplication(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "MatrixMultiplication") 
        {            
        }

       //
       // Properties with known cardinality restrictions to MatrixMultiplication
       //

        ///<summary>Sets relationship from this instance to an instance of Matrix</summary>
        public void set_firstMatrix(Matrix instance) { SetObjectProperty("firstMatrix", instance); }
        ///<summary>Get related instance</summary>
        public Matrix get_firstMatrix() 
        {
            var propId = GetPropertyId("firstMatrix");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Matrix(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets relationship from this instance to an instance of Matrix</summary>
        public void set_secondMatrix(Matrix instance) { SetObjectProperty("secondMatrix", instance); }
        ///<summary>Get related instance</summary>
        public Matrix get_secondMatrix() 
        {
            var propId = GetPropertyId("secondMatrix");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Matrix(values[0], null);
            }
            else
            {
                return null;
            }
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Mesh
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Mesh : Face
    {
        /// <summary>
        /// Create new instace of OWL class Mesh and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Mesh Create(Int64 model, string name=null) { return new Mesh(Instance.Create(model, "Mesh", name), "Mesh");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Mesh(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Mesh") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Mesh
       //

        ///<summary>Sets relationship from this instance to an instance of Point3DSet</summary>
        public void set_pointSet(Point3DSet instance) { SetObjectProperty("pointSet", instance); }
        ///<summary>Get related instance</summary>
        public Point3DSet get_pointSet() 
        {
            var propId = GetPropertyId("pointSet");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Point3DSet(values[0], null);
            }
            else
            {
                return null;
            }
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class NURBSCurve
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class NURBSCurve : BSplineCurve
    {
        /// <summary>
        /// Create new instace of OWL class NURBSCurve and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new NURBSCurve Create(Int64 model, string name=null) { return new NURBSCurve(Instance.Create(model, "NURBSCurve", name), "NURBSCurve");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public NURBSCurve(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "NURBSCurve") 
        {            
        }

       //
       // Properties with known cardinality restrictions to NURBSCurve
       //

        ///<summary>Sets values of weights. OWL cardinality 2..-1</summary>
        public void set_weights(double[] values) { SetDatatypeProperty ("weights", values); }
        ///<summary>Gets values of weights. OWL cardinality 2..-1</summary>
        public double[] get_weights() { return GetDatatypeProperty_double("weights"); }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class NURBSSurface
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class NURBSSurface : BSplineSurface
    {
        /// <summary>
        /// Create new instace of OWL class NURBSSurface and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new NURBSSurface Create(Int64 model, string name=null) { return new NURBSSurface(Instance.Create(model, "NURBSSurface", name), "NURBSSurface");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public NURBSSurface(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "NURBSSurface") 
        {            
        }

       //
       // Properties with known cardinality restrictions to NURBSSurface
       //

        ///<summary>Sets values of weights. OWL cardinality 4..-1</summary>
        public void set_weights(double[] values) { SetDatatypeProperty ("weights", values); }
        ///<summary>Gets values of weights. OWL cardinality 4..-1</summary>
        public double[] get_weights() { return GetDatatypeProperty_double("weights"); }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Parabola
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Parabola : ConicalCurve
    {
        /// <summary>
        /// Create new instace of OWL class Parabola and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Parabola Create(Int64 model, string name=null) { return new Parabola(Instance.Create(model, "Parabola", name), "Parabola");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Parabola(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Parabola") 
        {            
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Plane
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Plane : Mathematics
    {
        /// <summary>
        /// Create new instace of OWL class Plane and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Plane Create(Int64 model, string name=null) { return new Plane(Instance.Create(model, "Plane", name), "Plane");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Plane(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Plane") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Plane
       //

        ///<summary>Sets value of A</summary>
        public void set_A(double value) { SetDatatypeProperty ("A", value); }
        ///<summary>Gets value of A, returns null is the property was not set</summary>
        public double? get_A() { var arr = GetDatatypeProperty_double("A"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of B</summary>
        public void set_B(double value) { SetDatatypeProperty ("B", value); }
        ///<summary>Gets value of B, returns null is the property was not set</summary>
        public double? get_B() { var arr = GetDatatypeProperty_double("B"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of C</summary>
        public void set_C(double value) { SetDatatypeProperty ("C", value); }
        ///<summary>Gets value of C, returns null is the property was not set</summary>
        public double? get_C() { var arr = GetDatatypeProperty_double("C"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of D</summary>
        public void set_D(double value) { SetDatatypeProperty ("D", value); }
        ///<summary>Gets value of D, returns null is the property was not set</summary>
        public double? get_D() { var arr = GetDatatypeProperty_double("D"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class PlaneSurface
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class PlaneSurface : InfiniteSurface
    {
        /// <summary>
        /// Create new instace of OWL class PlaneSurface and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new PlaneSurface Create(Int64 model, string name=null) { return new PlaneSurface(Instance.Create(model, "PlaneSurface", name), "PlaneSurface");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public PlaneSurface(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "PlaneSurface") 
        {            
        }

       //
       // Properties with known cardinality restrictions to PlaneSurface
       //

        ///<summary>Sets relationship from this instance to an instance of Plane</summary>
        public void set_plane(Plane instance) { SetObjectProperty("plane", instance); }
        ///<summary>Get related instance</summary>
        public Plane get_plane() 
        {
            var propId = GetPropertyId("plane");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Plane(values[0], null);
            }
            else
            {
                return null;
            }
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Point
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Point : GeometricItem
    {
        /// <summary>
        /// Create new instace of OWL class Point and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Point Create(Int64 model, string name=null) { return new Point(Instance.Create(model, "Point", name), "Point");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Point(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Point") 
        {            
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Point3D
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Point3D : Point
    {
        /// <summary>
        /// Create new instace of OWL class Point3D and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Point3D Create(Int64 model, string name=null) { return new Point3D(Instance.Create(model, "Point3D", name), "Point3D");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Point3D(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Point3D") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Point3D
       //

        ///<summary>Sets value of asOpenGL</summary>
        public void set_asOpenGL(bool value) { SetDatatypeProperty ("asOpenGL", value); }
        ///<summary>Gets value of asOpenGL, returns null is the property was not set</summary>
        public bool? get_asOpenGL() { var arr = GetDatatypeProperty_bool("asOpenGL"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets values of coordinates. OWL cardinality 0..3</summary>
        public void set_coordinates(double[] values) { SetDatatypeProperty ("coordinates", values); }
        ///<summary>Gets values of coordinates. OWL cardinality 0..3</summary>
        public double[] get_coordinates() { return GetDatatypeProperty_double("coordinates"); }
        ///<summary>Sets values of points. OWL cardinality 0..3</summary>
        public void set_points(double[] values) { SetDatatypeProperty ("points", values); }
        ///<summary>Gets values of points. OWL cardinality 0..3</summary>
        public double[] get_points() { return GetDatatypeProperty_double("points"); }
        ///<summary>Sets value of x</summary>
        public void set_x(double value) { SetDatatypeProperty ("x", value); }
        ///<summary>Gets value of x, returns null is the property was not set</summary>
        public double? get_x() { var arr = GetDatatypeProperty_double("x"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of y</summary>
        public void set_y(double value) { SetDatatypeProperty ("y", value); }
        ///<summary>Gets value of y, returns null is the property was not set</summary>
        public double? get_y() { var arr = GetDatatypeProperty_double("y"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of z</summary>
        public void set_z(double value) { SetDatatypeProperty ("z", value); }
        ///<summary>Gets value of z, returns null is the property was not set</summary>
        public double? get_z() { var arr = GetDatatypeProperty_double("z"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Point3DSet
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Point3DSet : Point
    {
        /// <summary>
        /// Create new instace of OWL class Point3DSet and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Point3DSet Create(Int64 model, string name=null) { return new Point3DSet(Instance.Create(model, "Point3DSet", name), "Point3DSet");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Point3DSet(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Point3DSet") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Point3DSet
       //

        ///<summary>Sets value of asOpenGL</summary>
        public void set_asOpenGL(bool value) { SetDatatypeProperty ("asOpenGL", value); }
        ///<summary>Gets value of asOpenGL, returns null is the property was not set</summary>
        public bool? get_asOpenGL() { var arr = GetDatatypeProperty_bool("asOpenGL"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets values of coordinates. OWL cardinality 0..-1</summary>
        public void set_coordinates(double[] values) { SetDatatypeProperty ("coordinates", values); }
        ///<summary>Gets values of coordinates. OWL cardinality 0..-1</summary>
        public double[] get_coordinates() { return GetDatatypeProperty_double("coordinates"); }
        ///<summary>Sets values of points. OWL cardinality 0..-1</summary>
        public void set_points(double[] values) { SetDatatypeProperty ("points", values); }
        ///<summary>Gets values of points. OWL cardinality 0..-1</summary>
        public double[] get_points() { return GetDatatypeProperty_double("points"); }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Point3DSetByGeometricItem
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Point3DSetByGeometricItem : Point3DSet
    {
        /// <summary>
        /// Create new instace of OWL class Point3DSetByGeometricItem and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Point3DSetByGeometricItem Create(Int64 model, string name=null) { return new Point3DSetByGeometricItem(Instance.Create(model, "Point3DSetByGeometricItem", name), "Point3DSetByGeometricItem");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Point3DSetByGeometricItem(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Point3DSetByGeometricItem") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Point3DSetByGeometricItem
       //

        ///<summary>Sets relationship from this instance to an instance of GeometricItem</summary>
        public void set_object(GeometricItem instance) { SetObjectProperty("object", instance); }
        ///<summary>Get related instance</summary>
        public GeometricItem get_object() 
        {
            var propId = GetPropertyId("object");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new GeometricItem(values[0], null);
            }
            else
            {
                return null;
            }
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class PointLight
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class PointLight : DirectLight
    {
        /// <summary>
        /// Create new instace of OWL class PointLight and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new PointLight Create(Int64 model, string name=null) { return new PointLight(Instance.Create(model, "PointLight", name), "PointLight");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public PointLight(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "PointLight") 
        {            
        }

       //
       // Properties with known cardinality restrictions to PointLight
       //

        ///<summary>Sets relationship from this instance to an instance of Point3D</summary>
        public void set_position(Point3D instance) { SetObjectProperty("position", instance); }
        ///<summary>Get related instance</summary>
        public Point3D get_position() 
        {
            var propId = GetPropertyId("position");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Point3D(values[0], null);
            }
            else
            {
                return null;
            }
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class PointLoop
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class PointLoop : Curve
    {
        /// <summary>
        /// Create new instace of OWL class PointLoop and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new PointLoop Create(Int64 model, string name=null) { return new PointLoop(Instance.Create(model, "PointLoop", name), "PointLoop");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public PointLoop(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "PointLoop") 
        {            
        }

       //
       // Properties with known cardinality restrictions to PointLoop
       //

        ///<summary>Sets values of coordinates. OWL cardinality 0..3</summary>
        public void set_coordinates(double[] values) { SetDatatypeProperty ("coordinates", values); }
        ///<summary>Gets values of coordinates. OWL cardinality 0..3</summary>
        public double[] get_coordinates() { return GetDatatypeProperty_double("coordinates"); }
        ///<summary>Sets value of x</summary>
        public void set_x(double value) { SetDatatypeProperty ("x", value); }
        ///<summary>Gets value of x, returns null is the property was not set</summary>
        public double? get_x() { var arr = GetDatatypeProperty_double("x"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of y</summary>
        public void set_y(double value) { SetDatatypeProperty ("y", value); }
        ///<summary>Gets value of y, returns null is the property was not set</summary>
        public double? get_y() { var arr = GetDatatypeProperty_double("y"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of z</summary>
        public void set_z(double value) { SetDatatypeProperty ("z", value); }
        ///<summary>Gets value of z, returns null is the property was not set</summary>
        public double? get_z() { var arr = GetDatatypeProperty_double("z"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Polygon2D
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Polygon2D : Curve
    {
        /// <summary>
        /// Create new instace of OWL class Polygon2D and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Polygon2D Create(Int64 model, string name=null) { return new Polygon2D(Instance.Create(model, "Polygon2D", name), "Polygon2D");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Polygon2D(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Polygon2D") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Polygon2D
       //

        ///<summary>Sets relationships from this instance to an array of Curve. OWL cardinality 1..-1</summary>
        public void set_lineParts(Curve[] instances) { SetObjectProperty("lineParts", instances); }
        ///<summary>Sets relationships from this instance to an array of Int64. OWL cardinality 1..-1</summary>
        public void set_lineParts(Int64[] instances) { SetObjectProperty("lineParts", instances); }
        ///<summary>Get an array of related instances. OWL cardinality 1..-1</summary>
        public Curve[] get_lineParts() 
        {
            var propId = GetPropertyId("lineParts");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                var ret = new Curve[card];
                for (int i = 0; i < card; i++)
                {
                    ret[i] = new Curve(values[i], null);
                }

                return ret;
            }
            else
            {
                return null;
            }
        }
        ///<summary>Get an array of handles of related instances. OWL cardinality 1..-1</summary>
        public Int64[] get_lineParts_Int64()  
        {
            var propId = GetPropertyId("lineParts");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                return values;
            }
            else
            {
                return null;
            }
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Polygon3D
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Polygon3D : Curve
    {
        /// <summary>
        /// Create new instace of OWL class Polygon3D and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Polygon3D Create(Int64 model, string name=null) { return new Polygon3D(Instance.Create(model, "Polygon3D", name), "Polygon3D");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Polygon3D(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Polygon3D") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Polygon3D
       //

        ///<summary>Sets relationships from this instance to an array of Curve. OWL cardinality 1..-1</summary>
        public void set_lineParts(Curve[] instances) { SetObjectProperty("lineParts", instances); }
        ///<summary>Sets relationships from this instance to an array of Int64. OWL cardinality 1..-1</summary>
        public void set_lineParts(Int64[] instances) { SetObjectProperty("lineParts", instances); }
        ///<summary>Get an array of related instances. OWL cardinality 1..-1</summary>
        public Curve[] get_lineParts() 
        {
            var propId = GetPropertyId("lineParts");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                var ret = new Curve[card];
                for (int i = 0; i < card; i++)
                {
                    ret[i] = new Curve(values[i], null);
                }

                return ret;
            }
            else
            {
                return null;
            }
        }
        ///<summary>Get an array of handles of related instances. OWL cardinality 1..-1</summary>
        public Int64[] get_lineParts_Int64()  
        {
            var propId = GetPropertyId("lineParts");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                return values;
            }
            else
            {
                return null;
            }
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class PolyLine3D
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class PolyLine3D : Curve
    {
        /// <summary>
        /// Create new instace of OWL class PolyLine3D and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new PolyLine3D Create(Int64 model, string name=null) { return new PolyLine3D(Instance.Create(model, "PolyLine3D", name), "PolyLine3D");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public PolyLine3D(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "PolyLine3D") 
        {            
        }

       //
       // Properties with known cardinality restrictions to PolyLine3D
       //

        ///<summary>Sets values of coordinates. OWL cardinality 0..-1</summary>
        public void set_coordinates(double[] values) { SetDatatypeProperty ("coordinates", values); }
        ///<summary>Gets values of coordinates. OWL cardinality 0..-1</summary>
        public double[] get_coordinates() { return GetDatatypeProperty_double("coordinates"); }
        ///<summary>Sets relationships from this instance to an array of Point3D. OWL cardinality 0..-1</summary>
        public void set_pointReferences(Point3D[] instances) { SetObjectProperty("pointReferences", instances); }
        ///<summary>Sets relationships from this instance to an array of Int64. OWL cardinality 0..-1</summary>
        public void set_pointReferences(Int64[] instances) { SetObjectProperty("pointReferences", instances); }
        ///<summary>Get an array of related instances. OWL cardinality 0..-1</summary>
        public Point3D[] get_pointReferences() 
        {
            var propId = GetPropertyId("pointReferences");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                var ret = new Point3D[card];
                for (int i = 0; i < card; i++)
                {
                    ret[i] = new Point3D(values[i], null);
                }

                return ret;
            }
            else
            {
                return null;
            }
        }
        ///<summary>Get an array of handles of related instances. OWL cardinality 0..-1</summary>
        public Int64[] get_pointReferences_Int64()  
        {
            var propId = GetPropertyId("pointReferences");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                return values;
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets values of points. OWL cardinality 0..-1</summary>
        public void set_points(double[] values) { SetDatatypeProperty ("points", values); }
        ///<summary>Gets values of points. OWL cardinality 0..-1</summary>
        public double[] get_points() { return GetDatatypeProperty_double("points"); }
        ///<summary>Sets values of tangent. OWL cardinality 0..-1</summary>
        public void set_tangent(double[] values) { SetDatatypeProperty ("tangent", values); }
        ///<summary>Gets values of tangent. OWL cardinality 0..-1</summary>
        public double[] get_tangent() { return GetDatatypeProperty_double("tangent"); }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Prism
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Prism : Solid
    {
        /// <summary>
        /// Create new instace of OWL class Prism and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Prism Create(Int64 model, string name=null) { return new Prism(Instance.Create(model, "Prism", name), "Prism");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Prism(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Prism") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Prism
       //

        ///<summary>Sets value of height</summary>
        public void set_height(double value) { SetDatatypeProperty ("height", value); }
        ///<summary>Gets value of height, returns null is the property was not set</summary>
        public double? get_height() { var arr = GetDatatypeProperty_double("height"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of length</summary>
        public void set_length(double value) { SetDatatypeProperty ("length", value); }
        ///<summary>Gets value of length, returns null is the property was not set</summary>
        public double? get_length() { var arr = GetDatatypeProperty_double("length"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Profile
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Profile : Curve
    {
        /// <summary>
        /// Create new instace of OWL class Profile and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Profile Create(Int64 model, string name=null) { return new Profile(Instance.Create(model, "Profile", name), "Profile");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Profile(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Profile") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Profile
       //

        ///<summary>Sets relationships from this instance to an array of Curve. OWL cardinality 0..-1</summary>
        public void set_innerPolygons(Curve[] instances) { SetObjectProperty("innerPolygons", instances); }
        ///<summary>Sets relationships from this instance to an array of Int64. OWL cardinality 0..-1</summary>
        public void set_innerPolygons(Int64[] instances) { SetObjectProperty("innerPolygons", instances); }
        ///<summary>Get an array of related instances. OWL cardinality 0..-1</summary>
        public Curve[] get_innerPolygons() 
        {
            var propId = GetPropertyId("innerPolygons");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                var ret = new Curve[card];
                for (int i = 0; i < card; i++)
                {
                    ret[i] = new Curve(values[i], null);
                }

                return ret;
            }
            else
            {
                return null;
            }
        }
        ///<summary>Get an array of handles of related instances. OWL cardinality 0..-1</summary>
        public Int64[] get_innerPolygons_Int64()  
        {
            var propId = GetPropertyId("innerPolygons");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                return values;
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets relationship from this instance to an instance of Curve</summary>
        public void set_outerPolygon(Curve instance) { SetObjectProperty("outerPolygon", instance); }
        ///<summary>Get related instance</summary>
        public Curve get_outerPolygon() 
        {
            var propId = GetPropertyId("outerPolygon");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Curve(values[0], null);
            }
            else
            {
                return null;
            }
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Projection
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Projection : Environment
    {
        /// <summary>
        /// Create new instace of OWL class Projection and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Projection Create(Int64 model, string name=null) { return new Projection(Instance.Create(model, "Projection", name), "Projection");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Projection(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Projection") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Projection
       //

        ///<summary>Sets relationship from this instance to an instance of Matrix</summary>
        public void set_matrix(Matrix instance) { SetObjectProperty("matrix", instance); }
        ///<summary>Get related instance</summary>
        public Matrix get_matrix() 
        {
            var propId = GetPropertyId("matrix");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Matrix(values[0], null);
            }
            else
            {
                return null;
            }
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Pyramid
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Pyramid : Solid
    {
        /// <summary>
        /// Create new instace of OWL class Pyramid and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Pyramid Create(Int64 model, string name=null) { return new Pyramid(Instance.Create(model, "Pyramid", name), "Pyramid");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Pyramid(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Pyramid") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Pyramid
       //

        ///<summary>Sets value of height</summary>
        public void set_height(double value) { SetDatatypeProperty ("height", value); }
        ///<summary>Gets value of height, returns null is the property was not set</summary>
        public double? get_height() { var arr = GetDatatypeProperty_double("height"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of length</summary>
        public void set_length(double value) { SetDatatypeProperty ("length", value); }
        ///<summary>Gets value of length, returns null is the property was not set</summary>
        public double? get_length() { var arr = GetDatatypeProperty_double("length"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of width</summary>
        public void set_width(double value) { SetDatatypeProperty ("width", value); }
        ///<summary>Gets value of width, returns null is the property was not set</summary>
        public double? get_width() { var arr = GetDatatypeProperty_double("width"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class RationalBezierCurve
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class RationalBezierCurve : BezierCurve
    {
        /// <summary>
        /// Create new instace of OWL class RationalBezierCurve and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new RationalBezierCurve Create(Int64 model, string name=null) { return new RationalBezierCurve(Instance.Create(model, "RationalBezierCurve", name), "RationalBezierCurve");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public RationalBezierCurve(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "RationalBezierCurve") 
        {            
        }

       //
       // Properties with known cardinality restrictions to RationalBezierCurve
       //

        ///<summary>Sets values of weights. OWL cardinality 2..-1</summary>
        public void set_weights(double[] values) { SetDatatypeProperty ("weights", values); }
        ///<summary>Gets values of weights. OWL cardinality 2..-1</summary>
        public double[] get_weights() { return GetDatatypeProperty_double("weights"); }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class RationalBezierSurface
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class RationalBezierSurface : BezierSurface
    {
        /// <summary>
        /// Create new instace of OWL class RationalBezierSurface and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new RationalBezierSurface Create(Int64 model, string name=null) { return new RationalBezierSurface(Instance.Create(model, "RationalBezierSurface", name), "RationalBezierSurface");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public RationalBezierSurface(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "RationalBezierSurface") 
        {            
        }

       //
       // Properties with known cardinality restrictions to RationalBezierSurface
       //

        ///<summary>Sets values of weights. OWL cardinality 4..-1</summary>
        public void set_weights(double[] values) { SetDatatypeProperty ("weights", values); }
        ///<summary>Gets values of weights. OWL cardinality 4..-1</summary>
        public double[] get_weights() { return GetDatatypeProperty_double("weights"); }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class RectangleCurve
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class RectangleCurve : Curve
    {
        /// <summary>
        /// Create new instace of OWL class RectangleCurve and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new RectangleCurve Create(Int64 model, string name=null) { return new RectangleCurve(Instance.Create(model, "RectangleCurve", name), "RectangleCurve");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public RectangleCurve(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "RectangleCurve") 
        {            
        }

       //
       // Properties with known cardinality restrictions to RectangleCurve
       //

        ///<summary>Sets value of height</summary>
        public void set_height(double value) { SetDatatypeProperty ("height", value); }
        ///<summary>Gets value of height, returns null is the property was not set</summary>
        public double? get_height() { var arr = GetDatatypeProperty_double("height"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of offsetX</summary>
        public void set_offsetX(double value) { SetDatatypeProperty ("offsetX", value); }
        ///<summary>Gets value of offsetX, returns null is the property was not set</summary>
        public double? get_offsetX() { var arr = GetDatatypeProperty_double("offsetX"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of offsetY</summary>
        public void set_offsetY(double value) { SetDatatypeProperty ("offsetY", value); }
        ///<summary>Gets value of offsetY, returns null is the property was not set</summary>
        public double? get_offsetY() { var arr = GetDatatypeProperty_double("offsetY"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of offsetZ</summary>
        public void set_offsetZ(double value) { SetDatatypeProperty ("offsetZ", value); }
        ///<summary>Gets value of offsetZ, returns null is the property was not set</summary>
        public double? get_offsetZ() { var arr = GetDatatypeProperty_double("offsetZ"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of width</summary>
        public void set_width(double value) { SetDatatypeProperty ("width", value); }
        ///<summary>Gets value of width, returns null is the property was not set</summary>
        public double? get_width() { var arr = GetDatatypeProperty_double("width"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Repetition
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Repetition : GeometricItem
    {
        /// <summary>
        /// Create new instace of OWL class Repetition and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Repetition Create(Int64 model, string name=null) { return new Repetition(Instance.Create(model, "Repetition", name), "Repetition");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Repetition(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Repetition") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Repetition
       //

        ///<summary>Sets value of count</summary>
        public void set_count(long value) { SetDatatypeProperty ("count", value); }
        ///<summary>Gets value of count, returns null is the property was not set</summary>
        public long? get_count() { var arr = GetDatatypeProperty_long("count"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets relationship from this instance to an instance of Matrix</summary>
        public void set_matrix(Matrix instance) { SetObjectProperty("matrix", instance); }
        ///<summary>Get related instance</summary>
        public Matrix get_matrix() 
        {
            var propId = GetPropertyId("matrix");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Matrix(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets relationship from this instance to an instance of GeometricItem</summary>
        public void set_object(GeometricItem instance) { SetObjectProperty("object", instance); }
        ///<summary>Get related instance</summary>
        public GeometricItem get_object() 
        {
            var propId = GetPropertyId("object");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new GeometricItem(values[0], null);
            }
            else
            {
                return null;
            }
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class RingTorus
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class RingTorus : Torus
    {
        /// <summary>
        /// Create new instace of OWL class RingTorus and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new RingTorus Create(Int64 model, string name=null) { return new RingTorus(Instance.Create(model, "RingTorus", name), "RingTorus");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public RingTorus(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "RingTorus") 
        {            
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Shadow
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Shadow : Face
    {
        /// <summary>
        /// Create new instace of OWL class Shadow and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Shadow Create(Int64 model, string name=null) { return new Shadow(Instance.Create(model, "Shadow", name), "Shadow");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Shadow(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Shadow") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Shadow
       //

        ///<summary>Sets relationship from this instance to an instance of Vector3</summary>
        public void set_lightDirection(Vector3 instance) { SetObjectProperty("lightDirection", instance); }
        ///<summary>Get related instance</summary>
        public Vector3 get_lightDirection() 
        {
            var propId = GetPropertyId("lightDirection");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Vector3(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets relationship from this instance to an instance of Point3D</summary>
        public void set_lightPoint(Point3D instance) { SetObjectProperty("lightPoint", instance); }
        ///<summary>Get related instance</summary>
        public Point3D get_lightPoint() 
        {
            var propId = GetPropertyId("lightPoint");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Point3D(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets relationship from this instance to an instance of GeometricItem</summary>
        public void set_object(GeometricItem instance) { SetObjectProperty("object", instance); }
        ///<summary>Get related instance</summary>
        public GeometricItem get_object() 
        {
            var propId = GetPropertyId("object");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new GeometricItem(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets relationship from this instance to an instance of Plane</summary>
        public void set_plane(Plane instance) { SetObjectProperty("plane", instance); }
        ///<summary>Get related instance</summary>
        public Plane get_plane() 
        {
            var propId = GetPropertyId("plane");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Plane(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets relationship from this instance to an instance of Vector3</summary>
        public void set_planeRefDirection(Vector3 instance) { SetObjectProperty("planeRefDirection", instance); }
        ///<summary>Get related instance</summary>
        public Vector3 get_planeRefDirection() 
        {
            var propId = GetPropertyId("planeRefDirection");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Vector3(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets value of type</summary>
        public void set_type(long value) { SetDatatypeProperty ("type", value); }
        ///<summary>Gets value of type, returns null is the property was not set</summary>
        public long? get_type() { var arr = GetDatatypeProperty_long("type"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class SineCurve
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class SineCurve : TransitionalCurve
    {
        /// <summary>
        /// Create new instace of OWL class SineCurve and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new SineCurve Create(Int64 model, string name=null) { return new SineCurve(Instance.Create(model, "SineCurve", name), "SineCurve");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public SineCurve(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "SineCurve") 
        {            
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class SkewedCone
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class SkewedCone : Solid
    {
        /// <summary>
        /// Create new instace of OWL class SkewedCone and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new SkewedCone Create(Int64 model, string name=null) { return new SkewedCone(Instance.Create(model, "SkewedCone", name), "SkewedCone");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public SkewedCone(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "SkewedCone") 
        {            
        }

       //
       // Properties with known cardinality restrictions to SkewedCone
       //

        ///<summary>Sets value of height</summary>
        public void set_height(double value) { SetDatatypeProperty ("height", value); }
        ///<summary>Gets value of height, returns null is the property was not set</summary>
        public double? get_height() { var arr = GetDatatypeProperty_double("height"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of offsetX</summary>
        public void set_offsetX(double value) { SetDatatypeProperty ("offsetX", value); }
        ///<summary>Gets value of offsetX, returns null is the property was not set</summary>
        public double? get_offsetX() { var arr = GetDatatypeProperty_double("offsetX"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of offsetY</summary>
        public void set_offsetY(double value) { SetDatatypeProperty ("offsetY", value); }
        ///<summary>Gets value of offsetY, returns null is the property was not set</summary>
        public double? get_offsetY() { var arr = GetDatatypeProperty_double("offsetY"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of radius</summary>
        public void set_radius(double value) { SetDatatypeProperty ("radius", value); }
        ///<summary>Gets value of radius, returns null is the property was not set</summary>
        public double? get_radius() { var arr = GetDatatypeProperty_double("radius"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of segmentationParts</summary>
        public void set_segmentationParts(long value) { SetDatatypeProperty ("segmentationParts", value); }
        ///<summary>Gets value of segmentationParts, returns null is the property was not set</summary>
        public long? get_segmentationParts() { var arr = GetDatatypeProperty_long("segmentationParts"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class SkewedFrustumCone
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class SkewedFrustumCone : Solid
    {
        /// <summary>
        /// Create new instace of OWL class SkewedFrustumCone and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new SkewedFrustumCone Create(Int64 model, string name=null) { return new SkewedFrustumCone(Instance.Create(model, "SkewedFrustumCone", name), "SkewedFrustumCone");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public SkewedFrustumCone(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "SkewedFrustumCone") 
        {            
        }

       //
       // Properties with known cardinality restrictions to SkewedFrustumCone
       //

        ///<summary>Sets value of factor</summary>
        public void set_factor(double value) { SetDatatypeProperty ("factor", value); }
        ///<summary>Gets value of factor, returns null is the property was not set</summary>
        public double? get_factor() { var arr = GetDatatypeProperty_double("factor"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of height</summary>
        public void set_height(double value) { SetDatatypeProperty ("height", value); }
        ///<summary>Gets value of height, returns null is the property was not set</summary>
        public double? get_height() { var arr = GetDatatypeProperty_double("height"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of offsetX</summary>
        public void set_offsetX(double value) { SetDatatypeProperty ("offsetX", value); }
        ///<summary>Gets value of offsetX, returns null is the property was not set</summary>
        public double? get_offsetX() { var arr = GetDatatypeProperty_double("offsetX"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of offsetY</summary>
        public void set_offsetY(double value) { SetDatatypeProperty ("offsetY", value); }
        ///<summary>Gets value of offsetY, returns null is the property was not set</summary>
        public double? get_offsetY() { var arr = GetDatatypeProperty_double("offsetY"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of radius</summary>
        public void set_radius(double value) { SetDatatypeProperty ("radius", value); }
        ///<summary>Gets value of radius, returns null is the property was not set</summary>
        public double? get_radius() { var arr = GetDatatypeProperty_double("radius"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of segmentationParts</summary>
        public void set_segmentationParts(long value) { SetDatatypeProperty ("segmentationParts", value); }
        ///<summary>Gets value of segmentationParts, returns null is the property was not set</summary>
        public long? get_segmentationParts() { var arr = GetDatatypeProperty_long("segmentationParts"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Solid
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Solid : GeometricItem
    {
        /// <summary>
        /// Create new instace of OWL class Solid and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Solid Create(Int64 model, string name=null) { return new Solid(Instance.Create(model, "Solid", name), "Solid");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Solid(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Solid") 
        {            
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class SolidBySurface
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class SolidBySurface : Solid
    {
        /// <summary>
        /// Create new instace of OWL class SolidBySurface and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new SolidBySurface Create(Int64 model, string name=null) { return new SolidBySurface(Instance.Create(model, "SolidBySurface", name), "SolidBySurface");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public SolidBySurface(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "SolidBySurface") 
        {            
        }

       //
       // Properties with known cardinality restrictions to SolidBySurface
       //

        ///<summary>Sets relationships from this instance to an array of Face. OWL cardinality 1..-1</summary>
        public void set_faces(Face[] instances) { SetObjectProperty("faces", instances); }
        ///<summary>Sets relationships from this instance to an array of Int64. OWL cardinality 1..-1</summary>
        public void set_faces(Int64[] instances) { SetObjectProperty("faces", instances); }
        ///<summary>Get an array of related instances. OWL cardinality 1..-1</summary>
        public Face[] get_faces() 
        {
            var propId = GetPropertyId("faces");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                var ret = new Face[card];
                for (int i = 0; i < card; i++)
                {
                    ret[i] = new Face(values[i], null);
                }

                return ret;
            }
            else
            {
                return null;
            }
        }
        ///<summary>Get an array of handles of related instances. OWL cardinality 1..-1</summary>
        public Int64[] get_faces_Int64()  
        {
            var propId = GetPropertyId("faces");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                return values;
            }
            else
            {
                return null;
            }
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class SolidLine
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class SolidLine : Solid
    {
        /// <summary>
        /// Create new instace of OWL class SolidLine and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new SolidLine Create(Int64 model, string name=null) { return new SolidLine(Instance.Create(model, "SolidLine", name), "SolidLine");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public SolidLine(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "SolidLine") 
        {            
        }

       //
       // Properties with known cardinality restrictions to SolidLine
       //

        ///<summary>Sets values of coordinates. OWL cardinality 0..6</summary>
        public void set_coordinates(double[] values) { SetDatatypeProperty ("coordinates", values); }
        ///<summary>Gets values of coordinates. OWL cardinality 0..6</summary>
        public double[] get_coordinates() { return GetDatatypeProperty_double("coordinates"); }
        ///<summary>Sets value of segmentationParts</summary>
        public void set_segmentationParts(long value) { SetDatatypeProperty ("segmentationParts", value); }
        ///<summary>Gets value of segmentationParts, returns null is the property was not set</summary>
        public long? get_segmentationParts() { var arr = GetDatatypeProperty_long("segmentationParts"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of thickness</summary>
        public void set_thickness(double value) { SetDatatypeProperty ("thickness", value); }
        ///<summary>Gets value of thickness, returns null is the property was not set</summary>
        public double? get_thickness() { var arr = GetDatatypeProperty_double("thickness"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Sphere
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Sphere : Solid
    {
        /// <summary>
        /// Create new instace of OWL class Sphere and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Sphere Create(Int64 model, string name=null) { return new Sphere(Instance.Create(model, "Sphere", name), "Sphere");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Sphere(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Sphere") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Sphere
       //

        ///<summary>Sets value of radius</summary>
        public void set_radius(double value) { SetDatatypeProperty ("radius", value); }
        ///<summary>Gets value of radius, returns null is the property was not set</summary>
        public double? get_radius() { var arr = GetDatatypeProperty_double("radius"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of segmentationParts</summary>
        public void set_segmentationParts(long value) { SetDatatypeProperty ("segmentationParts", value); }
        ///<summary>Gets value of segmentationParts, returns null is the property was not set</summary>
        public long? get_segmentationParts() { var arr = GetDatatypeProperty_long("segmentationParts"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class SphericalSurface
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class SphericalSurface : FiniteSurface
    {
        /// <summary>
        /// Create new instace of OWL class SphericalSurface and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new SphericalSurface Create(Int64 model, string name=null) { return new SphericalSurface(Instance.Create(model, "SphericalSurface", name), "SphericalSurface");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public SphericalSurface(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "SphericalSurface") 
        {            
        }

       //
       // Properties with known cardinality restrictions to SphericalSurface
       //

        ///<summary>Sets value of radius</summary>
        public void set_radius(double value) { SetDatatypeProperty ("radius", value); }
        ///<summary>Gets value of radius, returns null is the property was not set</summary>
        public double? get_radius() { var arr = GetDatatypeProperty_double("radius"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of segmentationParts</summary>
        public void set_segmentationParts(long value) { SetDatatypeProperty ("segmentationParts", value); }
        ///<summary>Gets value of segmentationParts, returns null is the property was not set</summary>
        public long? get_segmentationParts() { var arr = GetDatatypeProperty_long("segmentationParts"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class SpindleTorus
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class SpindleTorus : Torus
    {
        /// <summary>
        /// Create new instace of OWL class SpindleTorus and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new SpindleTorus Create(Int64 model, string name=null) { return new SpindleTorus(Instance.Create(model, "SpindleTorus", name), "SpindleTorus");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public SpindleTorus(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "SpindleTorus") 
        {            
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Spiral
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Spiral : Curve
    {
        /// <summary>
        /// Create new instace of OWL class Spiral and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Spiral Create(Int64 model, string name=null) { return new Spiral(Instance.Create(model, "Spiral", name), "Spiral");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Spiral(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Spiral") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Spiral
       //

        ///<summary>Sets value of height</summary>
        public void set_height(double value) { SetDatatypeProperty ("height", value); }
        ///<summary>Gets value of height, returns null is the property was not set</summary>
        public double? get_height() { var arr = GetDatatypeProperty_double("height"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of offsetZ</summary>
        public void set_offsetZ(double value) { SetDatatypeProperty ("offsetZ", value); }
        ///<summary>Gets value of offsetZ, returns null is the property was not set</summary>
        public double? get_offsetZ() { var arr = GetDatatypeProperty_double("offsetZ"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of radius</summary>
        public void set_radius(double value) { SetDatatypeProperty ("radius", value); }
        ///<summary>Gets value of radius, returns null is the property was not set</summary>
        public double? get_radius() { var arr = GetDatatypeProperty_double("radius"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of segmentationParts</summary>
        public void set_segmentationParts(long value) { SetDatatypeProperty ("segmentationParts", value); }
        ///<summary>Gets value of segmentationParts, returns null is the property was not set</summary>
        public long? get_segmentationParts() { var arr = GetDatatypeProperty_long("segmentationParts"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of size</summary>
        public void set_size(double value) { SetDatatypeProperty ("size", value); }
        ///<summary>Gets value of size, returns null is the property was not set</summary>
        public double? get_size() { var arr = GetDatatypeProperty_double("size"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of start</summary>
        public void set_start(double value) { SetDatatypeProperty ("start", value); }
        ///<summary>Gets value of start, returns null is the property was not set</summary>
        public double? get_start() { var arr = GetDatatypeProperty_double("start"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class SplineCurve
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class SplineCurve : Curve
    {
        /// <summary>
        /// Create new instace of OWL class SplineCurve and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new SplineCurve Create(Int64 model, string name=null) { return new SplineCurve(Instance.Create(model, "SplineCurve", name), "SplineCurve");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public SplineCurve(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "SplineCurve") 
        {            
        }

       //
       // Properties with known cardinality restrictions to SplineCurve
       //

        ///<summary>Sets value of closed</summary>
        public void set_closed(bool value) { SetDatatypeProperty ("closed", value); }
        ///<summary>Gets value of closed, returns null is the property was not set</summary>
        public bool? get_closed() { var arr = GetDatatypeProperty_bool("closed"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets relationships from this instance to an array of Point3D. OWL cardinality 2..-1</summary>
        public void set_controlPoints(Point3D[] instances) { SetObjectProperty("controlPoints", instances); }
        ///<summary>Sets relationships from this instance to an array of Int64. OWL cardinality 2..-1</summary>
        public void set_controlPoints(Int64[] instances) { SetObjectProperty("controlPoints", instances); }
        ///<summary>Get an array of related instances. OWL cardinality 2..-1</summary>
        public Point3D[] get_controlPoints() 
        {
            var propId = GetPropertyId("controlPoints");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                var ret = new Point3D[card];
                for (int i = 0; i < card; i++)
                {
                    ret[i] = new Point3D(values[i], null);
                }

                return ret;
            }
            else
            {
                return null;
            }
        }
        ///<summary>Get an array of handles of related instances. OWL cardinality 2..-1</summary>
        public Int64[] get_controlPoints_Int64()  
        {
            var propId = GetPropertyId("controlPoints");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                return values;
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets value of count</summary>
        public void set_count(long value) { SetDatatypeProperty ("count", value); }
        ///<summary>Gets value of count, returns null is the property was not set</summary>
        public long? get_count() { var arr = GetDatatypeProperty_long("count"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of degree</summary>
        public void set_degree(long value) { SetDatatypeProperty ("degree", value); }
        ///<summary>Gets value of degree, returns null is the property was not set</summary>
        public long? get_degree() { var arr = GetDatatypeProperty_long("degree"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of segmentationLength</summary>
        public void set_segmentationLength(double value) { SetDatatypeProperty ("segmentationLength", value); }
        ///<summary>Gets value of segmentationLength, returns null is the property was not set</summary>
        public double? get_segmentationLength() { var arr = GetDatatypeProperty_double("segmentationLength"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of segmentationParts</summary>
        public void set_segmentationParts(long value) { SetDatatypeProperty ("segmentationParts", value); }
        ///<summary>Gets value of segmentationParts, returns null is the property was not set</summary>
        public long? get_segmentationParts() { var arr = GetDatatypeProperty_long("segmentationParts"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class SplineSurface
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class SplineSurface : FiniteSurface
    {
        /// <summary>
        /// Create new instace of OWL class SplineSurface and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new SplineSurface Create(Int64 model, string name=null) { return new SplineSurface(Instance.Create(model, "SplineSurface", name), "SplineSurface");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public SplineSurface(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "SplineSurface") 
        {            
        }

       //
       // Properties with known cardinality restrictions to SplineSurface
       //

        ///<summary>Sets relationships from this instance to an array of Point3D. OWL cardinality 4..-1</summary>
        public void set_controlPoints(Point3D[] instances) { SetObjectProperty("controlPoints", instances); }
        ///<summary>Sets relationships from this instance to an array of Int64. OWL cardinality 4..-1</summary>
        public void set_controlPoints(Int64[] instances) { SetObjectProperty("controlPoints", instances); }
        ///<summary>Get an array of related instances. OWL cardinality 4..-1</summary>
        public Point3D[] get_controlPoints() 
        {
            var propId = GetPropertyId("controlPoints");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                var ret = new Point3D[card];
                for (int i = 0; i < card; i++)
                {
                    ret[i] = new Point3D(values[i], null);
                }

                return ret;
            }
            else
            {
                return null;
            }
        }
        ///<summary>Get an array of handles of related instances. OWL cardinality 4..-1</summary>
        public Int64[] get_controlPoints_Int64()  
        {
            var propId = GetPropertyId("controlPoints");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                return values;
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets value of segmentationLength</summary>
        public void set_segmentationLength(double value) { SetDatatypeProperty ("segmentationLength", value); }
        ///<summary>Gets value of segmentationLength, returns null is the property was not set</summary>
        public double? get_segmentationLength() { var arr = GetDatatypeProperty_double("segmentationLength"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of segmentationParts</summary>
        public void set_segmentationParts(long value) { SetDatatypeProperty ("segmentationParts", value); }
        ///<summary>Gets value of segmentationParts, returns null is the property was not set</summary>
        public long? get_segmentationParts() { var arr = GetDatatypeProperty_long("segmentationParts"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of uClosed</summary>
        public void set_uClosed(bool value) { SetDatatypeProperty ("uClosed", value); }
        ///<summary>Gets value of uClosed, returns null is the property was not set</summary>
        public bool? get_uClosed() { var arr = GetDatatypeProperty_bool("uClosed"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of uCount</summary>
        public void set_uCount(long value) { SetDatatypeProperty ("uCount", value); }
        ///<summary>Gets value of uCount, returns null is the property was not set</summary>
        public long? get_uCount() { var arr = GetDatatypeProperty_long("uCount"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of uDegree</summary>
        public void set_uDegree(long value) { SetDatatypeProperty ("uDegree", value); }
        ///<summary>Gets value of uDegree, returns null is the property was not set</summary>
        public long? get_uDegree() { var arr = GetDatatypeProperty_long("uDegree"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of uSegmentationParts</summary>
        public void set_uSegmentationParts(long value) { SetDatatypeProperty ("uSegmentationParts", value); }
        ///<summary>Gets value of uSegmentationParts, returns null is the property was not set</summary>
        public long? get_uSegmentationParts() { var arr = GetDatatypeProperty_long("uSegmentationParts"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of vClosed</summary>
        public void set_vClosed(bool value) { SetDatatypeProperty ("vClosed", value); }
        ///<summary>Gets value of vClosed, returns null is the property was not set</summary>
        public bool? get_vClosed() { var arr = GetDatatypeProperty_bool("vClosed"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of vCount</summary>
        public void set_vCount(long value) { SetDatatypeProperty ("vCount", value); }
        ///<summary>Gets value of vCount, returns null is the property was not set</summary>
        public long? get_vCount() { var arr = GetDatatypeProperty_long("vCount"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of vDegree</summary>
        public void set_vDegree(long value) { SetDatatypeProperty ("vDegree", value); }
        ///<summary>Gets value of vDegree, returns null is the property was not set</summary>
        public long? get_vDegree() { var arr = GetDatatypeProperty_long("vDegree"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of vSegmentationParts</summary>
        public void set_vSegmentationParts(long value) { SetDatatypeProperty ("vSegmentationParts", value); }
        ///<summary>Gets value of vSegmentationParts, returns null is the property was not set</summary>
        public long? get_vSegmentationParts() { var arr = GetDatatypeProperty_long("vSegmentationParts"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class SpotLight
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class SpotLight : DirectLight
    {
        /// <summary>
        /// Create new instace of OWL class SpotLight and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new SpotLight Create(Int64 model, string name=null) { return new SpotLight(Instance.Create(model, "SpotLight", name), "SpotLight");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public SpotLight(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "SpotLight") 
        {            
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Surface
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Surface : Face
    {
        /// <summary>
        /// Create new instace of OWL class Surface and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Surface Create(Int64 model, string name=null) { return new Surface(Instance.Create(model, "Surface", name), "Surface");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Surface(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Surface") 
        {            
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class SurfaceBySweptCurve
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class SurfaceBySweptCurve : FiniteSurface
    {
        /// <summary>
        /// Create new instace of OWL class SurfaceBySweptCurve and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new SurfaceBySweptCurve Create(Int64 model, string name=null) { return new SurfaceBySweptCurve(Instance.Create(model, "SurfaceBySweptCurve", name), "SurfaceBySweptCurve");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public SurfaceBySweptCurve(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "SurfaceBySweptCurve") 
        {            
        }

       //
       // Properties with known cardinality restrictions to SurfaceBySweptCurve
       //

        ///<summary>Sets relationship from this instance to an instance of Curve</summary>
        public void set_path(Curve instance) { SetObjectProperty("path", instance); }
        ///<summary>Get related instance</summary>
        public Curve get_path() 
        {
            var propId = GetPropertyId("path");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Curve(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets relationship from this instance to an instance of Curve</summary>
        public void set_sweptArea(Curve instance) { SetObjectProperty("sweptArea", instance); }
        ///<summary>Get related instance</summary>
        public Curve get_sweptArea() 
        {
            var propId = GetPropertyId("sweptArea");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Curve(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets relationships from this instance to an array of Curve. OWL cardinality 0..-1</summary>
        public void set_sweptAreaOpenings(Curve[] instances) { SetObjectProperty("sweptAreaOpenings", instances); }
        ///<summary>Sets relationships from this instance to an array of Int64. OWL cardinality 0..-1</summary>
        public void set_sweptAreaOpenings(Int64[] instances) { SetObjectProperty("sweptAreaOpenings", instances); }
        ///<summary>Get an array of related instances. OWL cardinality 0..-1</summary>
        public Curve[] get_sweptAreaOpenings() 
        {
            var propId = GetPropertyId("sweptAreaOpenings");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                var ret = new Curve[card];
                for (int i = 0; i < card; i++)
                {
                    ret[i] = new Curve(values[i], null);
                }

                return ret;
            }
            else
            {
                return null;
            }
        }
        ///<summary>Get an array of handles of related instances. OWL cardinality 0..-1</summary>
        public Int64[] get_sweptAreaOpenings_Int64()  
        {
            var propId = GetPropertyId("sweptAreaOpenings");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                return values;
            }
            else
            {
                return null;
            }
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class SurfaceOfLinearExtrusion
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class SurfaceOfLinearExtrusion : FiniteSurface
    {
        /// <summary>
        /// Create new instace of OWL class SurfaceOfLinearExtrusion and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new SurfaceOfLinearExtrusion Create(Int64 model, string name=null) { return new SurfaceOfLinearExtrusion(Instance.Create(model, "SurfaceOfLinearExtrusion", name), "SurfaceOfLinearExtrusion");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public SurfaceOfLinearExtrusion(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "SurfaceOfLinearExtrusion") 
        {            
        }

       //
       // Properties with known cardinality restrictions to SurfaceOfLinearExtrusion
       //

        ///<summary>Sets relationship from this instance to an instance of Vector</summary>
        public void set_extrusion(Vector instance) { SetObjectProperty("extrusion", instance); }
        ///<summary>Get related instance</summary>
        public Vector get_extrusion() 
        {
            var propId = GetPropertyId("extrusion");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Vector(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets relationship from this instance to an instance of Curve</summary>
        public void set_polygon(Curve instance) { SetObjectProperty("polygon", instance); }
        ///<summary>Get related instance</summary>
        public Curve get_polygon() 
        {
            var propId = GetPropertyId("polygon");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Curve(values[0], null);
            }
            else
            {
                return null;
            }
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class SurfaceOfRevolution
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class SurfaceOfRevolution : Surface
    {
        /// <summary>
        /// Create new instace of OWL class SurfaceOfRevolution and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new SurfaceOfRevolution Create(Int64 model, string name=null) { return new SurfaceOfRevolution(Instance.Create(model, "SurfaceOfRevolution", name), "SurfaceOfRevolution");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public SurfaceOfRevolution(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "SurfaceOfRevolution") 
        {            
        }

       //
       // Properties with known cardinality restrictions to SurfaceOfRevolution
       //

        ///<summary>Sets relationship from this instance to an instance of Curve</summary>
        public void set_path(Curve instance) { SetObjectProperty("path", instance); }
        ///<summary>Get related instance</summary>
        public Curve get_path() 
        {
            var propId = GetPropertyId("path");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Curve(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets value of segmentationParts</summary>
        public void set_segmentationParts(long value) { SetDatatypeProperty ("segmentationParts", value); }
        ///<summary>Gets value of segmentationParts, returns null is the property was not set</summary>
        public long? get_segmentationParts() { var arr = GetDatatypeProperty_long("segmentationParts"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class SweptAreaSolid
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class SweptAreaSolid : Solid
    {
        /// <summary>
        /// Create new instace of OWL class SweptAreaSolid and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new SweptAreaSolid Create(Int64 model, string name=null) { return new SweptAreaSolid(Instance.Create(model, "SweptAreaSolid", name), "SweptAreaSolid");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public SweptAreaSolid(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "SweptAreaSolid") 
        {            
        }

       //
       // Properties with known cardinality restrictions to SweptAreaSolid
       //

        ///<summary>Sets relationship from this instance to an instance of Vector</summary>
        public void set_direction(Vector instance) { SetObjectProperty("direction", instance); }
        ///<summary>Get related instance</summary>
        public Vector get_direction() 
        {
            var propId = GetPropertyId("direction");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Vector(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets value of fraction</summary>
        public void set_fraction(double value) { SetDatatypeProperty ("fraction", value); }
        ///<summary>Gets value of fraction, returns null is the property was not set</summary>
        public double? get_fraction() { var arr = GetDatatypeProperty_double("fraction"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets relationship from this instance to an instance of Curve</summary>
        public void set_path(Curve instance) { SetObjectProperty("path", instance); }
        ///<summary>Get related instance</summary>
        public Curve get_path() 
        {
            var propId = GetPropertyId("path");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Curve(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets relationship from this instance to an instance of Curve</summary>
        public void set_sweptArea(Curve instance) { SetObjectProperty("sweptArea", instance); }
        ///<summary>Get related instance</summary>
        public Curve get_sweptArea() 
        {
            var propId = GetPropertyId("sweptArea");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Curve(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets relationships from this instance to an array of Curve. OWL cardinality 0..-1</summary>
        public void set_sweptAreaOpenings(Curve[] instances) { SetObjectProperty("sweptAreaOpenings", instances); }
        ///<summary>Sets relationships from this instance to an array of Int64. OWL cardinality 0..-1</summary>
        public void set_sweptAreaOpenings(Int64[] instances) { SetObjectProperty("sweptAreaOpenings", instances); }
        ///<summary>Get an array of related instances. OWL cardinality 0..-1</summary>
        public Curve[] get_sweptAreaOpenings() 
        {
            var propId = GetPropertyId("sweptAreaOpenings");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                var ret = new Curve[card];
                for (int i = 0; i < card; i++)
                {
                    ret[i] = new Curve(values[i], null);
                }

                return ret;
            }
            else
            {
                return null;
            }
        }
        ///<summary>Get an array of handles of related instances. OWL cardinality 0..-1</summary>
        public Int64[] get_sweptAreaOpenings_Int64()  
        {
            var propId = GetPropertyId("sweptAreaOpenings");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                return values;
            }
            else
            {
                return null;
            }
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class SweptAreaSolidSet
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class SweptAreaSolidSet : Solid
    {
        /// <summary>
        /// Create new instace of OWL class SweptAreaSolidSet and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new SweptAreaSolidSet Create(Int64 model, string name=null) { return new SweptAreaSolidSet(Instance.Create(model, "SweptAreaSolidSet", name), "SweptAreaSolidSet");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public SweptAreaSolidSet(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "SweptAreaSolidSet") 
        {            
        }

       //
       // Properties with known cardinality restrictions to SweptAreaSolidSet
       //

        ///<summary>Sets relationship from this instance to an instance of Vector</summary>
        public void set_direction(Vector instance) { SetObjectProperty("direction", instance); }
        ///<summary>Get related instance</summary>
        public Vector get_direction() 
        {
            var propId = GetPropertyId("direction");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Vector(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets value of fraction</summary>
        public void set_fraction(double value) { SetDatatypeProperty ("fraction", value); }
        ///<summary>Gets value of fraction, returns null is the property was not set</summary>
        public double? get_fraction() { var arr = GetDatatypeProperty_double("fraction"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets relationship from this instance to an instance of Curve</summary>
        public void set_path(Curve instance) { SetObjectProperty("path", instance); }
        ///<summary>Get related instance</summary>
        public Curve get_path() 
        {
            var propId = GetPropertyId("path");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Curve(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets relationships from this instance to an array of Curve. OWL cardinality 1..-1</summary>
        public void set_sweptAreaSet(Curve[] instances) { SetObjectProperty("sweptAreaSet", instances); }
        ///<summary>Sets relationships from this instance to an array of Int64. OWL cardinality 1..-1</summary>
        public void set_sweptAreaSet(Int64[] instances) { SetObjectProperty("sweptAreaSet", instances); }
        ///<summary>Get an array of related instances. OWL cardinality 1..-1</summary>
        public Curve[] get_sweptAreaSet() 
        {
            var propId = GetPropertyId("sweptAreaSet");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                var ret = new Curve[card];
                for (int i = 0; i < card; i++)
                {
                    ret[i] = new Curve(values[i], null);
                }

                return ret;
            }
            else
            {
                return null;
            }
        }
        ///<summary>Get an array of handles of related instances. OWL cardinality 1..-1</summary>
        public Int64[] get_sweptAreaSet_Int64()  
        {
            var propId = GetPropertyId("sweptAreaSet");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                return values;
            }
            else
            {
                return null;
            }
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class SweptAreaSolidTapered
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class SweptAreaSolidTapered : Solid
    {
        /// <summary>
        /// Create new instace of OWL class SweptAreaSolidTapered and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new SweptAreaSolidTapered Create(Int64 model, string name=null) { return new SweptAreaSolidTapered(Instance.Create(model, "SweptAreaSolidTapered", name), "SweptAreaSolidTapered");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public SweptAreaSolidTapered(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "SweptAreaSolidTapered") 
        {            
        }

       //
       // Properties with known cardinality restrictions to SweptAreaSolidTapered
       //

        ///<summary>Sets relationship from this instance to an instance of Vector</summary>
        public void set_direction(Vector instance) { SetObjectProperty("direction", instance); }
        ///<summary>Get related instance</summary>
        public Vector get_direction() 
        {
            var propId = GetPropertyId("direction");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Vector(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets value of fraction</summary>
        public void set_fraction(double value) { SetDatatypeProperty ("fraction", value); }
        ///<summary>Gets value of fraction, returns null is the property was not set</summary>
        public double? get_fraction() { var arr = GetDatatypeProperty_double("fraction"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets relationship from this instance to an instance of Curve</summary>
        public void set_path(Curve instance) { SetObjectProperty("path", instance); }
        ///<summary>Get related instance</summary>
        public Curve get_path() 
        {
            var propId = GetPropertyId("path");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Curve(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets relationship from this instance to an instance of Curve</summary>
        public void set_sweptArea(Curve instance) { SetObjectProperty("sweptArea", instance); }
        ///<summary>Get related instance</summary>
        public Curve get_sweptArea() 
        {
            var propId = GetPropertyId("sweptArea");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Curve(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets relationship from this instance to an instance of Curve</summary>
        public void set_sweptAreaEnd(Curve instance) { SetObjectProperty("sweptAreaEnd", instance); }
        ///<summary>Get related instance</summary>
        public Curve get_sweptAreaEnd() 
        {
            var propId = GetPropertyId("sweptAreaEnd");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Curve(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets relationships from this instance to an array of Curve. OWL cardinality 0..-1</summary>
        public void set_sweptAreaEndOpenings(Curve[] instances) { SetObjectProperty("sweptAreaEndOpenings", instances); }
        ///<summary>Sets relationships from this instance to an array of Int64. OWL cardinality 0..-1</summary>
        public void set_sweptAreaEndOpenings(Int64[] instances) { SetObjectProperty("sweptAreaEndOpenings", instances); }
        ///<summary>Get an array of related instances. OWL cardinality 0..-1</summary>
        public Curve[] get_sweptAreaEndOpenings() 
        {
            var propId = GetPropertyId("sweptAreaEndOpenings");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                var ret = new Curve[card];
                for (int i = 0; i < card; i++)
                {
                    ret[i] = new Curve(values[i], null);
                }

                return ret;
            }
            else
            {
                return null;
            }
        }
        ///<summary>Get an array of handles of related instances. OWL cardinality 0..-1</summary>
        public Int64[] get_sweptAreaEndOpenings_Int64()  
        {
            var propId = GetPropertyId("sweptAreaEndOpenings");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                return values;
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets relationships from this instance to an array of Curve. OWL cardinality 0..-1</summary>
        public void set_sweptAreaOpenings(Curve[] instances) { SetObjectProperty("sweptAreaOpenings", instances); }
        ///<summary>Sets relationships from this instance to an array of Int64. OWL cardinality 0..-1</summary>
        public void set_sweptAreaOpenings(Int64[] instances) { SetObjectProperty("sweptAreaOpenings", instances); }
        ///<summary>Get an array of related instances. OWL cardinality 0..-1</summary>
        public Curve[] get_sweptAreaOpenings() 
        {
            var propId = GetPropertyId("sweptAreaOpenings");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                var ret = new Curve[card];
                for (int i = 0; i < card; i++)
                {
                    ret[i] = new Curve(values[i], null);
                }

                return ret;
            }
            else
            {
                return null;
            }
        }
        ///<summary>Get an array of handles of related instances. OWL cardinality 0..-1</summary>
        public Int64[] get_sweptAreaOpenings_Int64()  
        {
            var propId = GetPropertyId("sweptAreaOpenings");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                return values;
            }
            else
            {
                return null;
            }
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class SweptBlend
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class SweptBlend : Solid
    {
        /// <summary>
        /// Create new instace of OWL class SweptBlend and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new SweptBlend Create(Int64 model, string name=null) { return new SweptBlend(Instance.Create(model, "SweptBlend", name), "SweptBlend");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public SweptBlend(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "SweptBlend") 
        {            
        }

       //
       // Properties with known cardinality restrictions to SweptBlend
       //

        ///<summary>Sets relationship from this instance to an instance of Curve</summary>
        public void set_bottomPolygon(Curve instance) { SetObjectProperty("bottomPolygon", instance); }
        ///<summary>Get related instance</summary>
        public Curve get_bottomPolygon() 
        {
            var propId = GetPropertyId("bottomPolygon");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Curve(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets values of connectionMap. OWL cardinality 0..-1</summary>
        public void set_connectionMap(long[] values) { SetDatatypeProperty ("connectionMap", values); }
        ///<summary>Gets values of connectionMap. OWL cardinality 0..-1</summary>
        public long[] get_connectionMap() { return GetDatatypeProperty_long("connectionMap"); }
        ///<summary>Sets values of forcedStaticDirection. OWL cardinality 0..3</summary>
        public void set_forcedStaticDirection(double[] values) { SetDatatypeProperty ("forcedStaticDirection", values); }
        ///<summary>Gets values of forcedStaticDirection. OWL cardinality 0..3</summary>
        public double[] get_forcedStaticDirection() { return GetDatatypeProperty_double("forcedStaticDirection"); }
        ///<summary>Sets value of forceSolid</summary>
        public void set_forceSolid(bool value) { SetDatatypeProperty ("forceSolid", value); }
        ///<summary>Gets value of forceSolid, returns null is the property was not set</summary>
        public bool? get_forceSolid() { var arr = GetDatatypeProperty_bool("forceSolid"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of fraction</summary>
        public void set_fraction(double value) { SetDatatypeProperty ("fraction", value); }
        ///<summary>Gets value of fraction, returns null is the property was not set</summary>
        public double? get_fraction() { var arr = GetDatatypeProperty_double("fraction"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of hasBottom</summary>
        public void set_hasBottom(bool value) { SetDatatypeProperty ("hasBottom", value); }
        ///<summary>Gets value of hasBottom, returns null is the property was not set</summary>
        public bool? get_hasBottom() { var arr = GetDatatypeProperty_bool("hasBottom"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of hasTop</summary>
        public void set_hasTop(bool value) { SetDatatypeProperty ("hasTop", value); }
        ///<summary>Gets value of hasTop, returns null is the property was not set</summary>
        public bool? get_hasTop() { var arr = GetDatatypeProperty_bool("hasTop"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets relationship from this instance to an instance of Curve</summary>
        public void set_path(Curve instance) { SetObjectProperty("path", instance); }
        ///<summary>Get related instance</summary>
        public Curve get_path() 
        {
            var propId = GetPropertyId("path");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Curve(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets relationship from this instance to an instance of Curve</summary>
        public void set_topPolygon(Curve instance) { SetObjectProperty("topPolygon", instance); }
        ///<summary>Get related instance</summary>
        public Curve get_topPolygon() 
        {
            var propId = GetPropertyId("topPolygon");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Curve(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets value of usesAbsolutePlacement</summary>
        public void set_usesAbsolutePlacement(bool value) { SetDatatypeProperty ("usesAbsolutePlacement", value); }
        ///<summary>Gets value of usesAbsolutePlacement, returns null is the property was not set</summary>
        public bool? get_usesAbsolutePlacement() { var arr = GetDatatypeProperty_bool("usesAbsolutePlacement"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Texture
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Texture : Appearance
    {
        /// <summary>
        /// Create new instace of OWL class Texture and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Texture Create(Int64 model, string name=null) { return new Texture(Instance.Create(model, "Texture", name), "Texture");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Texture(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Texture") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Texture
       //

        ///<summary>Sets value of name</summary>
        public void set_name(string value) { SetDatatypeProperty ("name", value); }
        ///<summary>Gets value of name, returns null is the property was not set</summary>
        public string get_name() { var arr = GetDatatypeProperty_string("name"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of offsetX</summary>
        public void set_offsetX(double value) { SetDatatypeProperty ("offsetX", value); }
        ///<summary>Gets value of offsetX, returns null is the property was not set</summary>
        public double? get_offsetX() { var arr = GetDatatypeProperty_double("offsetX"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of offsetY</summary>
        public void set_offsetY(double value) { SetDatatypeProperty ("offsetY", value); }
        ///<summary>Gets value of offsetY, returns null is the property was not set</summary>
        public double? get_offsetY() { var arr = GetDatatypeProperty_double("offsetY"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets values of origin. OWL cardinality 0..3</summary>
        public void set_origin(double[] values) { SetDatatypeProperty ("origin", values); }
        ///<summary>Gets values of origin. OWL cardinality 0..3</summary>
        public double[] get_origin() { return GetDatatypeProperty_double("origin"); }
        ///<summary>Sets value of rotation</summary>
        public void set_rotation(double value) { SetDatatypeProperty ("rotation", value); }
        ///<summary>Gets value of rotation, returns null is the property was not set</summary>
        public double? get_rotation() { var arr = GetDatatypeProperty_double("rotation"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of scalingX</summary>
        public void set_scalingX(double value) { SetDatatypeProperty ("scalingX", value); }
        ///<summary>Gets value of scalingX, returns null is the property was not set</summary>
        public double? get_scalingX() { var arr = GetDatatypeProperty_double("scalingX"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of scalingY</summary>
        public void set_scalingY(double value) { SetDatatypeProperty ("scalingY", value); }
        ///<summary>Gets value of scalingY, returns null is the property was not set</summary>
        public double? get_scalingY() { var arr = GetDatatypeProperty_double("scalingY"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of type</summary>
        public void set_type(long value) { SetDatatypeProperty ("type", value); }
        ///<summary>Gets value of type, returns null is the property was not set</summary>
        public long? get_type() { var arr = GetDatatypeProperty_long("type"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class ToroidalSurface
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class ToroidalSurface : FiniteSurface
    {
        /// <summary>
        /// Create new instace of OWL class ToroidalSurface and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new ToroidalSurface Create(Int64 model, string name=null) { return new ToroidalSurface(Instance.Create(model, "ToroidalSurface", name), "ToroidalSurface");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public ToroidalSurface(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "ToroidalSurface") 
        {            
        }

       //
       // Properties with known cardinality restrictions to ToroidalSurface
       //

        ///<summary>Sets value of majorRadius</summary>
        public void set_majorRadius(double value) { SetDatatypeProperty ("majorRadius", value); }
        ///<summary>Gets value of majorRadius, returns null is the property was not set</summary>
        public double? get_majorRadius() { var arr = GetDatatypeProperty_double("majorRadius"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of minorRadius</summary>
        public void set_minorRadius(double value) { SetDatatypeProperty ("minorRadius", value); }
        ///<summary>Gets value of minorRadius, returns null is the property was not set</summary>
        public double? get_minorRadius() { var arr = GetDatatypeProperty_double("minorRadius"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of segmentationParts</summary>
        public void set_segmentationParts(long value) { SetDatatypeProperty ("segmentationParts", value); }
        ///<summary>Gets value of segmentationParts, returns null is the property was not set</summary>
        public long? get_segmentationParts() { var arr = GetDatatypeProperty_long("segmentationParts"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Torus
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Torus : Solid
    {
        /// <summary>
        /// Create new instace of OWL class Torus and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Torus Create(Int64 model, string name=null) { return new Torus(Instance.Create(model, "Torus", name), "Torus");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Torus(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Torus") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Torus
       //

        ///<summary>Sets value of majorRadius</summary>
        public void set_majorRadius(double value) { SetDatatypeProperty ("majorRadius", value); }
        ///<summary>Gets value of majorRadius, returns null is the property was not set</summary>
        public double? get_majorRadius() { var arr = GetDatatypeProperty_double("majorRadius"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of minorRadius</summary>
        public void set_minorRadius(double value) { SetDatatypeProperty ("minorRadius", value); }
        ///<summary>Gets value of minorRadius, returns null is the property was not set</summary>
        public double? get_minorRadius() { var arr = GetDatatypeProperty_double("minorRadius"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of segmentationParts</summary>
        public void set_segmentationParts(long value) { SetDatatypeProperty ("segmentationParts", value); }
        ///<summary>Gets value of segmentationParts, returns null is the property was not set</summary>
        public long? get_segmentationParts() { var arr = GetDatatypeProperty_long("segmentationParts"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Transformation
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Transformation : Curve
    {
        /// <summary>
        /// Create new instace of OWL class Transformation and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Transformation Create(Int64 model, string name=null) { return new Transformation(Instance.Create(model, "Transformation", name), "Transformation");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Transformation(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Transformation") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Transformation
       //

        ///<summary>Sets relationship from this instance to an instance of Matrix</summary>
        public void set_matrix(Matrix instance) { SetObjectProperty("matrix", instance); }
        ///<summary>Get related instance</summary>
        public Matrix get_matrix() 
        {
            var propId = GetPropertyId("matrix");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Matrix(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets relationship from this instance to an instance of GeometricItem</summary>
        public void set_object(GeometricItem instance) { SetObjectProperty("object", instance); }
        ///<summary>Get related instance</summary>
        public GeometricItem get_object() 
        {
            var propId = GetPropertyId("object");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new GeometricItem(values[0], null);
            }
            else
            {
                return null;
            }
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class TransitionalCurve
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class TransitionalCurve : Curve
    {
        /// <summary>
        /// Create new instace of OWL class TransitionalCurve and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new TransitionalCurve Create(Int64 model, string name=null) { return new TransitionalCurve(Instance.Create(model, "TransitionalCurve", name), "TransitionalCurve");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public TransitionalCurve(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "TransitionalCurve") 
        {            
        }

       //
       // Properties with known cardinality restrictions to TransitionalCurve
       //

        ///<summary>Sets relationship from this instance to an instance of Vector</summary>
        public void set_direction(Vector instance) { SetObjectProperty("direction", instance); }
        ///<summary>Get related instance</summary>
        public Vector get_direction() 
        {
            var propId = GetPropertyId("direction");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Vector(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets value of length</summary>
        public void set_length(double value) { SetDatatypeProperty ("length", value); }
        ///<summary>Gets value of length, returns null is the property was not set</summary>
        public double? get_length() { var arr = GetDatatypeProperty_double("length"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets relationship from this instance to an instance of Curve</summary>
        public void set_path(Curve instance) { SetObjectProperty("path", instance); }
        ///<summary>Get related instance</summary>
        public Curve get_path() 
        {
            var propId = GetPropertyId("path");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Curve(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets value of radiusI</summary>
        public void set_radiusI(double value) { SetDatatypeProperty ("radiusI", value); }
        ///<summary>Gets value of radiusI, returns null is the property was not set</summary>
        public double? get_radiusI() { var arr = GetDatatypeProperty_double("radiusI"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of radiusII</summary>
        public void set_radiusII(double value) { SetDatatypeProperty ("radiusII", value); }
        ///<summary>Gets value of radiusII, returns null is the property was not set</summary>
        public double? get_radiusII() { var arr = GetDatatypeProperty_double("radiusII"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of segmentationParts</summary>
        public void set_segmentationParts(long value) { SetDatatypeProperty ("segmentationParts", value); }
        ///<summary>Gets value of segmentationParts, returns null is the property was not set</summary>
        public long? get_segmentationParts() { var arr = GetDatatypeProperty_long("segmentationParts"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class TriangleCurve
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class TriangleCurve : Curve
    {
        /// <summary>
        /// Create new instace of OWL class TriangleCurve and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new TriangleCurve Create(Int64 model, string name=null) { return new TriangleCurve(Instance.Create(model, "TriangleCurve", name), "TriangleCurve");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public TriangleCurve(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "TriangleCurve") 
        {            
        }

       //
       // Properties with known cardinality restrictions to TriangleCurve
       //

        ///<summary>Sets values of coordinates. OWL cardinality 0..9</summary>
        public void set_coordinates(double[] values) { SetDatatypeProperty ("coordinates", values); }
        ///<summary>Gets values of coordinates. OWL cardinality 0..9</summary>
        public double[] get_coordinates() { return GetDatatypeProperty_double("coordinates"); }
        ///<summary>Sets value of offsetX</summary>
        public void set_offsetX(double value) { SetDatatypeProperty ("offsetX", value); }
        ///<summary>Gets value of offsetX, returns null is the property was not set</summary>
        public double? get_offsetX() { var arr = GetDatatypeProperty_double("offsetX"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of offsetY</summary>
        public void set_offsetY(double value) { SetDatatypeProperty ("offsetY", value); }
        ///<summary>Gets value of offsetY, returns null is the property was not set</summary>
        public double? get_offsetY() { var arr = GetDatatypeProperty_double("offsetY"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of offsetZ</summary>
        public void set_offsetZ(double value) { SetDatatypeProperty ("offsetZ", value); }
        ///<summary>Gets value of offsetZ, returns null is the property was not set</summary>
        public double? get_offsetZ() { var arr = GetDatatypeProperty_double("offsetZ"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets relationships from this instance to an array of Point3D. OWL cardinality 0..3</summary>
        public void set_pointReferences(Point3D[] instances) { SetObjectProperty("pointReferences", instances); }
        ///<summary>Sets relationships from this instance to an array of Int64. OWL cardinality 0..3</summary>
        public void set_pointReferences(Int64[] instances) { SetObjectProperty("pointReferences", instances); }
        ///<summary>Get an array of related instances. OWL cardinality 0..3</summary>
        public Point3D[] get_pointReferences() 
        {
            var propId = GetPropertyId("pointReferences");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                var ret = new Point3D[card];
                for (int i = 0; i < card; i++)
                {
                    ret[i] = new Point3D(values[i], null);
                }

                return ret;
            }
            else
            {
                return null;
            }
        }
        ///<summary>Get an array of handles of related instances. OWL cardinality 0..3</summary>
        public Int64[] get_pointReferences_Int64()  
        {
            var propId = GetPropertyId("pointReferences");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                return values;
            }
            else
            {
                return null;
            }
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class TriangleReduction
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class TriangleReduction : Solid
    {
        /// <summary>
        /// Create new instace of OWL class TriangleReduction and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new TriangleReduction Create(Int64 model, string name=null) { return new TriangleReduction(Instance.Create(model, "TriangleReduction", name), "TriangleReduction");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public TriangleReduction(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "TriangleReduction") 
        {            
        }

       //
       // Properties with known cardinality restrictions to TriangleReduction
       //

        ///<summary>Sets value of innerFraction</summary>
        public void set_innerFraction(double value) { SetDatatypeProperty ("innerFraction", value); }
        ///<summary>Gets value of innerFraction, returns null is the property was not set</summary>
        public double? get_innerFraction() { var arr = GetDatatypeProperty_double("innerFraction"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets relationship from this instance to an instance of GeometricItem</summary>
        public void set_object(GeometricItem instance) { SetObjectProperty("object", instance); }
        ///<summary>Get related instance</summary>
        public GeometricItem get_object() 
        {
            var propId = GetPropertyId("object");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new GeometricItem(values[0], null);
            }
            else
            {
                return null;
            }
        }
        ///<summary>Sets value of outerFraction</summary>
        public void set_outerFraction(double value) { SetDatatypeProperty ("outerFraction", value); }
        ///<summary>Gets value of outerFraction, returns null is the property was not set</summary>
        public double? get_outerFraction() { var arr = GetDatatypeProperty_double("outerFraction"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class TriangleSet
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class TriangleSet : Surface
    {
        /// <summary>
        /// Create new instace of OWL class TriangleSet and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new TriangleSet Create(Int64 model, string name=null) { return new TriangleSet(Instance.Create(model, "TriangleSet", name), "TriangleSet");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public TriangleSet(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "TriangleSet") 
        {            
        }

       //
       // Properties with known cardinality restrictions to TriangleSet
       //

        ///<summary>Sets values of indices. OWL cardinality 0..-1</summary>
        public void set_indices(long[] values) { SetDatatypeProperty ("indices", values); }
        ///<summary>Gets values of indices. OWL cardinality 0..-1</summary>
        public long[] get_indices() { return GetDatatypeProperty_long("indices"); }
        ///<summary>Sets values of vertices. OWL cardinality 3..-1</summary>
        public void set_vertices(double[] values) { SetDatatypeProperty ("vertices", values); }
        ///<summary>Gets values of vertices. OWL cardinality 3..-1</summary>
        public double[] get_vertices() { return GetDatatypeProperty_double("vertices"); }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Vector
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Vector : Mathematics
    {
        /// <summary>
        /// Create new instace of OWL class Vector and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Vector Create(Int64 model, string name=null) { return new Vector(Instance.Create(model, "Vector", name), "Vector");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Vector(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Vector") 
        {            
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class Vector3
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Vector3 : Vector
    {
        /// <summary>
        /// Create new instace of OWL class Vector3 and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new Vector3 Create(Int64 model, string name=null) { return new Vector3(Instance.Create(model, "Vector3", name), "Vector3");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Vector3(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "Vector3") 
        {            
        }

       //
       // Properties with known cardinality restrictions to Vector3
       //

        ///<summary>Sets values of coordinates. OWL cardinality 0..3</summary>
        public void set_coordinates(double[] values) { SetDatatypeProperty ("coordinates", values); }
        ///<summary>Gets values of coordinates. OWL cardinality 0..3</summary>
        public double[] get_coordinates() { return GetDatatypeProperty_double("coordinates"); }
        ///<summary>Sets value of x</summary>
        public void set_x(double value) { SetDatatypeProperty ("x", value); }
        ///<summary>Gets value of x, returns null is the property was not set</summary>
        public double? get_x() { var arr = GetDatatypeProperty_double("x"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of y</summary>
        public void set_y(double value) { SetDatatypeProperty ("y", value); }
        ///<summary>Gets value of y, returns null is the property was not set</summary>
        public double? get_y() { var arr = GetDatatypeProperty_double("y"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
        ///<summary>Sets value of z</summary>
        public void set_z(double value) { SetDatatypeProperty ("z", value); }
        ///<summary>Gets value of z, returns null is the property was not set</summary>
        public double? get_z() { var arr = GetDatatypeProperty_double("z"); return (arr != null && arr.Length > 0) ? arr[0] : null; }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class View
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class View : Environment
    {
        /// <summary>
        /// Create new instace of OWL class View and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new View Create(Int64 model, string name=null) { return new View(Instance.Create(model, "View", name), "View");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public View(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "View") 
        {            
        }

       //
       // Properties with known cardinality restrictions to View
       //

        ///<summary>Sets relationship from this instance to an instance of Matrix</summary>
        public void set_matrix(Matrix instance) { SetObjectProperty("matrix", instance); }
        ///<summary>Get related instance</summary>
        public Matrix get_matrix() 
        {
            var propId = GetPropertyId("matrix");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Matrix(values[0], null);
            }
            else
            {
                return null;
            }
        }
    }


    /// <summary>
    /// Provides utility methods to interact with an instnace of OWL class World
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class World : Environment
    {
        /// <summary>
        /// Create new instace of OWL class World and returns object of this C# class to interact with
        /// </summary>
        /// <param name="model">The handle to the model</param>
        /// <param name="name">This attribute represents the name of the instance (given as char array / ASCII). The name is given by the host and the attribute is not changed</param>
        /// <returns></returns>
        public static new World Create(Int64 model, string name=null) { return new World(Instance.Create(model, "World", name), "World");}
        
        /// <summary>
        /// Constructs object of this C# class that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public World(Int64 instance, string checkClassName = null) 
            : base (instance, (checkClassName!=null) ? checkClassName : "World") 
        {            
        }

       //
       // Properties with known cardinality restrictions to World
       //

        ///<summary>Sets relationship from this instance to an instance of Matrix</summary>
        public void set_matrix(Matrix instance) { SetObjectProperty("matrix", instance); }
        ///<summary>Get related instance</summary>
        public Matrix get_matrix() 
        {
            var propId = GetPropertyId("matrix");

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetObjectProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new Int64[1];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)1);

                return new Matrix(values[0], null);
            }
            else
            {
                return null;
            }
        }
    }


    /// <summary>
    /// Provides utility methods to interact with a genetic instnace of OWL class
    /// You also can use object of this C# class instead of Int64 handle of the OWL instance in any place where the handle is required
    /// </summary>
    public class Instance : IEquatable<Instance>, IComparable, IComparable<Instance>
    {
        /// <summary>
        /// Create an instance of specified class
        /// </summary>
        public static Int64 Create(Int64 model, string className, string instanseName)
        {
            Int64 clsid = engine.GetClassByName(model, className);
            System.Diagnostics.Debug.Assert(clsid != 0);

            Int64 instance = engine.CreateInstance(clsid, instanseName);
            System.Diagnostics.Debug.Assert(instance != 0);

            return instance;
        }

        /// <summary>
        /// Create an instance of specified class
        /// </summary>
        public static Int64 Create(Int64 model, string className) { return Create(model, className, null); }

        /// <summary>
        /// underlyed instance handle
        /// </summary>
        protected Int64 m_instance = 0;

        /// <summary>
        /// Constructs object that wraps existing OWL instance
        /// </summary>
        /// <param name="instance">OWL instance to interact with</param>
        /// <param name="checkClassName">Expected OWL class of the instance, used for diagnostic (optionally)</param>
        public Instance(Int64 instance, string cls)
        {
            m_instance = instance;
#if DEBUG
            if (m_instance != 0 && cls != null)
            {
                var clsid1 = engine.GetInstanceClass(m_instance);
                var model = engine.GetModel(m_instance);
                var clsid2 = engine.GetClassByName(model, cls);
                System.Diagnostics.Trace.Assert(clsid1 == clsid2);
            }
#endif
        }


        /// <summary>
        /// Conversion to instance handle, so the object of the class can be used anywhere where a handle required
        /// </summary>
        public static implicit operator Int64(Instance instance) => instance.m_instance;

        /// <summary>
        /// Get property id from property name
        /// </summary>
        public Int64 GetPropertyId(string name)
        {
            var model = engine.GetModel(m_instance);
            System.Diagnostics.Debug.Assert(model != 0);

            var propId = engine.GetPropertyByName(model, name);
            System.Diagnostics.Debug.Assert(propId != 0);

            return propId;
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetDatatypeProperty(string name, double value)
        {
            var propId = GetPropertyId(name);
            var res = engine.SetDatatypeProperty(m_instance, propId, ref value, 1);
            System.Diagnostics.Debug.Assert(res == 0);
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetDatatypeProperty(string name, double[] values)
        {
            var propId = GetPropertyId(name);
            var res = engine.SetDatatypeProperty(m_instance, propId, values, values.Length);
            System.Diagnostics.Debug.Assert(res == 0);
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetDatatypeProperty(string name, long value)
        {
            var propId = GetPropertyId(name);
            var res = engine.SetDatatypeProperty(m_instance, propId, ref value, 1);
            System.Diagnostics.Debug.Assert(res == 0);
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetDatatypeProperty(string name, long[] values)
        {
            var propId = GetPropertyId(name);
            var res = engine.SetDatatypeProperty(m_instance, propId, values, values.Length);
            System.Diagnostics.Debug.Assert(res == 0);
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetDatatypeProperty(string name, bool value)
        {
            var propId = GetPropertyId(name);
            byte v = (byte)(value ? 1 : 0);
            var res = engine.SetDatatypeProperty(m_instance, propId, ref v, 1);
            System.Diagnostics.Debug.Assert(res == 0);
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetDatatypeProperty(string name, bool[] values)
        {
            byte[] bytes = new byte[values.Length];
            for (int i = 0; i < values.Length; i++)
                bytes[i] = values[i] ? (byte)1 : (byte)0;

            var propId = GetPropertyId(name);
            var res = engine.SetDatatypeProperty(m_instance, propId, bytes, values.Length);
            System.Diagnostics.Debug.Assert(res == 0);
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetDatatypeProperty(string name, string value)
        {
            var propId = GetPropertyId(name);
            var res = engine.SetDatatypeProperty(m_instance, propId, ref value, 1);
            System.Diagnostics.Debug.Assert(res == 0);
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetDatatypeProperty(string name, string[] values)
        {
            var propId = GetPropertyId(name);
            var res = engine.SetDatatypeProperty(m_instance, propId, values, values.Length);
            System.Diagnostics.Debug.Assert(res == 0);
        }

        /// <summary>
        /// 
        /// </summary>
        public double[] GetDatatypeProperty_double(string name)
        {
            var propId = GetPropertyId(name);

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetDatatypeProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new double[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                return values;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public long[] GetDatatypeProperty_long(string name)
        {
            var propId = GetPropertyId(name);

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetDatatypeProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new long[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                return values;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool[] GetDatatypeProperty_bool(string name)
        {
            var propId = GetPropertyId(name);

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetDatatypeProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new long[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                var bools = new bool[card];
                for (int i = 0; i < card; i++)
                {
                    bools[i] = values[i] != 0;
                }

                return bools;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string[] GetDatatypeProperty_string(string name)
        {
            var propId = GetPropertyId(name);

            Int64 card = 0;
            IntPtr valuesPtr = IntPtr.Zero;
            var res = engine.GetDatatypeProperty(m_instance, propId, out valuesPtr, out card);
            System.Diagnostics.Debug.Assert(res == 0);

            if (card > 0)
            {
                var values = new IntPtr[card];
                System.Runtime.InteropServices.Marshal.Copy(valuesPtr, values, 0, (int)card);

                var strings = new string[card];
                for (int i = 0; i < (int)card; i++)
                {
                    strings[i] = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(values[i]);
                }
                return strings;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetObjectProperty(string name, Int64 instance)
        {
            var propId = GetPropertyId(name);
            var res = engine.SetObjectProperty(m_instance, propId, ref instance, 1);
            System.Diagnostics.Debug.Assert(res == 0);
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetObjectProperty(string name, Instance[] instances)
        {
            var inst = new Int64[instances.Length];
            for (int i = 0; i < instances.Length; i++)
                inst[i] = instances[i];

            SetObjectProperty(name, inst);
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetObjectProperty(string name, Int64[] instances)
        {
            var propId = GetPropertyId(name);

            var inst = new Int64[instances.Length];
            for (int i = 0; i < instances.Length; i++)
                inst[i] = instances[i];

            var res = engine.SetObjectProperty(m_instance, propId, ref inst[0], inst.Length);
            System.Diagnostics.Debug.Assert(res == 0);
        }


        /// <summary>
        /// 
        /// </summary>
        public static bool operator ==(Instance i1, Instance i2) => (Equals(i1, i2));

        /// <summary>
        /// 
        /// </summary>
        public static bool operator !=(Instance i1, Instance i2) => (!(i1 == i2));

        /// <summary>
        /// 
        /// </summary>
        public override bool Equals(Object obj) 
        {
            return Equals(obj as Instance); 
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Equals(Instance other)     
        {
            return (other == null) ? false : (other.m_instance == m_instance);
        }

        /// <summary>
        /// 
        /// </summary>
        public int CompareTo(object obj)
        {
            return CompareTo (obj as Instance);
        }

        /// <summary>
        /// 
        /// </summary>
        public int CompareTo(Instance other)
        {
            return (other==null)?1:m_instance.CompareTo (other.m_instance);
        }

        /// <summary>
        /// 
        /// </summary>
        public override int GetHashCode()
        {
            return m_instance.GetHashCode();
        }

    }
}

