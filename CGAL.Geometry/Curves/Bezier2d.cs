using System;
using System.Collections.Generic;

using Common.Core.LinearAlgebra;

namespace CGAL.Geometry.Curves
{
    /// <summary>
    /// A bezier curve of arbitrary degree using a Bernstein Polynominal.
    /// </summary>
    public class Bezier2d : Bezier
    {

        /// <summary>
        /// The curves degree. 1 is linear, 2 quadratic, etc.
        /// </summary>
        public int Degree {  get { return Control.Length - 1; } }

        /// <summary>
        /// The control points.
        /// </summary>
        public Vector2d[] Control { get; private set; }

        public Bezier2d(BEZIER_DEGREE degree) 
            : this((int)degree)
        {

        }

        public Bezier2d(int degree)
        {
            if (degree > MAX_DEGREE || degree < MIN_DEGREE)
                throw new ArgumentException(string.Format("Degree can not be greater than {0} or less than {1}.", MAX_DEGREE, MIN_DEGREE));

            Control = new Vector2d[degree + 1];
        }

        public Bezier2d(IList<Vector2d> control)
        {
            int degree = control.Count - 1;
            if (degree > MAX_DEGREE || degree < MIN_DEGREE)
                throw new ArgumentException(string.Format("Degree can not be greater than {0} or less than {1}.", MAX_DEGREE, MIN_DEGREE));

            int count = control.Count;
            Control = new Vector2d[count];
            for (int i = 0; i < count; i++)
                Control[i] = control[i];
        }

        /// <summary>
        /// The position on the curve at t.
        /// </summary>
        /// <param name="t">Number between 0 and 1.</param>
        public Vector2d Position(double t)
        {
            if (t < 0) t = 0;
            if (t > 1) t = 1;

            int n = Control.Length;
            int degree = Degree;
            Vector2d p = new Vector2d();

            for (int i = 0; i < n; i++)
            {
                double basis = Bernstein(degree, i, t);
                p += basis * Control[i];
            }

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
            if (t < 0) t = 0;
            if (t > 1) t = 1;

            int n = Control.Length;
            int degree = Degree;
            double inv = 1.0 / degree;
            Vector2d d = new Vector2d();

            for (int i = 0; i < n - 1; i++)
            {
                double basis = Bernstein(degree - 1, i, t);
                d += basis * inv * (Control[i+1] - Control[i]);
            }

            return d * 4.0f;
        }

        /// <summary>
        /// Fills the array with positions on the curve.
        /// </summary>
        public void GetPositions(IList<Vector2d> points)
        {
            int count = points.Count;
            int n = Control.Length;
            int degree = Degree;

            double t = 0;
            double step = 1.0 / (count - 1.0);

            for (int i = 0; i < count; i++)
            {
                //if ((1.0 - t) < 5e-6)
                //    t = 1.0;

                for (int j = 0; j < n; j++)
                {
                    double basis = Bernstein(degree, j, t);
                    points[i] += basis * Control[j];
                }

                t += step;
            }
        }

        /// <summary>
        /// Arc length of curve via intergration.
        /// </summary>
        public double Length(int steps, double tmax = 1.0f)
        {
            if (tmax <= 0) return 0;
            if (tmax > 1) tmax = 1;

            if (Degree == 1)
                return Vector2d.Distance(Control[0], Control[1]) * tmax;
            else
            {
                steps = Math.Max(steps, 2);
                double len = 0;
                Vector2d previous = Position(0);

                for (int i = 1; i < steps; i++)
                {
                    double t = i / (steps - 1.0f) * tmax;
                    Vector2d p = Position(t);

                    len += Vector2d.Distance(previous, p);
                    previous = p;
                }

                return len;
            }
        }

        /// <summary>
        /// Returns the position at t using DeCasteljau's algorithm.
        /// </summary>
        internal Vector2d DeCasteljau(double t)
        {
            int count = Control.Length;
            Vector2d[] Q = new Vector2d[count];
            Array.Copy(Control, Q, count);

            for(int k = 1; k < count; k++)
            {
                for (int i = 0; i < count - k; i++)
                    Q[i] = (1.0 - t) * Q[i] + t * Q[i + 1];
            }

            return Q[0];
        }

        public void Split(double t, out Bezier2d b0, out Bezier2d b1)
        {
            int count = Control.Length;
            Vector2d[] Q = new Vector2d[count];
            Array.Copy(Control, Q, count);

            b0 = new Bezier2d(Degree);
            b1 = new Bezier2d(Degree);

            b0.Control[0] = Control[0];
            b1.Control[count - 1] = Control[count - 1];

            for (int k = 1; k < count; k++)
            {
                int len = count - k;
                for (int i = 0; i < len; i++)
                    Q[i] = (1.0 - t) * Q[i] + t * Q[i + 1];

                b0.Control[k] = Q[0];
                b1.Control[len-1] = Q[len-1];
            }
        }

    }
}
