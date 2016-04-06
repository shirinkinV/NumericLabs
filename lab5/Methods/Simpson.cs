using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lab5.Methods
{
    public class Simpson
    {
        public static ValueAndArgument nextStep(double h, List<ValueAndArgument> preSteps,Func<double,double,double> f)
        {
            ValueAndArgument result = new ValueAndArgument();
            int li = preSteps.Count - 1;
            int li1 = li - 1;
            result.x = preSteps[li].x + h;
            Func<double,double> equation= y=>-y+ preSteps[li1].y + h / 3 * (f(preSteps[li].x+h,y)+4*f(preSteps[li].x, preSteps[li].y)+f(preSteps[li1].x,preSteps[li1].y));
            Func<double, double> derivative = new MathNet.Numerics.Differentiation.NumericalDerivative(5, 2).CreateDerivativeFunctionHandle(equation, 1);
            double lowerBound = 0;
            double upperBound =0;
            if (preSteps[li].y >= preSteps[li1].y)
            {
                lowerBound = preSteps[li].y-3*(preSteps[li].y-preSteps[li1].y);
                upperBound = lowerBound + 6 * (preSteps[li].y - preSteps[li1].y);
            }
            else
            {
                upperBound = preSteps[li].y -  3*(preSteps[li].y - preSteps[li1].y);
                lowerBound = lowerBound + 6 * (preSteps[li].y - preSteps[li1].y);
            }
            result.y = MathNet.Numerics.RootFinding.NewtonRaphson.FindRoot(equation, derivative, lowerBound, upperBound,1e-12,2000);
            result.N = preSteps[li].N+1;
            return result;
        }
    }
}
