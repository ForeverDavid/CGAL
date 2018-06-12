using System;
using System.Collections.Generic;

using Common.Core.LinearAlgebra;

namespace CGAL.Meshes.HalfEdgeBased
{

    public class HBMesh<VERTEX, EDGE, FACE>
            where VERTEX : HBVertex, new()
            where EDGE : HBEdge, new()
            where FACE : HBFace, new()
    {

        public List<VERTEX> Vertices { get; private set; }

        public List<EDGE> Edges { get; private set; }

        public List<FACE> Faces { get; private set; }

        public HBMesh()
        {
            Vertices = new List<VERTEX>();
            Edges = new List<EDGE>();
            Faces = new List<FACE>();
        }

        public HBMesh(int numVertices, int numEdges, int numFaces)
        {
            Vertices = new List<VERTEX>(numVertices);
            Edges = new List<EDGE>(numEdges);
            Faces = new List<FACE>(numFaces);
        }

        public override string ToString()
        {
            return string.Format("[HBMesh: Vertices={0}, Edges={1}, Faces={2}]", 
                Vertices.Count, Edges.Count, Faces.Count);
        }

        public void Clear()
        {
            Vertices.Clear();
            Edges.Clear();
            Faces.Clear();
        }

        public int IndexOf(HBVertex vertex)
        {
            VERTEX v = vertex as VERTEX;
            if (v == null) return -1;
            return Vertices.IndexOf(v);
        }

        public int IndexOf(HBEdge edge)
        {
            EDGE e = edge as EDGE;
            if (e == null) return -1;
            return Edges.IndexOf(e);
        }

        public int IndexOf(HBFace face)
        {
            FACE f = face as FACE;
            if (f == null) return -1;
            return Faces.IndexOf(f);
        }

        public void Fill(int numVertices, int numEdges, int numFaces) 
        {
            Clear();

            Vertices.Capacity = numVertices;
            Edges.Capacity = numEdges;
            Faces.Capacity = numFaces;

            for (int i = 0; i < numVertices; i++)
                Vertices.Add(new VERTEX());

            for (int i = 0; i < numEdges; i++)
                Edges.Add(new EDGE());

            for (int i = 0; i < numFaces; i++)
                Faces.Add(new FACE());
        }

        public void Transform(Matrix3x3f m)
        {
            int numVerts = Vertices.Count;
            for (int i = 0; i < numVerts; i++)
                Vertices[i].Transform(m);
        }

        public void Transform(Matrix2x2f m)
        {
            int numVerts = Vertices.Count;
            for (int i = 0; i < numVerts; i++)
                Vertices[i].Transform(m);
        }

        public void RemoveFaces()
        {
            Faces.Clear();

            int numEdges = Edges.Count;
            for (int i = 0; i < numEdges; i++)
                Edges[i].Face = null;
        }

        public void RemoveFace(FACE face)
        {
            Faces.Remove(face);

            int numEdges = Edges.Count;
            for (int i = 0; i < numEdges; i++)
            {
                if (ReferenceEquals(face, Edges[i].Face))
                    Edges[i].Face = null;
            }
        }

    }
}
