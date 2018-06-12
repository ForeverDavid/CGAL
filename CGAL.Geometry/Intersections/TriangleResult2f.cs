using System;
using System.Collections.Generic;

using Common.Core.LinearAlgebra;

namespace CGAL.Geometry.Intersections
{
    public struct TriangleResult2f
    {
        public static readonly TriangleResult2f NoHit = new TriangleResult2f();

        public bool Hit;

        public int A, B, C;

        public Vector3f Barycentric;

        public override string ToString()
        {
            return string.Format("[TriangleResult2f: Hit={0}, A={1}, B={2}, C={3}, Barycentric={4}]",
                Hit, A, B, C, Barycentric);
        }

        public Vector2f Interpolate(IList<Vector2f> arr)
        {
            return arr[A] * Barycentric.x + arr[B] * Barycentric.y + arr[C] * Barycentric.z;
        }

        public Vector3f Interpolate(IList<Vector3f> arr)
        {
            return arr[A] * Barycentric.x + arr[B] * Barycentric.y + arr[C] * Barycentric.z;
        }

        public float Interpolate(IList<float> arr)
        {
            return arr[A] * Barycentric.x + arr[B] * Barycentric.y + arr[C] * Barycentric.z;
        }

        public double Interpolate(IList<double> arr)
        {
            return arr[A] * Barycentric.x + arr[B] * Barycentric.y + arr[C] * Barycentric.z;
        }

    }
}