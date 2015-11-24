using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    class Program
    {

        static double epsilon = 0.5e-10;
 
        static void Main(string[] args)
        {
            double[][] matrix = {
                new double[]{ 1.56, -0.14, 1.2 },
                new double[]{ -0.6, 0.94, 0.12 },
                new double[]{ -0.24, -0.26, -0.5 },
            };

            double[][] matrix2 =
            {
                new double[]{ 10  , 4  , 2 ,  5 ,  2  , 4  },
              new double[]{  3  , 20  , 3  , 5  , 4  , 3    },
                new double[]{ 1  , 5  , 30   ,1  , 3 ,  1   },
                new double[]{ 4  , 5  , 2  , 20,   4 ,  3    },
            new double[]{     4 ,  5  , 3  , 4  , 20 ,  2    },
              new double[]{  2  , 1  , 4  , 3  , 5  , 25    },
            };

            double[] vector = { 4.88, 1.64, -2.26 };

            double[] vector2 = {10, 2, 3, 6, 7, 4 };

            double[] solution = getSolutionWithIteration(getMatrixForJacobiMethod(matrix2), getVectorForYacobiMethod(matrix2, vector2));
            for (int i = 0; i < solution.Length; i++)
            {
                Console.Write(solution[i] + " ");
            }
            Console.WriteLine();
            Console.WriteLine();

            Func<double[], double[]> mapping = getMappingForJacobiMethod(matrix2, vector2);

            double[] solution2 = getSolutionWithIterationUsingMapping(mapping, vector2.Length);
            for (int i = 0; i < solution2.Length; i++)
            {
                Console.Write(solution2[i] + " ");
            }
            Console.WriteLine();
            Console.Read();
        }

        static double[] getSolutionWithIterationUsingMapping(Func<double[], double[]> mapping, int count)
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

        static double[] getSolutionWithIteration(double[][] iterationMatrix, double[] iterationVector)
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

        static Func<double[], double[]> getMappingForZeidelMethod()
        {
            return null;
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
