using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioAmateurHandbook.Utils
{
    internal static class ConsoleUtils
    {
        private const int _width = 62;

        public static void WaitForEnter()
        {
            Console.Write("Press Enter to continue...");
            Console.ReadLine();
        }

        public static void PrintLine()
        {
            Console.WriteLine(new string('-', _width));
        }

        public static void PrintCentered(string text)
        {
            int contentLength = text.Length + 2;
            int paddingLeft = (_width - contentLength) / 2;
            int paddingRight = _width - contentLength - paddingLeft;

            Console.WriteLine(
                $"{new string('-', paddingLeft)} {text} {new string('-', paddingRight)}"
            );
        }

        public static void ClearScreen()
        {
            Console.Clear();
        }
    }
}
