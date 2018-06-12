using System;
using System.Collections.Generic;

using Common.Core.LinearAlgebra;
using CGAL.Meshes.IndexBased;

namespace CGAL.Geometry.Lines
{
    public class Line2f : Mesh2f
    {

        public float Length { get; private set; }

        public Line2f()
        {

        }

        public Line2f(int size)
            : base(size)
        {

        }

        public Line2f(IList<Vector2f> positions)
            : base(positions)
        {

        }

        public Line2f(IList<Vector2f> positions, IList<int> indices)
            : base(positions, indices)
        {

        }

        public void BuildIndices()
        {
            int numSegments = VerticesCount - 1;
            if (numSegments == 0) return;

            int size = numSegments * 2;
            SetIndices(size);

            for (int i = 0; i < numSegments; i++)
            {
                Indices[i * 2 + 0] = i;
                Indices[i * 2 + 1] = i + 1;
            }
        }

        public void CalculateLength()
        {
            Length = 0;
            int count = VerticesCount;
            for (int i = 0; i < count - 1; i++)
            {
                Length += Vector2f.Distance(Positions[i], Positions[i + 1]);
            }
        }

    }
}
