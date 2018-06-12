using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Common.Core.LinearAlgebra;
using CGAL.Polygons;

namespace CGAL.Triangulation.ConvexHull
{
    public static class ConvexHull2
    {

        public static bool IsStronglyConvex(Vector2f[] points, bool ccw)
        {
            CGAL_LoadPoints(points, points.Length);
            bool b = CGAL_IsStronglyConvex(ccw);
            CGAL_Clear();

            return b;
        }

        public static Polygon2f FindHull(Vector2f[] points)
        {
            CGAL_LoadPoints(points, points.Length);
            int size = CGAL_FindHull();

            Polygon2f hull = new Polygon2f(size);

            for (int i = 0; i < size; i++)
                hull.Positions[i] = CGAL_GetHullVector2f(i);

            CGAL_Clear();

            hull.CalculatePolygon();

            return hull;
        }

        [DllImport("CGALWrapper", EntryPoint = "ConvexHull2_LoadPoints2f", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int CGAL_LoadPoints(Vector2f[] inPoints, int inSize);

        [DllImport("CGALWrapper", EntryPoint = "ConvexHull2_Clear", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int CGAL_Clear();

        [DllImport("CGALWrapper", EntryPoint = "ConvexHull2_Release", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int CGAL_Release();

        [DllImport("CGALWrapper", EntryPoint = "ConvexHull2_FindHull", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int CGAL_FindHull();

        [DllImport("CGALWrapper", EntryPoint = "ConvexHull2_IsStronglyConvex", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool CGAL_IsStronglyConvex(bool ccw);

        [DllImport("CGALWrapper", EntryPoint = "ConvexHull2_GetHullPoint2f", CallingConvention = CallingConvention.Cdecl)]
        internal static extern Vector2f CGAL_GetHullVector2f(int i);

    }
}
