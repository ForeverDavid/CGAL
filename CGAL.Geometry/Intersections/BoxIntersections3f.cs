using System;
using System.Collections.Generic;

using Common.Core.LinearAlgebra;
using Common.Core.Mathematics;
using CGAL.Geometry.Shapes;

namespace CGAL.Geometry.Intersections
{
    public static class BoxIntersections3f
    {

        public static float SqrDistanceFromBox(Box3f box, Vector3f p)
        {
            float sqDist = 0.0f;

            if (p.x < box.Min.x) sqDist += (box.Min.x - p.x) * (box.Min.x - p.x);
            if (p.x > box.Max.x) sqDist += (p.x - box.Max.x) * (p.x - box.Max.x);

            if (p.y < box.Min.y) sqDist += (box.Min.y - p.y) * (box.Min.y - p.y);
            if (p.y > box.Max.y) sqDist += (p.y - box.Max.y) * (p.y - box.Max.y);

            if (p.z < box.Min.z) sqDist += (box.Min.z - p.z) * (box.Min.z - p.z);
            if (p.z > box.Max.z) sqDist += (p.z - box.Max.z) * (p.z - box.Max.z);

            return sqDist;
        }

        public static Vector3f ClosestPointOnBox(Box3f box, Vector3f p)
        {
            Vector3f c;

            if (p.x < box.Min.x)
                c.x = box.Min.x;
            else if (p.x > box.Max.x)
                c.x = box.Max.x;
            else
                c.x = p.x;

            if (p.y < box.Min.y)
                c.y = box.Min.y;
            else if (p.y > box.Max.y)
                c.y = box.Max.y;
            else
                c.y = p.y;

            if (p.z < box.Min.z)
                c.z = box.Min.z;
            else if (p.z > box.Max.z)
                c.z = box.Max.z;
            else
                c.z = p.z;

            return c;
        }

        public static bool BoxContainsPoint(Box3f box, Vector3f p)
        {
            if (p.x > box.Max.x || p.x < box.Min.x) return false;
            if (p.y > box.Max.y || p.y < box.Min.y) return false;
            if (p.z > box.Max.z || p.z < box.Min.z) return false;
            return true;
        }

        public static bool BoxIntersectsCircle(Box3f box, Sphere3f sphere)
        {
            if (sphere.Center.x - sphere.Radius > box.Max.x ||
                sphere.Center.x + sphere.Radius < box.Min.x) return false;

            if (sphere.Center.y - sphere.Radius > box.Max.y ||
                sphere.Center.y + sphere.Radius < box.Min.y) return false;

            if (sphere.Center.z - sphere.Radius > box.Max.z ||
                sphere.Center.z + sphere.Radius < box.Min.z) return false;

            return true;
        }

        public static bool BoxIntersectsBox(Box3f box0, Box3f box1)
        {
            if (box0.Max.x < box1.Min.x || box0.Min.x > box1.Max.x) return false;
            if (box0.Max.y < box1.Min.y || box0.Min.y > box1.Max.y) return false;
            if (box0.Max.z < box1.Min.z || box0.Min.z > box1.Max.z) return false;
            return true;
        }

    }
}
