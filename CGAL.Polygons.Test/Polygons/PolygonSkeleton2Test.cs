using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Common.Core.LinearAlgebra;
using CGAL.Polygons;
using CGAL.Meshes.HalfEdgeBased;

namespace CGAL.Polygons.Test
{
    [TestClass]
    public class PolygonSkeleton2Test
    {
        [TestMethod]
        public void CreateInteriorSkeleton()
        {
            Polygon2f polygon = CreatePolygon2.FromBox(new Vector2f(-1), new Vector2f(1));

            var constructor = new HBMeshConstructor<HBVertex2f, HBEdge, HBFace>();
            PolygonSkeleton2.CreateInteriorSkeleton(polygon, constructor);

            var mesh = constructor.PopMesh();

            Assert.AreEqual(5, mesh.Vertices.Count);
            Assert.AreEqual(8, mesh.Edges.Count);
            Assert.AreEqual(0, mesh.Faces.Count);
        }
    }
}
