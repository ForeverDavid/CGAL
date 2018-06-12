using System;
using System.Collections.Generic;
using System.Linq;

using Common.Core.LinearAlgebra;
using CGAL.Geometry.Curves;

namespace CGAL.Geometry.Lines
{
    public static class CreateLine2f
    {

        public static Line2f FromBezier(Bezier2f bezier, float spacing, bool normals = false, float simplify = 0.0f)
        {
            float length = bezier.Length(64);
            int count = (int)Math.Max(2, (length / spacing));

            Line2f line = new Line2f(count);

            if (normals)
                line.SetNormals(count);

            for (int i = 0; i < count; i++)
            {
                float t = i / (count - 1.0f);

                line.Positions[i] = bezier.Position(t);

                if(normals)
                    line.Normals[i] = bezier.Normal(t);
            }

            if (simplify > 0.0f)
                SimplifyLine2f.Simplify(line, simplify);

            return line;
        }

        public static Line2f FromParametricBezier(ParametricBezier2f bezier, float spacing, bool normals = false, float simplify = 0.0f)
        {
            float length = bezier.Length(64);
            int count = (int)Math.Max(2, (length / spacing));

            Line2f line = new Line2f(count);

            if (normals)
                line.SetNormals(count);

            for (int i = 0; i < count; i++)
            {
                float s = i / (count - 1.0f) * length;
                float t = bezier.Parametrize(s, length);

                line.Positions[i] = bezier.Position(t);

                if (normals)
                    line.Normals[i] = bezier.Normal(t);
            }

            if (simplify > 0.0f)
                SimplifyLine2f.Simplify(line, simplify);

            return line;
        }

        public static Line2f FromOffset(Line2f line, float offset, bool normals = false)
        {
            if (!line.HasNormals)
                throw new ArgumentException("Line must have normals to create a offset from.");

            int count = line.VerticesCount;
            Line2f offsetLine = new Line2f(count);

            for (int i = 0; i < count; i++)
            {
                Vector2f n = line.Normals[i];
                offsetLine.Positions[i] = line.Positions[i] + n * offset;
            }

            if (normals)
                offsetLine.SetNormals(line.Normals);

            return offsetLine;
        }

    }
}
