using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Common.Core.LinearAlgebra;
using CGAL.Polygons;
using CGAL.Meshes.Constructors;
using CGAL.Meshes.IndexBased;
using CGAL.Meshes.Descriptors;

namespace CGAL.Triangulation.Constrainted
{
    public static class ConstraintedTriangulation2
    {

        private const int SUCCESS = 0;
        private const int ERROR = 1;

        public static Mesh2f Triangulate(Polygon2f polygon)
        {
            var constructor = new MeshConstructor2f();
            Triangulate(polygon, constructor);
            return constructor.PopMesh();
        }

        public static void Triangulate<MESH>(Polygon2f polygon, IMeshConstructor<MESH> constructor)
        {
            Insert(polygon);

            MeshDescriptor descriptor;
            if(CGAL_Triangulate(out descriptor) != SUCCESS)
                throw new Exception("Error triangulating points.");

            PolygonIntersection2.PushPolygon(polygon);
            CreateMesh(constructor, descriptor);
            PolygonIntersection2.PopPolygon();

            CGAL_Clear();
        }

        private static void Insert(Polygon2f polygon)
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
                    if(CGAL_InsertPoints(hole.Positions, hole.Positions.Length, true) != SUCCESS)
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
                Vector2f a = CGAL_GetPoint2f(triangle.i0);
                Vector2f b = CGAL_GetPoint2f(triangle.i1);
                Vector2f c = CGAL_GetPoint2f(triangle.i2);

                Vector2f p = (a + b + c) / 3.0f;

                if (PolygonIntersection2.ContainsPoint(p))
                    constructor.AddFace(triangle);
            }

        }

        [DllImport("CGALWrapper", EntryPoint = "Constrainted2_InsertPoints2f", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CGAL_InsertPoints(Vector2f[] inPoints, int inSize, bool close);

        [DllImport("CGALWrapper", EntryPoint = "Constrainted2_Clear", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CGAL_Clear();

        [DllImport("CGALWrapper", EntryPoint = "Constrainted2_Release", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CGAL_Release();

        [DllImport("CGALWrapper", EntryPoint = "Constrainted2_Triangulate", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CGAL_Triangulate(out MeshDescriptor descriptor);

        [DllImport("CGALWrapper", EntryPoint = "Constrainted2_GetPoint2f", CallingConvention = CallingConvention.Cdecl)]
        private static extern Vector2f CGAL_GetPoint2f(int i);

        [DllImport("CGALWrapper", EntryPoint = "Constrainted2_GetTriangle", CallingConvention = CallingConvention.Cdecl)]
        private static extern TriangleIndex CGAL_GetTriangle(int i);

    }
}
