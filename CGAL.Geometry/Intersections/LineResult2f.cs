using System;
using System.Collections.Generic;

using Common.Core.LinearAlgebra;

namespace CGAL.Geometry.Intersections
{
    public struct LineResult2f
    {

        public static readonly LineResult2f NoHit = new LineResult2f();

        public bool Hit;

        public int A0, B0;

        public int A1, B1;

        public float T0, T1;

        public Vector2f Point0, Point1;

        public override string ToString()
        {
            return string.Format("[LineResult2f: Hit={0}, A0={1}, B0={2}, T0={3}, A1={4}, B1={5}, T1={6}]",
                Hit, A0, B0, T0, A1, B1, T1);
        }

        public Vector2f Interpolate0(IList<Vector2f> arr)
        {
            return arr[A0] * (1.0f - T0) + arr[B0] * T0;
        }

        public float Interpolate0(IList<float> arr)
        {
            return arr[A0] * (1.0f - T0) + arr[B0] * T0;
        }

        public Vector2f Interpolate1(IList<Vector2f> arr)
        {
            return arr[A1] * (1.0f - T1) + arr[B1] * T1;
        }

        public float Interpolate1(IList<float> arr)
        {
            return arr[A1] * (1.0f - T1) + arr[B1] * T1;
        }
    }
}
