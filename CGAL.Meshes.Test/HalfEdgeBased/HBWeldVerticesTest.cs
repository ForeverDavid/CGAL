using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Common.Core.LinearAlgebra;
using CGAL.Meshes.HalfEdgeBased;

namespace CGAL.Meshes.Test.HalfEdgeBased
{
    [TestClass]
    public class HBWeldVerticesTest
    {

        [TestMethod]
        public void WeldVerticesNoEdges()
        {
            var mesh = new HBMesh<HBVertex2f, HBEdge, HBFace>();

            HBVertex2f[] verts = new HBVertex2f[]
            {
                new HBVertex2f(new Vector2f(-1,1)),
                new HBVertex2f(new Vector2f(1, 1)),
                new HBVertex2f(new Vector2f(1, -1)),
                new HBVertex2f(new Vector2f(-1, -1)),
                new HBVertex2f(new Vector2f(0, 0.1f)),
                new HBVertex2f(new Vector2f(0, -0.1f))
            };

            mesh.Vertices.AddRange(verts);

            HBWeldVertices.WeldVertices(mesh, 0.21f);

            Assert.AreEqual(5, mesh.Vertices.Count);
            Assert.IsTrue(mesh.Vertices.Contains(verts[0]));
            Assert.IsTrue(mesh.Vertices.Contains(verts[1]));
            Assert.IsTrue(mesh.Vertices.Contains(verts[2]));
            Assert.IsTrue(mesh.Vertices.Contains(verts[3]));
            Assert.IsTrue(mesh.Vertices.Contains(verts[4]) || mesh.Vertices.Contains(verts[5]));

        }

    }
}
