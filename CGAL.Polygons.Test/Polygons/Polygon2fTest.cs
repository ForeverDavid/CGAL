using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Common.Core.LinearAlgebra;
using CGAL.Polygons;

namespace CGAL.Polygons.Test
{
    [TestClass]
    public class Polygon2fTest
    {
        [TestMethod]
        public void CreatePolygon()
        {
            Vector2f[] points = new Vector2f[]
            {
                new Vector2f(0,0),
                new Vector2f(1,0),
                new Vector2f(1, 1),
                new Vector2f(0,1)
            };

            Polygon2f polygon = new Polygon2f(points);

            CollectionAssert.AreEqual(points, polygon.Positions);

            Assert.IsTrue(polygon.IsSimple);
            Assert.IsTrue(polygon.IsConvex);
            Assert.AreEqual(1.0f, polygon.SignedArea);
            Assert.IsTrue(polygon.IsCCW);
            Assert.IsTrue(polygon.Orientation == ORIENTATION.COUNTERCLOCKWISE);

            Array.Reverse(points);
            polygon = new Polygon2f(points);

            CollectionAssert.AreEqual(points, polygon.Positions);

            Assert.IsTrue(polygon.IsSimple);
            Assert.IsTrue(polygon.IsConvex);
            Assert.AreEqual(-1.0f, polygon.SignedArea);
            Assert.IsTrue(polygon.IsCW);
            Assert.IsTrue(polygon.Orientation == ORIENTATION.CLOCKWISE);
        }

    }
}
