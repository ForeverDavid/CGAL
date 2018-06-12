using System;
using System.Collections.Generic;

namespace CGAL.Meshes.HalfEdgeBased
{
    public class HBEdge
    {
        public HBVertex Vertex { get; set; }

        public HBFace Face { get; set; }

        public HBEdge Next { get; set; }

        public HBEdge Previous { get; set; }

        public HBEdge Opposite { get; set; }

        public HBEdge()
        {

        }

        public HBEdge(HBVertex vertex, HBFace face, HBEdge previous, HBEdge next, HBEdge opposite)
        {
            Set(vertex, face, previous, next, opposite);
        }

        public void Set(HBVertex vertex, HBFace face, HBEdge previous, HBEdge next, HBEdge opposite)
        {
            Vertex = vertex;
            Face = face;
            Previous = previous;
            Next = next;
            Opposite = opposite;
        }

        public VERTEX GetVertex<VERTEX>() where VERTEX : HBVertex
        {
            if (Vertex == null) return null;

            VERTEX vert = Vertex as VERTEX;
            if (vert == null)
                throw new InvalidCastException("Vertex is not a " + typeof(VERTEX));

            return vert;
        }

        public FACE GetFace<FACE>() where FACE : HBFace
        {
            if (Face == null) return null;

            FACE face = Face as FACE;
            if (face == null)
                throw new InvalidCastException("Face is not a " + typeof(FACE));

            return face;
        }

        public EDGE GetNext<EDGE>() where EDGE : HBEdge
        {
            if (Next == null) return null;

            EDGE edge = Next as EDGE;
            if (edge == null)
                throw new InvalidCastException("Edge is not a " + typeof(EDGE));

            return edge;
        }

        public EDGE GetPrevious<EDGE>() where EDGE : HBEdge
        {
            if (Previous == null) return null;

            EDGE edge = Previous as EDGE;
            if (edge == null)
                throw new InvalidCastException("Edge is not a " + typeof(EDGE));

            return edge;
        }

        public EDGE GetOpposite<EDGE>() where EDGE : HBEdge
        {
            if (Opposite == null) return null;

            EDGE edge = Opposite as EDGE;
            if (edge == null)
                throw new InvalidCastException("Edge is not a " + typeof(EDGE));

            return edge;
        }

        public int EdgeCount
        {
            get
            {
                var start = this;
                var e = start;
                int count = 0;

                do
                {
                    count++;
                    if (e.Next == null) return count;
                    e = e.Next;
                }
                while (!ReferenceEquals(start, e));

                return count;
            }
        }

        public bool IsClosed
        {
            get
            {
                HBEdge current = this;
            
                while (current.Next != null && !ReferenceEquals(this, current.Next))
                    current = current.Next;

                return ReferenceEquals(this, current.Next);
            }
        }

        public IEnumerable<HBEdge> EnumerateEdges(bool ccw = true)
        {
            var start = this;
            var e = start;

            do
            {
                if (e == null) yield break;
                yield return e;
                e = (ccw) ? e.Next : e.Previous;
            }
            while (!ReferenceEquals(start, e));
        }

        public IEnumerable<HBVertex> EnumerateVertices(bool ccw = true)
        {
            var start = this;
            var e = start;

            do
            {
                if (e == null) yield break;
                if (e.Vertex == null) yield break;
                yield return e.Vertex;
                e = (ccw) ? e.Next : e.Previous;
            }
            while (!ReferenceEquals(start, e));
        }

        public virtual void Clear()
        {
            Vertex = null;
            Face = null;
            Next = null;
            Previous = null;
            Opposite = null;
        }

    }
}
