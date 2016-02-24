using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;       

namespace lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<double, double> function = x => Math.Cos(x + Math.Pow(x, 2));
            double h = 0.1;
            double res = SimpsonRule(function, h, 2, 3);
            double error = (res - SimpsonRule(function, h * 2, 2, 3)) / 15;
            Console.WriteLine("h     = " + h);
            Console.WriteLine("value = " + res);
            Console.WriteLine("error = " + error);
            Console.WriteLine();

            while (Math.Abs(error) > 1e-14)
            {
                h /= 2;
                res = SimpsonRule(function, h, 2, 3);
                error = (res - SimpsonRule(function, h * 2, 2, 3)) / 15;
                Console.WriteLine("h     = " + h);
                Console.WriteLine("value = "+res);
                Console.WriteLine("error = "+error);
                Console.WriteLine();
            }
            Console.WriteLine("done");
            Console.Read();
        }
        

        static double SimpsonRule(Func<double,double> function, double h, double a, double b)
        {
            double value = 0; 
            double x_i = a;
            double h05 = h / 2;
            while (x_i+h  < b)
            {
                value += function(x_i) + 4 * function(x_i + h05)+function(x_i+ h);
                x_i += h;
            }                      
            value *= h / 6;
            value += (b - x_i) / 6 * (function(x_i) + function(b) + 4 * function((x_i+ b)/2));
            return value;
        }

        static double GregoryFormula(Func<double,double> function, double h, double a, double b)
        {
            double value = 0.5*(function(a)+function(b));
            double x_i = a + h;
            while (x_i + h < b)
            {
                value += function(x_i);
                x_i += h;
            }
            double hSmall = b - x_i;
            value += 1.0 / 24 * (-3 * function(a) + 4 * function(a + h) - function(a + 2 * h));
            value *= h;
            double f3 = function(x_i - h),f4= function(x_i),f5= function(b);
            value -= (h*h*(f3-2*f4+f5)+2*h*hSmall*(f5-f4)) / (12 * (h + hSmall));
            return value;
        }
    }
}
