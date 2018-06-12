using System;
using System.Collections.Generic;

using Common.Core.LinearAlgebra;
using Common.Core.Mathematics;
using CGAL.Geometry.Shapes;

namespace CGAL.Geometry.Intersections
{

    public static class RayIntersections3f
    {

        public static bool RayIntersectsSphere(Ray3f ray, Sphere3f sphere, out float t)
        {
            t = 0;
            Vector3f m = ray.Position - sphere.Center;

            float b = Vector3f.Dot(m, ray.Direction);
            float c = Vector3f.Dot(m, m) - sphere.Radius2;

            if (c > 0.0f && b > 0.0f) return false;

            float discr = b * b - c;
            if (discr < 0.0f) return false;

            t = -b - (float)Math.Sqrt(discr);

            if (t < 0) t = 0;
            return true;
        }

    }

}
