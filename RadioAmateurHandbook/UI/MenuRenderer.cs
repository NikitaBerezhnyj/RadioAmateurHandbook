using RadioAmateurHandbook.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioAmateurHandbook.UI
{
    internal class MenuRenderer
    {
        public static void Render(string userRole, string radioType)
        {
            ConsoleUtils.PrintLine();
            ConsoleUtils.PrintCentered("Object-oriented radio model");
            ConsoleUtils.PrintLine();
            ConsoleUtils.PrintCentered("Select one of these options");
            ConsoleUtils.PrintLine();
            ConsoleUtils.PrintCentered($"User role: {userRole}");
            ConsoleUtils.PrintCentered($"Active radio: {radioType}");
            ConsoleUtils.PrintLine();

            Console.WriteLine("[  0 ]  Turn off");
            Console.WriteLine("[  1 ]  Turn on");
            Console.WriteLine("[  2 ]  Set volume");
            Console.WriteLine("[  3 ]  Set frequency");
            Console.WriteLine("[  4 ]  Save frequency");
            Console.WriteLine("[  5 ]  Load frequency");
            Console.WriteLine("[  6 ]  Change radio");
            Console.WriteLine("[  7 ]  Change user type");
            Console.WriteLine("[  8 ]  Reset all radios");
            Console.WriteLine("[  9 ]  Exit");
            Console.WriteLine("[ 10 ]  Help");

            ConsoleUtils.PrintLine();
            Console.WriteLine();
        }
    }
}
