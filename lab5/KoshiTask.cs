using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lab5
{
    public class KoshiTask
    {
        public static List<ValueAndArgument> integrateDifferencialEquation(double h, double x0,double xn,double y0,Func<double,double,double> f,Func<double,ValueAndArgument,Func<double,double,double>,ValueAndArgument> method)
        {
            List<ValueAndArgument> result = new List<ValueAndArgument>();
            result.Add(new ValueAndArgument() { N = 1, x = x0, y = y0 });        
            while (result.Last().x <= xn)
            {
                result.Add(method(h, result.Last(), f)); 
            }
            return result;
        }
    }
}
