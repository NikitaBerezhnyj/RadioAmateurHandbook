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
            int padding = (_width - text.Length) / 2;
            Console.WriteLine($"{new string('-', padding)}{text}{new string('-', _width - text.Length - padding)}");
        }

        public static void ClearScreen()
        {
            Console.Clear();
        }
    }
}
