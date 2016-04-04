using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.Interpolation;

namespace lab5
{
    public class Interpolate
    {
        public static Func<double,double> interpolate(List< ValueAndArgument> src)
        {
            List<double> x = new List<double>();
            List<double> y = new List<double>();

            for(int i = 0; i < src.Count; i++)
            {
                x.Add(src[i].x);
                y.Add(src[i].y);
            }

            LinearSpline spline = LinearSpline.Interpolate(x, y);
            return arg => spline.Interpolate(arg);
        }
    }
}
