using System;
using System.Collections.Generic;

using Common.Core.LinearAlgebra;
using Common.Core.Mathematics;
using CGAL.Geometry.Shapes;

namespace CGAL.Geometry.Intersections
{
    public static class CircleIntersections2f
    {
        public static Vector2f ClosestPointOnCircle(Circle2f circle, Vector2f p)
        {
            float cpx = p.x - circle.Center.x;
            float cpy = p.y - circle.Center.y;
            float dist = (float)Math.Sqrt(cpx * cpx + cpy * cpy);

            return new Vector2f(circle.Center.x + circle.Radius * dist, circle.Center.y + circle.Radius * dist);
        }

        public static bool CirlceContainsPoint(Circle2f circle, Vector2f p)
        {
            float cpx = p.x - circle.Center.x;
            float cpy = p.y - circle.Center.y;
            float r2 = circle.Radius * circle.Radius;
            return (cpx * cpx + cpy * cpy) <= r2;
        }

        public static bool CircleIntersectsCircle(Circle2f circle0, Circle2f circle1)
        {
            float r = circle0.Radius + circle1.Radius;
            return (circle0.Center - circle1.Center).SqrMagnitude <= r * r;
        }

    }
}
