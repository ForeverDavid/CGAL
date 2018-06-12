using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Common.Core.LinearAlgebra;

namespace CGAL.Polygons
{

    public static class PolygonBoolean2
    {

        public static bool DoIntersect(Polygon2f A, Polygon2f B)
        {
            if (!A.IsSimple || !B.IsSimple)
                throw new ArgumentException("Polygon must be simple.");

            if (!A.IsCCW || !B.IsCCW)
                throw new ArgumentException("Polygon must have counter clock wise orientation. Reverse polygon.");

            LoadPoints(A, B);
            AddHoles(A, B);

            bool intersect = CGAL_DoIntersect();

            CGAL_Clear();

            return intersect;
        }

        public static bool Union(Polygon2f A, Polygon2f B, out List<Polygon2f> polygons)
        {
            return PerformBoolean(A, B, CGAL_Union, out polygons);
        }

        public static bool Intersection(Polygon2f A, Polygon2f B, out List<Polygon2f> polygons)
        {
            return PerformBoolean(A, B, CGAL_Intersection, out polygons);
        }

        public static bool Difference(Polygon2f A, Polygon2f B, out List<Polygon2f> polygons)
        {
            return PerformBoolean(A, B, CGAL_Difference, out polygons);
        }

        public static bool SymmetricDifference(Polygon2f A, Polygon2f B, out List<Polygon2f> polygons)
        {
            return PerformBoolean(A, B, CGAL_SymmetricDifference, out polygons);
        }

        private static bool PerformBoolean(Polygon2f A, Polygon2f B, Func<int> func, out List<Polygon2f> polygons)
        {
            polygons = null;

            if (!A.IsSimple || !B.IsSimple)
                throw new ArgumentException("Polygon must be simple.");

            if (!A.IsCCW || !B.IsCCW)
                throw new ArgumentException("Polygon must have counter clock wise orientation. Reverse polygon.");

            LoadPoints(A, B);
            AddHoles(A, B);

            int numPolygons = func();
            if (numPolygons > 0)
            {
                polygons = new List<Polygon2f>(numPolygons);

                for (int i = 0; i < numPolygons; i++)
                    polygons.Add(CreatePolygon(i));
            }

            CGAL_Clear();

            return polygons != null;
        }

        private static void LoadPoints(Polygon2f A, Polygon2f B)
        {
            CGAL_A_LoadPoints(A.Positions, A.Positions.Length);
            CGAL_B_LoadPoints(B.Positions, B.Positions.Length);
        }

        private static void AddHoles(Polygon2f A, Polygon2f B)
        {
            AddHoles(A, CGAL_A_AddHole);
            AddHoles(B, CGAL_B_AddHole);
        }

        private static void AddHoles(Polygon2f polygon, Action<Vector2f[], int> action)
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

                action(hole.Positions, hole.Positions.Length);
            }
        }

        private static Polygon2f CreatePolygon(int index)
        {
            CGAL_PointToPolygon(index);

            int numPoints = CGAL_NumPolygonPoints();
            int numHoles = CGAL_NumPolygonHoles();
            Polygon2f polygon = new Polygon2f(numPoints);

            for (int i = 0; i < numPoints; i++)
                polygon.Positions[i] = CGAL_GetPolygonPoint2f(i);

            for (int i = 0; i < numHoles; i++)
            {
                int holePoints = CGAL_NumHolePoints(i);
                Polygon2f hole = new Polygon2f(holePoints);

                for (int j = 0; j < holePoints; j++)
                    hole.Positions[j] = CGAL_GetHolePoint2f(i, j);

                polygon.AddHole(hole);
            }

            polygon.CalculatePolygon();

            return polygon;
        }

        [DllImport("CGALWrapper", EntryPoint = "Boolean2_A_LoadPoints2f", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CGAL_A_LoadPoints(Vector2f[] inPoints, int inSize);

        [DllImport("CGALWrapper", EntryPoint = "Boolean2_A_AddHole2f", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CGAL_A_AddHole(Vector2f[] inPoints, int inSize);

        [DllImport("CGALWrapper", EntryPoint = "Boolean2_B_LoadPoints2f", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CGAL_B_LoadPoints(Vector2f[] inPoints, int inSize);

        [DllImport("CGALWrapper", EntryPoint = "Boolean2_B_AddHole2f", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CGAL_B_AddHole(Vector2f[] inPoints, int inSize);

        [DllImport("CGALWrapper", EntryPoint = "Boolean2_Clear", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CGAL_Clear();

        [DllImport("CGALWrapper", EntryPoint = "Boolean2_Release", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CGAL_Release();

        [DllImport("CGALWrapper", EntryPoint = "Boolean2_DoIntersect", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool CGAL_DoIntersect();

        [DllImport("CGALWrapper", EntryPoint = "Boolean2_Union", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CGAL_Union();

        [DllImport("CGALWrapper", EntryPoint = "Boolean2_Intersection", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CGAL_Intersection();

        [DllImport("CGALWrapper", EntryPoint = "Boolean2_Difference", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CGAL_Difference();

        [DllImport("CGALWrapper", EntryPoint = "Boolean2_SymmetricDifference", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CGAL_SymmetricDifference();

        [DllImport("CGALWrapper", EntryPoint = "Boolean2_PointToPolygon", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CGAL_PointToPolygon(int polyIndex);

        [DllImport("CGALWrapper", EntryPoint = "Boolean2_NumPolygonPoints", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CGAL_NumPolygonPoints();

        [DllImport("CGALWrapper", EntryPoint = "Boolean2_NumPolygonHoles", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CGAL_NumPolygonHoles();

        [DllImport("CGALWrapper", EntryPoint = "Boolean2_NumHolePoints", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CGAL_NumHolePoints(int holeIndex);

        [DllImport("CGALWrapper", EntryPoint = "Boolean2_GetPolygonPoint2f", CallingConvention = CallingConvention.Cdecl)]
        private static extern Vector2f CGAL_GetPolygonPoint2f(int pointIndex);

        [DllImport("CGALWrapper", EntryPoint = "Boolean2_GetHolePoint2f", CallingConvention = CallingConvention.Cdecl)]
        private static extern Vector2f CGAL_GetHolePoint2f(int holeIndex, int pointIndex);

    }
}
