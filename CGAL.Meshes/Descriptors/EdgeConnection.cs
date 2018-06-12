using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CGAL.Meshes.Descriptors
{
    [StructLayout(LayoutKind.Sequential)]
    public struct EdgeConnection
    {
        public int Edge, Previous, Next, Opposite;

        public override string ToString()
        {
            return string.Format("[EdgeConnection: edge={0}, previous={1}, next={2}, opposite={3}]", 
                Edge, Previous, Next, Opposite);
        }
    }
}
