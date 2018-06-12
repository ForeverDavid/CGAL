using System;
using System.Runtime.InteropServices;

using Common.Core.LinearAlgebra;

namespace CGAL.Geometry.Shapes
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Segment2d
    {

        public Vector2d A;

        public Vector2d B;

        public Segment2d(Vector2d a, Vector2d b)
        {
            A = a;
            B = b;
        }

        public Segment2d(double ax, double ay, double bx, double by)
        {
            A = new Vector2d(ax, ay);
            B = new Vector2d(bx, by);
        }

        public Vector2d Center
        {
            get { return (A + B) / 2.0; }
        }

        public double Length
        {
            get { return Vector2d.Distance(A, B); }
        }

        public double SqrLength
        {
            get { return Vector2d.SqrDistance(A, B); }
        }

        public Vector2d Normal
        {
            get
            {
                return (B - A).Normalized.PerpendicularCW;
            }
        }

        public Box2d Bounds
        {
            get
            {
                double xmin = Math.Min(A.x, B.x);
                double xmax = Math.Max(A.x, B.x);
                double ymin = Math.Min(A.y, B.y);
                double ymax = Math.Max(A.y, B.y);

                return new Box2d(xmin, xmax, ymin, ymax);
            }
        }

        public override string ToString()
        {
            return string.Format("[Segment2d: A={0}, B={1}]", A, B);
        }

    }
}

