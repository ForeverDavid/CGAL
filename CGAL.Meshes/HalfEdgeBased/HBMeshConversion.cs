using System;
using System.Collections.Generic;

using Common.Core.LinearAlgebra;
using CGAL.Meshes.IndexBased;

namespace CGAL.Meshes.HalfEdgeBased
{
    public static class HBMeshConversion
    {
        public static Mesh3f ToIndexableMesh3f<VERTEX, EDGE, FACE>(HBMesh<VERTEX, EDGE, FACE> mesh, int faceVertices = 0)
            where VERTEX : HBVertex3f, new()
            where EDGE : HBEdge, new()
            where FACE : HBFace, new()
        {
            int numPositions = mesh.Vertices.Count;
            Mesh3f indexed = new Mesh3f(numPositions);

            for (int i = 0; i < numPositions; i++)
                indexed.Positions[i] = mesh.Vertices[i].Position;

            if (faceVertices == 0)
            {
                //no faces. Mesh represents edge lines.
                indexed.SetIndices(CreateEdgeIndices(mesh));
            }
            else
            {
                indexed.SetIndices(CreateFaceIndices(mesh, faceVertices));
            }

            return indexed;
        }

        public static Mesh2f ToIndexableMesh2f<VERTEX, EDGE, FACE>(HBMesh<VERTEX, EDGE, FACE> mesh, int faceVertices = 0)
            where VERTEX : HBVertex2f, new()
            where EDGE : HBEdge, new()
            where FACE : HBFace, new()
        {
            int numPositions = mesh.Vertices.Count;
            Mesh2f indexed = new Mesh2f(numPositions);

            for (int i = 0; i < numPositions; i++)
                indexed.Positions[i] = mesh.Vertices[i].Position;

            if (faceVertices == 0)
            {
                //no faces. Mesh represents edge lines.
                indexed.SetIndices(CreateEdgeIndices(mesh));
            }
            else
            {
                indexed.SetIndices(CreateFaceIndices(mesh, faceVertices));
            }

            return indexed;
        }

        public static List<int> CreateFaceIndices<VERTEX, EDGE, FACE>(HBMesh<VERTEX, EDGE, FACE> mesh, int faceVertices)
            where VERTEX : HBVertex, new()
            where EDGE : HBEdge, new()
            where FACE : HBFace, new()
        {
            int count = mesh.Faces.Count;
            int size = mesh.Faces.Count * faceVertices;

            List<int> indices = new List<int>(size);

            for (int i = 0; i < count; i++)
            {
                int num = 0;
                foreach (var v in mesh.Faces[i].Edge.EnumerateVertices())
                {
                    int index = mesh.IndexOf(v);
                    if (index == -1) continue;
                    indices.Add(index);
                    num++;
                }

                if (num != faceVertices)
                    throw new InvalidOperationException("Face did not have the expected number of vertices.");
            }

            return indices;
        }

        public static List<int> CreateEdgeIndices<VERTEX, EDGE, FACE>(HBMesh<VERTEX, EDGE, FACE> mesh)
            where VERTEX : HBVertex, new()
            where EDGE : HBEdge, new()
            where FACE : HBFace, new()
        {
            int count = mesh.Edges.Count;

            List<int> indices = new List<int>(count / 2);
            HashSet<HBEdge> set = new HashSet<HBEdge>();

            for (int i = 0; i < count; i++)
            {
                var edge = mesh.Edges[i];
                if (edge.Opposite == null) continue;
                if (set.Contains(edge)) continue;

                var v0 = edge.Vertex;
                var v1 = edge.Opposite.Vertex;

                int i0 = mesh.IndexOf(v0);
                int i1 = mesh.IndexOf(v1);
                if (i0 == -1 || i1 == -1) continue;
                indices.Add(i0);
                indices.Add(i1);

                set.Add(edge.Opposite);
            }

            return indices;
        }

    }
}
