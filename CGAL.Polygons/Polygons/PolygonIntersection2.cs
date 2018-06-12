using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Common.Core.LinearAlgebra;

namespace CGAL.Polygons
{
    public static class PolygonIntersection2
    {

        public static void PushPolygon(Polygon2f polygon)
        {
            CGAL_PushPolygon(polygon.Positions, polygon.Positions.Length);

            if (polygon.HasHoles)
            {
                int numHoles = polygon.Holes.Count;
                for (int i = 0; i < numHoles; i++)
                {
                    Polygon2f hole = polygon.Holes[i];
                    CGAL_AddHole(hole.Positions, hole.Positions.Length);
                }
            }
        }

        public static void PopPolygon()
        {
            CGAL_PopPolygon();
        }

        public static void PopAll()
        {
            CGAL_PopAll();
        }

        public static bool ContainsPoint(Vector2f point)
        {
            return CGAL_ContainsPoint(point);
        }

        [DllImport("CGALWrapper", EntryPoint = "Intersection2_PushPolygon2f", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CGAL_PushPolygon(Vector2f[] inPoints, int inSize);

        [DllImport("CGALWrapper", EntryPoint = "Intersection2_AddHole2f", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CGAL_AddHole(Vector2f[] inPoints, int inSize);

        [DllImport("CGALWrapper", EntryPoint = "Intersection2_PopPolygon", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CGAL_PopPolygon();

        [DllImport("CGALWrapper", EntryPoint = "Intersection2_PopAll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CGAL_PopAll();

        [DllImport("CGALWrapper", EntryPoint = "Intersection2_ContainsPoint2f", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool CGAL_ContainsPoint(Vector2f point);
    }
}
