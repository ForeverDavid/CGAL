using System;
using System.Collections.Generic;

namespace CGAL.Meshes.HalfEdgeBased
{
    public class HBFace
    {
        public HBEdge Edge { get; set; }

        public HBFace()
        {

        }

        public int EdgeCount
        {
            get
            {
                if (Edge == null) return 0;
                return Edge.EdgeCount;
            }
        }

        public EDGE GetEdge<EDGE>() where EDGE : HBEdge
        {
            if (Edge == null) return null;

            EDGE edge = Edge as EDGE;
            if (edge == null)
                throw new InvalidCastException("Edge is not a " + typeof(EDGE));

            return edge;
        }

        public virtual void Clear()
        {
            Edge = null;
        }

    }
}
