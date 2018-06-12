using System;
using System.Xml;
using System.Collections.Generic;

using Common.Core.LinearAlgebra;

namespace CGAL.Meshes.HalfEdgeBased
{
    public class HBVertex2f : HBVertex
    {
        public Vector2f Position;

        public HBVertex2f()
        {

        }

        public HBVertex2f(Vector2f pos)
        {
            Position = pos;
        }

        public override void Initialize(Vector2f pos)
        {
            Position = pos;
        }

        public override void Transform(Matrix2x2f m)
        {
            Position = m * Position;
        }

        public void InsertEdgeByAngle(HBEdge edge)
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
                var p0 = Edge.Opposite.GetVertex<HBVertex2f>().Position - Position;
                var p1 = edge.Opposite.GetVertex<HBVertex2f>().Position - Position;
                float a01 = Vector2f.Angle360(p0, p1);
                var previous = Edge;

                foreach (var e in EnumerateEdges())
                {
                    var p2 = e.Opposite.GetVertex<HBVertex2f>().Position - Position;
                    float a02 = Vector2f.Angle360(p0, p2);

                    if (a01 <= a02)
                    {
                        edge.Next = e.Opposite;
                        e.Opposite.Previous = edge;

                        edge.Opposite.Previous = previous;
                        previous.Next = edge.Opposite;
                        return;
                    }

                    previous = e;
                }

                edge.Next = Edge.Opposite;
                Edge.Opposite.Previous = edge;

                edge.Opposite.Previous = previous;
                previous.Next = edge.Opposite;
            }
        }

    }
}
