using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Common.Core.LinearAlgebra;

namespace CGAL.Geometry.Shapes
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Sphere3d
    {

        public Vector3d Center;

        public double Radius;

        public Sphere3d(Vector3d center, double radius)
        {
            Center = center;
            Radius = radius;
        }

        public double Radius2
        {
            get { return Radius * Radius; }
        }

        public double Diameter
        {
            get { return Radius * 2.0; }
        }

        public double Area
        {
            get { return 4.0 / 3.0 * Math.PI * Radius * Radius * Radius; }
        }

        public double SurfaceArea
        {
            get { return 4.0 * Math.PI * Radius2; }
        }

        public Box3d Bounds
        {
            get
            {
                double xmin = Center.x - Radius;
                double xmax = Center.x + Radius;
                double ymin = Center.y - Radius;
                double ymax = Center.y + Radius;
                double zmin = Center.z - Radius;
                double zmax = Center.z + Radius;

                return new Box3d(xmin, xmax, ymin, ymax, zmin, zmax);
            }
        }

        public override string ToString()
        {
            return string.Format("[Sphere3d: Center={0}, Radius={1}]", Center, Radius);
        }

    }

}