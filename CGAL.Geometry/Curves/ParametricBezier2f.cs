using System;
using System.Collections.Generic;

using Common.Core.LinearAlgebra;

namespace CGAL.Geometry.Curves
{
    public class ParametricBezier2f : Bezier2f
    {

        public int MaxIterations = 8;

        public float MaxError = 1e-4f;

        public int Steps = 100;

        public ParametricBezier2f(BEZIER_DEGREE degree) 
            : base((int)degree)
        {

        }

        public ParametricBezier2f(int degree)
            : base(degree)
        {

        }

        public ParametricBezier2f(IList<Vector2f> control)
            : base(control)
        {

        }

        /// <summary>
        /// Finds the t parameter at length s on the curve.
        /// </summary>
        /// <param name="s">Value between 0 and length</param>
        /// <param name="length">Length of the curve</param>
        /// <returns>The t parameter that s represents.</returns>
        public float Parametrize(float s, float length)
        {

            float t = s / length;
            if (t <= 0) return 0;
            if (t >= 1) return 1;

            float error = Math.Max(MaxError, 1e-9f);
            float lower = 0;
            float upper = 1;

            for (int i = 0; i < MaxIterations; i++)
            {
                int n = (int)(Steps * t);
                float f = Length(n, t) - s;

                if (Math.Abs(f) < error)
                    return t;

                float df = FirstDerivative(t).Magnitude;
                float tApprox = t - f / df;

                if (f > 0)
                {
                    upper = t;
                    if (tApprox <= lower)
                        t = 0.5f * (upper + lower);
                    else
                        t = tApprox;
                }
                else
                {
                    lower = t;
                    if (tApprox >= upper)
                        t = 0.5f * (upper + lower);
                    else
                        t = tApprox;
                }
            }

            return t;
        }

    }
}
