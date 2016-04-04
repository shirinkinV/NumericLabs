using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lab5.Methods
{
    public class EulerWithRecalculation
    {
        public static ValueAndArgument nextStep(double h, ValueAndArgument prev, Func<double, double, double> f)
        {
            ValueAndArgument next = new ValueAndArgument();
            next.N = prev.N + 1;
            next.x = prev.x + h;
            double multiplicator = f(prev.x, prev.y);
            double y = prev.y + h * multiplicator;
            next.y = prev.y + h * (multiplicator + f(prev.x+h, y)) / 2;
            return next;
        }
    }
}
