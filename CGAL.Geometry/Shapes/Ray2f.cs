using System;
using System.Runtime.InteropServices;

using Common.Core.LinearAlgebra;

namespace CGAL.Geometry.Shapes
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Ray2f
    {

        public Vector2f Position;

        public Vector2f Direction;

        public Ray2f(Vector2f position, Vector2f direction)
        {
            Position = position;
            Direction = direction;
        }

        public override string ToString()
        {
            return string.Format("[Ray2f: Position={0}, Direction={1}]", Position, Direction);
        }

    }
}

