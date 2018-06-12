using System;
using System.Runtime.InteropServices;

using Common.Core.LinearAlgebra;

namespace CGAL.Geometry.Shapes
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Ray3f
    {

        public Vector3f Position;

        public Vector3f Direction;

        public Ray3f(Vector3f position, Vector3f direction)
        {
            Position = position;
            Direction = direction;
        }

        public override string ToString()
        {
            return string.Format("[Ray3f: Position={0}, Direction={1}]", Position, Direction);
        }

    }
}

