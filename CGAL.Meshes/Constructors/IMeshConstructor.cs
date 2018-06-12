using System;
using System.Collections.Generic;

using Common.Core.LinearAlgebra;
using CGAL.Meshes.Descriptors;

namespace CGAL.Meshes.Constructors
{
    public interface IMeshConstructor<MESH>
    {

        void PushTriangleMesh(int numVertices, int numFaces);

        void PushEdgeMesh(int numVertices, int numEdges);

        MESH PopMesh();

        void AddVertex(Vector2f pos);

        void AddFace(TriangleIndex triangle);

        void AddEdge(EdgeIndex edge);

        void AddFaceConnection(int faceIndex, TriangleIndex neighbors);

        void AddEdgeConnection(EdgeConnection connection);
    }
}
