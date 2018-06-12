using System;
using System.Collections.Generic;

using Common.Core.LinearAlgebra;

namespace CGAL.Geometry.Curves
{
    /// <summary>
    /// A bezier curve of arbitrary degree using a Bernstein Polynominal.
    /// </summary>
    public class Bezier2f : Bezier
    {

        /// <summary>
        /// The curves degree. 1 is linear, 2 quadratic, etc.
        /// </summary>
        public int Degree { get { return Control.Length - 1; } }

        /// <summary>
        /// The control points.
        /// </summary>
        public Vector2f[] Control { get; private set; }

        public Bezier2f(BEZIER_DEGREE degree) 
            : this((int)degree)
        {

        }

        public Bezier2f(int degree)
        {
            if (degree > MAX_DEGREE || degree < MIN_DEGREE)
                throw new ArgumentException(string.Format("Degree can not be greater than {0} or less than {1}.", MAX_DEGREE, MIN_DEGREE));

            Control = new Vector2f[degree + 1];
        }

        public Bezier2f(IList<Vector2f> control)
        {
            int degree = control.Count - 1;
            if (degree > MAX_DEGREE || degree < MIN_DEGREE)
                throw new ArgumentException(string.Format("Degree can not be greater than {0} or less than {1}.", MAX_DEGREE, MIN_DEGREE));

            int count = control.Count;
            Control = new Vector2f[count];
            for (int i = 0; i < count; i++)
                Control[i] = control[i];
        }

        /// <summary>
        /// The position on the curve at t.
        /// </summary>
        /// <param name="t">Number between 0 and 1.</param>
        public Vector2f Position(float t)
        {
            if (t < 0) t = 0;
            if (t > 1) t = 1;

            int n = Control.Length;
            int degree = Degree;
            Vector2f p = new Vector2f();

            for (int i = 0; i < n; i++)
            {
                float basis = Bernstein(degree, i, t);
                p += basis * Control[i];
            }

            return p;
        }

        /// <summary>
        /// The tangent on the curve at t.
        /// </summary>
        /// <param name="t">Number between 0 and 1.</param>
        public Vector2f Tangent(float t)
        {
            Vector2f d = FirstDerivative(t);
            return d.Normalized;
        }

        /// <summary>
        /// The normal on the curve at t.
        /// </summary>
        /// <param name="t">Number between 0 and 1.</param>
        public Vector2f Normal(float t)
        {
            Vector2f d = FirstDerivative(t);
            return d.Normalized.PerpendicularCW;
        }

        /// <summary>
        /// The first derivative on the curve at t.
        /// </summary>
        /// <param name="t">Number between 0 and 1.</param>
        public Vector2f FirstDerivative(float t)
        {
            if (t < 0) t = 0;
            if (t > 1) t = 1;

            int n = Control.Length;
            int degree = Degree;
            float inv = 1.0f / degree;
            Vector2f d = new Vector2f();

            for (int i = 0; i < n - 1; i++)
            {
                float basis = Bernstein(degree - 1, i, t);
                d += basis * inv * (Control[i + 1] - Control[i]);
            }

            return d * 4.0f;
        }

        /// <summary>
        /// Fills the array with positions on the curve.
        /// </summary>
        public void GetPositions(IList<Vector2f> points)
        {
            int count = points.Count;
            int n = Control.Length;
            int degree = Degree;

            float t = 0;
            float step = 1.0f / (count - 1.0f);

            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    float basis = Bernstein(degree, j, t);
                    points[i] += basis * Control[j];
                }

                t += step;
            }
        }

        /// <summary>
        /// Arc length of curve via intergration.
        /// </summary>
        public float Length(int steps, float tmax = 1.0f)
        {
            if (tmax <= 0) return 0;
            if (tmax > 1) tmax = 1;

            if(Degree == 1)
                return Vector2f.Distance(Control[0], Control[1]) * tmax;
            else
            {
                steps = Math.Max(steps, 2);
                float len = 0;
                Vector2f previous = Position(0);

                for (int i = 1; i < steps; i++)
                {
                    float t = i / (steps - 1.0f) * tmax;
                    Vector2f p = Position(t);

                    len += Vector2f.Distance(previous, p);
                    previous = p;
                }

                return len;
            }
        }

        /// <summary>
        /// Returns the position at t using DeCasteljau's algorithm.
        /// Same as Position(t) but slower. Used for Testing.
        /// </summary>
        internal Vector2f DeCasteljau(float t)
        {
            int count = Control.Length;
            Vector2f[] Q = new Vector2f[count];
            Array.Copy(Control, Q, count);

            for (int k = 1; k < count; k++)
            {
                for (int i = 0; i < count - k; i++)
                    Q[i] = (1.0f - t) * Q[i] + t * Q[i + 1];
            }

            return Q[0];
        }

        /// <summary>
        /// Splits the bezier at t and returns the two curves.
        /// </summary>
        /// <param name="t">Position to split (0 to 1).</param>
        /// <param name="b0">The curve from 0 to t.</param>
        /// <param name="b1">The curve from t to 1.</param>
        public void Split(float t, out Bezier2f b0, out Bezier2f b1)
        {
            int count = Control.Length;
            Vector2f[] Q = new Vector2f[count];
            Array.Copy(Control, Q, count);

            b0 = new Bezier2f(Degree);
            b1 = new Bezier2f(Degree);

            b0.Control[0] = Control[0];
            b1.Control[count - 1] = Control[count - 1];

            for (int k = 1; k < count; k++)
            {
                int len = count - k;
                for (int i = 0; i < len; i++)
                    Q[i] = (1.0f - t) * Q[i] + t * Q[i + 1];

                b0.Control[k] = Q[0];
                b1.Control[len - 1] = Q[len - 1];
            }
        }

    }
}
