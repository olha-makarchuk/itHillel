using System;
using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Methods methods = new Methods();

            Console.Write("Enter a number to calculate factorial:  ");
            int number = WriteNumber();
            long factorial = methods.Factorial(number);
            Console.WriteLine(factorial);

            Console.Write("Enter a number to display it in reverse order:  ");
            number = WriteNumber();
            string reversed = methods.ReverseNumber(number);
            Console.WriteLine(reversed);

            Console.Write("Enter a number for shifting by N digits:  ");
            number = WriteNumber();
            Console.Write("Enter the shift number:");
            int shift = WriteNumber();
            int numberWithShift = methods.ShiftNumbersByNDigits(number, shift);
            Console.WriteLine(numberWithShift);

            Console.WriteLine("Finding the sum of elements of the array between the largest and smallest elements: ");
            (int[,] array, int sum, int maxElement, int minElement) = methods.FindSumBetweenMinAndMaxElements();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write($"{array[i, j]}  ");
                }
                Console.WriteLine();
            }
            Console.WriteLine($"Maximum element: {maxElement}");
            Console.WriteLine($"Minimum element: {minElement}");
            Console.WriteLine($"Sum of numbers: {sum}");
        }


        static int WriteNumber()
        {
            while (true)
            {
                string inputNumber = Console.ReadLine();
                if (int.TryParse(inputNumber, out int number))
                {
                    return number;
                }
                else
                {
                    Console.WriteLine("Invalid input! Please try again.");
                }
            }
        }

    }
}
