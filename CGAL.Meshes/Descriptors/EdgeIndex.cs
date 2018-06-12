using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CGAL.Meshes.Descriptors
{
    [StructLayout(LayoutKind.Sequential)]
    public struct EdgeIndex
    {
        public int i0;
        public int i1;

        public override string ToString()
        {
            return string.Format("[EdgeIndex: i0={0}, i1={1}]", i0, i1);
        }
    }
}
