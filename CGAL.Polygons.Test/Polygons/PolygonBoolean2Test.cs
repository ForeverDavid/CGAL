using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Common.Core.LinearAlgebra;
using CGAL.Polygons;

namespace CGAL.Polygons.Test
{
    [TestClass]
    public class PolygonBoolean2Test
    {
        [TestMethod]
        public void DoIntersect()
        {

            Polygon2f A = CreatePolygon2.FromBox(new Vector2f(-1), new Vector2f(1));
            Polygon2f B = CreatePolygon2.FromBox(new Vector2f(0), new Vector2f(2));

            Polygon2f C = CreatePolygon2.FromBox(new Vector2f(-1, 1), new Vector2f(1, 3));
            Polygon2f D = CreatePolygon2.FromBox(new Vector2f(-3, -1), new Vector2f(-2, 1));

            //Intersect
            Assert.IsTrue(PolygonBoolean2.DoIntersect(A, B));
            //Edge case
            Assert.IsFalse(PolygonBoolean2.DoIntersect(A, C));
            //Dont intersect
            Assert.IsFalse(PolygonBoolean2.DoIntersect(A, D));
        }

        [TestMethod]
        public void Union()
        {

            Polygon2f A = CreatePolygon2.FromBox(new Vector2f(-1), new Vector2f(1));
            Polygon2f B = CreatePolygon2.FromBox(new Vector2f(0), new Vector2f(2));

            List<Polygon2f> list;
            PolygonBoolean2.Union(A, B, out list);

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(0, list[0].HoleCount);
            Assert.AreEqual(7, list[0].Area);
        }

        [TestMethod]
        public void Intersection()
        {

            Polygon2f A = CreatePolygon2.FromBox(new Vector2f(-1), new Vector2f(1));
            Polygon2f B = CreatePolygon2.FromBox(new Vector2f(0), new Vector2f(2));

            List<Polygon2f> list;
            PolygonBoolean2.Intersection(A, B, out list);

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(0, list[0].HoleCount);
            Assert.AreEqual(1, list[0].Area);
        }

        [TestMethod]
        public void Difference()
        {

            Polygon2f A = CreatePolygon2.FromBox(new Vector2f(-1), new Vector2f(1));
            Polygon2f B = CreatePolygon2.FromBox(new Vector2f(0), new Vector2f(2));

            List<Polygon2f> list;
            PolygonBoolean2.Difference(A, B, out list);

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(0, list[0].HoleCount);
            Assert.AreEqual(3, list[0].Area);
        }

        [TestMethod]
        public void SymmetricDifference()
        {

            Polygon2f A = CreatePolygon2.FromBox(new Vector2f(-1), new Vector2f(1));
            Polygon2f B = CreatePolygon2.FromBox(new Vector2f(0), new Vector2f(2));

            List<Polygon2f> list;
            PolygonBoolean2.SymmetricDifference(A, B, out list);

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(1, list[0].HoleCount);
            Assert.AreEqual(6, list[0].Area);
        }
    }
}
