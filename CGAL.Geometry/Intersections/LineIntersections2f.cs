using System;
using System.Collections.Generic;

using Common.Core.LinearAlgebra;
using Common.Core.Mathematics;
using CGAL.Geometry.Lines;
using CGAL.Geometry.Shapes;

namespace CGAL.Geometry.Intersections
{
    public static class LineIntersections2f
    {

        public static LineResult2f LineIntersectsSegment(IList<Vector2f> line, Segment2f seg, bool reverse = false, float radius = 0.0f)
        {
            Segment2f closest, seg0;
            float r2 = radius > 0.0f ? radius * radius : FMath.EPS;

            int count = line.Count;
            for (int i = 0; i < count - 1; i++)
            {
                int I0 = i;
                int I1 = i + 1;

                if(reverse)
                {
                    I0 = count - i - 2;
                    I1 = count - i - 1;
                }

                seg0.A = line[I0];
                seg0.B = line[I1];

                float s, t;
                SegmentIntersections2f.ClosestSegmentToSegments(seg0, seg, out s, out t);

                closest.A = seg0.A + (seg0.B - seg0.A) * s;
                closest.B = seg.A + (seg.B - seg.A) * t;

                if (closest.SqrLength < r2)
                {
                    LineResult2f result = new LineResult2f();
                    result.Hit = true;
                    result.Point0 = closest.A;
                    result.T0 = s;
                    result.A0 = I0;
                    result.B0 = I1;

                    result.Point1 = closest.B;
                    result.T1 = t;
                    result.A1 = 0;
                    result.B1 = 1;

                    return result;
                }
            }

            return LineResult2f.NoHit;
        }

        public static PointResult2f LineIntersectsRay(IList<Vector2f> line, Vector2f p, Vector2f n, bool reverse = false)
        {
            int count = line.Count;
            for (int i = 0; i < count - 1; i++)
            {
                int I0 = i;
                int I1 = i + 1;

                if (reverse)
                {
                    I0 = count - i - 2;
                    I1 = count - i - 1;
                }

                Vector2f a = line[I0];
                Vector2f b = line[I1];

                float t;
                if (RayIntersectsSegment(p, n, a, b, out t))
                {
                    PointResult2f result = new PointResult2f();
                    result.Hit = true;
                    result.Point = a + t * (b - a);
                    result.T = t;
                    result.A = I0;
                    result.B = I1;

                    return result;
                }
            }

            return PointResult2f.NoHit;
        }

        public static LineResult2f LineIntersectsLine(IList<Vector2f> line0, IList<Vector2f> line1, bool reverse0 = false, bool reverse1 = false, float radius = 0.0f)
        {
            Segment2f closest, seg0, seg1;
            float r2 = radius > 0.0f ? radius * radius : FMath.EPS;

            int count = line0.Count;
            for (int i = 0; i < count - 1; i++)
            {
                int I0 = i;
                int I1 = i + 1;

                if (reverse0)
                {
                    I0 = count - i - 2;
                    I1 = count - i - 1;
                }

                seg0.A = line0[I0];
                seg0.B = line0[I1];

                for (int j = 0; j < line1.Count - 1; j++)
                {
                    int J0 = j;
                    int J1 = j + 1;

                    if (reverse1)
                    {
                        J0 = count - j - 2;
                        J1 = count - j - 1;
                    }

                    seg1.A = line1[j];
                    seg1.B = line1[j + 1];

                    float s, t;
                    SegmentIntersections2f.ClosestSegmentToSegments(seg0, seg1, out s, out t);

                    closest.A = seg0.A + (seg0.B - seg0.A) * s;
                    closest.B = seg1.A + (seg1.B - seg1.A) * t;

                    if (closest.SqrLength < r2)
                    {
                        LineResult2f result = new LineResult2f();
                        result.Hit = true;
                        result.Point0 = closest.A;
                        result.T0 = s;
                        result.A0 = I0;
                        result.B0 = I1;

                        result.Point1 = closest.B;
                        result.T1 = t;
                        result.A1 = J0;
                        result.B1 = J1;

                        return result;
                    }

                }
            }

            return LineResult2f.NoHit;
        }

        public static LineResult2f ClosestSegment(IList<Vector2f> line0, IList<Vector2f> line1)
        {
            Segment2f closest, seg0, seg1;

            float dist = float.PositiveInfinity;
            LineResult2f result = new LineResult2f();

            int count = line0.Count;
            for (int i = 0; i < count - 1; i++)
            {
                int I0 = i;
                int I1 = i + 1;

                seg0.A = line0[I0];
                seg0.B = line0[I1];

                for (int j = 0; j < line1.Count - 1; j++)
                {
                    int J0 = j;
                    int J1 = j + 1;

                    seg1.A = line1[j];
                    seg1.B = line1[j + 1];

                    float s, t;
                    SegmentIntersections2f.ClosestSegmentToSegments(seg0, seg1, out s, out t);

                    closest.A = seg0.A + (seg0.B - seg0.A) * s;
                    closest.B = seg1.A + (seg1.B - seg1.A) * t;

                    float d2 = closest.SqrLength;

                    if (d2 < dist)
                    {
                        dist = d2;

                        result.Hit = true;
                        result.Point0 = closest.A;
                        result.T0 = s;
                        result.A0 = I0;
                        result.B0 = I1;

                        result.Point1 = closest.B;
                        result.T1 = t;
                        result.A1 = J0;
                        result.B1 = J1;
                    }

                }
            }

            return result;
        }
		
		public static PointResult2f ClosestPoint(IList<Vector2f> line, Vector2f point)
        {
            float dist = float.PositiveInfinity;
            PointResult2f result = new PointResult2f();
            Segment2f seg;

            int count = line.Count;
            for (int i = 0; i < count - 1; i++)
            {
                int I0 = i;
                int I1 = i + 1;

                seg.A = line[I0];
                seg.B = line[I1];

                float t;
                SegmentIntersections2f.ClosestPointOnSegment(seg, point, out t);

                Vector2f closest = seg.A + (seg.B - seg.A) * t;
                float d2 = Vector2f.SqrDistance(closest, point);
                if (d2 < dist)
                {
                    result.Hit = true;
                    result.Point = closest;
                    result.T = t;
                    result.A = I0;
                    result.B = I1;
                    dist = d2;
                }
            }

            return result;
        }

        private static float SignedTriArea(Vector2f a, Vector2f b, Vector2f c)
        {
            return (a.x - c.x) * (b.y - c.y) - (a.y - c.y) * (b.x - c.x);
        }

        private static bool RayIntersectsSegment(Vector2f p, Vector2f n, Vector2f a, Vector2f b, out float t)
        {
            t = 0;

            float dx = a.x - p.x;
            float dy = a.y - p.y;

            float len = Vector2f.Distance(a, b);
            if (FMath.IsZero(len)) return false;

            Vector2f n1;
            n1.x = (b.x - a.x) / len;
            n1.y = (b.y - a.y) / len;

            float det = n1.x * n.y - n1.y * n.x;
            if (FMath.IsZero(det)) return false;

            float s = (dy * n1.x - dx * n1.y) / det;
            t = (dy * n.x - dx * n.y) / det;
            t /= len;

            return s >= 0 && t >= 0 && t <= 1.0f;
        }

    }
}
