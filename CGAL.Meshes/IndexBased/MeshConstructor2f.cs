using System;
using System.Collections.Generic;

using Common.Core.LinearAlgebra;
using CGAL.Meshes.Constructors;
using CGAL.Meshes.Descriptors;

namespace CGAL.Meshes.IndexBased
{
    public class MeshConstructor2f : MeshConstructor<Mesh2f>
    {

        private Mesh2f m_mesh;

        private int m_vertexIndex;

        private int m_faceIndex;

        private int m_edgeIndex;

        public override void PushTriangleMesh(int numVertices, int numFaces)
        {
            m_mesh = new Mesh2f(numVertices, numFaces * 3);
        }

        public override void PushEdgeMesh(int numVertices, int numEdges)
        {
            m_mesh = new Mesh2f(numVertices, numEdges * 2);
        }

        public override Mesh2f PopMesh()
        {
            Mesh2f tmp = m_mesh;
            m_mesh = null;

            ResetIndex();

            return tmp;
        }

        private void ResetIndex()
        {
            m_vertexIndex = 0;
            m_faceIndex = 0;
            m_edgeIndex = 0;
        }

        public override void AddVertex(Vector2f pos)
        {
            m_mesh.Positions[m_vertexIndex] = pos;
            m_vertexIndex++;
        }

        public override void AddFace(TriangleIndex triangle)
        {
            m_mesh.Indices[m_faceIndex * 3 + 0] = triangle.i0;
            m_mesh.Indices[m_faceIndex * 3 + 1] = triangle.i1;
            m_mesh.Indices[m_faceIndex * 3 + 2] = triangle.i2;
            m_faceIndex++;
        }

        public override void AddEdge(EdgeIndex edge)
        {
            m_mesh.Indices[m_edgeIndex * 2 + 0] = edge.i0;
            m_mesh.Indices[m_edgeIndex * 2 + 1] = edge.i1;
            m_edgeIndex++;
        }
    }
}
