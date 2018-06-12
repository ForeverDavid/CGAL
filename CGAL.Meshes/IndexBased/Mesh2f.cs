using System;
using System.Collections.Generic;

using Common.Core.LinearAlgebra;
using CGAL.Meshes.IndexBased;

namespace CGAL.Meshes.IndexBased
{
    public class Mesh2f : IndexableMesh
    {
        public int VerticesCount { get { return (Positions != null) ? Positions.Length : 0; } }

        public Vector2f[] Positions { get; private set; }

        public bool HasNormals { get { return Normals != null; } }

        public Vector2f[] Normals { get; private set; }

        public bool HasTexCoords0 { get { return TexCoords0 != null; } }

        public Vector2f[] TexCoords0 { get; private set; }

        public Mesh2f()
        {

        }

        public Mesh2f(int numPositions)
        {
            Positions = new Vector2f[numPositions];
        }

        public Mesh2f(IList<Vector2f> positions)
        {
            SetPositions(positions);
        }

        public Mesh2f(IList<Vector2f> positions, IList<int> indices)
        {
            SetPositions(positions);
            SetIndices(indices);
        }

        public Mesh2f(int numPositions, int numIndices)
        {
            Positions = new Vector2f[numPositions];
            Indices = new int[numIndices];
        }

        public override string ToString()
        {
            return string.Format("[Mesh2f: Vertices={0}, Indices={1}]", VerticesCount, IndicesCount);
        }

        public void SetPositions(int size)
        {
            if (Positions == null || Positions.Length != size)
                Positions = new Vector2f[size];
        }

        public void SetPositions(IList<Vector2f> positions)
        {
            SetPositions(positions.Count);
            positions.CopyTo(Positions, 0);
        }

        public void SetNormals(int size)
        {
            if (Normals == null || Normals.Length != size)
                Normals = new Vector2f[size];
        }

        public void SetNormals(IList<Vector2f> normals)
        {
            SetNormals(normals.Count);
            normals.CopyTo(Normals, 0);
        }

        public void SetTexCoords0(int size)
        {
            if (TexCoords0 == null || TexCoords0.Length != size)
                TexCoords0 = new Vector2f[size];
        }

        public void SetTexCoords0(IList<Vector2f> texCoords)
        {
            SetTexCoords0(texCoords.Count);
            texCoords.CopyTo(TexCoords0, 0);
        }

        public void FlipTriangles()
        {
            if (Indices == null) return;
            int count = IndicesCount;
            for (int i = 0; i < count / 3; i++)
            {
                int tmp = Indices[i * 3 + 0];
                Indices[i * 3 + 0] = Indices[i * 3 + 2];
                Indices[i * 3 + 2] = tmp;
            }
        }

    }
}
