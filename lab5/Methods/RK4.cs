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
    }
}
