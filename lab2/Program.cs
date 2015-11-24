using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    //c# code
    class Program
    {
        static void Main(string[] args)
        {
            //определение функции фи
            //из теоретических выводов
            Func<double, double> phi = x => 0.5 /
                Math.Sqrt(21) * Math.Log(22 - x * x);
            Func<double, double> function = x => Math.Exp(x)
                - 22 + x * x;
            //получение корня
            double root = getRoot(
                phi, function, 0.5e-4, 2);
            Console.WriteLine("This is root: " + root);
        }

        static double getRoot(Func<double,double> phi,
            Func<double,double> function,double epsilon, double x0)
        {
            //промежуточные хначения x
            double x_pre = x0;
            double x_next = phi(x_pre);
            Console.WriteLine("" + x_next + " " + 
                function(x_next));
            //переменная для подсчёта количества итераций
            int n = 1;
            //пока разность между соседними приближениями
            //больше заданной погрешности
            while (Math.Abs(x_next - x_pre) > epsilon)
            {
                x_pre = x_next;
                //подсчет следующего приближения по
                //методу простой итерации
                x_next = phi(x_pre);
                Console.WriteLine("" + x_next + " " + 
                    function(x_next));
                n++;
            }
            Console.WriteLine("steps: " + n);
            return x_next;
        }
    }
}
