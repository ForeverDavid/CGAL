using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Common.Core.LinearAlgebra;

namespace CGAL.Geometry.Shapes
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Box2i
    {

        public Vector2i Min;

        public Vector2i Max;

        public Box2i(int min, int max)
        {
            Min = new Vector2i(min);
            Max = new Vector2i(max);
        }

        public Box2i(int minX, int maxX, int minY, int maxY)
        {
            Min = new Vector2i(minX, minY);
            Max = new Vector2i(maxX, maxY);
        }

        public Box2i(Vector2i min, Vector2i max)
        {
            Min = min;
            Max = max;
        }

        public Vector2f Center 
        { 
            get { return new Vector2f((Min.x + Max.x) * 0.5f, (Min.y + Max.y) * 0.5f); } 
        }

        public Vector2i Size 
        { 
            get { return new Vector2i(Width, Height); } 
        }

        public int Width 
        { 
            get { return Max.x - Min.x; } 
        }

        public int Height 
        { 
            get { return Max.y - Min.y; } 
        }

        public int Area 
        { 
            get { return (Max.x - Min.x) * (Max.y - Min.y); } 
        }

        public override string ToString()
        {
            return string.Format("[Box2i: Min={0}, Max={1}, Width={2}, Height={3}]", Min, Max, Width, Height);
        }

        public void GetCorners(IList<Vector2i> corners)
        {
            corners[0] = new Vector2i(Min.x, Min.y);
            corners[1] = new Vector2i(Min.x, Max.y);
            corners[2] = new Vector2i(Max.x, Max.y);
            corners[3] = new Vector2i(Max.x, Min.y);
        }

        public void GetCornersXZ(IList<Vector3i> corners, int y = 0)
        {
            corners[0] = new Vector3i(Min.x, y, Min.y);
            corners[1] = new Vector3i(Min.x, y, Max.y);
            corners[2] = new Vector3i(Max.x, y, Max.y);
            corners[3] = new Vector3i(Max.x, y, Min.y);
        }

        /// <summary>
        /// Returns the bounding box containing this box and the given point.
        /// </summary>
        public void Enlarge(Vector2i p)
        {
            Min.x = Math.Min(Min.x, p.x);
            Min.y = Math.Min(Min.y, p.y);
            Max.x = Math.Max(Max.x, p.x);
            Max.y = Math.Max(Max.y, p.y);
        }

        /// <summary>
        /// Returns the bounding box containing this box and the given box.
        /// </summary>
        public void Enlarge(Box2i box)
        {
            Min.x = Math.Min(Min.x, box.Min.x);
            Min.y = Math.Min(Min.y, box.Min.y);
            Max.x = Math.Max(Max.x, box.Max.x);
            Max.y = Math.Max(Max.y, box.Max.y);
        }

        /// <summary>
        /// Returns true if this bounding box contains the given bounding box.
        /// </summary>
        public bool IntersectsBox(Box2i a)
        {
            if (Max.x < a.Min.x || Min.x > a.Max.x) return false;
            if (Max.y < a.Min.y || Min.y > a.Max.y) return false;
            return true;
        }

        /// <summary>
        /// Does the box contain the point.
        /// </summary>
        public bool ContainsPoint(Vector2i p)
        {
            if (p.x > Max.x || p.x < Min.x) return false;
            if (p.y > Max.y || p.y < Min.y) return false;
            return true;
        }

        /// <summary>
        /// Returns the closest point on the shape.
        /// </summary>
        public Vector2i ClosestPoint(Vector2i p)
        {
            Vector2i c;

            if (p.x < Min.x)
                c.x = Min.x;
            else if (p.x > Max.x)
                c.x = Max.x;
            else
                c.x = p.x;

            if (p.y < Min.y)
                c.y = Min.y;
            else if (p.y > Max.y)
                c.y = Max.y;
            else
                c.y = p.y;

            return c;
        }

        public static Box2i CalculateBoundsXZ(Box2f box, Matrix4x4f localToWorld)
        {

            Box2i bounds = new Box2i(int.MaxValue, int.MinValue);

            Vector3f corners0 = localToWorld * new Vector3f(box.Min.x, 0, box.Min.y);
            Vector3f corners1 = localToWorld * new Vector3f(box.Min.x, 0, box.Max.y);
            Vector3f corners2 = localToWorld * new Vector3f(box.Max.x, 0, box.Max.y);
            Vector3f corners3 = localToWorld * new Vector3f(box.Max.x, 0, box.Min.y);

            int x = (int)Math.Round(corners0.x);
            int y = (int)Math.Round(corners0.z);

            if (x < bounds.Min.x) bounds.Min.x = x;
            if (y < bounds.Min.y) bounds.Min.y = y;
            if (x > bounds.Max.x) bounds.Max.x = x;
            if (y > bounds.Max.y) bounds.Max.y = y;

            x = (int)Math.Round(corners1.x);
            y = (int)Math.Round(corners1.z);

            if (x < bounds.Min.x) bounds.Min.x = x;
            if (y < bounds.Min.y) bounds.Min.y = y;
            if (x > bounds.Max.x) bounds.Max.x = x;
            if (y > bounds.Max.y) bounds.Max.y = y;

            x = (int)Math.Round(corners2.x);
            y = (int)Math.Round(corners2.z);

            if (x < bounds.Min.x) bounds.Min.x = x;
            if (y < bounds.Min.y) bounds.Min.y = y;
            if (x > bounds.Max.x) bounds.Max.x = x;
            if (y > bounds.Max.y) bounds.Max.y = y;

            x = (int)Math.Round(corners3.x);
            y = (int)Math.Round(corners3.z);

            if (x < bounds.Min.x) bounds.Min.x = x;
            if (y < bounds.Min.y) bounds.Min.y = y;
            if (x > bounds.Max.x) bounds.Max.x = x;
            if (y > bounds.Max.y) bounds.Max.y = y;

            return bounds;
        }

    }

}

















