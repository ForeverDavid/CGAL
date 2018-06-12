using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Common.Core.LinearAlgebra;
using CGAL.Polygons;

namespace CGAL.Polygons.Test
{
    [TestClass]
    public class MinkowskiSums2Test
    {
        [TestMethod]
        public void ComputeSum()
        {
            Vector2f[] pointsA = new Vector2f[]
            {
                new Vector2f(-1, -1),
                new Vector2f(1, -1),
                new Vector2f(0, 1)
            };

            Vector2f[] pointsB = new Vector2f[]
            {
                new Vector2f(3, -1),
                new Vector2f(5, -1),
                new Vector2f(5, 1),
                new Vector2f(3, 1)
            };

            Polygon2f A = new Polygon2f(pointsA);
            Polygon2f B = new Polygon2f(pointsB);

            Vector2f[] points = new Vector2f[]
            {
                new Vector2f(2, -2),
                new Vector2f(6, -2),
                new Vector2f(6, 0),
                new Vector2f(5, 2),
                new Vector2f(3, 2),
                new Vector2f(2, 0)
            };

            Polygon2f sum = MinkowskiSums2.ComputeSum(A, B);
            CollectionAssert.AreEqual(points, sum.Positions);

        }
    }
}
