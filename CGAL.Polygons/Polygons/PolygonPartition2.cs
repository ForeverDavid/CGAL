using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Common.Core.LinearAlgebra;

namespace CGAL.Polygons
{

    public enum PARTITION_METHOD { APPROX, GREENE, YMONOTONE, OPTIMAL}

    public static class PolygonPartition2
    {

        public static List<Polygon2f> Partition(Polygon2f polygon, PARTITION_METHOD method = PARTITION_METHOD.APPROX)
        {

            if (!polygon.IsSimple)
                throw new ArgumentException("Polygon must be simple.");

            if (!polygon.IsCCW)
                throw new ArgumentException("Polygon must have counter clock wise orientation. Reverse polygon.");

            if (polygon.HasHoles)
                throw new NotImplementedException("Polygon with holes not implemented.");

            CGAL_LoadPoints(polygon.Positions, polygon.Positions.Length);

            int numPolygons = PerformPartition(method);

            List<Polygon2f> partition = new List<Polygon2f>(numPolygons);

            for (int i = 0; i < numPolygons; i++)
            {
                int numPoints = CGAL_GetPolygonSize(i);
                Polygon2f poly = new Polygon2f(numPoints);

                for (int j = 0; j < numPoints; j++)
                    poly.Positions[j] = CGAL_GetPolygonVector2f(i, j);

                poly.CalculatePolygon();
                partition.Add(poly);
            }

            CGAL_Clear();

            return partition;
        }

        private static int PerformPartition(PARTITION_METHOD method)
        {

            int numPolygons = 0;

            switch (method)
            {
                case PARTITION_METHOD.APPROX:
                    numPolygons = CGAL_ApproxConvexPartition();
                    break;

                case PARTITION_METHOD.GREENE:
                    numPolygons = CGAL_GreeneApproxConvexPartition();
                    break;

                case PARTITION_METHOD.YMONOTONE:
                    numPolygons = CGAL_YMonotonePartition();
                    break;

                case PARTITION_METHOD.OPTIMAL:
                    numPolygons = CGAL_OptimalConvexPartition();
                    break;
            }

            return numPolygons;
        }

        [DllImport("CGALWrapper", EntryPoint = "Partition2_LoadPoints2f", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CGAL_LoadPoints(Vector2f[] inPoints, int inSize);

        [DllImport("CGALWrapper", EntryPoint = "Partition2_Clear", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CGAL_Clear();

        [DllImport("CGALWrapper", EntryPoint = "Partition2_Release", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CGAL_Release();

        [DllImport("CGALWrapper", EntryPoint = "Partition2_ApproxConvexPartition", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CGAL_ApproxConvexPartition();

        [DllImport("CGALWrapper", EntryPoint = "Partition2_GreeneApproxConvexPartition", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CGAL_GreeneApproxConvexPartition();

        [DllImport("CGALWrapper", EntryPoint = "Partition2_YMonotonePartition", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CGAL_YMonotonePartition();

        [DllImport("CGALWrapper", EntryPoint = "Partition2_OptimalConvexPartition", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CGAL_OptimalConvexPartition();

        [DllImport("CGALWrapper", EntryPoint = "Partition2_GetPolygonSize", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CGAL_GetPolygonSize(int i);

        [DllImport("CGALWrapper", EntryPoint = "Partition2_GetPolygonPoint2f", CallingConvention = CallingConvention.Cdecl)]
        private static extern Vector2f CGAL_GetPolygonVector2f(int i, int j);
    }
}
