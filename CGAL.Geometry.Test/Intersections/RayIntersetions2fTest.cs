using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Common.Core.LinearAlgebra;
using CGAL.Geometry.Intersections;
using CGAL.Geometry.Shapes;

namespace CGAL.Geometry.Test.Intersections
{
    [TestClass]
    public class RayIntersetions2fTest
    {
        [TestMethod]
        public void RayIntersectsRay()
        {
            float eps = 1e-6f;
            Vector2f point;
            Ray2f ray0, ray1;

            ray0.Position = new Vector2f(0, 0);
            ray0.Direction = new Vector2f(1, 0);
            ray1.Position = new Vector2f(0, 0);
            ray1.Direction = new Vector2f(0, 1);

            bool intersect = RayIntersections2f.RayIntersectsRay(ray0, ray1, out point);
            Assert.IsFalse(intersect); //at tangent

            ray0.Position = new Vector2f(0, 0);
            ray0.Direction = new Vector2f(1, 0);
            ray1.Position = new Vector2f(0, 0);
            ray1.Direction = new Vector2f(-1, 0);

            intersect = RayIntersections2f.RayIntersectsRay(ray0, ray1, out point);
            Assert.IsFalse(intersect); //opposite

            ray0.Position = new Vector2f(0, 0);
            ray0.Direction = new Vector2f(1, 0);
            ray1.Position = new Vector2f(0, 0);
            ray1.Direction = new Vector2f(1, 0);

            intersect = RayIntersections2f.RayIntersectsRay(ray0, ray1, out point);
            Assert.IsFalse(intersect); //parallel

            ray0.Position = new Vector2f(0, 0);
            ray0.Direction = new Vector2f(0, 0);
            ray1.Position = new Vector2f(0, 0);
            ray1.Direction = new Vector2f(0, 0);

            intersect = RayIntersections2f.RayIntersectsRay(ray0, ray1, out point);
            Assert.IsFalse(intersect); //degenerate

            ray0.Position = new Vector2f(0, 1);
            ray0.Direction = new Vector2f(1, 0);
            ray1.Position = new Vector2f(1, 0);
            ray1.Direction = new Vector2f(0, 1);

            intersect = RayIntersections2f.RayIntersectsRay(ray0, ray1, out point);
            Assert.IsTrue(intersect); // at 90 degree
            Assert.IsTrue(Vector2f.Distance(point, new Vector2f(1, 1)) < eps);

            ray0.Position = new Vector2f(0, 1);
            ray0.Direction = new Vector2f(1, -1);
            ray1.Position = new Vector2f(1, 0);
            ray1.Direction = new Vector2f(-1, 1);

            ray0.Direction.Normalize();
            ray1.Direction.Normalize();

            intersect = RayIntersections2f.RayIntersectsRay(ray0, ray1, out point);
            Assert.IsFalse(intersect); //parallel at 45 degree

            ray0.Position = new Vector2f(0, 2);
            ray0.Direction = new Vector2f(1, -1);
            ray1.Position = new Vector2f(1, 0);
            ray1.Direction = new Vector2f(0, 1);

            ray0.Direction.Normalize();

            intersect = RayIntersections2f.RayIntersectsRay(ray0, ray1, out point);
            Assert.IsTrue(intersect); // at 45 degree
            Assert.IsTrue(Vector2f.Distance(point, new Vector2f(1, 1)) < eps);

        }

        [TestMethod]
        public void RayIntersectsSegment()
        {
            float eps = 1e-6f;
            Vector2f point;
            Ray2f ray;
            Segment2f seg;

            ray.Position = new Vector2f(0, 0);
            ray.Direction = new Vector2f(1, 0);
            seg.A = new Vector2f(0, 0);
            seg.B = new Vector2f(0, 1);

            bool intersect = RayIntersections2f.RayIntersectsSegment(ray, seg, out point);
            Assert.IsFalse(intersect); //at tangent

            ray.Position = new Vector2f(0, 0);
            ray.Direction = new Vector2f(1, 0);
            seg.A = new Vector2f(0, 0);
            seg.B = new Vector2f(-1, 0);

            intersect = RayIntersections2f.RayIntersectsSegment(ray, seg, out point);
            Assert.IsFalse(intersect); //opposite

            ray.Position = new Vector2f(0, 0);
            ray.Direction = new Vector2f(1, 0);
            seg.A = new Vector2f(0, 0);
            seg.B = new Vector2f(1, 0);

            intersect = RayIntersections2f.RayIntersectsSegment(ray, seg, out point);
            Assert.IsFalse(intersect); //parallel

            ray.Position = new Vector2f(0, 0);
            ray.Direction = new Vector2f(0, 0);
            seg.A = new Vector2f(0, 0);
            seg.B = new Vector2f(0, 0);

            intersect = RayIntersections2f.RayIntersectsSegment(ray, seg, out point);
            Assert.IsFalse(intersect); //degenerate

            ray.Position = new Vector2f(0, 1);
            ray.Direction = new Vector2f(1, 0);
            seg.A = new Vector2f(1, 0);
            seg.B = new Vector2f(1, 2);

            intersect = RayIntersections2f.RayIntersectsSegment(ray, seg, out point);
            Assert.IsTrue(intersect); // at 90 degree
            Assert.IsTrue(Vector2f.Distance(point, new Vector2f(1, 1)) < eps);

            ray.Position = new Vector2f(0, 1);
            ray.Direction = new Vector2f(1, -1);
            seg.A = new Vector2f(1, 0);
            seg.B = new Vector2f(-1, 1);

            ray.Direction.Normalize();

            intersect = RayIntersections2f.RayIntersectsSegment(ray, seg, out point);
            Assert.IsFalse(intersect); //parallel at 45 degree

            ray.Position = new Vector2f(0, 2);
            ray.Direction = new Vector2f(1, -1);
            seg.A = new Vector2f(1, 0);
            seg.B = new Vector2f(1, 2);

            ray.Direction.Normalize();

            intersect = RayIntersections2f.RayIntersectsSegment(ray, seg, out point);
            Assert.IsTrue(intersect); // at 45 degree
            Assert.IsTrue(Vector2f.Distance(point, new Vector2f(1, 1)) < eps);

        }

    }
}
