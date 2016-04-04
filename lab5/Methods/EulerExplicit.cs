using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lab5.Methods
{
    public class EulerExplicit
    {
        public static ValueAndArgument nextStep(double h,ValueAndArgument prev, Func<double,double,double> f)
        {
            ValueAndArgument next = new ValueAndArgument();
            next.N = prev.N + 1;
            next.x = prev.x + h;
            next.y = prev.y + h * f(prev.x, prev.y);
            return next;
        }
    }
}
