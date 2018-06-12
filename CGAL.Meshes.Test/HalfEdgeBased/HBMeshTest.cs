using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CGAL.Meshes.HalfEdgeBased;

namespace CGAL.Meshes.Test.HalfEdgeBased
{
    [TestClass]
    public class HBMeshTest
    {
        [TestMethod]
        public void RemoveFaces()
        {

            var mesh = CreateTestMesh.CreateSquareWithCenter();
            mesh.RemoveFaces();

            Assert.AreEqual(0, mesh.Faces.Count);
            Assert.AreEqual(12, mesh.Edges.Count);

            foreach (var edge in mesh.Edges)
                Assert.IsNull(edge.Face);
        }

        [TestMethod]
        public void RemoveFace()
        {

            var mesh = CreateTestMesh.CreateSquareWithCenter();

            var face = mesh.Faces[0];
            mesh.RemoveFace(face);

            Assert.IsFalse(mesh.Faces.Contains(face));
            Assert.AreEqual(3, mesh.Faces.Count);

            foreach (var edge in mesh.Edges)
                Assert.AreNotEqual(edge.Face, face);
        }
    }
}
