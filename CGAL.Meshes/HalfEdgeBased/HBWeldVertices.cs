using System;
using System.Collections.Generic;

using Common.Core.LinearAlgebra;
using CGAL.Meshes.IndexBased;

namespace CGAL.Meshes.HalfEdgeBased
{
    public static class HBWeldVertices
    {

        public static void WeldVertices<VERTEX, EDGE, FACE>(HBMesh<VERTEX, EDGE, FACE> mesh, float minDistance)
            where VERTEX : HBVertex2f, new()
            where EDGE : HBEdge, new()
            where FACE : HBFace, new()
        {

            int vertCount = mesh.Vertices.Count;
            if (vertCount < 2) return;

            List<VERTEX> vertices = new List<VERTEX>(vertCount);
            List<VERTEX> removed = new List<VERTEX>();

            float d2 = minDistance * minDistance;
            for (int i = 0; i < vertCount; i++)
            {
                var v = mesh.Vertices[i];
                bool wasWelded = FindClosest(v, vertices, d2) != null;

                if (!wasWelded)
                    vertices.Add(v);
                else
                    removed.Add(v);
            }

            mesh.Vertices.Clear();
            mesh.Vertices.AddRange(vertices);

            int edgeCount = mesh.Edges.Count;
            if (edgeCount > 0)
            {
                List<EDGE> edges = new List<EDGE>(edgeCount);

                for (int i = 0; i < edgeCount; i++)
                {
                    var edge = mesh.Edges[i];
                    VERTEX v0 = edge.GetVertex<VERTEX>();

                    if (!removed.Contains(v0))
                    {
                        edges.Add(edge);
                    }
                    else
                    {
                        VERTEX v1 = null;

                        if (edge.Opposite != null)
                            v1 = edge.Opposite.GetVertex<VERTEX>();
                        else if (edge.Previous != null)
                            v1 = edge.Previous.GetVertex<VERTEX>();

                        if (!removed.Contains(v1))
                        {
                            edge.Vertex = FindClosest(v0, vertices, d2, false);
                            edges.Add(edge);
                        }
                        else
                        {
                            if (edge.Previous != null)
                               edge.Previous.Next = edge.Next;

                            if (edge.Next != null)
                               edge.Next.Previous = edge.Previous;
                        }
                        
                    }
    
                }

                mesh.Edges.Clear();
                mesh.Edges.AddRange(edges);
            }

        }

        private static VERTEX FindClosest<VERTEX>(VERTEX vertex, IList<VERTEX> vertices, float d2, bool canBeNull = true)
            where VERTEX : HBVertex2f
        {

            for (int i = 0; i < vertices.Count; i++)
            {
                var v = vertices[i];
                if (Vector2f.SqrDistance(vertex.Position, v.Position) < d2)
                {
                    return v;
                }
            }

            if (canBeNull)
                return null;
            else
                throw new InvalidOperationException("Failed to find closest vertex.");
        }

    }
}
