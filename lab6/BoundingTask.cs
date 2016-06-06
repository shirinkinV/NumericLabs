using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lab6
{

    public class BoundingTask
    {
        public static double EPS = 1e-10;
        public static Func<double, double> getSolveOfTask(Func<double, double> p, Func<double, double> q, int n, double alpha0, double alpha1, double beta0, double beta1, double begin, double end)
        {
            double[] yList = new double[n + 1];
            double[] xList = new double[n + 1];
            double h = (end - begin) / n;

            for (int i = 0; i < n + 1; i++)
            {
                xList[i] = begin + i * h;
            }

            double[] a = new double[n + 1];
            double[] b = new double[n + 1];
            double[] c = new double[n + 1];
            double[] d = new double[n + 1];

            for (int i = 0; i < n + 1; i++)
            {
                a[i] = 1;
                b[i] = -(2 + p(xList[i]) * h * h);
                c[i] = 1;
                d[i] = h * h * q(xList[i]);
            }

            a[0] = 1 + alpha0 * h;
            b[0] = -1;
            c[0] = 0;
            d[0] = -alpha1 * h;
            a[n] = -1;
            b[n] = 1 + beta0 * h;
            c[n] = 0;
            d[n] = beta1 * h;

            yList = Algebra.solve3Diagonal(a, b, c, d);

            MathNet.Numerics.Interpolation.CubicSpline spline = MathNet.Numerics.Interpolation.CubicSpline.InterpolateAkima(xList, yList);
            return x => spline.Interpolate(x);
        }

        public static double metric(double[] y1, double[] y2)
        {
            double squaredSum = 0;
            for (int i = 0; i < y1.Length; i++)
            {
                squaredSum += Math.Pow(y1[i] - y2[i], 2);
            }
            return Math.Sqrt(squaredSum);
        }
    }
}
