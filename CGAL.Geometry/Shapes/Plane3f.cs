using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Common.Core.LinearAlgebra;

namespace CGAL.Geometry.Shapes
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Plane3f
    {

        public Vector3f Normal;

        public Vector3f Position;

        public Plane3f(Vector3f position, Vector3f normal)
        {
            Normal = normal;
            Position = position;
        }

        public Plane3f(Vector3f normal, float distance)
        {
            Normal = normal;
            Position = Normal * distance;
        }

        /// <summary>
        /// From three noncollinear points (ordered ccw).
        /// </summary>
        public Plane3f(Vector3f a, Vector3f b, Vector3f c)
        {
            Normal = Vector3f.Cross(b - a, c - a);
            Normal.Normalize();
            Position = Normal * Vector3f.Dot(Normal, a);
        }

        public override string ToString()
        {
            return string.Format("[Plane3f: Positions{0}, Normal={1}]", Position, Normal);
        }

    }
    
}
