using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CGAL.Geometry.Curves
{

    /// <summary>
    /// Cubic polynomial ax^3 + b*x^2 + c*x + d = 0
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct Polynomial3d
    {

        internal double a;

        internal double b;

        internal double c;

        internal double d;

        internal Polynomial3d(double a, double b, double c, double d)
        {
            if (a == 0.0)
                throw new InvalidOperationException("First term in polynomial can not be 0.");

            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
        }

        internal double Solve(double x)
        {
            return a * x * x * x + b * x * x + c * x + d;
        }

        internal PolynomialRoots3d Solve()
        {
            return Solve(a, b, c, d);
        }

        internal static PolynomialRoots3d Solve(double a, double b, double c, double d)
        {
            if (a == 0.0)
                throw new InvalidOperationException("First term in polynomial can not be 0.");

            PolynomialRoots3d roots = new PolynomialRoots3d();

            double B = b / a;
            double C = c / a;
            double D = d / a;

            double b2 = B * B;
            double q = (b2 - 3 * C) / 9.0;
            double r = (B * (2.0 * b2 - 9.0 * C) + 27 * D) / 54.0;

            // equation x^3 + q*x + r = 0
            double r2 = r * r;
            double q3 = q * q * q;
         
            if (r2 < q3)
            {
                double t = r / Math.Sqrt(q3);
                if (t < -1.0) t = -1.0;
                if (t > 1.0) t = 1.0;
                t = Math.Acos(t);
                B /= 3.0;
                q = -2.0 * Math.Sqrt(q);
                roots.real = 3;
                roots.x0 = q * Math.Cos(t / 3.0) - B;
                roots.x1 = q * Math.Cos((t + Math.PI * 2.0) / 3.0) - B;
                roots.x2 = q * Math.Cos((t - Math.PI * 2.0) / 3.0) - B;
            }
            else
            {
                double s = -CubeRoot(Math.Abs(r) + Math.Sqrt(r2 - q3));
                if (r < 0) s = -s;
                double t = s == 0 ? 0 : t = q / s;

                B /= 3.0;
                roots.real = 1;
                roots.x0 = (s + t) - B;
            }

            return roots;
        }

        private static double CubeRoot(double x)
        {
            if (x > 0)
                return Root3(x);
            else if (x < 0)
                return -Root3(-x);
            else
                return 0.0;
        }

        private static double Root3(double x)
        {
            double s = 1.0;
            while (x < 1.0)
            {
                x *= 8.0;
                s *= 0.50;
            }
            while (x > 8.0)
            {
                x *= 0.125;
                s *= 2.0;
            }
            double r = 1.5;
            r -= 1.0 / 3.0 * (r - x / (r * r));
            r -= 1.0 / 3.0 * (r - x / (r * r));
            r -= 1.0 / 3.0 * (r - x / (r * r));
            r -= 1.0 / 3.0 * (r - x / (r * r));
            r -= 1.0 / 3.0 * (r - x / (r * r));
            r -= 1.0 / 3.0 * (r - x / (r * r));
            return r * s;
        }

    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct PolynomialRoots3d
    {
        internal int real;
        internal double x0, x1, x2;

        internal double this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return x0;
                    case 1: return x1;
                    case 2: return x2;
                    default: throw new IndexOutOfRangeException("Index out of range: " + i);
                }
            }
        }
    }
}
