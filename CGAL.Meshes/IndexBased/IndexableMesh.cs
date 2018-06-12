using System;
using System.Collections.Generic;

using Common.Core.LinearAlgebra;
using Common.Core.Colors;

namespace CGAL.Meshes.IndexBased
{
    public abstract class IndexableMesh
    {

        public bool HasIndice { get { return Indices != null; } }

        public int IndicesCount { get { return (Indices != null) ? Indices.Length : 0; } }

        public int[] Indices { get; protected set; }

        public bool HasColors { get { return Colors != null; } }

        public ColorRGBA[] Colors { get; protected set; }

        public void SetIndices(int size)
        {
            if (Indices == null || Indices.Length != size)
                Indices = new int[size];
        }

        public void SetIndices(IList<int> indices)
        {
            SetIndices(indices.Count);
            indices.CopyTo(Indices, 0);
        }

        public void SetColors(int size)
        {
            if (Colors == null || Colors.Length != size)
                Colors = new ColorRGBA[size];
        }

        public void SetColors(IList<ColorRGBA> colors)
        {
            SetColors(colors.Count);
            colors.CopyTo(Colors, 0);
        }

    }
}
