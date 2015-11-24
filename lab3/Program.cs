using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    class Program
    {

        static void Main(string[] args)
        {
            taskForTeacher();
            hugeTest();
            Console.Read();
        }

        static void taskForTeacher()
        {
            double[][] matrix = {
                new double[]{ 1.56, -0.14, 1.2 },
                new double[]{ -0.6, 0.94, 0.12 },
                new double[]{ -0.24, -0.26, -0.5 },
            };

            double[] vector = { 4.88, 1.64, -2.26 };

            double[] solution = getSolutionWithIteration(getMatrixForJacobiMethod(matrix), getVectorForYacobiMethod(matrix, vector), 0.5e-4);
            for (int i = 0; i < solution.Length; i++)
            {
                Console.Write(solution[i] + " ");
            }
            Console.WriteLine();
            Console.WriteLine();

            Func<double[], double[]> mapping = getMappingForJacobiMethod(matrix, vector);

            double[] solution2 = getSolutionWithIterationUsingMapping(mapping, vector.Length, 0.5e-4);
            for (int i = 0; i < solution2.Length; i++)
            {
                Console.Write(solution2[i] + " ");
            }
            Console.WriteLine();
            Console.WriteLine();

            Func<double[], double[]> mappingZ = getMappingForZeidelMethod(matrix, vector);

            double[] solution3 = getSolutionWithIterationUsingMapping(mappingZ, vector.Length, 0.5e-4);
            for (int i = 0; i < solution3.Length; i++)
            {
                Console.Write(solution3[i] + " ");
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        static void hugeTest()
        {
            double[][] matrix =
            {
                new double[]{ 100, 4, 2, 5, 2, 4, 5, 1, 5, 7 },
                new double[]{ 3, 20, 3, 5, 4, 3, 1, 3, 2, 6 },
                new double[]{ 1, 5, 50, 1, 3, 1, 4, 1, 4, 7 },
                new double[]{ 4, 5, 2, 50, 4, 3, 6, 2, 7, 2 },
                new double[]{ 4, 5, 3, 4, 20, 2, 5, 2, 7, 2 },
                new double[]{ 2, 1, 4, 3, 5, 25, 3, 5, 2, 7 },
                new double[]{ 1, 1, 3, 3, 6, 2, 30, 1, 3, 2 },
                new double[]{ 1, 7, 3, 6, 4, 2, 1, 80, 3, 5 },
                new double[]{ 8, 2, 7, 2, 6, 3, 5, 2, 60, 4 },
                new double[]{ 1, 4, 2, 6, 1, 6, 5, 6, 2, 100 },

            };

            double[] vector = { 100000, 0.22, 30000, 6, 70, 4, 400, 8, 1, 23 };

            double[] solution = getSolutionWithIteration(getMatrixForJacobiMethod(matrix), getVectorForYacobiMethod(matrix, vector), 1e-12);
            for (int i = 0; i < solution.Length; i++)
            {
                Console.Write(solution[i] + " ");
            }
            Console.WriteLine();
            Console.WriteLine();

            Func<double[], double[]> mapping = getMappingForJacobiMethod(matrix, vector);

            double[] solution2 = getSolutionWithIterationUsingMapping(mapping, vector.Length, 1e-12);
            for (int i = 0; i < solution2.Length; i++)
            {
                Console.Write(solution2[i] + " ");
            }
            Console.WriteLine();
            Console.WriteLine();

            Func<double[], double[]> mappingZ = getMappingForZeidelMethod(matrix, vector);

            double[] solution3 = getSolutionWithIterationUsingMapping(mappingZ, vector.Length, 1e-12);
            for (int i = 0; i < solution3.Length; i++)
            {
                Console.Write(solution3[i] + " ");
            }
            Console.WriteLine();
            Console.WriteLine();

        }

        static double[] getSolutionWithIteration(double[][] iterationMatrix, double[] iterationVector, double epsilon)
        {
            double[] x_k = new double[iterationVector.Length];

            double[] x_k1 = sum(mul(iterationMatrix, x_k), iterationVector);

            for (int i = 0; i < x_k1.Length; i++)
            {
                Console.Write(x_k1[i] + " ");
            }
            Console.WriteLine();

            int n = 1;
            while (distance(x_k, x_k1) > epsilon)
            {
                n++;
                x_k = x_k1;
                x_k1 = sum(mul(iterationMatrix, x_k), iterationVector);
                for (int i = 0; i < x_k1.Length; i++)
                {
                    Console.Write(x_k1[i] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("iterations " + n);
            return x_k1;
        }

        static double[][] getMatrixForJacobiMethod(double[][] srcMatrix)
        {
            double[][] result = new double[srcMatrix.Length][];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new double[result.Length];
                for (int j = 0; j < result.Length; j++)
                {
                    if (i != j)
                    {
                        result[i][j] = -srcMatrix[i][j] / srcMatrix[i][i];
                    }

                }
            }
            return result;
        }

        static double[] getVectorForYacobiMethod(double[][] srcMatrix, double[] srcVector)
        {
            double[] result = new double[srcVector.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = srcVector[i] / srcMatrix[i][i];
            }
            return result;
        }

        static double[] getSolutionWithIterationUsingMapping(Func<double[], double[]> mapping, int count, double epsilon)
        {
            double[] x_k = new double[count];
            double[] x_k1 = mapping(x_k);

            for (int i = 0; i < x_k1.Length; i++)
            {
                Console.Write(x_k1[i] + " ");
            }
            Console.WriteLine();

            int n = 1;

            while (distance(x_k, x_k1) > epsilon)
            {
                n++;
                x_k = x_k1;
                x_k1 = mapping(x_k);
                for (int i = 0; i < x_k1.Length; i++)
                {
                    Console.Write(x_k1[i] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("iterations " + n);

            return x_k1;
        }

        static Func<double[], double[]> getMappingForJacobiMethod(double[][] srcMatrix, double[] srcVector)
        {
            return x =>
            {
                double[] result = new double[srcVector.Length];
                //новый вектор получаем по формуле x_i^(k+1)=(b_i-a_i1*x_1^(k)-a_i2*x_2^(k)-...-a_ij*x_j^(k)...)/a_ii  i!=j
                for (int i = 0; i < srcVector.Length; i++)
                {
                    for (int j = 0; j < srcVector.Length; j++)
                    {
                        if (j != i)
                        {
                            result[i] -= srcMatrix[i][j] * x[j];
                        }
                    }
                    result[i] += srcVector[i];
                    result[i] /= srcMatrix[i][i];
                }

                return result;
            };
        }

        static Func<double[], double[]> getMappingForZeidelMethod(double[][] srcMatrix, double[] srcVector)
        {
            return x =>
            {
                double[] result = new double[srcVector.Length];
                //новый вектор получаем по формуле x_i^(k+1)=(b_i-a_i1*x_1^(k+1)-a_i2*x_2^(k+1)-...-a_i(i-1)*x_(i-1)^(k+1)-a_i(i+1)*x_(i+1)^(k)-...)/a_ii 
                for (int i = 0; i < srcVector.Length; i++)
                {
                    for (int j = 0; j < srcVector.Length; j++)
                    {
                        if (j < i)
                        {
                            result[i] -= srcMatrix[i][j] * result[j];
                        }
                        //здесь различие
                        if (j > i)
                        {
                            result[i] -= srcMatrix[i][j] * x[j];
                        }
                    }
                    result[i] += srcVector[i];
                    result[i] /= srcMatrix[i][i];
                }

                return result;
            };
        }

        static double[] sum(double[] v1, double[] v2)
        {
            double[] result = new double[v1.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = v1[i] + v2[i];
            }
            return result;
        }

        static double[] mul(double[][] matrix, double[] vector)
        {
            double[] result = new double[vector.Length];
            for (int i = 0; i < vector.Length; i++)
            {
                for (int j = 0; j < vector.Length; j++)
                {
                    result[i] += vector[j] * matrix[i][j];
                }
            }
            return result;
        }

        static double distance(double[] v1, double[] v2)
        {
            double result = 0;
            for (int i = 0; i < v1.Length; i++)
            {
                result += Math.Abs(v1[i] - v2[i]);
            }
            return result;
        }
    }
}
