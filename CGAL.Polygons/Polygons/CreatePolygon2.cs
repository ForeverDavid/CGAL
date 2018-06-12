using System;
using System.Collections.Generic;

using Common.Core.LinearAlgebra;
using CGAL.Geometry.Shapes;
using CGAL.Geometry.Lines;

namespace CGAL.Polygons
{

    public static class CreatePolygon2
    {

        public static Polygon2f FromTriangle(Vector2f A, Vector2f B, Vector2f C)
        {
            Polygon2f polygon = new Polygon2f(3);
            polygon.Positions[0] = A;
            polygon.Positions[1] = B;
            polygon.Positions[2] = C;

            polygon.CalculatePolygon();
            polygon.MakeCCW();
            return polygon;
        }

        public static Polygon2f FromBox(Vector2f min, Vector2f max)
        {
            Polygon2f polygon = new Polygon2f(4);
            polygon.Positions[0] = min;
            polygon.Positions[1] = new Vector2f(max.x, min.y);
            polygon.Positions[2] = max;
            polygon.Positions[3] = new Vector2f(min.x, max.y);

            polygon.CalculatePolygon();
            polygon.MakeCCW();
            return polygon;
        }

        public static Polygon2f FromCircle(Vector2f center, float radius, int segments)
        {
            Polygon2f polygon = new Polygon2f(segments);

            float pi = (float)Math.PI;
            float fseg = segments;

            for (int i = 0; i < segments; i++)
            {
                float theta = 2.0f * pi * i / fseg;

                float x = radius * (float)Math.Cos(theta);
                float y = radius * (float)Math.Sin(theta);

                polygon.Positions[i] = center + new Vector2f(x, y);
            }

            polygon.CalculatePolygon();
            polygon.MakeCCW();
            return polygon;
        }

        public static Polygon2f FromCapsule(Vector2f center, float radius, float length, int segments)
        {
            int num = segments + 1;
            Polygon2f polygon = new Polygon2f(num * 2);

            float pi = (float)Math.PI;
            float fseg = segments;
            float half = length * 0.5f;

            for (int i = 0; i < num; i++)
            {
                float theta = pi * i / fseg;

                float x = radius * (float)Math.Cos(theta);
                float y = radius * (float)Math.Sin(theta);

                polygon.Positions[i] = center + new Vector2f(x, y + half);
            }

            for (int i = 0; i < num; i++)
            {
                float theta = pi * i / fseg + pi;

                float x = radius * (float)Math.Cos(theta);
                float y = radius * (float)Math.Sin(theta);

                polygon.Positions[i + num] = center + new Vector2f(x, y - half);
            }

            polygon.CalculatePolygon();
            polygon.MakeCCW();
            return polygon;
        }

        public static Polygon2f FromStar4(Vector2f center, float scale)
        {

            Polygon2f polygon = new Polygon2f(8);
            polygon.Positions[0] = new Vector2f(0,-1) * scale;
            polygon.Positions[1] = new Vector2f(0.2f, -0.2f) * scale;
            polygon.Positions[2] = new Vector2f(1,0) * scale;
            polygon.Positions[3] = new Vector2f(0.2f, 0.2f) * scale;
            polygon.Positions[4] = new Vector2f(0,1) * scale;
            polygon.Positions[5] = new Vector2f(-0.2f, 0.2f) * scale;
            polygon.Positions[6] = new Vector2f(-1,0) * scale;
            polygon.Positions[7] = new Vector2f(-0.2f, -0.2f) * scale;

            polygon.CalculatePolygon();
            polygon.MakeCCW();
            return polygon;
        }

        public static Polygon2f FromLine(Line2f line, float width)
        {
            if (!line.HasNormals)
                throw new ArgumentException("Line must have normals to create a polygon from.");

            if (line.VerticesCount < 2)
                throw new ArgumentException("Line must have at least to positions to create a polygon from.");

            int count = line.VerticesCount;
            float half = width * 0.5f;

            Polygon2f polygon = new Polygon2f(count * 2);

            for(int i = 0; i < count; i++)
            {
                polygon.Positions[i] = line.Positions[i] + line.Normals[i] * half;
            }

            for (int i = 0; i < count; i++)
            {
                int j = count - i - 1;
                polygon.Positions[i + count] = line.Positions[j] - line.Normals[j] * half;
            }

            return polygon;
        }

    }

}