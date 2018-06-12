using System;
using System.Collections.Generic;
using Common.Core.LinearAlgebra;

using CGAL.Meshes.Descriptors;

namespace CGAL.Meshes.Constructors
{
    public abstract class MeshConstructor<MESH> : IMeshConstructor<MESH>
    {

        public abstract void PushTriangleMesh(int numVertices, int numFaces);

        public abstract void PushEdgeMesh(int numVertices, int numEdges);

        public abstract MESH PopMesh();

        public virtual void AddVertex(Vector2f pos)
        {

        }

        public virtual void AddFace(TriangleIndex triangle)
        {

        }

        public virtual void AddEdge(EdgeIndex edge)
        {

        }

        public virtual void AddFaceConnection(int faceIndex, TriangleIndex neighbors)
        {

        }

        public virtual void AddEdgeConnection(EdgeConnection connection)
        {

        }

    }
}
