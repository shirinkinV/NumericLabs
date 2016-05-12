using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lab5.Methods
{
    public class A3
    {
        public static ValueAndArgument nextStep(double h, List<ValueAndArgument> preSteps, Func<double, double, double> f)
        {
            ValueAndArgument result = new ValueAndArgument();
            int li = preSteps.Count - 1;
            int li1 = li - 1;
            int li2 = li - 2;
            result.x = preSteps[li].x + h;
            result.y = preSteps[li].y + h * (double)1 / 3 * (5.75*f(preSteps[li].x,preSteps[li].y)-4*f(preSteps[li1].x,preSteps[li1].y)+1.25*f(preSteps[li2].x,preSteps[li2].y));
            result.N = preSteps[li].N + 1;
            return result;                                                                                                                                              
        }

        public static ValueAndArgumentVector nextStep(double h, List<ValueAndArgumentVector> preSteps, Func<double, double[], double[]> f)
        {
            ValueAndArgumentVector result = new ValueAndArgumentVector();
            int li = preSteps.Count - 1;
            int li1 = li - 1;
            int li2 = li - 2;
            result.x = preSteps[li].x + h;
            for(int i = 0; i < preSteps[0].y.Length; i++)
            {
                result.y[i] = preSteps[li].y[i] + h * (double)1 / 3 * (5.75 * f(preSteps[li].x, preSteps[li].y)[i] - 4 * f(preSteps[li1].x, preSteps[li1].y)[i] + 1.25 * f(preSteps[li2].x, preSteps[li2].y)[i]);
            }

            result.N = preSteps[li].N + 1;
            return result;
        }

    }
}