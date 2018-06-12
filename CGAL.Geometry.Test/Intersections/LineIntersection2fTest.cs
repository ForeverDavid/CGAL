using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Common.Core.LinearAlgebra;
using CGAL.Geometry.Lines;
using CGAL.Geometry.Shapes;
using CGAL.Geometry.Intersections;

namespace CGAL.Geometry.Test.Intersections
{
    [TestClass]
    public class LineIntersection2fTest
    {
        [TestMethod]
        public void LineIntersectsSegment()
        {

            Vector2f[] line = new Vector2f[]
            {
                new Vector2f(0,0),
                new Vector2f(1,0),
                new Vector2f(2,0),
                new Vector2f(3,0)
            };

            Segment2f seg;
            LineResult2f result;

            seg.A = new Vector2f(0, 1);
            seg.B = new Vector2f(0, -1);

            //intersects start
            result = LineIntersections2f.LineIntersectsSegment(line, seg);
            Assert.IsTrue(result.Hit);
            Assert.AreEqual(0, result.A0);
            Assert.AreEqual(1, result.B0);
            Assert.AreEqual(0.0f, result.T0);
            Assert.AreEqual(0.5f, result.T1);

            seg.A = new Vector2f(1, 1);
            seg.B = new Vector2f(1, -1);

            //intersects join
            result = LineIntersections2f.LineIntersectsSegment(line, seg);
            Assert.IsTrue(result.Hit);
            Assert.AreEqual(0, result.A0);
            Assert.AreEqual(1, result.B0);
            Assert.AreEqual(1.0f, result.T0);
            Assert.AreEqual(0.5f, result.T1);

            seg.A = new Vector2f(2.5f, 3);
            seg.B = new Vector2f(2.5f, -1);

            //intersects mid
            result = LineIntersections2f.LineIntersectsSegment(line, seg);
            Assert.IsTrue(result.Hit);
            Assert.AreEqual(2, result.A0);
            Assert.AreEqual(3, result.B0);
            Assert.AreEqual(0.5f, result.T0);
            Assert.AreEqual(0.75f, result.T1);

            seg.A = new Vector2f(0, 1);
            seg.B = new Vector2f(4, 1);

            //parallel
            result = LineIntersections2f.LineIntersectsSegment(line, seg);
            Assert.IsFalse(result.Hit);

            seg.A = new Vector2f(-1, 1);
            seg.B = new Vector2f(-1, -1);

            //no intersection
            result = LineIntersections2f.LineIntersectsSegment(line, seg);
            Assert.IsFalse(result.Hit);

            seg.A = new Vector2f(4, 1);
            seg.B = new Vector2f(4, -1);

            //no intersection
            result = LineIntersections2f.LineIntersectsSegment(line, seg);
            Assert.IsFalse(result.Hit);

        }

        [TestMethod]
        public void LineIntersectsRay()
        {

            Vector2f[] line = new Vector2f[]
            {
                new Vector2f(0,0),
                new Vector2f(1,0),
                new Vector2f(2,0),
                new Vector2f(3,0)
            };

            Vector2f p = new Vector2f(0, -1);
            Vector2f n = new Vector2f(0, 1);

            //intersects start
            var result = LineIntersections2f.LineIntersectsRay(line, p, n);
            Assert.IsTrue(result.Hit);
            Assert.AreEqual(0, result.A);
            Assert.AreEqual(1, result.B);
            Assert.AreEqual(0.0f, result.T);

            p = new Vector2f(3, -1);
            n = new Vector2f(0, 1);

            //intersects end
            result = LineIntersections2f.LineIntersectsRay(line, p, n);
            Assert.IsTrue(result.Hit);
            Assert.AreEqual(2, result.A);
            Assert.AreEqual(3, result.B);
            Assert.AreEqual(1.0f, result.T);

            p = new Vector2f(0, 0);
            n = new Vector2f(0, 1);

            //intersects ray origin
            result = LineIntersections2f.LineIntersectsRay(line, p, n);
            Assert.IsTrue(result.Hit);
            Assert.AreEqual(0, result.A);
            Assert.AreEqual(1, result.B);
            Assert.AreEqual(0.0f, result.T);

            p = new Vector2f(1, -1);
            n = new Vector2f(0, 1);

            //intersects join
            result = LineIntersections2f.LineIntersectsRay(line, p, n);
            Assert.IsTrue(result.Hit);
            Assert.AreEqual(0, result.A);
            Assert.AreEqual(1, result.B);
            Assert.AreEqual(1.0f, result.T);

            p = new Vector2f(1.5f, -1);
            n = new Vector2f(0, 1);

            //intersects mid
            result = LineIntersections2f.LineIntersectsRay(line, p, n);
            Assert.IsTrue(result.Hit);
            Assert.AreEqual(1, result.A);
            Assert.AreEqual(2, result.B);
            Assert.AreEqual(0.5f, result.T);

            p = new Vector2f(0, 0);
            n = new Vector2f(0, 0);

            //degenerate
            result = LineIntersections2f.LineIntersectsRay(line, p, n);
            Assert.IsFalse(result.Hit);

            p = new Vector2f(0, 0);
            n = new Vector2f(1, 0);

            //parallel
            result = LineIntersections2f.LineIntersectsRay(line, p, n);
            Assert.IsFalse(result.Hit);

            p = new Vector2f(3, 0);
            n = new Vector2f(-1, 0);

            //parallel
            result = LineIntersections2f.LineIntersectsRay(line, p, n);
            Assert.IsFalse(result.Hit);

            p = new Vector2f(0, 0);
            n = new Vector2f(-1, 0);

            //parallel
            result = LineIntersections2f.LineIntersectsRay(line, p, n);
            Assert.IsFalse(result.Hit);

            p = new Vector2f(0, 1);
            n = new Vector2f(1, 0);

            //no intersection
            result = LineIntersections2f.LineIntersectsRay(line, p, n);
            Assert.IsFalse(result.Hit);

            p = new Vector2f(-1, -1);
            n = new Vector2f(0, 1);

            //no intersection
            result = LineIntersections2f.LineIntersectsRay(line, p, n);
            Assert.IsFalse(result.Hit);

            p = new Vector2f(4, 1);
            n = new Vector2f(0, -1);

            //no intersection
            result = LineIntersections2f.LineIntersectsRay(line, p, n);
            Assert.IsFalse(result.Hit);

        }

        [TestMethod]
        public void LineIntersectsLine()
        {
            Vector2f[] line0 = new Vector2f[]
            {
                new Vector2f(0,0),
                new Vector2f(1,0),
                new Vector2f(2,0),
                new Vector2f(3,0)
            };

            Vector2f[] line1 = new Vector2f[]
            {
                new Vector2f(1,1),
                new Vector2f(1,0),
                new Vector2f(1,-1)
            };

            var result = LineIntersections2f.LineIntersectsLine(line0, line1);

            Assert.IsTrue(result.Hit);
            Assert.AreEqual(0, result.A0);
            Assert.AreEqual(1, result.B0);
            Assert.AreEqual(1.0f, result.T0);
            Assert.AreEqual(1.0f, result.T1);

        }
    }
}
