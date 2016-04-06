using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lab5.Methods
{
    public class Koshi
    {
        public static ValueAndArgument nextStep(double h, ValueAndArgument prev, Func<double, double, double> f)
        {
            ValueAndArgument result = new ValueAndArgument();
            result.N = prev.N + 1;
            result.x = prev.x + h;
            double h2 = h / 2;
            result.y = prev.y + h * f(prev.x + h2, prev.y + h2 * f(prev.x, prev.y));
            return result;
        }
    }
}
