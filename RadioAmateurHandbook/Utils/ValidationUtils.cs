using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioAmateurHandbook.Utils
{
    internal static class ValidationUtils
    {
        public static bool IsValidChar(string input)
        {
            return !string.IsNullOrEmpty(input) && (input == "y" || input == "n");
        }

        public static bool IsValidNumber(string input)
        {
            return !string.IsNullOrEmpty(input) && input.All(char.IsDigit);
        }

        public static bool IsValidDouble(string input, out double value)
        {
            value = 0;

            if (string.IsNullOrWhiteSpace(input))
                return false;

            input = input.Trim().Replace(',', '.');

            return double.TryParse(
                input,
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out value
            );
        }
    }
}
