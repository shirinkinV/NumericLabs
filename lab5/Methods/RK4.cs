using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lab5.Methods
{
    public class RK4
    {
        public static ValueAndArgument nextStep(double h, ValueAndArgument prev, Func<double, double, double> f)
        {
            ValueAndArgument result = new ValueAndArgument();
            result.x = prev.x + h;
            result.N = prev.N + 1;

            double k1 = f(prev.x, prev.y);
            double k2 = f(prev.x + h / 2, prev.y + h / 2 * k1);
            double k3 = f(prev.x + h / 2, prev.y + h / 2 * k2);
            double k4 = f(prev.x + h, prev.y + h * k3);
            result.y = prev.y + h / 6 * (k1 + 2 * k2 + 2 * k3 + k4);
            return result;
        }

        public static ValueAndArgumentVector nextStep(double h, ValueAndArgumentVector prev, Func<double, double[], double[]> f)
        {
            ValueAndArgumentVector result = new ValueAndArgumentVector();
            result.x = prev.x + h;
            result.N = prev.N + 1;
            double[] k1 = new double[prev.y.Length];
            double[] k2 = new double[prev.y.Length];
            double[] k3 = new double[prev.y.Length];
            double[] k4 = new double[prev.y.Length];

            for (int i = 0; i < prev.y.Length; i++)
            {
                k1[i] = f(prev.x, prev.y)[i];
            }
            for (int i = 0; i < prev.y.Length; i++)
            {
                double[] yi = new double[prev.y.Length];
                for(int j = 0; j < prev.y.Length; j++)
                {
                    yi[i] = prev.y[i] + h / 2 * k1[i];
                }
                k2[i] = f(prev.x + h / 2, yi)[i];
            }
            for (int i = 0; i < prev.y.Length; i++)
            {
                double[] yi = new double[prev.y.Length];
                for (int j = 0; j < prev.y.Length; j++)
                {
                    yi[i] = prev.y[i] + h / 2 * k2[i];
                }
                k3[i] = f(prev.x + h / 2, yi)[i];
            }
            for (int i = 0; i < prev.y.Length; i++)
            {
                double[] yi = new double[prev.y.Length];
                for (int j = 0; j < prev.y.Length; j++)
                {
                    yi[i] = prev.y[i] + h * k3[i];
                }
                k4[i] = f(prev.x + h, yi)[i];
            }
            for (int i = 0; i < prev.y.Length; i++)
            {
                result.y[i] = prev.y[i] + h / 6 * (k1[i] + 2 * k2[i] + 2 * k3[i] + k4[i]);
            }
            return result;
        }
    }
}
