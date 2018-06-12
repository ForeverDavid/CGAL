using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Common.Core.LinearAlgebra;

namespace CGAL.Geometry.Shapes
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Box3d
    {

        public Vector3d Min;

        public Vector3d Max;

        public Box3d(double min, double max)
        {
            Min = new Vector3d(min);
            Max = new Vector3d(max);
        }

        public Box3d(double minX, double maxX, double minY, double maxY, double minZ, double maxZ)
        {
            Min = new Vector3d(minX, minY, minZ);
            Max = new Vector3d(maxX, maxY, maxZ);
        }

        public Box3d(Vector3d min, Vector3d max)
        {
            Min = min;
            Max = max;
        }

        public Box3d(Vector3i min, Vector3i max)
        {
            Min = new Vector3d(min.x, min.y, min.z);
            Max = new Vector3d(max.x, max.y, max.z); ;
        }

        public Vector3d Center 
        { 
            get { return (Min + Max) * 0.5f; } 
        }

        public Vector3d Size 
        { 
            get { return new Vector3d(Width, Height, Depth); } 
        }

        public double Width 
        { 
            get { return Max.x - Min.x; } 
        }

        public double Height 
        { 
            get { return Max.y - Min.y; } 
        }

        public double Depth 
        { 
            get { return Max.z - Min.z; } 
        }

        public double Area
        {
            get
            {
                return (Max.x - Min.x) * (Max.y - Min.y) * (Max.z - Min.z);
            }
        }

        public double SurfaceArea
        {
            get
            {
                Vector3d d = Max - Min;
                return 2.0 * (d.x * d.y + d.x * d.z + d.y * d.z);
            }
        }

        public override string ToString()
        {
            return string.Format("[Box3d: Min={0}, Max={1}, Width={2}, Height={3}, Depth={4}]", Min, Max, Width, Height, Depth);
        }

        public void GetCorners(IList<Vector3d> corners)
        {
            corners[0] = new Vector3d(Min.x, Min.y, Min.z);
            corners[1] = new Vector3d(Min.x, Min.y, Max.z);
            corners[2] = new Vector3d(Max.x, Min.y, Max.z);
            corners[3] = new Vector3d(Max.x, Min.y, Min.z);

            corners[4] = new Vector3d(Min.x, Max.y, Min.z);
            corners[5] = new Vector3d(Min.x, Max.y, Max.z);
            corners[6] = new Vector3d(Max.x, Max.y, Max.z);
            corners[7] = new Vector3d(Max.x, Max.y, Min.z);
        }

        public void GetCorners(IList<Vector4d> corners)
        {
            corners[0] = new Vector4d(Min.x, Min.y, Min.z, 1);
            corners[1] = new Vector4d(Min.x, Min.y, Max.z, 1);
            corners[2] = new Vector4d(Max.x, Min.y, Max.z, 1);
            corners[3] = new Vector4d(Max.x, Min.y, Min.z, 1);

            corners[4] = new Vector4d(Min.x, Max.y, Min.z, 1);
            corners[5] = new Vector4d(Min.x, Max.y, Max.z, 1);
            corners[6] = new Vector4d(Max.x, Max.y, Max.z, 1);
            corners[7] = new Vector4d(Max.x, Max.y, Min.z, 1);
        }

        /// <summary>
        /// Returns the bounding box containing this box and the given point.
        /// </summary>
        public void Enlarge(Vector3d p)
        {
            Min.x = Math.Min(Min.x, p.x);
            Min.y = Math.Min(Min.y, p.y);
            Min.z = Math.Min(Min.z, p.z);
            Max.x = Math.Max(Max.x, p.x);
            Max.y = Math.Max(Max.y, p.y);
            Max.z = Math.Max(Max.z, p.z);
        }

        /// <summary>
        /// Returns the bounding box containing this box and the given box.
        /// </summary>
        public void Enlarge(Box3d box)
        {
            Min.x = Math.Min(Min.x, box.Min.x);
            Min.y = Math.Min(Min.y, box.Min.y);
            Min.z = Math.Min(Min.z, box.Min.z);
            Max.x = Math.Max(Max.x, box.Max.x);
            Max.y = Math.Max(Max.y, box.Max.y);
            Max.z = Math.Max(Max.z, box.Max.z);
        }

        public static Box3d CalculateBounds(IList<Vector3d> vertices)
        {
            Vector3d min = Vector3d.PositiveInfinity;
            Vector3d max = Vector3d.NegativeInfinity;

            int count = vertices.Count;
            for (int i = 0; i < count; i++)
            {
                Vector3d v = vertices[i];

                if (v.x < min.x) min.x = v.x;
                if (v.y < min.y) min.y = v.y;
                if (v.z < min.z) min.z = v.z;

                if (v.x > max.x) max.x = v.x;
                if (v.y > max.y) max.y = v.y;
                if (v.z > max.z) max.z = v.z;
            }

            return new Box3d(min, max);
        }

        public static Box3d CalculateBounds(Vector3d a, Vector3d b)
        {
            double xmin = Math.Min(a.x, b.x);
            double xmax = Math.Max(a.x, b.x);
            double ymin = Math.Min(a.y, b.y);
            double ymax = Math.Max(a.y, b.y);
            double zmin = Math.Min(a.z, b.z);
            double zmax = Math.Max(a.z, b.z);

            return new Box3d(xmin, xmax, ymin, ymax, zmin, zmax);
        }
    }

}




















