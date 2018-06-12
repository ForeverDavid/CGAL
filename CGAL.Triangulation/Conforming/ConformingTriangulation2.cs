using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Common.Core.LinearAlgebra;
using CGAL.Polygons;
using Common.Geometry.Shapes;
using CGAL.Meshes.Constructors;
using CGAL.Meshes.Descriptors;
using CGAL.Meshes.IndexBased;

namespace CGAL.Triangulation.Conforming
{
    public struct ConformingCriteria
    {
        public float lenBounds;
        public float angBounds;
        public int iterations;
        public IEnumerable<Vector2f> seeds;
    }

    public static class ConformingTriangulation2
    {

        private const int SUCCESS = 0;
        private const int ERROR = 1;

        private const float MAX_ANGLE_BOUNDS = 0.125f;

        public static Mesh2f Triangulate(Polygon2f polygon,  ConformingCriteria crit = new ConformingCriteria())
        {
            var constructor = new MeshConstructor2f();
            Triangulate(polygon, constructor, crit);
            return constructor.PopMesh();
        }

        public static void Triangulate<MESH>(Polygon2f polygon, IMeshConstructor<MESH> constructor, ConformingCriteria crit = new ConformingCriteria())
        {
            InsertPolygon(polygon);
            InsertSeeds(crit.seeds);

            Box2f bounds = Box2f.CalculateBounds(polygon.Positions);
            CGAL_InsertSeed(bounds.Min - 0.1f);

            MeshDescriptor des = Triangulate(crit.iterations, crit.angBounds, crit.lenBounds);

            CreateMesh(constructor, des);

            CGAL_Clear();

        }

        private static MeshDescriptor Triangulate(int iterations, float angBounds, float lenBounds)
        {
            if (angBounds < 0.0f) angBounds = 0.0f;
            if (angBounds > MAX_ANGLE_BOUNDS) angBounds = MAX_ANGLE_BOUNDS;
            if (lenBounds < 0.0f) lenBounds = 0.0f;

            if(CGAL_RefineMesh(iterations, angBounds, lenBounds) != SUCCESS)
                throw new Exception("Error refining points.");

            MeshDescriptor descriptor;
            if (CGAL_Triangulate(out descriptor) != SUCCESS)
                throw new Exception("Error triangulating points.");

            return descriptor;
        }

        private static void InsertSeeds(IEnumerable<Vector2f> seeds)
        {
            if (seeds == null) return;

            foreach (var seed in seeds)
                CGAL_InsertSeed(seed);
        }

        private static void InsertPolygon(Polygon2f polygon)
        {
            if (!polygon.IsSimple)
              throw new ArgumentException("Polygon must be simple.");

            if (CGAL_InsertPoints(polygon.Positions, polygon.Positions.Length, true) != SUCCESS)
                throw new Exception("Error inserting points.");

            if (polygon.HasHoles)
            {
                int numHoles = polygon.Holes.Count;
                for (int i = 0; i < numHoles; i++)
                {
                    Polygon2f hole = polygon.Holes[i];
                    if (CGAL_InsertPoints(hole.Positions, hole.Positions.Length, true) != SUCCESS)
                        throw new Exception("Error inserting hole points.");
                }
            }

        }

        private static void CreateMesh<MESH>(IMeshConstructor<MESH> constructor, MeshDescriptor des)
        {
            constructor.PushTriangleMesh(des.Vertices, des.Faces);

            for (int i = 0; i < des.Vertices; i++)
                constructor.AddVertex(CGAL_GetPoint2f(i));

            for (int i = 0; i < des.Faces; i++)
            {
                TriangleIndex triangle = CGAL_GetTriangle(i);
                constructor.AddFace(triangle);
            }

            for (int i = 0; i < des.Faces; i++)
            {
                TriangleIndex triangle = CGAL_GetNeighbor(i);
                constructor.AddFaceConnection(i, triangle);
            }

        }

        [DllImport("CGALWrapper", EntryPoint = "Conforming2_InsertPoints2f", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CGAL_InsertPoints(Vector2f[] inPoints, int inSize, bool close);

        [DllImport("CGALWrapper", EntryPoint = "Conforming2_InsertSeed2f", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CGAL_InsertSeed(Vector2f point);

        [DllImport("CGALWrapper", EntryPoint = "Conforming2_Clear", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CGAL_Clear();

        [DllImport("CGALWrapper", EntryPoint = "Conforming2_Release", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CGAL_Release();

        [DllImport("CGALWrapper", EntryPoint = "Conforming2_RefineMesh", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CGAL_RefineMesh(int iterations, float angleBounds, float lengthBounds);

        [DllImport("CGALWrapper", EntryPoint = "Conforming2_Triangulate", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CGAL_Triangulate(out MeshDescriptor descriptor);

        [DllImport("CGALWrapper", EntryPoint = "Conforming2_GetPoint2f", CallingConvention = CallingConvention.Cdecl)]
        private static extern Vector2f CGAL_GetPoint2f(int i);

        [DllImport("CGALWrapper", EntryPoint = "Conforming2_GetTriangle", CallingConvention = CallingConvention.Cdecl)]
        private static extern TriangleIndex CGAL_GetTriangle(int i);

        [DllImport("CGALWrapper", EntryPoint = "Conforming2_GetNeighbor", CallingConvention = CallingConvention.Cdecl)]
        private static extern TriangleIndex CGAL_GetNeighbor(int i);

    }
}
