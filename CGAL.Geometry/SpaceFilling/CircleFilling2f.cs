using System;
using System.Collections.Generic;

using Common.Core.LinearAlgebra;
using CGAL.Geometry.Shapes;

namespace CGAL.Geometry.SpaceFilling
{

    public class CircleFilling2f
    {

        private List<Circle2f>[,] m_grid;

        public CircleFilling2f(float width, float height, float cellsize)
        {
            GridWidth = (int)Math.Ceiling(width / cellsize);
            GridHeight = (int)Math.Ceiling(height / cellsize);
            Width = width;
            Height = height;
            CellSize = cellsize;

            m_grid = new List<Circle2f>[GridWidth, GridHeight];
        }

        public int Count { get; private set; }

        public int GridWidth { get; private set; }

        public int GridHeight { get; private set; }

        public float Width { get; private set; }

        public float Height { get; private set; }

        public float CellSize { get; private set; }

        public bool Add(Vector2f position, float radius)
        {
            if (position.x < 0 || position.x > Width) return false;
            if (position.y < 0 || position.y > Height) return false;

            int x = (int)Math.Floor(position.x / CellSize);
            int y = (int)Math.Floor(position.y / CellSize);

            if (x < 0 || x >= GridWidth) return false;
            if (y < 0 || y >= GridHeight) return false;

            Circle2f circle = new Circle2f();
            circle.Center = position;
            circle.Radius = radius;

            if (m_grid[x, y] == null)
                m_grid[x, y] = new List<Circle2f>();

            var list = m_grid[x, y];
            list.Add(circle);
            Count++;

            return true;
        }

        public void Clear()
        {
            Count = 0;
            for (int y = 0; y < GridHeight; y++)
            {
                for (int x = 0; x < GridWidth; x++)
                {
                    var list = m_grid[x, y];
                    if (list != null)
                        list.Clear();
                }
            }
        }

        public void GetCircles(List<Circle2f> circles)
        {
            for (int y = 0; y < GridHeight; y++)
            {
                for (int x = 0; x < GridWidth; x++)
                {
                    var list = m_grid[x, y];
                    if (list != null)
                        circles.AddRange(list);
                }
            }
        }

        public bool Intersects(Vector2f position, float radius)
        {

            int i = (int)Math.Floor(position.x / CellSize);
            int j = (int)Math.Floor(position.y / CellSize);

            for (int y = -1; y <= 1; y++)
            {
                for (int x = -1; x <= 1; x++)
                {
                    int xi = x + i;
                    int yj = y + j;

                    if (xi < 0 || xi >= GridWidth) continue;
                    if (yj < 0 || yj >= GridHeight) continue;

                    var list = m_grid[xi, yj];
                    if (list == null) continue;

                    int count = list.Count;
                    for (int k = 0; k < count; k++)
                    {
                        var circle = list[k];

                        float d2 = Vector2f.SqrDistance(circle.Center, position);
                        float r = circle.Radius + radius;

                        if (d2 < r * r) return true;
                    }
                }
            }

            return false;
        }

        public void Fill(Random rnd, float radius)
        {
            if (radius <= 0) return;

            float spacing = radius * 0.25f;
            int countX = (int)(Width / spacing) + 1;
            int countY = (int)(Height / spacing) + 1;

            List<Vector2f> samples = new List<Vector2f>();

            for (int y = 0; y < countX; y++)
            {
                for (int x = 0; x < countY; x++)
                {
                    samples.Add(new Vector2f(x, y) * spacing);
                }
            }

            int count = samples.Count;
            while (count > 1)
            {
                int k = rnd.Next(count--);
                Vector2f temp = samples[count];
                samples[count] = samples[k];
                samples[k] = temp;
            }

            count = samples.Count;
            for (int i = 0; i < count; i++)
            {
                float x = samples[i].x;
                float y = samples[i].y;

                Vector2f position;
                position.x = x + (float)rnd.NextDouble() * spacing;
                position.y = y + (float)rnd.NextDouble() * spacing;

                if (position.x < 0 || position.x > Width) continue;
                if (position.y < 0 || position.y > Height) continue;

                if (!Intersects(position, radius))
                    Add(position, radius);
            }

        }

    }
}
