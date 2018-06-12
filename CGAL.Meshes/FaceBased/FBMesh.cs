using System;
using System.Collections.Generic;

namespace CGAL.Meshes.FaceBased
{
    public class FBMesh<VERTEX, FACE>
           where VERTEX : FBVertex, new()
           where FACE : FBFace, new()
    {

        public List<VERTEX> Vertices { get; private set; }

        public List<FACE> Faces { get; private set; }

        public FBMesh()
        {
            Vertices = new List<VERTEX>();
            Faces = new List<FACE>();
        }

        public FBMesh(int numVertices, int numFaces)
        {
            Vertices = new List<VERTEX>(numVertices);
            Faces = new List<FACE>(numFaces);
        }

        public override string ToString()
        {
            return string.Format("[FBMesh: Vertices={0}, Faces={1}]",
                Vertices.Count, Faces.Count);
        }

        public void Clear()
        {
            Vertices.Clear();
            Faces.Clear();
        }

        public void Fill(int numVertices, int numFaces)
        {
            Clear();

            Vertices.Capacity = numVertices;
            Faces.Capacity = numFaces;

            for (int i = 0; i < numVertices; i++)
                Vertices.Add(new VERTEX());

            for (int i = 0; i < numFaces; i++)
                Faces.Add(new FACE());
        }

        public List<int> CreateFaceIndices(int faceVertices)
        {
            int count = Faces.Count;
            int size = Faces.Count * faceVertices;

            List<int> indices = new List<int>(size);

            for(int i = 0; i < count; i++)
            {
                for(int j = 0; j < faceVertices; j++)
                {
                    VERTEX v = Faces[i].Vertices[j] as VERTEX;
                    if (v == null) continue;

                    int index = Vertices.IndexOf(v);

                    indices.Add(index);
                }
            }

            return indices;
        }

    }
}
