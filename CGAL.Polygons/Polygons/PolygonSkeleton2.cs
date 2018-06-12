using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Common.Core.LinearAlgebra;
using CGAL.Meshes.Constructors;
using CGAL.Meshes.Descriptors;

namespace CGAL.Polygons
{

    public static class PolygonSkeleton2
    {

        private const int SUCCESS = 0;
        private const int ERROR = 1;

        public static void CreateInteriorSkeleton<MESH>(Polygon2f polygon, IMeshConstructor<MESH> constructor, bool includeBorder = false)
        {

            if (!polygon.IsSimple)
                throw new ArgumentException("Polygon must be simple.");

            if (!polygon.IsCCW)
                throw new ArgumentException("Polygon must have counter clock wise orientation.");

            CGAL_LoadPoints(polygon.Positions, polygon.Positions.Length);
            AddHoles(polygon);

            MeshDescriptor descriptor;
            if (CGAL_CreateInteriorSkeleton(includeBorder, out descriptor) != SUCCESS)
                throw new Exception("Error creating interior skeleton.");

            CreateLine(constructor, descriptor);

            CGAL_Clear();
        }

        public static void CreateExteriorSkeleton<MESH>(Polygon2f polygon, IMeshConstructor<MESH> constructor, float maxOffset, bool includeBorder = true)
        {

            if (!polygon.IsSimple)
                throw new ArgumentException("Polygon must be simple.");

            if (!polygon.IsCCW)
                throw new ArgumentException("Polygon must have counter clock wise orientation.");

            CGAL_LoadPoints(polygon.Positions, polygon.Positions.Length);
            AddHoles(polygon);

            MeshDescriptor descriptor;
            if (CGAL_CreateExteriorSkeleton(maxOffset, includeBorder, out descriptor) != SUCCESS)
                throw new Exception("Error creating interior skeleton.");

            CreateLine(constructor, descriptor);

            CGAL_Clear();
        }

        public static List<Polygon2f> CreateInteriorOffset(Polygon2f polygon, float offset)
        {

            if (!polygon.IsSimple)
                throw new ArgumentException("Polygon must be simple.");

            if (!polygon.IsCCW)
                throw new ArgumentException("Polygon must have counter clock wise orientation.");

            CGAL_LoadPoints(polygon.Positions, polygon.Positions.Length);
            AddHoles(polygon);

            List<Polygon2f> polygons = new List<Polygon2f>();

            int numPolygons = CGAL_CreateInteriorOffset(offset);

            for (int i = 0; i < numPolygons; i++)
                polygons.Add(CreatePolygon(i));

            CGAL_Clear();

            return polygons;
        }

        private static void AddHoles(Polygon2f polygon)
        {
            if (!polygon.HasHoles) return;

            int holes = polygon.Holes.Count;
            for (int i = 0; i < holes; i++)
            {
                Polygon2f hole = polygon.Holes[i];

                if (!hole.IsSimple)
                    throw new ArgumentException("Hole must be simple.");

                if (!hole.IsCW)
                    throw new ArgumentException("Hole must have clock wise orientation.");

                CGAL_AddHole(hole.Positions, hole.Positions.Length);
            }
        }

        private static void CreateLine<MESH>(IMeshConstructor<MESH> constructor, MeshDescriptor des)
        {

            constructor.PushEdgeMesh(des.Vertices, des.Edges);

            for (int i = 0; i < des.Vertices; i++)
            {
                Vector2f v = CGAL_GetSkeletonPoint(i);
                constructor.AddVertex(v);
            }

            for (int i = 0; i < des.Edges; i++)
            {
                EdgeIndex edge = CGAL_GetSkeletonEdge(i);
                constructor.AddEdge(edge);
            }

            int numConnections = CGAL_NumEdgeConnection();
            for (int i = 0; i < numConnections; i++)
            {
                EdgeConnection con = CGAL_GetEdgeConnection(i);
                constructor.AddEdgeConnection(con);
            }

        }

        private static Polygon2f CreatePolygon(int index)
        {
            int numPoints = CGAL_NumPolygonPoints(index);

            Polygon2f polygon = new Polygon2f(numPoints);

            for (int i = 0; i < numPoints; i++)
                polygon.Positions[i] = CGAL_GetPolygonPoint2f(index, i);

            polygon.CalculatePolygon();

            return polygon;
        }

        [DllImport("CGALWrapper", EntryPoint = "Skeleton2_LoadPoints2f", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CGAL_LoadPoints(Vector2f[] inPoints, int inSize);

        [DllImport("CGALWrapper", EntryPoint = "Skeleton2_AddHole2f", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CGAL_AddHole(Vector2f[] inPoints, int inSize);

        [DllImport("CGALWrapper", EntryPoint = "Skeleton2_Clear", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CGAL_Clear();

        [DllImport("CGALWrapper", EntryPoint = "Skeleton2_Release", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CGAL_Release();

        [DllImport("CGALWrapper", EntryPoint = "Skeleton2_CreateInteriorSkeleton", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CGAL_CreateInteriorSkeleton(bool includeBorder, out MeshDescriptor descriptor);

        [DllImport("CGALWrapper", EntryPoint = "Skeleton2_CreateExteriorSkeleton", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CGAL_CreateExteriorSkeleton(double maxOffset, bool includeBorder, out MeshDescriptor descriptor);

        [DllImport("CGALWrapper", EntryPoint = "Skeleton2_CreateInteriorOffset", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CGAL_CreateInteriorOffset(double offset);

        [DllImport("CGALWrapper", EntryPoint = "Skeleton2_GetSkeletonPoint2f", CallingConvention = CallingConvention.Cdecl)]
        private static extern Vector2f CGAL_GetSkeletonPoint(int i);

        [DllImport("CGALWrapper", EntryPoint = "Skeleton2_GetSkeletonEdge", CallingConvention = CallingConvention.Cdecl)]
        private static extern EdgeIndex CGAL_GetSkeletonEdge(int i);

        [DllImport("CGALWrapper", EntryPoint = "Skeleton2_NumEdgeConnection", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CGAL_NumEdgeConnection();

        [DllImport("CGALWrapper", EntryPoint = "Skeleton2_GetEdgeConnection", CallingConvention = CallingConvention.Cdecl)]
        private static extern EdgeConnection CGAL_GetEdgeConnection(int i);

        [DllImport("CGALWrapper", EntryPoint = "Skeleton2_NumPolygonPoints", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CGAL_NumPolygonPoints(int polygonIndex);

        [DllImport("CGALWrapper", EntryPoint = "Skeleton2_GetPolygonPoint2f", CallingConvention = CallingConvention.Cdecl)]
        private static extern Vector2f CGAL_GetPolygonPoint2f(int polygonIndex, int pointIndex);

    }
}
