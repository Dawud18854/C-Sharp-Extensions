using System;

namespace Dawud.Extensions.GeneralExtensions.Extensions
{
    public static class NumericExtensions
    {
        public static bool IsEven(this int number)
        {
            return number % 2 == 0;
        }

        public static bool IsOdd(this int number)
        {
            return number % 2 != 0;
        }

        public static int Factorial(this int number)
        {
            if (number < 0)
                throw new ArgumentException("Number must be non-negative.");
            if (number == 0)
                return 1;
            return number * Factorial(number - 1);
        }
    }
}