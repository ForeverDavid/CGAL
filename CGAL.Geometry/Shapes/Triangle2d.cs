using System;
using System.Runtime.InteropServices;

using Common.Core.LinearAlgebra;

namespace CGAL.Geometry.Shapes
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Triangle2d
    {

        public Vector2d A;

        public Vector2d B;

        public Vector2d C;

        public Triangle2d(Vector2d a, Vector2d b, Vector2d c)
        {
            A = a;
            B = b;
            C = c;
        }

        public Triangle2d(double ax, double ay, double bx, double by, double cx, double cy)
        {
            A = new Vector2d(ax, ay);
            B = new Vector2d(bx, by);
            C = new Vector2d(cx, cy);
        }

        public Vector2d Center
        {
            get { return (A + B + C) / 3.0; }
        }

        /// <summary>
        /// Calculate the bounding box.
        /// </summary>
        public Box2d Bounds
        {
            get
            {
                double xmin = Math.Min(A.x, Math.Min(B.x, C.x));
                double xmax = Math.Max(A.x, Math.Max(B.x, C.x));
                double ymin = Math.Min(A.y, Math.Min(B.y, C.y));
                double ymax = Math.Max(A.y, Math.Max(B.y, C.y));

                return new Box2d(xmin, xmax, ymin, ymax);
            }
        }

        public override string ToString()
        {
            return string.Format("[Triangle2d: A={0}, B={1}, C={1}]", A, B, C);
        }

    }
}