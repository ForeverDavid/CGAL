using System;
using System.Runtime.InteropServices;

using Common.Core.LinearAlgebra;

namespace CGAL.Geometry.Shapes
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Capsule2d
    {
        public Vector2d A;

        public Vector2d B;

        public double Radius;

        public Capsule2d(Vector2d a, Vector2d b, double radius)
        {
            A = a;
            B = b;
            Radius = radius;
        }

        public Capsule2d(double ax, double ay, double bx, double by, double radius)
		{
            A = new Vector2d(ax, ay);
            B = new Vector2d(bx, by);
            Radius = radius;
		}

        public Vector2d Center
        {
            get { return (A + B) / 2.0; }
        }

        /// <summary>
        /// Calculate the bounding box.
        /// </summary>
        public Box2d Bounds
        {
            get
            {
                double xmin = Math.Min(A.x, B.x) - Radius;
                double xmax = Math.Max(A.x, B.x) + Radius;
                double ymin = Math.Min(A.y, B.y) - Radius;
                double ymax = Math.Max(A.y, B.y) + Radius;

                return new Box2d(xmin, xmax, ymin, ymax);
            }
        }

        public override string ToString()
        {
            return string.Format("[Capsule2d: A={0}, B={1}, Radius={2}]", A, B, Radius);
        }

    }
}