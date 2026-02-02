using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioAmateurHandbook.Utils
{
    internal static class InputUtils
    {
        public static char GetChar(string prompt)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();

            if (ValidationUtils.IsValidChar(input))
            {
                return input.Length == 1 ? input[0] : ' ';
            }
            return ' ';
        }

        public static int GetNumber(string prompt)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();

            return ValidationUtils.IsValidNumber(input) ? int.Parse(input) : -1;
        }

        public static double GetDouble(string prompt)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();

            return ValidationUtils.IsValidDouble(input, out double value)
                ? value
                : -1;
        }
    }
}
