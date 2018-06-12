using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CGAL.Meshes.HalfEdgeBased;

namespace CGAL.Meshes.Test.HalfEdgeBased
{
    [TestClass]
    public class HBVertexTest
    {
        [TestMethod]
        public void EdgeCount()
        {
            var mesh = CreateTestMesh.CreateCross();
            var vertex = mesh.Vertices[4];

            Assert.AreEqual(4, vertex.EdgeCount);
        }

        [TestMethod]
        public void EmptyEnumerateEdges()
        {
            var vertex = new HBVertex();
            var edges = new List<HBEdge>();

            foreach (var edge in vertex.EnumerateEdges(true))
                edges.Add(edge);

            Assert.AreEqual(0, edges.Count);

            vertex = new HBVertex();
            edges = new List<HBEdge>();

            foreach (var edge in vertex.EnumerateEdges(false))
                edges.Add(edge);

            Assert.AreEqual(0, edges.Count);
        }

        /// <summary>
        /// See CGALCSharp.Test/Meshes/HalfEdgeBased/Cross.png
        /// See CGALCSharp.Test/Meshes/HalfEdgeBased/SquareWithCenter.png
        /// </summary>
        [TestMethod]
        public void EnumerateEdges()
        {
            var mesh = CreateTestMesh.CreateCross();
            var vertex = mesh.Vertices[4];
            var edges = new List<HBEdge>();

            foreach (var edge in vertex.EnumerateEdges(true))
                edges.Add(edge);

            Assert.AreEqual(4, edges.Count);
            Assert.AreEqual(mesh.Edges[0], edges[0]);
            Assert.AreEqual(mesh.Edges[6], edges[1]);
            Assert.AreEqual(mesh.Edges[4], edges[2]);
            Assert.AreEqual(mesh.Edges[2], edges[3]);

            mesh = CreateTestMesh.CreateCross();
            vertex = mesh.Vertices[4];
            edges = new List<HBEdge>();

            foreach (var edge in vertex.EnumerateEdges(false))
                edges.Add(edge);

            Assert.AreEqual(4, edges.Count);
            Assert.AreEqual(mesh.Edges[0], edges[0]);
            Assert.AreEqual(mesh.Edges[2], edges[1]);
            Assert.AreEqual(mesh.Edges[4], edges[2]);
            Assert.AreEqual(mesh.Edges[6], edges[3]);
        }

        [TestMethod]
        public void InsertEdge()
        {
            HBVertex v0 = new HBVertex();

            HBEdge e0 = new HBEdge();
            HBEdge e1 = new HBEdge();
            HBEdge e2 = new HBEdge();
            HBEdge e3 = new HBEdge();
            HBEdge e4 = new HBEdge();
            HBEdge e5 = new HBEdge();

            e0.Opposite = e1;
            e1.Opposite = e0;
            e2.Opposite = e3;
            e3.Opposite = e2;
            e4.Opposite = e5;
            e5.Opposite = e4;

            v0.InsertEdge(e0);
            v0.InsertEdge(e2);
            v0.InsertEdge(e4);

            List<HBEdge> edges = new List<HBEdge>();
            foreach (var e in v0.EnumerateEdges(true))
                edges.Add(e);

            Assert.AreEqual(edges[0], e0);
            Assert.AreEqual(edges[1], e2);
            Assert.AreEqual(edges[2], e4);

            edges.Clear();
            foreach (var e in v0.EnumerateEdges(false))
                edges.Add(e);

            Assert.AreEqual(edges[0], e0);
            Assert.AreEqual(edges[1], e4);
            Assert.AreEqual(edges[2], e2);
        }

    }
}
