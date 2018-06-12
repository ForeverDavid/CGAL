using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Common.Core.LinearAlgebra;
using CGAL.Meshes.HalfEdgeBased;

namespace CGAL.Meshes.Test.HalfEdgeBased
{
    [TestClass]
    public class HBVertex2fTest
    {

        /// <summary>
        /// See CGALCSharp.Test/Meshes/HalfEdgeBased/InsertEdge.png
        /// </summary>
        [TestMethod]
        public void InsertEdgeByAngle()
        {
            Vector2f offset = new Vector2f(-4, 6);

            HBVertex2f v0 = new HBVertex2f(offset + new Vector2f(0, 0));
            HBVertex2f v1 = new HBVertex2f(offset + new Vector2f(0, -1));
            HBVertex2f v2 = new HBVertex2f(offset + new Vector2f(0, 1));
            HBVertex2f v3 = new HBVertex2f(offset + new Vector2f(1, 0));
            HBVertex2f v4 = new HBVertex2f(offset + new Vector2f(-1, 0));

            HBEdge e0 = new HBEdge();
            HBEdge e1 = new HBEdge();
            HBEdge e2 = new HBEdge();
            HBEdge e3 = new HBEdge();
            HBEdge e4 = new HBEdge();
            HBEdge e5 = new HBEdge();
            HBEdge e6 = new HBEdge();
            HBEdge e7 = new HBEdge();

            e0.Opposite = e1;
            e1.Opposite = e0;
            e2.Opposite = e3;
            e3.Opposite = e2;
            e4.Opposite = e5;
            e5.Opposite = e4;
            e6.Opposite = e7;
            e7.Opposite = e6;

            v0.InsertEdgeByAngle(e0);
            v1.InsertEdgeByAngle(e1);
            v2.InsertEdgeByAngle(e3);
            v3.InsertEdgeByAngle(e5);
            v4.InsertEdgeByAngle(e7);

            Assert.AreEqual(v0.Edge, e0);
            Assert.AreEqual(e0.Vertex, v0);
            Assert.AreEqual(e0.Opposite, e1);
            Assert.AreEqual(e0.Next, null);
            Assert.AreEqual(e0.Previous, null);

            v0.InsertEdgeByAngle(e2);
            Assert.AreEqual(e0.Next, e3);
            Assert.AreEqual(e1.Previous, e2);

            Assert.AreEqual(e2.Next, e1);
            Assert.AreEqual(e3.Previous, e0);

            v0.InsertEdgeByAngle(e4);
            Assert.AreEqual(e0.Next, e5);
            Assert.AreEqual(e1.Previous, e2);

            Assert.AreEqual(e4.Next, e3);
            Assert.AreEqual(e5.Previous, e0);

            Assert.AreEqual(e2.Next, e1);
            Assert.AreEqual(e3.Previous, e4);

            v0.InsertEdgeByAngle(e6);
            Assert.AreEqual(e0.Next, e5);
            Assert.AreEqual(e1.Previous, e6);

            Assert.AreEqual(e2.Next, e7);
            Assert.AreEqual(e3.Previous, e4);

            Assert.AreEqual(e6.Next, e1);
            Assert.AreEqual(e7.Previous, e2);

        }

    }
}
