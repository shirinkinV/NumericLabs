using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lab6
{
    public class Algebra
    {
        public static double[] solve3Diagonal(double[] a, double[] b, double[] c, double[] d)
        {
            double[] result = new double[d.Length];
            double[] lambda = new double[d.Length+1];
            double[] mu = new double[d.Length+1];
            lambda[0] = mu[0] = 0;
            for(int i = 1; i < d.Length+1; i++)
            {
                lambda[i] = -c[i - 1] / (a[i - 1] * lambda[i - 1] + b[i - 1]);
                mu[i] = (d[i - 1] - a[i - 1] * mu[i - 1]) / (a[i-1]*lambda[i-1]+b[i-1]);
            }

            result[d.Length - 1] = mu[d.Length];

            for(int i = d.Length - 2; i >= 0; i--)
            {
                result[i] = lambda[i + 1] * result[i + 1] + mu[i + 1];
            }

            return result;
        }
    }
}
