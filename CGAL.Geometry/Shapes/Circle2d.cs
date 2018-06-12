
using System;
using System.Runtime.InteropServices;

using Common.Core.LinearAlgebra;

namespace CGAL.Geometry.Shapes
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Circle2d
    {
        public Vector2d Center;

        public double Radius;

        public Circle2d(Vector2d centre, double radius)
        {
            Center = centre;
            Radius = radius;
        }

		public Circle2d(double x, double y, double radius)
		{
            Center = new Vector2d(x, y);
            Radius = radius;
		}

        public double Radius2
        {
            get { return Radius * Radius; }
        }

        public double Diameter
        {
            get { return Radius * 2.0; }
        }

        public double Area
        {
            get { return Math.PI * Radius * Radius; }
        }

        public double Circumference
        {
            get { return Math.PI * Radius * 2.0; }
        }

        /// <summary>
        /// Calculate the bounding box.
        /// </summary>
        public Box2d Bounds
        {
            get
            {
                double xmin = Center.x - Radius;
                double xmax = Center.x + Radius;
                double ymin = Center.y - Radius;
                double ymax = Center.y + Radius;

                return new Box2d(xmin, xmax, ymin, ymax);
            }
        }

        public override string ToString()
        {
            return string.Format("[Circle2d: Center={0}, Radius={1}]", Center, Radius);
        }

        /// <summary>
        /// Enlarge the circle so it contains the point p.
        /// </summary>
        public Circle2d Enlarge(Vector2d p)
        {
            Vector2d d = p - Center;
            double dist2 = d.SqrMagnitude;

            if (dist2 > Radius2)
            {
                double dist = Math.Sqrt(dist2);
                double radius = (Radius + dist) * 0.5f;
                double k = (radius - Radius) / dist;
                return new Circle2d(Center + d * k, radius);
            }
            else
            {
                return new Circle2d(Center, Radius);
            }
        }

    }
}