using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Common.Core.LinearAlgebra;

namespace CGAL.Geometry.Shapes
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Sphere3f
    {

        public Vector3f Center;

        public float Radius;

        public Sphere3f(Vector3f center, float radius)
        {
            Center = center;
            Radius = radius;
        }

        public float Radius2
        {
            get { return Radius * Radius; }
        }

        public float Diameter
        {
            get { return Radius * 2.0f; }
        }

        public float Area
        {
            get { return 4.0f / 3.0f * (float)Math.PI * Radius * Radius * Radius; }
        }

        public float SurfaceArea
        {
            get { return 4.0f * (float)Math.PI * Radius2; }
        }

        public Box3f Bounds
        {
            get
            {
                float xmin = Center.x - Radius;
                float xmax = Center.x + Radius;
                float ymin = Center.y - Radius;
                float ymax = Center.y + Radius;
                float zmin = Center.z - Radius;
                float zmax = Center.z + Radius;

                return new Box3f(xmin, xmax, ymin, ymax, zmin, zmax);
            }
        }

        public override string ToString()
        {
            return string.Format("[Sphere3f: Center={0}, Radius={1}]", Center, Radius);
        }

    }

}