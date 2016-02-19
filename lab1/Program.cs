using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            double sum0 = computeSum(n => 0.4 * (n + 1) / (n * n * n - 0.4), 8000000);
            double sum3 = 0.4* computeSum(
                n => ((n + 1) * n * n * n - (n + 2) * (n * n * n - 0.4)) / ((n * n * n - 0.4) * n * n * n)
                , 2900);
            double sum1 = 0.4 * 4.0490478732 + sum3;                                                      
            Console.WriteLine(""+sum0+" "+sum1);
            Console.Read();
        }

        static double computeSum(Func<long,double> element, long count)
        {
            double sum = 0;  
            long n = 1;
            while (n<count)
            { 
                sum += element(n);  
                n++;
            }                                    
            return sum;
        }
    }
}
