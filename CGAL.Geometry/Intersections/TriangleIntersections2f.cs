using System;
using System.Collections.Generic;

using Common.Core.LinearAlgebra;
using Common.Core.Mathematics;
using CGAL.Geometry.Shapes;

namespace CGAL.Geometry.Intersections
{
    public static class TriangleIntersections2f
    {

        public static Vector2f ClosestPointOnTriangle(Triangle2f triangle, Vector2f p)
        {
            Vector2f ab = triangle.B - triangle.A;
            Vector2f ac = triangle.C - triangle.A;
            Vector2f ap = p - triangle.A;

            // Check if P in vertex region outside A
            float d1 = Vector2f.Dot(ab, ap);
            float d2 = Vector2f.Dot(ac, ap);
            if (d1 <= 0.0 && d2 <= 0.0)
            {
                // barycentric coordinates (1,0,0)
                return triangle.A;
            }

            float v, w;

            // Check if P in vertex region outside B
            Vector2f bp = p - triangle.B;
            float d3 = Vector2f.Dot(ab, bp);
            float d4 = Vector2f.Dot(ac, bp);
            if (d3 >= 0.0 && d4 <= d3)
            {
                // barycentric coordinates (0,1,0)
                return triangle.B;
            }

            // Check if P in edge region of AB, if so return projection of P onto AB
            float vc = d1 * d4 - d3 * d2;
            if (vc <= 0.0 && d1 >= 0.0f && d3 <= 0.0)
            {
                v = d1 / (d1 - d3);
                // barycentric coordinates (1-v,v,0)
                return triangle.A + v * ab;
            }

            // Check if P in vertex region outside C
            Vector2f cp = p - triangle.C;
            float d5 = Vector2f.Dot(ab, cp);
            float d6 = Vector2f.Dot(ac, cp);
            if (d6 >= 0.0 && d5 <= d6)
            {
                // barycentric coordinates (0,0,1)
                return triangle.C;
            }

            // Check if P in edge region of AC, if so return projection of P onto AC
            float vb = d5 * d2 - d1 * d6;
            if (vb <= 0.0 && d2 >= 0.0 && d6 <= 0.0)
            {
                w = d2 / (d2 - d6);
                // barycentric coordinates (1-w,0,w)
                return triangle.A + w * ac;
            }

            // Check if P in edge region of BC, if so return projection of P onto BC
            float va = d3 * d6 - d5 * d4;
            if (va <= 0.0 && (d4 - d3) >= 0.0 && (d5 - d6) >= 0.0)
            {
                w = (d4 - d3) / ((d4 - d3) + (d5 - d6));
                // barycentric coordinates (0,1-w,w)
                return triangle.B + w * (triangle.C - triangle.B);
            }

            // P inside face region. Compute Q through its barycentric coordinates (u,v,w)
            float denom = 1.0f / (va + vb + vc);
            v = vb * denom;
            w = vc * denom;

            // = u*a + v*b + w*c, u = va * denom = 1.0f - v - w
            return triangle.A + ab * v + ac * w;
        }

        public static bool TriangleContainsPoint(Triangle2f triangle, Vector2f p)
        {
            float pab = Vector2f.Cross(p - triangle.A, triangle.B - triangle.A);
            float pbc = Vector2f.Cross(p - triangle.B, triangle.C - triangle.B);

            if (Math.Sign(pab) != Math.Sign(pbc)) return false;

            float pca = Vector2f.Cross(p - triangle.C, triangle.A - triangle.C);

            if (Math.Sign(pab) != Math.Sign(pca)) return false;

            return true;
        }

        public static bool TriangleContainsPointCCW(Triangle2f triangle, Vector2f p)
        {
            if (Vector2f.Cross(p - triangle.A, triangle.B - triangle.A) > 0.0) return false;
            if (Vector2f.Cross(p - triangle.B, triangle.C - triangle.B) > 0.0) return false;
            if (Vector2f.Cross(p - triangle.C, triangle.A - triangle.C) > 0.0) return false;

            return true;
        }
    }
}
