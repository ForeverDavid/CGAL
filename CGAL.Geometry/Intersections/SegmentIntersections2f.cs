using System;
using System.Collections.Generic;

using Common.Core.LinearAlgebra;
using Common.Core.Mathematics;
using CGAL.Geometry.Shapes;

namespace CGAL.Geometry.Intersections
{
    public static class SegmentIntersections2f
    {

        public static float SqrDistanceFromSegment(Segment2f seg, Vector2f p)
        {
            Vector2f ab = seg.B - seg.A;
            Vector2f ac = p - seg.A;
            Vector2f bc = p - seg.B;

            float e = Vector2f.Dot(ac, ab);
            // Handle cases where c projects outside ab
            if (e <= 0.0) return Vector2f.Dot(ac, ac);

            float f = Vector2f.Dot(ab, ab);
            if (e >= f) return Vector2f.Dot(bc, bc);

            // Handle case where p projects onto ab
            return Vector2f.Dot(ac, ac) - e * e / f;
        }

        public static Vector2f ClosestPointOnSegment(Segment2f seg, Vector2f p)
        {
            float t;
            ClosestPointOnSegment(seg, p, out t);
            return seg.A + t * (seg.B - seg.A);
        }

        public static void ClosestPointOnSegment(Segment2f seg, Vector2f p, out float t)
        {
            t = 0.0f;
            Vector2f ab = seg.B - seg.A;
            Vector2f ap = p - seg.A;

            float len = ab.x * ab.x + ab.y * ab.y;
            if (len < FMath.EPS) return;

            t = (ab.x * ap.x + ab.y * ap.y) / len;

            if (t < 0.0f) t = 0.0f;
            if (t > 1.0f) t = 1.0f;
        }

        public static Segment2f ClosestSegmentToSegments(Segment2f seg0, Segment2f seg1)
        {
            float s, t;
            ClosestSegmentToSegments(seg0, seg1, out s, out t);

            return new Segment2f(seg0.A + (seg0.B - seg0.A) * s, seg1.A + (seg1.B - seg1.A) * t);
        }

        public static void ClosestSegmentToSegments(Segment2f seg0, Segment2f seg1, out float s, out float t)
        {

            Vector2f ab0 = seg0.B - seg0.A;
            Vector2f ab1 = seg1.B - seg1.A;
            Vector2f a01 = seg0.A - seg1.A;

            float d00 = Vector2f.Dot(ab0, ab0);
            float d11 = Vector2f.Dot(ab1, ab1);
            float d1 = Vector2f.Dot(ab1, a01);

            s = 0;
            t = 0;

            //Check if either or both segments degenerate into points.
            if (d00 < FMath.EPS && d11 < FMath.EPS)
                return;

            if (d00 < FMath.EPS)
            {
                //First segment degenerates into a point.
                s = 0;
                t = FMath.Clamp01(d1 / d11);
            }
            else
            {
                float c = Vector2f.Dot(ab0, a01);

                if (d11 < FMath.EPS)
                {
                    //Second segment degenerates into a point.
                    s = FMath.Clamp01(-c / d00);
                    t = 0;
                }
                else
                {
                    //The generate non degenerate case starts here.
                    float d2 = Vector2f.Dot(ab0, ab1);
                    float denom = d00 * d11 - d2 * d2;

                    //if segments not parallel compute closest point and clamp to segment.
                    if (!FMath.IsZero(denom))
                        s = FMath.Clamp01((d2 * d1 - c * d11) / denom);
                    else
                        s = 0;

                    t = (d2 * s + d1) / d11;

                    if (t < 0.0f)
                    {
                        t = 0.0f;
                        s = FMath.Clamp01(-c / d00);
                    }
                    else if (t > 1.0f)
                    {
                        t = 1.0f;
                        s = FMath.Clamp01((d2 - c) / d00);
                    }
                }
            }
        }

        public static bool SegmentIntersectsSegment(Segment2f seg0, Segment2f seg1, out Vector2f p)
        {
            float t;
            p = Vector2f.Zero;
            if (SegmentIntersectsSegment(seg0, seg1, out t))
            {
                p = seg0.A + t * (seg0.B - seg0.A);
                return true;
            }
            else
                return false;
        }

        public static bool SegmentIntersectsSegment(Segment2f seg0, Segment2f seg1, out float t)
        {

            float area1 = SignedTriArea(seg0.A, seg0.B, seg1.B);
            float area2 = SignedTriArea(seg0.A, seg0.B, seg1.A);
            t = 0.0f;

            if (area1 * area2 < 0.0)
            {
                float area3 = SignedTriArea(seg1.A, seg1.B, seg0.A);
                float area4 = area3 + area2 - area1;

                if (area3 * area4 < 0.0)
                {
                    t = area3 / (area3 - area4);
                    return true;
                }
            }

            return false;
        }

        public static bool SegmentIntersectsSegment(Segment2f seg0, Segment2f seg1, out float s, out float t)
        {

            float area1 = SignedTriArea(seg0.A, seg0.B, seg1.B);
            float area2 = SignedTriArea(seg0.A, seg0.B, seg1.A);
            s = 0.0f;
            t = 0.0f;

            if (area1 * area2 < 0.0)
            {
                float area3 = SignedTriArea(seg1.A, seg1.B, seg0.A);
                float area4 = area3 + area2 - area1;

                if (area3 * area4 < 0.0)
                {
                    s = area3 / (area3 - area4);

                    area1 = SignedTriArea(seg1.A, seg1.B, seg0.B);
                    area2 = SignedTriArea(seg1.A, seg1.B, seg0.A);
                    area3 = SignedTriArea(seg0.A, seg0.B, seg1.A);
                    area4 = area3 + area2 - area1;

                    t = area3 / (area3 - area4);
                    return true;
                }
            }

            return false;
        }

        private static float SignedTriArea(Vector2f a, Vector2f b, Vector2f c)
        {
            return (a.x - c.x) * (b.y - c.y) - (a.y - c.y) * (b.x - c.x);
        }


    }

}
