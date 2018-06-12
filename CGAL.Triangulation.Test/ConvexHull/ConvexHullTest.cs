using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Common.Core.LinearAlgebra;
using CGAL.Polygons;
using CGAL.Triangulation.ConvexHull;

namespace CGALCSharp.ConvexHull.Test
{
    [TestClass]
    public class ConvexHullTest
    {
        [TestMethod]
        public void FindHull()
        {
            Vector2f[] points = new Vector2f[]
            {
                new Vector2f(0,0),
                new Vector2f(10, 0),
                new Vector2f(10, 10),
                new Vector2f(6, 5),
                new Vector2f(4, 1)
            };

            Polygon2f convex = ConvexHull2.FindHull(points);

            Assert.AreEqual(3, convex.VerticesCount);
            Assert.AreEqual(points[0], convex.Positions[0]);
            Assert.AreEqual(points[1], convex.Positions[1]);
            Assert.AreEqual(points[2], convex.Positions[2]);
            Assert.IsTrue(ConvexHull2.IsStronglyConvex(convex.Positions, convex.IsCCW));

        }
    }
}
