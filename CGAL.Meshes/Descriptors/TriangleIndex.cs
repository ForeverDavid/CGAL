using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Common.Core.LinearAlgebra;

namespace CGAL.Meshes.Descriptors
{
    [StructLayout(LayoutKind.Sequential)]
    public struct TriangleIndex
    {
        public int i0, i1, i2;

        public override string ToString()
        {
            return string.Format("[TriangleIndex: i0={0}, i1={1}, i2={2}]", i0, i1, i2);
        }

        public Vector2f Center(IList<Vector2f> positions)
        {
            return (positions[i0] + positions[i1] + positions[i2]) / 3.0f;
        }

    }
}
