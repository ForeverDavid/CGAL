using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Common.Core.LinearAlgebra;
using CGAL.Polygons;

namespace CGAL.Polygons.Test
{
    [TestClass]
    public class PolygonSimplify2Test
    {
        [TestMethod]
        public void Simplify()
        {
            Vector2f[] points = new Vector2f[]
            {
                new Vector2f(391, 374),
                new Vector2f(240, 431),
                new Vector2f(252, 340),
                new Vector2f(374, 320),
                new Vector2f(289, 214),
                new Vector2f(134, 390),
                new Vector2f(68, 186),
                new Vector2f(154, 259),
                new Vector2f(161, 107),
                new Vector2f(435, 108),
                new Vector2f(208, 148),
                new Vector2f(295, 160),
                new Vector2f(421, 212),
                new Vector2f(441, 303)
            };

            Polygon2f polygon = new Polygon2f(points);

            Polygon2f simpified = PolygonSimplify2.Simplify(polygon, 0.5f);

            Assert.IsTrue(simpified.IsSimple);
            Assert.IsFalse(simpified.IsConvex);
            Assert.AreEqual(ORIENTATION.COUNTERCLOCKWISE, simpified.Orientation);
            Assert.AreEqual(7, simpified.Positions.Length);
        }
    }
}
