using System;
using System.Collections.Generic;

using Common.Core.LinearAlgebra;
using Common.Core.Mathematics;
using CGAL.Geometry.Shapes;

namespace CGAL.Geometry.Intersections
{
    public static class CapsuleIntersections2f
    {
        public static bool CapsuleContainsPoint(Capsule2f cap, Vector2f p)
        {
            float r2 = cap.Radius * cap.Radius;

            Vector2f ap = p - cap.A;

            if (ap.x * ap.x + ap.y * ap.y <= r2) return true;

            Vector2f bp = p - cap.B.x;

            if (bp.x * bp.x + bp.y * bp.y <= r2) return true;

            Vector2f ab = cap.B - cap.A;

            float t = (ab.x * cap.A.x + ab.y * cap.A.y) / (ab.x * ab.x + ab.y * ab.y);

            if (t < 0.0) t = 0.0f;
            if (t > 1.0) t = 1.0f;

            p = p - (cap.A + t * ab);

            if (p.x * p.x + p.y * p.y <= r2) return true;

            return false;
        }

    }
}
