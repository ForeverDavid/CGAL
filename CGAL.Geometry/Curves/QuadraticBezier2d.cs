using System;
using System.Collections.Generic;

using Common.Core.LinearAlgebra;

namespace CGAL.Geometry.Curves
{
    /// <summary>
    /// A bezier curve of quadratic degree using a polynominal.
    /// </summary>
    public class QuadraticBezier2d
    {

        /// <summary>
        /// The control points.
        /// </summary>
        public Vector2d C0 { get; set; }
        public Vector2d C1 { get; set; }
        public Vector2d C2 { get; set; }

        public QuadraticBezier2d()
        {

        }

        public QuadraticBezier2d(Vector2d c0, Vector2d c1, Vector2d c2)
        {
            C0 = c0;
            C1 = c1;
            C2 = c2;
        }

        /// <summary>
        /// The length of the curve.
        /// </summary>
        public double Length
        {
            get
            {
                double ax = C0.x - 2.0 * C1.x + C2.x;
                double az = C0.y - 2.0 * C1.y + C2.y;
                double bx = 2.0 * (C1.x - C0.x);
                double bz = 2.0 * (C1.y - C0.y);

                double A = 4.0 * (ax * ax + az * az);
                double B = 4.0 * (ax * bx + az * bz);
                double C = bx * bx + bz * bz;

                double Sabc = 2.0 * Math.Sqrt(A + B + C);
                double A2 = Math.Sqrt(A);
                double A32 = 2.0 * A * A2;
                double CSQ = 2.0 * Math.Sqrt(C);
                double BA = B / A2;

                return (A32 * Sabc + A2 * B * (Sabc - CSQ) + (4 * C * A - B * B) * Math.Log((2 * A2 + BA + Sabc) / (BA + CSQ))) / (4 * A32);
            }
        }

        /// <summary>
        /// The position on the curve at t.
        /// </summary>
        /// <param name="t">Number between 0 and 1.</param>
        public Vector2d Position(double t)
        {
            if (t < 0.0) t = 0.0;
            if (t > 1.0) t = 1.0;

            double t1 = 1.0 - t;

            Vector2d p = new Vector2d();
            p.x = t1 * (t1 * C0.x + t * C1.x) + t * (t1 * C1.x + t * C2.x);
            p.y = t1 * (t1 * C0.y + t * C1.y) + t * (t1 * C1.y + t * C2.y);

            return p;
        }

        /// <summary>
        /// The tangent on the curve at t.
        /// </summary>
        /// <param name="t">Number between 0 and 1.</param>
        public Vector2d Tangent(double t)
        {
            Vector2d d = FirstDerivative(t);
            return d.Normalized;
        }

        /// <summary>
        /// The normal on the curve at t.
        /// </summary>
        /// <param name="t">Number between 0 and 1.</param>
        public Vector2d Normal(double t)
        {
            Vector2d d = FirstDerivative(t);
            return d.Normalized.PerpendicularCW;
        }

        /// <summary>
        /// The first derivative on the curve at t.
        /// </summary>
        /// <param name="t">Number between 0 and 1.</param>
        public Vector2d FirstDerivative(double t)
        {
            if (t < 0.0) t = 0.0;
            if (t > 1.0) t = 1.0;

            double t1 = 1.0 - t;

            Vector2d p = new Vector2d();
            p.x = 2.0 * t1 * (C1.x - C0.x) + 2.0 * t * (C2.x - C1.x);
            p.y = 2.0 * t1 * (C1.y - C0.y) + 2.0 * t * (C2.y - C1.y);

            return p;
        }

        /// <summary>
        /// The closest point on the curve to the point p.
        /// </summary>
        public Vector2d Closest(Vector2d p)
        {
            double px = C0.x - p.x;
            double pz = C0.y - p.y;
            double ax = C1.x - C0.x;
            double az = C1.y - C0.y;
            double bx = C0.x - 2.0 * C1.x + C2.x;
            double bz = C0.y - 2.0 * C1.y + C2.y;

            double a = bx * bx + bz * bz;
            double b = 3 * (ax * bx + az * bz);
            double c = 2 * (ax * ax + az * az) + px * bx + pz * bz;
            double d = px * ax + pz * az;

            var roots = Polynomial3d.Solve(a, b, c, d);

            double min = double.PositiveInfinity;
            Vector2d closest = new Vector2d(min, min);

            for(int i = 0; i < roots.real; i++)
            {
                double t = roots[i];

                Vector2d v = Position(t);
                double dist = Vector2d.SqrDistance(v, p);
                if (dist < min)
                {
                    min = dist;
                    closest = v;
                }
            }

            return closest;
        }

        /// <summary>
        /// If the segment ab intersects the curve.
        /// </summary>
        public bool Intersects(Vector2d a, Vector2d b)
        {
            //coefficients of quadratic
            Vector2d c2 = C0 + C1 * -2.0 + C2;
            Vector2d c1 = C0 * -2.0 + C1 * 2.0;

            //Convert line to normal form: ax + by + c = 0
            //Find normal to line: negative inverse of original line's slope
            Vector2d n = new Vector2d(a.y - b.y, b.x - a.x);

            //c coefficient for normal form of line
            double c = a.x * b.y - b.x * a.y;

            //Transform coefficients to line's coordinate system and find roots of cubic
            var roots = Polynomial3d.Solve(1, Vector2d.Dot(n, c2), Vector2d.Dot(n, c1), Vector2d.Dot(n, C0) + c);

            Vector2d min, max;
            min.x = Math.Min(a.x, b.x);
            min.y = Math.Min(a.y, b.y);

            max.x = Math.Max(a.x, b.x);
            max.y = Math.Max(a.y, b.y);

            for (int i = 0; i < roots.real; i++)
            {
                double t = roots[i];
                if (t < 0.0 || t > 1.0) continue;

                Vector2d v0 = Position(t);

                if (a.x == b.x)
                {
                    if (min.y <= v0.y && v0.y <= max.y)
                        return true;
                }
                else if (a.y == b.y)
                {
                    if (min.x <= v0.x && v0.x <= max.x)
                        return true;
                }
                else if(min.x <= v0.x && v0.x <= max.x && min.y <= v0.y && v0.y <= max.y)
                    return true;
            }

            return false;

        }

    }
}
