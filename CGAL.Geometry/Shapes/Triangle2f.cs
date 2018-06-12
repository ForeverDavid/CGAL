using System;
using System.Runtime.InteropServices;

using Common.Core.LinearAlgebra;

namespace CGAL.Geometry.Shapes
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Triangle2f
    {

        public Vector2f A;

        public Vector2f B;

        public Vector2f C;

        public Triangle2f(Vector2f a, Vector2f b, Vector2f c)
        {
            A = a;
            B = b;
            C = c;
        }

        public Triangle2f(float ax, float ay, float bx, float by, float cx, float cy)
        {
            A = new Vector2f(ax, ay);
            B = new Vector2f(bx, by);
            C = new Vector2f(cx, cy);
        }

        public Vector2f Center
        {
            get { return (A + B + C) / 3.0f; }
        }

        public float Area
        {
            get { return Math.Abs(SignedArea); }
        }

        public float SignedArea
        {
            get { return (A.x - C.x) * (B.y - C.y) - (A.y - C.y) * (B.x - C.x); }
        }

        public Box2f Bounds
        {
            get
            {
                float xmin = Math.Min(A.x, Math.Min(B.x, C.x));
                float xmax = Math.Max(A.x, Math.Max(B.x, C.x));
                float ymin = Math.Min(A.y, Math.Min(B.y, C.y));
                float ymax = Math.Max(A.y, Math.Max(B.y, C.y));

                return new Box2f(xmin, xmax, ymin, ymax);
            }
        }

        public override string ToString()
        {
            return string.Format("[Triangle2f: A={0}, B={1}, C={1}]", A, B, C);
        }

    }
}