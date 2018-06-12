using System;
using System.Runtime.InteropServices;

using Common.Core.LinearAlgebra;

namespace CGAL.Geometry.Shapes
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Ray2d
    {

        public Vector2d Position;

        public Vector2d Direction;

        public Ray2d(Vector2d position, Vector2d direction)
        {
            Position = position;
            Direction = direction;
        }

        public override string ToString()
        {
            return string.Format("[Ray2d: Position={0}, Direction={1}]", Position, Direction);
        }

    }
}

