using System;
using System.Xml;
using System.Collections.Generic;

using Common.Core.LinearAlgebra;

namespace CGAL.Meshes.HalfEdgeBased
{
    public class HBVertex3f : HBVertex
    {
        public Vector3f Position;

        public HBVertex3f()
        {

        }

        public HBVertex3f(Vector3f pos)
        {
            Position = pos;
        }

        public override void Initialize(Vector3f pos)
        {
            Position = pos;
        }

        public override void Transform(Matrix3x3f m)
        {
            Position = m * Position;
        }

    }
}
