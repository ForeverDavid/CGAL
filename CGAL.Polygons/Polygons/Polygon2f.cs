using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Common.Core.LinearAlgebra;
using CGAL.Meshes.IndexBased;

namespace CGAL.Polygons
{

    public enum ORIENTATION
    {
        CLOCKWISE = -1,
        COLLINEAR = 0,
        COUNTERCLOCKWISE = 1
    };

    public class Polygon2f : Mesh2f
    {

        private const int SUCCESS = 0;
        private const int ERROR = 1;

        public bool IsSimple { get; protected set; }

        public bool IsConvex { get; protected set; }

        public bool IsCCW { get { return Orientation == ORIENTATION.COUNTERCLOCKWISE; } }

        public bool IsCW { get { return Orientation == ORIENTATION.CLOCKWISE; } }

        public ORIENTATION Orientation { get; protected set; }

        public int HoleCount { get { return (Holes != null) ? Holes.Count : 0; } }

        public float Area { get { return Math.Abs(SignedArea); } }

        public bool HasHoles { get { return Holes != null && Holes.Count > 0; } }

        public List<Polygon2f> Holes { get; private set; }

        private float m_signedArea;

        public Polygon2f(int size)
            : base(size)
        {
        }

        public Polygon2f(IList<Vector2f> positions) 
            : base(positions)
        {
            CalculatePolygon();
        }

        public Polygon2f(IList<Vector2f> positions, IList<int> indices)
            : base(positions, indices)
        {
            CalculatePolygon();
        }

        public override string ToString()
        {
            return string.Format("[Polygon2f: Positions={0}, IsSimple={1}, IsConvex={2}, Orientation={3}, Area={4}, Holes={5}]",
                VerticesCount, IsSimple, IsConvex, Orientation, Area, HoleCount);
        }

        public float SignedArea
        {
            get
            {
                float signedArea = m_signedArea;
                for (int i = 0; i < HoleCount; i++)
                    signedArea += Holes[i].SignedArea;

                return signedArea;
            }
        }

        public void BuildIndices()
        {
            int numPoints = VerticesCount;
            if (numPoints == 0) return;

            int size = numPoints * 2;
            SetIndices(size);

            for (int i = 0; i < numPoints; i++)
            {
                Indices[i * 2 + 0] = i;
                Indices[i * 2 + 1] = (i + 1) % numPoints;
            }
        }

        public void BuildHoleIndices()
        {
            if (Holes == null) return;

            int size = Holes.Count;
            for (int i = 0; i < size; i++)
                Holes[i].BuildIndices();
        }

        public void CalculatePolygon()
        {
            CGAL_LoadPoints(Positions, Positions.Length);

            IsSimple = CGAL_IsSimple();
            IsConvex = CGAL_IsConvex();
            m_signedArea = CGAL_SignedArea();
            Orientation = (ORIENTATION)CGAL_Orientation();

            CGAL_Release();

            for (int i = 0; i < HoleCount; i++)
                Holes[i].CalculatePolygon();
        }

        public void AddHole(Polygon2f hole)
        {
            if (hole.HasHoles)
                throw new  NotImplementedException("Hole with holes not implemented.");

            if (Holes == null)
                Holes = new List<Polygon2f>();

            Holes.Add(hole);
        }

        public void ClearHoles()
        {
            if (Holes == null) return;
            Holes.Clear();
        }

        public void MakeCCW()
        {
            if (Orientation == ORIENTATION.CLOCKWISE)
            {
                Array.Reverse(Positions);
                m_signedArea *= -1;
                Orientation = ORIENTATION.COUNTERCLOCKWISE;
            }

            for (int i = 0; i < HoleCount; i++)
                Holes[i].MakeCW();
        }

        public void MakeCW()
        {
            if (Orientation == ORIENTATION.COUNTERCLOCKWISE)
            {
                Array.Reverse(Positions);
                m_signedArea *= -1;
                Orientation = ORIENTATION.CLOCKWISE;
            }

            for (int i = 0; i < HoleCount; i++)
                Holes[i].MakeCCW();
        }

        [DllImport("CGALWrapper", EntryPoint = "Polygon2_LoadPoints2f", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CGAL_LoadPoints(Vector2f[] inPoints, int inSize);

        [DllImport("CGALWrapper", EntryPoint = "Polygon2_Release", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CGAL_Release();

        [DllImport("CGALWrapper", EntryPoint = "Polygon2_IsSimple", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool CGAL_IsSimple();

        [DllImport("CGALWrapper", EntryPoint = "Polygon2_IsConvex", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool CGAL_IsConvex();

        [DllImport("CGALWrapper", EntryPoint = "Polygon2_Orientation", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CGAL_Orientation();

        [DllImport("CGALWrapper", EntryPoint = "Polygon2_SignedArea", CallingConvention = CallingConvention.Cdecl)]
        private static extern float CGAL_SignedArea();

    }
}
