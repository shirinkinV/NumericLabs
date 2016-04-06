using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lab5
{
    public class KoshiTask
    {

        
        /// <summary>
        /// Одношаговые методы
        /// </summary>
        /// <param name="h">шаг</param>
        /// <param name="x0">левая граница</param>
        /// <param name="xn">правая граница</param>
        /// <param name="y0">начальное условие в левой границе</param>
        /// <param name="f">производная искомой функции f(x,y)</param>
        /// <param name="method">метод интегрирования</param>
        /// <returns>сетку значений</returns>
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


        /// <summary>
        /// Многошаговые методы
        /// </summary>
        /// <param name="steps">количество шагов в методе</param>
        /// <param name="h">шаг</param>
        /// <param name="x0">левая граница</param>
        /// <param name="xn">правая граница</param>
        /// <param name="y0">начальное условие в левой границе</param>
        /// <param name="f">производная искомой функции f(x,y)</param>
        /// <param name="method">метод интегрирования</param>
        /// <param name="accel">одношаговый метод разгона</param>
        /// <returns>сетку значений</returns>
        public static List<ValueAndArgument> integrateDifferencialEquation(int steps, double h,double x0,double xn,double y0,Func<double,double,double> f,Func<double,List<ValueAndArgument>, Func<double, double, double>,ValueAndArgument> method, Func<double, ValueAndArgument, Func<double, double, double>, ValueAndArgument> accel)
        {
            List<ValueAndArgument> result = new List<ValueAndArgument>();
            result.Add(new ValueAndArgument() { N = 1, x = x0, y = y0 });
            for (int i = 1; i < steps; i++)
            {
                result.Add(accel(h, result.Last(), f));
            }
            //разогнались

            while (result.Last().x <= xn)
            {
                result.Add(method(h, result, f));
            }

            return result; 
        }
    }
}
