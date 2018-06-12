using System;
using System.Collections.Generic;

using Common.Core.LinearAlgebra;
using Common.Core.Mathematics;
using CGAL.Geometry.Shapes;

namespace CGAL.Geometry.Intersections
{
    public static class BoxIntersections2f
    {

        public static float SqrDistanceFromBox(Box2f box, Vector2f p)
        {
            float sqDist = 0.0f;

            if (p.x < box.Min.x) sqDist += (box.Min.x - p.x) * (box.Min.x - p.x);
            if (p.x > box.Max.x) sqDist += (p.x - box.Max.x) * (p.x - box.Max.x);

            if (p.y < box.Min.y) sqDist += (box.Min.y - p.y) * (box.Min.y - p.y);
            if (p.y > box.Max.y) sqDist += (p.y - box.Max.y) * (p.y - box.Max.y);

            return sqDist;
        }

        public static Vector2f ClosestPointOnBox(Box2f box, Vector2f p)
        {
            Vector2f c;

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

            return c;
        }

        public static bool BoxContainsPoint(Box2f box, Vector2f p)
        {
            if (p.x > box.Max.x || p.x < box.Min.x) return false;
            if (p.y > box.Max.y || p.y < box.Min.y) return false;
            return true;
        }

        public static bool BoxIntersectsCircle(Box2f box, Circle2f circle)
        {
            if (circle.Center.x - circle.Radius > box.Max.x || 
                circle.Center.x + circle.Radius < box.Min.x) return false;

            if (circle.Center.y - circle.Radius > box.Max.y || 
                circle.Center.y + circle.Radius < box.Min.y) return false;

            return true;
        }

        public static bool BoxIntersectsBox(Box2f box0, Box2f box1)
        {
            if (box0.Max.x < box1.Min.x || box0.Min.x > box1.Max.x) return false;
            if (box0.Max.y < box1.Min.y || box0.Min.y > box1.Max.y) return false;
            return true;
        }

    }
}
