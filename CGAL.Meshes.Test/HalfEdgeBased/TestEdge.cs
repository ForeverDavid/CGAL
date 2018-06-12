using System;
using System.Collections.Generic;
using Common.Core.LinearAlgebra;
using CGAL.Meshes.HalfEdgeBased;

namespace CGAL.Meshes.Test.HalfEdgeBased
{
    public class TestEdge : HBEdge
    {
        public string Name;

        public TestEdge(string name)
        {
            Name = name;
        }
    }
}
