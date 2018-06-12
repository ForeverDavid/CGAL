using System;
using System.Xml;
using System.Collections.Generic;

using Common.Core.LinearAlgebra;

namespace CGAL.Meshes.HalfEdgeBased
{
    public class HBVertex
    {

        public HBEdge Edge { get; set; }

        public HBVertex()
        {

        }

        public virtual void Initialize(Vector2f pos)
        {
    
        }

        public virtual void Initialize(Vector3f pos)
        {

        }

        public virtual void Transform(Matrix3x3f m)
        {

        }

        public virtual void Transform(Matrix2x2f m)
        {

        }

        public HBVertex Previous
        {
            get
            {
                if (Edge == null) return null;
                if (Edge.Previous == null) return null;
                return Edge.Previous.Vertex;
            }
        }

        public HBVertex Next
        {
            get
            {
                if (Edge == null) return null;
                if (Edge.Next == null) return null;
                return Edge.Next.Vertex;
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

        public int EdgeCount
        {
            get
            {
                HBEdge start = Edge;
                HBEdge e = start;
                int count = 0;

                do
                {
                    if (e == null) return count;
                    count++;
                    if (e.Next == null) return count;
                    e = e.Next.Opposite;
                }
                while (!ReferenceEquals(start, e));

                return count;
            }
        }

        public IEnumerable<HBEdge> EnumerateEdges(bool ccw = true)
        {
            HBEdge start = Edge;
            HBEdge e = start;

            do
            {
                if (e == null) yield break;
                yield return e;

                if(ccw)
                {
                    if (e.Next == null) yield break;
                    e = e.Next.Opposite;
                }
                else
                {
                    if (e.Opposite == null) yield break;
                    e = e.Opposite.Previous;
                }
            }
            while (!ReferenceEquals(start, e));
        }

        public virtual void Clear()
        {
            Edge = null;
        }

        public void InsertEdge(HBEdge edge)
        {
            edge.Vertex = this;

            if (Edge == null)
            {
                Edge = edge;
            }
            else if (EdgeCount == 1)
            {
                Edge.Next = edge.Opposite;
                edge.Opposite.Previous = Edge;
                edge.Next = Edge.Opposite;
                Edge.Opposite.Previous = edge;
            }
            else
            {
                var last = Edge;
                foreach (var e in EnumerateEdges())
                    last = e;

                edge.Next = Edge.Opposite;
                Edge.Opposite.Previous = edge;

                edge.Opposite.Previous = last;
                last.Next = edge.Opposite;
            }
        }

        public void RemoveEdge(HBEdge edge)
        {
            if (Edge == null) return;
            if (!ReferenceEquals(edge, Edge)) return;

            HBEdge tmp = null;
            foreach (var e in EnumerateEdges())
            {
                if (!ReferenceEquals(edge, e))
                {
                    tmp = e;
                    break;
                }
            }

            Edge = tmp;
        }

    }
}
