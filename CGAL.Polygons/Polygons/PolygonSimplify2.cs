using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Common.Core.LinearAlgebra;

namespace CGAL.Polygons
{

    public enum SIMPLIFY_METHOD {  SQUARE_DIST, SCALED_SQUARE_DIST };

    public static class PolygonSimplify2
    {

        public static Polygon2f Simplify(Polygon2f polygon, float threshold, SIMPLIFY_METHOD method = SIMPLIFY_METHOD.SQUARE_DIST)
        {

            if (!polygon.IsSimple)
                throw new ArgumentException("Polygon must be simple.");

            if (polygon.HasHoles)
                throw new NotImplementedException("Polygon with holes not implemented.");

            if (polygon.VerticesCount < 3)
            {
                Polygon2f simplified = new Polygon2f(polygon.Positions);
                simplified.CalculatePolygon();
                return simplified;
            }
            else
            {
                CGAL_LoadPoints(polygon.Positions, polygon.Positions.Length);

                int size = PerformSimplification(threshold, method);

                Polygon2f simplified = new Polygon2f(size);

                for (int i = 0; i < size; i++)
                    simplified.Positions[i] = CGAL_GetSimplifiedVector2f(i);

                simplified.CalculatePolygon();

                CGAL_Clear();

                return simplified;
            }
        }

        private static int PerformSimplification(float threshold, SIMPLIFY_METHOD method)
        {
            int size = 0;

            if (threshold < 0.0f) threshold = 0.0f;
            if (threshold > 1.0f) threshold = 1.0f;

            switch (method)
            {
                case SIMPLIFY_METHOD.SQUARE_DIST:
                    size = CGAL_SquareDistCostSimplify(threshold);
                    break;

                case SIMPLIFY_METHOD.SCALED_SQUARE_DIST:
                    size = CGAL_ScaledSquareDistCostSimplify(threshold);
                    break;
            }

            return size;
        }

        [DllImport("CGALWrapper", EntryPoint = "Simplify2_LoadPoints2f", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CGAL_LoadPoints(Vector2f[] inPoints, int inSize);

        [DllImport("CGALWrapper", EntryPoint = "Simplify2_Clear", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CGAL_Clear();

        [DllImport("CGALWrapper", EntryPoint = "Simplify2_Release", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CGAL_Release();

        [DllImport("CGALWrapper", EntryPoint = "Simplify2_SquareDistCostSimplify", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CGAL_SquareDistCostSimplify(float threshold);

        [DllImport("CGALWrapper", EntryPoint = "Simplify2_ScaledSquareDistCostSimplify", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CGAL_ScaledSquareDistCostSimplify(float threshold);

        [DllImport("CGALWrapper", EntryPoint = "Simplify2_GetSimplifiedPoint2f", CallingConvention = CallingConvention.Cdecl)]
        private static extern Vector2f CGAL_GetSimplifiedVector2f(int i);
    }
}
