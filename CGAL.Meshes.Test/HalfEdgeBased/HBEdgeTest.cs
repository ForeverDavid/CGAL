using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CGAL.Meshes.HalfEdgeBased;

namespace CGAL.Meshes.Test.HalfEdgeBased
{
    [TestClass]
    public class HBEdgeTest
    {

        [TestMethod]
        public void IsClosed()
        {
            var f0 = new HBFace();

            var v0 = new HBVertex();
            var v1 = new HBVertex();
            var v2 = new HBVertex();

            var e0 = new HBEdge();
            var e1 = new HBEdge();
            var e2 = new HBEdge();

            Assert.AreEqual(0, f0.EdgeCount);

            f0.Edge = e0;
            v0.Edge = e0;
            v1.Edge = e1;
            v2.Edge = e2;

            e0.Set(v0, f0, null, null, null);
            Assert.IsFalse(e0.IsClosed);

            e0.Next = e1;
            e1.Set(v0, f0, e0, null, null);
            Assert.IsFalse(e0.IsClosed);

            e1.Next = e2;
            e2.Set(v0, f0, e1, e0, null);
            Assert.IsTrue(e0.IsClosed);
        }

        [TestMethod]
        public void EdgeCount()
        {
            var f0 = new HBFace();

            var v0 = new HBVertex();
            var v1 = new HBVertex();
            var v2 = new HBVertex();

            var e0 = new HBEdge();
            var e1 = new HBEdge();
            var e2 = new HBEdge();

            Assert.AreEqual(0, f0.EdgeCount);

            f0.Edge = e0;
            v0.Edge = e0;
            v1.Edge = e1;
            v2.Edge = e2;

            e0.Set(v0, f0, null, null, null);
            Assert.AreEqual(1, e0.EdgeCount);

            e0.Next = e1;
            e1.Set(v0, f0, e0, null, null);
            Assert.AreEqual(2, e0.EdgeCount);

            e1.Next = e2;
            e2.Set(v0, f0, e1, e0, null);
            Assert.AreEqual(3, e0.EdgeCount);
        }

        [TestMethod]
        public void EmptyEnumerateEdges()
        {
            var edge = new HBEdge();
            var edges = new List<HBEdge>();

            foreach (var e in edge.EnumerateEdges(true))
                edges.Add(e);

            Assert.AreEqual(1, edges.Count);

            edge = new HBEdge();
            edges = new List<HBEdge>();

            foreach (var e in edge.EnumerateEdges(false))
                edges.Add(e);

            Assert.AreEqual(1, edges.Count);
        }

        [TestMethod]
        public void EnumerateEdges()
        {
            var mesh = CreateTestMesh.CreateTriangle();
            var edge = mesh.Edges[0];

            var edges = new List<HBEdge>();

            foreach (var e in edge.EnumerateEdges(true))
                edges.Add(e);

            Assert.AreEqual(3, edges.Count);
            Assert.AreEqual(mesh.Edges[0], edges[0]);
            Assert.AreEqual(mesh.Edges[1], edges[1]);
            Assert.AreEqual(mesh.Edges[2], edges[2]);

            mesh = CreateTestMesh.CreateTriangle();
            edge = mesh.Edges[0];
            edges = new List<HBEdge>();

            foreach (var e in edge.EnumerateEdges(false))
                edges.Add(e);

            Assert.AreEqual(3, edges.Count);
            Assert.AreEqual(mesh.Edges[0], edges[0]);
            Assert.AreEqual(mesh.Edges[2], edges[1]);
            Assert.AreEqual(mesh.Edges[1], edges[2]);

        }

        [TestMethod]
        public void EnumerateVertices()
        {
            var mesh = CreateTestMesh.CreateTriangle();
            var edge = mesh.Edges[0];
            var vertices = new List<HBVertex>();

            foreach (var vertex in edge.EnumerateVertices(true))
                vertices.Add(vertex);

            Assert.AreEqual(3, vertices.Count);
            Assert.AreEqual(mesh.Vertices[0], vertices[0]);
            Assert.AreEqual(mesh.Vertices[1], vertices[1]);
            Assert.AreEqual(mesh.Vertices[2], vertices[2]);

            mesh = CreateTestMesh.CreateTriangle();
            edge = mesh.Edges[0];
            vertices = new List<HBVertex>();

            foreach (var vertex in edge.EnumerateVertices(false))
                vertices.Add(vertex);

            Assert.AreEqual(3, vertices.Count);
            Assert.AreEqual(mesh.Vertices[0], vertices[0]);
            Assert.AreEqual(mesh.Vertices[2], vertices[1]);
            Assert.AreEqual(mesh.Vertices[1], vertices[2]);
        }
    }
}
