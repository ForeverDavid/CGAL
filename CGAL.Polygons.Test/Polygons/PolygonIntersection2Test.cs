using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Common.Core.LinearAlgebra;
using CGAL.Polygons;

namespace CGAL.Polygons.Test
{
    [TestClass]
    public class PolygonIntersection2Test
    {
        [TestMethod]
        public void ContainsPoint()
        {

            Polygon2f polygon = CreatePolygon2.FromBox(new Vector2f(-2), new Vector2f(2));
            Polygon2f hole = CreatePolygon2.FromBox(new Vector2f(-1), new Vector2f(1));
            hole.MakeCW();

            polygon.AddHole(hole);

            PolygonIntersection2.PushPolygon(polygon);

            //in polygon
            Assert.IsTrue(PolygonIntersection2.ContainsPoint(new Vector2f(1.5f, 1.5f)));
            Assert.IsTrue(PolygonIntersection2.ContainsPoint(new Vector2f(1.5f, -1.5f)));
            Assert.IsTrue(PolygonIntersection2.ContainsPoint(new Vector2f(-1.5f, 1.5f)));
            Assert.IsTrue(PolygonIntersection2.ContainsPoint(new Vector2f(-1.5f, -1.5f)));

            //in hole
            Assert.IsFalse(PolygonIntersection2.ContainsPoint(new Vector2f(0,0)));

            //on polygon boundary
            Assert.IsFalse(PolygonIntersection2.ContainsPoint(new Vector2f(2, 2)));
            Assert.IsFalse(PolygonIntersection2.ContainsPoint(new Vector2f(2, -2)));
            Assert.IsFalse(PolygonIntersection2.ContainsPoint(new Vector2f(-2, 2)));
            Assert.IsFalse(PolygonIntersection2.ContainsPoint(new Vector2f(-2, -2)));

            //on hole boundary
            Assert.IsFalse(PolygonIntersection2.ContainsPoint(new Vector2f(1, 1)));
            Assert.IsFalse(PolygonIntersection2.ContainsPoint(new Vector2f(1, -1)));
            Assert.IsFalse(PolygonIntersection2.ContainsPoint(new Vector2f(-1, 1)));
            Assert.IsFalse(PolygonIntersection2.ContainsPoint(new Vector2f(-1, -1)));

            PolygonIntersection2.PopPolygon();
        }
    }
}
