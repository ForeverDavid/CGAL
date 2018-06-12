using System;
using System.Collections.Generic;

using Common.Core.LinearAlgebra;

using CGAL.Meshes.HalfEdgeBased;

namespace CGAL.Meshes.Test.HalfEdgeBased
{
    public static class CreateTestMesh
    {

        public static HBMesh<HBVertex, HBEdge, HBFace> CreateTriangle()
        {
            var mesh = new HBMesh<HBVertex, HBEdge, HBFace>();
            mesh.Fill(3, 3, 1);

            var E = mesh.Edges;
            var V = mesh.Vertices;
            var F = mesh.Faces;

            F[0].Edge = E[0];

            V[0].Edge = E[0];
            V[1].Edge = E[1];
            V[2].Edge = E[2];

            E[0].Set(V[0], F[0], E[2], E[1], null);
            E[1].Set(V[1], F[0], E[0], E[2], null);
            E[2].Set(V[2], F[0], E[1], E[0], null);

            return mesh;
        }

        public static HBMesh<HBVertex2f, HBEdge, HBFace> CreateTriangle(Vector2f A, Vector2f B, Vector2f C)
        {
            var mesh = new HBMesh<HBVertex2f, HBEdge, HBFace>();
            mesh.Fill(3, 3, 1);

            var E = mesh.Edges;
            var V = mesh.Vertices;
            var F = mesh.Faces;

            F[0].Edge = E[0];

            V[0].Edge = E[0];
            V[1].Edge = E[1];
            V[2].Edge = E[2];

            V[0].Position = A;
            V[1].Position = B;
            V[2].Position = C;

            E[0].Set(V[0], F[0], E[2], E[1], null);
            E[1].Set(V[1], F[0], E[0], E[2], null);
            E[2].Set(V[2], F[0], E[1], E[0], null);

            return mesh;
        }

        /// <summary>
        /// See CGALCSharp.Test/Meshes/HalfEdgeBased/Cross.png
        /// </summary>
        /// <returns></returns>
        public static HBMesh<HBVertex, HBEdge, HBFace> CreateCross()
        {
            var mesh = new HBMesh<HBVertex, HBEdge, HBFace>();
            mesh.Fill(5, 8, 0);

            var E = mesh.Edges;
            var V = mesh.Vertices;

            V[0].Edge = E[1];
            V[1].Edge = E[3];
            V[2].Edge = E[5];
            V[3].Edge = E[7];
            V[4].Edge = E[0];

            E[0].Set(V[4], null, null, E[7], E[1]);
            E[1].Set(V[0], null, E[2], null, E[0]);
            E[2].Set(V[4], null, null, E[1], E[3]);
            E[3].Set(V[1], null, E[4], null, E[2]);
            E[4].Set(V[4], null, null, E[3], E[5]);
            E[5].Set(V[2], null, E[6], null, E[4]);
            E[6].Set(V[4], null, null, E[5], E[7]);
            E[7].Set(V[3], null, E[0], null, E[6]);

            return mesh;
        }

        /// <summary>
        /// See CGALCSharp.Test/Meshes/HalfEdgeBased/SquareWithCenter.png
        /// </summary>
        /// <returns></returns>
        public static HBMesh<HBVertex, HBEdge, HBFace> CreateSquareWithCenter()
        {
            var mesh = new HBMesh<HBVertex, HBEdge, HBFace>();
            mesh.Fill(5, 12, 4);

            var E = mesh.Edges;
            var V = mesh.Vertices;
            var F = mesh.Faces;

            V[0].Edge = E[1];
            V[1].Edge = E[3];
            V[2].Edge = E[5];
            V[3].Edge = E[7];
            V[4].Edge = E[0];

            F[0].Edge = E[8];
            F[1].Edge = E[9];
            F[2].Edge = E[10];
            F[3].Edge = E[11];

            E[0].Set(V[4], F[3], E[11], E[7], E[1]);
            E[1].Set(V[0], F[0], E[2], E[8], E[0]);
            E[2].Set(V[4], F[0], E[8], E[1], E[3]);
            E[3].Set(V[1], F[1], E[4], E[9], E[2]);
            E[4].Set(V[4], F[1], E[9], E[3], E[5]);
            E[5].Set(V[2], F[2], E[6], E[10], E[4]);
            E[6].Set(V[4], F[2], E[10], E[5], E[7]);
            E[7].Set(V[3], F[3], E[0], E[11], E[6]);

            E[8].Set(V[1], F[0], E[1], E[2], null);
            E[9].Set(V[2], F[1], E[3], E[4], null);
            E[10].Set(V[3], F[2], E[5], E[6], null);
            E[11].Set(V[0], F[3], E[7], E[0], null);

            return mesh;
        }

    }
}
