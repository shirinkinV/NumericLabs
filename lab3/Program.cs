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
            //hugeTest();
            Console.Read();
        }

        static void taskForTeacher()
        {
            double[][] matrix = {
                new double[]{ 1.56, -0.14, 1.2  },
                new double[]{  - 0.6, 0.94, 0.12},
                new double[]{ -0.24, -0.26, -0.5 },
            };

            double[] vector = { 1.64, -2.26, 4.88 };

            double[] solutionGauss = getSolutionWithGaussMethod(matrix, vector);

            for (int i = 0; i < solutionGauss.Length; i++)
            {
                Console.WriteLine(solutionGauss[i] + " ");
            }
            Console.WriteLine();

            double[] solutionGauss2 = getSolutionWithModyfiedGaussMethod(matrix, vector);

            for (int i = 0; i < solutionGauss2.Length; i++)
            {
                Console.WriteLine(solutionGauss2[i] + " ");
            }
            Console.WriteLine();


            Console.WriteLine();


            double[] solution = getSolutionWithIteration(getMatrixForJacobiMethod(matrix), getVectorForYacobiMethod(matrix, vector), 0.5e-4);
            for (int i = 0; i < solution.Length; i++)
            {
                Console.WriteLine(solution[i] + " ");
            }
            Console.WriteLine();
            Console.WriteLine();

            Func<double[], double[]> mapping = getMappingForJacobiMethod(matrix, vector);

            double[] solution2 = getSolutionWithIterationUsingMapping(mapping, vector.Length, 0.5e-4);
            for (int i = 0; i < solution2.Length; i++)
            {
                Console.WriteLine(solution2[i] + " ");
            }
            Console.WriteLine();
            Console.WriteLine();

            Func<double[], double[]> mappingZ = getMappingForZeidelMethod(matrix, vector);

            double[] solution3 = getSolutionWithIterationUsingMapping(mappingZ, vector.Length, 0.5e-4);
            for (int i = 0; i < solution3.Length; i++)
            {
                Console.WriteLine(solution3[i] + " ");
            }
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("proverka");
            double[] resultat = mul(matrix, solution3);
            for (int i = 0; i < resultat.Length; i++)
            {
                Console.WriteLine(resultat[i] + " ");
            }
            Console.WriteLine();
            for (int i = 0; i < vector.Length; i++)
            {
                Console.WriteLine(vector[i] + " ");
            }
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

            double[] solutionGauss = getSolutionWithGaussMethod(matrix, vector);

            for (int i = 0; i < solutionGauss.Length; i++)
            {
                Console.WriteLine(solutionGauss[i] + " ");
            }
            Console.WriteLine();
            Console.WriteLine();

            double[] solutionGauss2 = getSolutionWithModyfiedGaussMethod(matrix, vector);

            for (int i = 0; i < solutionGauss2.Length; i++)
            {
                Console.WriteLine(solutionGauss2[i] + " ");
            }
            Console.WriteLine();
            Console.WriteLine();



            double[] solution = getSolutionWithIteration(getMatrixForJacobiMethod(matrix), getVectorForYacobiMethod(matrix, vector), 1e-12);
            for (int i = 0; i < solution.Length; i++)
            {
                Console.WriteLine(solution[i] + " ");
            }
            Console.WriteLine();
            Console.WriteLine();

            Func<double[], double[]> mapping = getMappingForJacobiMethod(matrix, vector);

            double[] solution2 = getSolutionWithIterationUsingMapping(mapping, vector.Length, 1e-12);
            for (int i = 0; i < solution2.Length; i++)
            {
                Console.WriteLine(solution2[i] + " ");
            }
            Console.WriteLine();
            Console.WriteLine();

            Func<double[], double[]> mappingZ = getMappingForZeidelMethod(matrix, vector);

            double[] solution3 = getSolutionWithIterationUsingMapping(mappingZ, vector.Length, 1e-12);
            for (int i = 0; i < solution3.Length; i++)
            {
                Console.WriteLine(solution3[i] + " ");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("proverka");
            double[] resultat = mul(matrix, solution3);
            for (int i = 0; i < resultat.Length; i++)
            {
                Console.WriteLine(resultat[i] + " ");
            }
            Console.WriteLine();
            for (int i = 0; i < vector.Length; i++)
            {
                Console.WriteLine(vector[i] + " ");
            }
            Console.WriteLine();

        }

        static double[] getSolutionWithIteration(double[][] iterationMatrix, double[] iterationVector, double epsilon)
        {
            //к-тое приближение
            double[] x_k = new double[iterationVector.Length];
            //к+1-ое приближение (сразу расчёт по формуле)
            double[] x_k1 = sum(mul(iterationMatrix, x_k), iterationVector);
            //вывод к+1-ого
            for (int i = 0; i < x_k1.Length; i++)
            {
                Console.Write(x_k1[i] + " ");
            }
            Console.WriteLine();
            //счетчик итераций
            int n = 1;
            //пока разница между к-тым и к+1-вым больше эпсилон
            while (difference(x_k, x_k1) > epsilon)
            {
                n++;
                //итерационный переход
                x_k = x_k1;
                //рассчет следующего приближения
                //берется сумма вектора С и вектора, получившегося в 
                //результате умножения матрицы на вектор
                x_k1 = sum(mul(iterationMatrix, x_k), iterationVector);
                //вывод шага в консоль
                for (int i = 0; i < x_k1.Length; i++)
                {
                    Console.Write(x_k1[i] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("iterations " + n);
            Console.WriteLine();
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
            //к-тое приближение
            double[] x_k = new double[count];
            //к+1-ое приближение (сразу расчёт по формуле)
            double[] x_k1 = mapping(x_k);

            for (int i = 0; i < x_k1.Length; i++)
            {
                Console.Write(x_k1[i] + " ");
            }
            Console.WriteLine();

            int n = 1;
            //пока разница между приближениями > епсилон
            while (difference(x_k, x_k1) > epsilon)
            {
                n++;
                //итерационный переход
                x_k = x_k1;
                //применение отображения (формул метода Якоби),
                //чтобы получить следующее приближение
                x_k1 = mapping(x_k);
                for (int i = 0; i < x_k1.Length; i++)
                {
                    Console.Write(x_k1[i] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("iterations " + n);
            Console.WriteLine();
            return x_k1;
        }

        static Func<double[], double[]> getMappingForJacobiMethod(double[][] srcMatrix, double[] srcVector)
        {
            return x =>
            {
                //новый вектор, который следует вернуть
                double[] result = new double[srcVector.Length];
                //новый вектор получаем по формуле x_i^(k+1)=
                //=(b_i-a_i1*x_1^(k)-a_i2*x_2^(k)-...-a_ij*x_j^(k)...)/a_ii  i!=j
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
                    //деление выполняется один раз для каждой компоненты вектора x
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

        static double difference(double[] v1, double[] v2)
        {
            double result = 0;
            for (int i = 0; i < v1.Length; i++)
            {
                result += Math.Abs(v1[i] - v2[i]);
            }
            return result;
        }

        class Matrix
        {
            public class Row
            {
                Matrix m;
                public int i;
                public Row(Matrix m, int i)
                {
                    this.m = m;
                    this.i = i;
                }

                public double this[int j]
                {
                    get
                    {
                        if (i == 0)
                        {
                            return m.row[j];
                        }
                        if (j == 0)
                        {
                            return m.column[i - 1];
                        }
                        return m.minor[i - 1][j - 1];
                    }
                    set
                    {
                        if (i == 0)
                        {
                            m.row[j] = value;
                            return;
                        }
                        if (j == 0)
                        {
                            m.column[i - 1] = value;
                            return;
                        }
                        m.minor[i - 1][j - 1] = value;
                    }
                }
            }

            public int count;
            public Matrix minor;
            double[] row;
            double[] column;    

            private Matrix(double[][] src, double[] vector, int count)
            {
                this.count = count;
                if (count == 1)
                {
                    this.minor = null;
                    row = new double[] { src[src.Length - 1][src.Length - 1], vector[vector.Length - 1] };
                    return;
                }
                row = new double[count + 1];
                for (int i = 0; i < count; i++)
                {
                    row[i] = src[src.Length - count][src.Length - count + i];
                }
                row[count] = vector[vector.Length - count];
                column = new double[count - 1];
                for (int j = 1; j < count; j++)
                {
                    column[j - 1] = src[src.Length - count + j][src.Length - count];
                }
                minor = new Matrix(src, vector, count - 1);
            }

            public static Matrix convertFrom(double[][] src, double[] vector)
            {
                return new Matrix(src, vector, src.Length);
            }

            public Row this[int i]
            {
                get
                {
                    return new Row(this, i); 
                }
            }

            public void exchangeRows(int i,int j)
            {
                
                for (int k = 0; k < count + 1; k++)
                {
                    double cash = this[i][k];
                    this[i][k] = this[j][k];
                    this[j][k] = cash;
                }
                 
            }

            public void exchangeColumns(int i, int j)
            {
                for (int k = 0; k < count; k++)
                {
                    double cash = this[k][i];
                    this[k][i] = this[k][j];
                    this[k][j] = cash;
                }
            }


        }

        static void changeMatrixForGaussMethod(Matrix matrix)
        {
            if (matrix.count == 1)
            {
                matrix[0][1] /= matrix[0][0];
                return;
            }
            for(int j = 1; j <= matrix.count; j++)
            {
                matrix[0][j] /= matrix[0][0];
                for(int i = 1; i < matrix.count; i++)
                {
                    matrix[i][j] -= matrix[0][j] * matrix[i][0];
                }
            }
            changeMatrixForGaussMethod(matrix.minor);
        }

        static double[] getSolutionWithGaussMethod(double[][] srcMatrix, double[] srcVector)
        {
            Matrix m = Matrix.convertFrom(srcMatrix, srcVector);
            changeMatrixForGaussMethod(m);
            double[] result = new double[srcVector.Length];
            for(int i = srcVector.Length - 1; i >= 0; i--)
            {
                result[i] = m[i][m.count];
                for(int j = i + 1; j < m.count; j++)
                {
                    result[i] -= result[j] * m[i][j];
                }
            }
            return result;
        }

        static void changeMatrixForModyfiedGaussMethod(Matrix matrix, int[] columnOrder, Matrix main)
        {
            if (matrix.count == 1)
            {
                matrix[0][1] /= matrix[0][0];
                return;
            }
            double max = double.MinValue;
            int rowMax = -1;
            int columnMax = -1;

            for(int i = 0; i < matrix.count; i++)
            {
                for(int j = 0; j < matrix.count; j++)
                {
                    double check = Math.Abs(matrix[i][j]);
                    if (check > max)
                    {
                        rowMax = i;
                        columnMax = j;
                        max = check;
                    }
                }
            }
            
            int iRow= columnOrder.Length - (matrix.count - rowMax);
            int jRow = columnOrder.Length - matrix.count;   
            main.exchangeRows(iRow, jRow);

            int iColumn = columnOrder.Length - (matrix.count - columnMax);
            int jColumn = columnOrder.Length - matrix.count;
            main.exchangeColumns(iColumn, jColumn);

            int cash_index = columnOrder[iColumn];
            columnOrder[iColumn]=columnOrder[jColumn];
            columnOrder[jColumn] = cash_index;

            for (int j = 1; j <= matrix.count; j++)
            {
                matrix[0][j] /= matrix[0][0];
                for (int i = 1; i < matrix.count; i++)
                {
                    matrix[i][j] -= matrix[0][j] * matrix[i][0];
                }
            }

            changeMatrixForModyfiedGaussMethod(matrix.minor, columnOrder, main);
        }

        static double[] getSolutionWithModyfiedGaussMethod(double[][] srcMatrix, double[] srcVector)
        {
            Matrix m = Matrix.convertFrom(srcMatrix, srcVector);
            int[] columnOrder = new int[srcVector.Length];
            for(int i = 0; i < columnOrder.Length; i++)
            {
                columnOrder[i] = i;
            }
            changeMatrixForModyfiedGaussMethod(m, columnOrder,m);
            double[] result = new double[srcVector.Length];
            for (int i = srcVector.Length - 1; i >= 0; i--)
            {
                result[columnOrder[i]] = m[i][m.count];
                for (int j = i + 1; j < m.count; j++)
                {
                    result[columnOrder[i]] -= result[columnOrder[j]] * m[i][j];
                }
            }
            
            return result;
        }
    }
}
