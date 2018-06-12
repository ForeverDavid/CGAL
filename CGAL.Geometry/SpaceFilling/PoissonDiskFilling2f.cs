using System;
using System.Collections.Generic;
using System.Linq;

using Common.Core.LinearAlgebra;

namespace CGAL.Geometry.SpaceFilling
{

    public class PoissonDiskFilling2f
    {

        private int[,] m_grid;

        public PoissonDiskFilling2f(float width, float height, float radius)
        {
            float cellsize = radius / (float)Math.Sqrt(2);

            GridWidth = (int)Math.Ceiling(width / cellsize);
            GridHeight = (int)Math.Ceiling(height / cellsize);
            Width = width;
            Height = height;
            Radius = radius;
            CellSize = cellsize;
            Disks = new List<Vector2f>();

            m_grid = new int[GridWidth, GridHeight];
        }

        public List<Vector2f> Disks { get; private set; }

        public int GridWidth { get; private set; }

        public int GridHeight { get; private set; }

        public float Width { get; private set; }

        public float Height { get; private set; }

        public float Radius { get; private set; }

        public float CellSize { get; private set; }

        public void Clear()
        {
            Disks.Clear();

            for (int y = 0; y < GridHeight; y++)
            {
                for (int x = 0; x < GridWidth; x++)
                    m_grid[x, y] = -1;
            }
        }

        public void Fill(System.Random rnd)
        {
            Clear();

            float s = (float)rnd.NextDouble() * Width;
            float t = (float)rnd.NextDouble() * Height;

            List<int> active = new List<int>();
            active.Add(AddDisk(s,t));

            while(active.Count > 0)
            {
                int index = rnd.Next(0, active.Count - 1);
                Vector2f p = Disks[active[index]];

                bool wasAdded = false;
  
                for (int k = 0; k < 30; k++)
                {
                    Vector2f dir = new Vector2f();
                    dir.x = (float)rnd.NextDouble() * 2 - 1;
                    dir.y = (float)rnd.NextDouble() * 2 - 1;
                    dir.Normalize();

                    float x = p.x + dir.x * Radius * 2;
                    float y = p.y + dir.y * Radius * 2;

                    if (OutOfBounds(x,y) || Intersects(x,y)) continue;

                    active.Add(AddDisk(x, y));
                    wasAdded = true;
                    break;
                }

                if (!wasAdded)
                    active.RemoveAt(index);
            }

        }

        private int AddDisk(float x, float y)
        {
            Vector2i index = GridIndex(x, y);

            Disks.Add(new Vector2f(x, y));
            m_grid[index.x, index.y] = Disks.Count - 1;

            return m_grid[index.x, index.y];
        }

        private Vector2i GridIndex(float x, float y)
        {
            int i = (int)Math.Floor(x / CellSize);
            int j = (int)Math.Floor(y / CellSize);
            return new Vector2i(i, j);
        }

        private bool OutOfBounds(float x, float y)
        {
            if (x < 0 || x > Width) return true;
            if (y < 0 || y > Height) return true;
            return false;
        }

        private bool Intersects(float x, float y)
        {
            Vector2i index = GridIndex(x, y);
            Vector2f p = new Vector2f(x, y);

            if (m_grid[index.x, index.y] != -1) return true;

            for (int j = -3; j <= 3; j++)
            {
                for (int i = -3; i <= 3; i++)
                {
                    int xi = index.x + i;
                    int yj = index.y + j;

                    if (xi < 0 || xi >= GridWidth) continue;
                    if (yj < 0 || yj >= GridHeight) continue;

                    int k = m_grid[xi, yj];
                    if (k == -1) continue;

                    float d2 = Vector2f.SqrDistance(Disks[k], p);
                    float r = Radius * 2;

                    if (d2 < r * r) return true;
                }
            }

            return false;
        }

        private bool IntersectsSlow(float x, float y)
        {
            Vector2f p = new Vector2f(x, y);
            foreach(var q in Disks)
            {
                float d2 = Vector2f.SqrDistance(q, p);
                float r = Radius * 2;

                if (d2 < r * r) return true;
            }

            return false;
        }

    }
}
