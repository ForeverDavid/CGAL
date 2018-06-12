using System;
using System.Collections.Generic;

using Common.Core.LinearAlgebra;
using CGAL.Meshes.Constructors;
using CGAL.Meshes.Descriptors;

namespace CGAL.Meshes.HalfEdgeBased
{

    public class HBMeshConstructor<VERTEX, EDGE, FACE> : MeshConstructor<HBMesh<VERTEX, EDGE, FACE>>
            where VERTEX : HBVertex, new()
            where EDGE : HBEdge, new()
            where FACE : HBFace, new()
    {

        private HBMesh<VERTEX, EDGE, FACE> Mesh { get; set; }

        public override void PushTriangleMesh(int numVertices, int numFaces)
        {
            Mesh = new HBMesh<VERTEX, EDGE, FACE>(numVertices, numFaces * 3 * 2, numFaces);
        }

        public override void PushEdgeMesh(int numVertices, int numEdges)
        {
            Mesh = new HBMesh<VERTEX, EDGE, FACE>(numVertices, numEdges, 0);
        }

        public override HBMesh<VERTEX, EDGE, FACE> PopMesh()
        {
            var tmp = Mesh;
            Mesh = null;
            return tmp;
        }

        public override void AddVertex(Vector2f pos)
        {
            VERTEX v = new VERTEX();
            v.Initialize(pos);
            Mesh.Vertices.Add(v);
        }

        public override void AddFace(TriangleIndex triangle)
        {
            var v0 = Mesh.Vertices[triangle.i0];
            var v1 = Mesh.Vertices[triangle.i1];
            var v2 = Mesh.Vertices[triangle.i2];

            var e0 = new EDGE();
            var e1 = new EDGE();
            var e2 = new EDGE();

            v0.Edge = e0;
            v1.Edge = e1;
            v2.Edge = e2;

            var face = new FACE();
            face.Edge = e0;

            e0.Set(v0, face, e2, e1, null);
            e1.Set(v1, face, e0, e2, null);
            e2.Set(v2, face, e1, e0, null);

            Mesh.Faces.Add(face);
            Mesh.Edges.Add(e0);
            Mesh.Edges.Add(e1);
            Mesh.Edges.Add(e2);
        }

        public override void AddEdge(EdgeIndex edge)
        {
            var v0 = Mesh.Vertices[edge.i0];
            var v1 = Mesh.Vertices[edge.i1];

            var e0 = new EDGE();
            var e1 = new EDGE();

            e0.Opposite = e1;
            e1.Opposite = e0;

            v0.Edge = e0;
            e0.Vertex = v0;

            v1.Edge = e1;
            e1.Vertex = v1;

            Mesh.Edges.Add(e0);
            Mesh.Edges.Add(e1);
        }

        public override void AddFaceConnection(int faceIndex, TriangleIndex neighbors)
        {
            var face = Mesh.Faces[faceIndex];
            var f0 = (neighbors.i0 != -1) ? Mesh.Faces[neighbors.i0] : null;
            var f1 = (neighbors.i1 != -1) ? Mesh.Faces[neighbors.i1] : null;
            var f2 = (neighbors.i2 != -1) ? Mesh.Faces[neighbors.i2] : null;

            foreach (var edge in face.Edge.EnumerateEdges())
                if (SetOppositeEdge(edge, f0)) break;

            foreach (var edge in face.Edge.EnumerateEdges())
                if (SetOppositeEdge(edge, f1)) break;

            foreach (var edge in face.Edge.EnumerateEdges())
                if (SetOppositeEdge(edge, f2)) break;

        }

        private bool SetOppositeEdge(HBEdge edge, HBFace neighbor)
        {

            if (neighbor == null) return false;

            if (edge == null)
                throw new NullReferenceException("Edge is null.");

            if (edge.Vertex == null)
                throw new NullReferenceException("Edge has null vertex.");

            if (neighbor.Edge == null)
                throw new NullReferenceException("Neighbor has null edge.");

            var v0 = edge.Vertex;
            var v1 = edge.Previous.Vertex;

            foreach (var nedge in neighbor.Edge.EnumerateEdges())
            {
                if (nedge.Vertex == null)
                    throw new NullReferenceException("Neighbor edge has null vertex.");

                if(ReferenceEquals(v0, nedge.Previous.Vertex) &&
                   ReferenceEquals(v1, nedge.Vertex))
                {
                    edge.Opposite = nedge;
                    nedge.Opposite = edge;
                    return true;
                }
            }

            return false;
        }

        public override void AddEdgeConnection(EdgeConnection con)
        {
            var edge = Mesh.Edges[con.Edge];
            var previous = (con.Previous != -1) ? Mesh.Edges[con.Previous] : null;
            var next = (con.Next != -1) ? Mesh.Edges[con.Next] : null;

            edge.Previous = previous;
            edge.Next = next;
        }

    }
}
