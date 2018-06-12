using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CGAL.Meshes.Descriptors
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MeshDescriptor
    {
        public int Vertices, Edges, Faces;

        public override string ToString()
        {
            return string.Format("[EdgeDescriptor: vertices={0}, edges={1}, faces={2}]", Vertices, Edges, Faces);
        }
    }
}
