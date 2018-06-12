using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Common.Core.LinearAlgebra;
using Common.Geometry.Shapes;
using CGAL.Polygons;
using CGAL.Meshes.IndexBased;
using CGAL.Triangulation.Conforming;

namespace CGALCSharp.MeshGeneration.Test
{
    [TestClass]
    public class ConformingTriangulation2Test
    {
        [TestMethod]
        public void Triangulate()
        {
            Polygon2f polygon = CreatePolygon2.FromBox(new Vector2f(-1), new Vector2f(1));

            Mesh2f mesh = ConformingTriangulation2.Triangulate(polygon);

            Assert.AreEqual(4, mesh.VerticesCount);
            Assert.AreEqual(6, mesh.IndicesCount);

            for (int i = 0; i < mesh.IndicesCount / 3; i++)
            {
                Vector2f a = mesh.Positions[mesh.Indices[i * 3 + 0]];
                Vector2f b = mesh.Positions[mesh.Indices[i * 3 + 1]];
                Vector2f c = mesh.Positions[mesh.Indices[i * 3 + 2]];
                Triangle2f tri = new Triangle2f(a, b, c);

                Assert.IsTrue(tri.SignedArea > 0);
            }
        }
    }
}
