using System;
using System.Runtime.InteropServices;

using Common.Core.LinearAlgebra;

namespace CGAL.Geometry.Shapes
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Circle2f
    {
        public Vector2f Center;

        public float Radius;

        public Circle2f(Vector2f centre, float radius)
        {
            Center = centre;
            Radius = radius;
        }

        public Circle2f(float x, float y, float radius)
        {
            Center = new Vector2f(x, y);
            Radius = radius;
        }

        public float Radius2
        {
            get { return Radius * Radius; }
        }

        public float Diameter
        {
            get { return Radius * 2.0f; }
        }

        public float Area
        {
            get { return (float)Math.PI * Radius * Radius; }
        }

        public float Circumference
        {
            get { return (float)Math.PI * Radius * 2.0f; }
        }

        /// <summary>
        /// Calculate the bounding box.
        /// </summary>
        public Box2f Bounds
        {
            get
            {
                float xmin = Center.x - Radius;
                float xmax = Center.x + Radius;
                float ymin = Center.y - Radius;
                float ymax = Center.y + Radius;

                return new Box2f(xmin, xmax, ymin, ymax);
            }
        }

        public override string ToString()
        {
            return string.Format("[Circle2f: Center={0}, Radius={1}]", Center, Radius);
        }

        /// <summary>
        /// Enlarge the circle so it contains the point p.
        /// </summary>
        public Circle2f Enlarge(Vector2f p)
        {
            Vector2f d = p - Center;
            float dist2 = d.SqrMagnitude;

            if (dist2 > Radius2)
            {
                float dist = (float)Math.Sqrt(dist2);
                float radius = (Radius + dist) * 0.5f;
                float k = (radius - Radius) / dist;
                return new Circle2f(Center + d * k, radius);
            }
            else
            {
                return new Circle2f(Center, Radius);
            }
        }

    }
}