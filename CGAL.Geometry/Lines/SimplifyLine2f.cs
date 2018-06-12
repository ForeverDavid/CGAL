using System;
using System.Collections.Generic;

using Common.Core.LinearAlgebra;
using Common.Core.Colors;

namespace CGAL.Geometry.Lines
{
    public static class SimplifyLine2f
    {

        private struct LineAttributes2f
        {
            public List<Vector2f> positions;
            public List<Vector2f> normals;
            public List<Vector2f> uvs;
            public List<ColorRGBA> colors;
        }

        public static void Simplify(List<Vector2f> positions, float error)
        {
            int count = positions.Count;
            if (count <= 2) return;
            if (error <= 0.0f) return;

            LineAttributes2f attributes = CreateAttributes(positions, null);

            LineAttributes2f line = new LineAttributes2f();
            line.positions = positions;

            SimplifyRecursive(line, error, 0, count - 1, attributes);

            positions.Clear();
            positions.AddRange(attributes.positions);
        }

        public static void Simplify(List<Vector2f> positions, List<Vector2f> normals, float error)
        {
            int count = positions.Count;
            if (count <= 2) return;
            if (error <= 0.0f) return;

            LineAttributes2f attributes = CreateAttributes(positions, normals);

            LineAttributes2f line = new LineAttributes2f();
            line.positions = positions;
            line.normals = normals;

            SimplifyRecursive(line, error, 0, count - 1, attributes);

            positions.Clear();
            positions.AddRange(attributes.positions);

            normals.Clear();
            normals.AddRange(attributes.normals);
        }

        public static void Simplify(Line2f line, float error)
        {
            int count = line.VerticesCount;
            if (count <= 2) return;
            if (error <= 0.0f) return;

            LineAttributes2f attributes = CreateAttributes(line);

            SimplifyRecursive(line, error, 0, count - 1, attributes);

            line.SetPositions(attributes.positions);
            if (line.HasColors) line.SetColors(attributes.colors);
            if (line.HasNormals) line.SetNormals(attributes.normals);
            if (line.HasTexCoords0) line.SetTexCoords0(attributes.uvs);
        }

        private static void SimplifyRecursive(LineAttributes2f line, float error, int start, int end, LineAttributes2f attributes)
        {

            int index;
            float dist;
            Furthest(line.positions, start, end, out index, out dist);

            if (dist > error)
            {
                SimplifyRecursive(line, error, start, index, attributes);
                SimplifyRecursive(line, error, index, end, attributes);
            }
            else
            {
                attributes.positions.Add(line.positions[end]);

                if (attributes.colors != null)
                    attributes.colors.Add(line.colors[end]);

                if (attributes.normals != null)
                    attributes.normals.Add(line.normals[end]);

                if (attributes.uvs != null)
                    attributes.uvs.Add(line.uvs[end]);
            }

        }

        private static void SimplifyRecursive(Line2f line, float error, int start, int end, LineAttributes2f attributes)
        {

            int index;
            float dist;
            Furthest(line.Positions, start, end, out index, out dist);

            if (dist > error)
            {
                SimplifyRecursive(line, error, start, index, attributes);
                SimplifyRecursive(line, error, index, end, attributes);
            }
            else
            {
                attributes.positions.Add(line.Positions[end]);

                if (attributes.colors != null)
                    attributes.colors.Add(line.Colors[end]);

                if (attributes.normals != null)
                    attributes.normals.Add(line.Normals[end]);

                if (attributes.uvs != null)
                    attributes.uvs.Add(line.TexCoords0[end]);
            }

        }

        private static void Furthest(IList<Vector2f> positions, int start, int end, out int index, out float dist)
        {
            dist = float.NegativeInfinity;
            index = -1;

            Vector2f a = positions[start];
            Vector2f b = positions[end];

            for (int i = start + 1; i < end; i++)
            {
                Vector2f p = positions[i];

                float d2 = Distance(p, a, b);

                if (d2 > dist)
                {
                    dist = d2;
                    index = i;
                }
            }

            dist = (float)Math.Sqrt(dist);
        }

        private static float Distance(Vector2f p, Vector2f a, Vector2f b)
        {
            Vector2f ab = b - a;
            Vector2f ap = p - a;

            float len = ab.x * ab.x + ab.y * ab.y;
            if (len < 1e-6f) return Vector2f.Distance(a, p);

            float t = (ab.x * ap.x + ab.y * ap.y) / len;

            if (t < 0.0f) t = 0.0f;
            if (t > 1.0f) t = 1.0f;

            Vector2f c = new Vector2f(a.x + t * ab.x, a.y + t * ab.y);

            return Vector2f.SqrDistance(c, p);
        }

        private static LineAttributes2f CreateAttributes(Line2f line)
        {
            int count = line.VerticesCount;

            LineAttributes2f attributes = new LineAttributes2f();

            attributes.positions = new List<Vector2f>(count);
            attributes.positions.Add(line.Positions[0]);

            if (line.HasColors)
            {
                attributes.colors = new List<ColorRGBA>(count);
                attributes.colors.Add(line.Colors[0]);
            } 

            if (line.HasNormals)
            {
                attributes.normals = new List<Vector2f>(count);
                attributes.normals.Add(line.Normals[0]);
            }
                
            if (line.HasTexCoords0)
            {
                attributes.uvs = new List<Vector2f>(count);
                attributes.uvs.Add(line.TexCoords0[0]);
            }
                
            return attributes;
        }

        private static LineAttributes2f CreateAttributes(IList<Vector2f> positions, IList<Vector2f> normals)
        {
            int count = positions.Count;

            LineAttributes2f attributes = new LineAttributes2f();

            attributes.positions = new List<Vector2f>(count);
            attributes.positions.Add(positions[0]);

            if (normals != null)
            {
                attributes.normals = new List<Vector2f>(count);
                attributes.normals.Add(normals[0]);
            }

            return attributes;
        }
    }
}
