using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Common.Core.LinearAlgebra;

namespace CGAL.Geometry.Shapes
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Plane3d
    {

        public Vector3d Normal;

        public Vector3d Position;

        public Plane3d(Vector3d position, Vector3d normal)
        {
            Normal = normal;
            Position = position;
        }

        public Plane3d(Vector3d normal, double distance)
        {
            Normal = normal;
            Position = Normal * distance;
        }

        /// <summary>
        /// From three noncollinear points (ordered ccw).
        /// </summary>
        public Plane3d(Vector3d a, Vector3d b, Vector3d c)
        {
            Normal = Vector3d.Cross(b - a, c - a);
            Normal.Normalize();
            Position = Normal * Vector3d.Dot(Normal, a);
        }

        public override string ToString()
        {
            return string.Format("[Plane3d: Positions{0}, Normal={1}]", Position, Normal);
        }

    }
    
}
