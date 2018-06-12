using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Common.Core.LinearAlgebra;

namespace CGAL.Geometry.Shapes
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Box2d
    {

        public Vector2d Min;

        public Vector2d Max;

        public Box2d(double min, double max)
        {
            Min = new Vector2d(min);
            Max = new Vector2d(max);
        }

        public Box2d(double minX, double maxX, double minY, double maxY)
        {
            Min = new Vector2d(minX, minY);
            Max = new Vector2d(maxX, maxY);
        }

        public Box2d(Vector2d min, Vector2d max)
        {
            Min = min;
            Max = max;
        }

        public Box2d(Vector2i min, Vector2i max)
        {
            Min = new Vector2d(min.x, min.y);
            Max = new Vector2d(max.x, max.y);
        }

        public Vector2d Center 
        { 
            get { return (Min + Max) * 0.5; } 
        }

        public Vector2d Size 
        { 
            get { return new Vector2d(Width, Height); } 
        }

        public double Width 
        { 
            get { return Max.x - Min.x; } 
        }

        public double Height 
        { 
            get { return Max.y - Min.y; } 
        }

        public double Area 
        { 
            get { return (Max.x - Min.x) * (Max.y - Min.y); } 
        }

        public override string ToString()
        {
            return string.Format("[Box2d: Min={0}, Max={1}, Width={2}, Height={3}]", Min, Max, Width, Height);
        }

        public void GetCorners(IList<Vector2d> corners)
        {
            corners[0] = new Vector2d(Min.x, Min.y);
            corners[1] = new Vector2d(Min.x, Max.y);
            corners[2] = new Vector2d(Max.x, Max.y);
            corners[3] = new Vector2d(Max.x, Min.y);
        }

        public void GetCornersXZ(IList<Vector3d> corners, double y = 0)
        {
            corners[0] = new Vector3d(Min.x, y, Min.y);
            corners[1] = new Vector3d(Min.x, y, Max.y);
            corners[2] = new Vector3d(Max.x, y, Max.y);
            corners[3] = new Vector3d(Max.x, y, Min.y);
        }

        /// <summary>
        /// Returns the bounding box containing this box and the given point.
        /// </summary>
        public void Enlarge(Vector2d p)
        {
            Min.x = Math.Min(Min.x, p.x);
            Min.y = Math.Min(Min.y, p.y);
            Max.x = Math.Max(Max.x, p.x);
            Max.y = Math.Max(Max.y, p.y);
        }

        /// <summary>
        /// Returns the bounding box containing this box and the given box.
        /// </summary>
        public void Enlarge(Box2d box)
        {
            Min.x = Math.Min(Min.x, box.Min.x);
            Min.y = Math.Min(Min.y, box.Min.y);
            Max.x = Math.Max(Max.x, box.Max.x);
            Max.y = Math.Max(Max.y, box.Max.y);
        }

        public static Box2d CalculateBounds(IList<Vector2d> vertices)
        {
            Vector2d min = Vector2d.PositiveInfinity;
            Vector2d max = Vector2d.NegativeInfinity;

            int count = vertices.Count;
            for (int i = 0; i < count; i++)
            {
                Vector2d v = vertices[i];

                if (v.x < min.x) min.x = v.x;
                if (v.y < min.y) min.y = v.y;

                if (v.x > max.x) max.x = v.x;
                if (v.y > max.y) max.y = v.y;
            }

            return new Box2d(min, max);
        }

        public static Box2d CalculateBounds(Vector2d a, Vector2d b)
        {
            double xmin = Math.Min(a.x, b.x);
            double xmax = Math.Max(a.x, b.x);
            double ymin = Math.Min(a.y, b.y);
            double ymax = Math.Max(a.y, b.y);

            return new Box2d(xmin, xmax, ymin, ymax);
        }

        public static Box2d CalculateBoundsXZ(Box2d box, Matrix4x4d localToWorld)
        {

            Box2d bounds = new Box2d(float.PositiveInfinity, float.NegativeInfinity);

            Vector3d corners0 = localToWorld * new Vector3d(box.Min.x, 0, box.Min.y);
            Vector3d corners1 = localToWorld * new Vector3d(box.Min.x, 0, box.Max.y);
            Vector3d corners2 = localToWorld * new Vector3d(box.Max.x, 0, box.Max.y);
            Vector3d corners3 = localToWorld * new Vector3d(box.Max.x, 0, box.Min.y);

            double x = corners0.x;
            double y = corners0.z;

            if (x < bounds.Min.x) bounds.Min.x = x;
            if (y < bounds.Min.y) bounds.Min.y = y;
            if (x > bounds.Max.x) bounds.Max.x = x;
            if (y > bounds.Max.y) bounds.Max.y = y;

            x = corners1.x;
            y = corners1.z;

            if (x < bounds.Min.x) bounds.Min.x = x;
            if (y < bounds.Min.y) bounds.Min.y = y;
            if (x > bounds.Max.x) bounds.Max.x = x;
            if (y > bounds.Max.y) bounds.Max.y = y;

            x = corners2.x;
            y = corners2.z;

            if (x < bounds.Min.x) bounds.Min.x = x;
            if (y < bounds.Min.y) bounds.Min.y = y;
            if (x > bounds.Max.x) bounds.Max.x = x;
            if (y > bounds.Max.y) bounds.Max.y = y;

            x = corners3.x;
            y = corners3.z;

            if (x < bounds.Min.x) bounds.Min.x = x;
            if (y < bounds.Min.y) bounds.Min.y = y;
            if (x > bounds.Max.x) bounds.Max.x = x;
            if (y > bounds.Max.y) bounds.Max.y = y;

            return bounds;
        }

    }

}

















