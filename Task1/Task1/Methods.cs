using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class Methods
    {
        public long Factorial(int number)
        {
            if (number == 0)
            {
                return 1;
            }

            long factorial = number;

            for (long i = factorial - 1; i > 0; i--)
            {
                factorial *= i;
            }

            return factorial;
        }

        public string ReverseNumber(int number)
        {
            string numberToString = number.ToString();
            char[] charArray = numberToString.ToCharArray();

            Array.Reverse(charArray);
            string reversedStr = new string(charArray);

            return reversedStr;
        }

        public int ShiftNumbersByNDigits(int num, int shift)
        {
            int numDigits = (int)Math.Log10(num) + 1;
            shift = shift % numDigits;
            int divisor = (int)Math.Pow(10, numDigits - shift);

            if (shift == 0)
            {
                return num;
            }
            return (num % divisor) * (int)Math.Pow(10, shift) + num / divisor;
        }

        public (int[,], int, int, int) FindSumBetweenMinAndMaxElements()
        {
            Random random = new Random();
            int[,] array = new int[5, 5];
            int maxElement = int.MinValue;
            int minElement = int.MaxValue;

            List<int> maxCoordinates = new();
            List<int> minCoordinates = new();

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = random.Next(-100, 100);
                    if (array[i, j] >= maxElement)
                    {
                        maxElement = array[i, j];
                        maxCoordinates.Clear();
                        maxCoordinates.Add(i);
                        maxCoordinates.Add(j);
                    }
                    if (array[i, j] <= minElement)
                    {
                        minElement = array[i, j];
                        minCoordinates.Clear();
                        minCoordinates.Add(i);
                        minCoordinates.Add(j);
                    }
                }
            }

            //взначення початкової та кінцевої координати для сумування елементів
            List<int> start = new List<int>();
            List<int> end = new List<int>();

            int sum = 0;
            if ((maxCoordinates[1] > minCoordinates[1]) && maxCoordinates[0] == minCoordinates[0])
            {
                start = minCoordinates;
                end = maxCoordinates;
            }
            else
            {
                start = maxCoordinates;
                end = minCoordinates;
            }

            bool reachedEnd = false;
            for (int i = start[0]; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (i == start[0] && j <= start[1])
                    {
                        continue;
                    }

                    if (i == end[0] && j == end[1])
                    {
                        reachedEnd = true;
                        break;
                    }
                    sum += array[i, j];
                }

                if (reachedEnd)
                {
                    break;
                }
            }

            return (array, sum, maxElement, minElement);
        }
    }
}
