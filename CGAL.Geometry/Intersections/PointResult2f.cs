using System;
using System.Collections.Generic;

using Common.Core.LinearAlgebra;

namespace CGAL.Geometry.Intersections
{
    public struct PointResult2f
    {

        public static readonly PointResult2f NoHit = new PointResult2f();

        public bool Hit;

        public int A, B;

        public float T;

        public Vector2f Point;

        public override string ToString()
        {
            return string.Format("[PointResult2f: Hit={0}, A={1}, B={2}, T={3}, Point={4}]",
                Hit, A, B, T, Point);
        }

        public Vector2f Interpolate(IList<Vector2f> arr)
        {
            return arr[A] * (1.0f - T) + arr[B] * T;
        }

        public Vector3f Interpolate(IList<Vector3f> arr)
        {
            return arr[A] * (1.0f - T) + arr[B] * T;
        }

        public float Interpolate(IList<float> arr)
        {
            return arr[A] * (1.0f - T) + arr[B] * T;
        }

        public double Interpolate(IList<double> arr)
        {
            return arr[A] * (1.0 - T) + arr[B] * T;
        }

    }
}
