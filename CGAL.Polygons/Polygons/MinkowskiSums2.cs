using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Common.Core.LinearAlgebra;

namespace CGAL.Polygons
{
    public static class MinkowskiSums2
    {

        private const int SUCCESS = 0;
        private const int ERROR = 1;

        public static Polygon2f ComputeSum(Polygon2f A, Polygon2f B)
        {
            if (!A.IsSimple || !B.IsSimple)
                throw new ArgumentException("Polygon must be simple.");

            if (!A.IsCCW || !B.IsCCW)
                throw new ArgumentException("Polygon must have counter clock wise orientation. Reverse polygon.");

            if (A.HasHoles || B.HasHoles)
                throw new NotImplementedException("Polygon with holes not implemented.");

            LoadPoints(A, B);

            if (CGAL_ComputeSum() != SUCCESS)
                throw new Exception("Error computing sum.");

            Polygon2f sum = CreatePolygon();

            CGAL_Clear();

            return sum;
        }

        private static void LoadPoints(Polygon2f A, Polygon2f B)
        {
            CGAL_A_LoadPoints(A.Positions, A.Positions.Length);
            CGAL_B_LoadPoints(B.Positions, B.Positions.Length);
        }

        private static Polygon2f CreatePolygon()
        {
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

        [DllImport("CGALWrapper", EntryPoint = "MinkowskiSums2_A_LoadPoints2f", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CGAL_A_LoadPoints(Vector2f[] inPoints, int inSize);

        [DllImport("CGALWrapper", EntryPoint = "MinkowskiSums2_B_LoadPoints2f", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CGAL_B_LoadPoints(Vector2f[] inPoints, int inSize);

        [DllImport("CGALWrapper", EntryPoint = "MinkowskiSums2_Clear", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CGAL_Clear();

        [DllImport("CGALWrapper", EntryPoint = "MinkowskiSums2_Release", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CGAL_Release();

        [DllImport("CGALWrapper", EntryPoint = "MinkowskiSums2_ComputeSum", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CGAL_ComputeSum();

        [DllImport("CGALWrapper", EntryPoint = "MinkowskiSums2_NumPolygonPoints", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CGAL_NumPolygonPoints();

        [DllImport("CGALWrapper", EntryPoint = "MinkowskiSums2_NumPolygonHoles", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CGAL_NumPolygonHoles();

        [DllImport("CGALWrapper", EntryPoint = "MinkowskiSums2_NumHolePoints", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CGAL_NumHolePoints(int holeIndex);

        [DllImport("CGALWrapper", EntryPoint = "MinkowskiSums2_GetPolygonPoint2f", CallingConvention = CallingConvention.Cdecl)]
        private static extern Vector2f CGAL_GetPolygonPoint2f(int pointIndex);

        [DllImport("CGALWrapper", EntryPoint = "MinkowskiSums2_GetHolePoint2f", CallingConvention = CallingConvention.Cdecl)]
        private static extern Vector2f CGAL_GetHolePoint2f(int holeIndex, int pointIndex);

    }
}
