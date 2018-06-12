using System;
using System.Collections.Generic;

namespace CGAL.Meshes.FaceBased
{
    public class FBFace
    {

        public FBVertex[] Vertices { get; set; }

        public FBFace[] Neighbors { get; set; }

        public FBFace()
        {

        }

        public FBFace(int size)
        {
            Vertices = new FBVertex[size];
            Neighbors = new FBFace[size];
        }

        public void SetSize(int size)
        {
            if(Vertices == null || Vertices.Length != size)
                Vertices = new FBVertex[size];

            if (Neighbors == null || Neighbors.Length != size)
                Neighbors = new FBFace[size];
        }

        public VERTEX GetVertex<VERTEX>(int i) where VERTEX : FBVertex
        {
            if (Vertices == null) return null;
            if (Vertices[i] == null) return null;

            VERTEX vert = Vertices[i] as VERTEX;
            if (vert == null)
                throw new InvalidCastException("Vertex is not a " + typeof(VERTEX));

            return vert;
        }

        public FACE GetNeighbor<FACE>(int i) where FACE : FBFace
        {
            if (Neighbors == null) return null;
            if (Neighbors[i] == null) return null;

            FACE face = Neighbors[i] as FACE;
            if (face == null)
                throw new InvalidCastException("Neighbor is not a " + typeof(FACE));

            return face;
        }

        public virtual void Clear()
        {
            if (Vertices != null)
                Vertices = null;

            if (Neighbors != null)
                Neighbors = null;
        }

    }
}
