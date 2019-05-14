using System;
namespace RecursiveDeterminant
{
    class Program
    {
        public static void Main()
        {
            double[,] matrix = new double[,] { { 1, 2 },
                                                { 3, 4 }};
            double result = determinant(matrix);
            Console.WriteLine(result);
        }

        public static double determinant(double[,] array)
        {
            double det = 0;
            double[,] tempArr = new double[array.GetLength(0) - 1, array.GetLength(1) - 1];

            if (array.GetLength(0) == 2)
            {
                det = array[0, 0] * array[1, 1] - array[0, 1] * array[1, 0];
            }

            else
            {

                for (int i = 0; i < 1; i++)
                {
                    for (int j = 0; j < array.GetLength(1); j++)
                    {
                        double subdet = determinant(fillNewArr(array, i, j));
                        if (j % 2 != 0) subdet *= -1;
                        det += array[i, j] * subdet;
                    }
                }
            }
            return det;
        }

        public static double[,] fillNewArr(double[,] originalArr, int row, int col)
        {
            double[,] tempArray = new double[originalArr.GetLength(0) - 1, originalArr.GetLength(1) - 1];

            for (int i = 0, newRow = 0; i < originalArr.GetLength(0); i++)
            {
                if (i == row)
                    continue;
                for (int j = 0, newCol = 0; j < originalArr.GetLength(1); j++)
                {
                    if (j == col) continue;
                    tempArray[newRow, newCol] = originalArr[i, j];

                    newCol++;
                }
                newRow++;
            }
            return tempArray;
        }
    }
}