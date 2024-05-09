using System;

namespace Win7Calc
{
    internal interface ICalculate
    {
        double Calculate(double num1, double num2);
    }

    internal class Plus : ICalculate
    {
        public double Calculate(double num1, double num2)
        {
            return num1 + num2;
        }
    }
    internal class Minus : ICalculate
    {
        public double Calculate(double num1, double num2)
        {
            return num1 - num2;
        }
    }
    internal class Degree : ICalculate
    {
        public double Calculate(double num1, double num2)
        {
            if (num2 == 0)
                throw new DivideByZeroException();
            return num1 / num2;
        }
    }
    internal class Multiply : ICalculate
    {
        public double Calculate(double num1, double num2)
        {
            return num1 * num2;
        }
    }
}