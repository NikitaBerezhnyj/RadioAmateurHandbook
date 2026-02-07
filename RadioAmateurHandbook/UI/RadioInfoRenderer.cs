using RadioAmateurHandbook.Radios;
using RadioAmateurHandbook.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioAmateurHandbook.UI
{
    internal class RadioInfoRenderer
    {

        public static void Print(Radio radio)
        {
            ConsoleUtils.PrintLine();
            ConsoleUtils.PrintCentered($"State of {radio.GetName()}");
            ConsoleUtils.PrintLine();

            Console.WriteLine($"Power: {(radio.IsPoweredOn() ? "On" : "Off")}");
            Console.WriteLine($"Volume: {radio.GetVolume()} Db");
            Console.WriteLine($"Frequency: {radio.GetFrequency():0.00}");

            var freqs = radio.GetInstalledFrequency();
            Console.WriteLine("Saved frequencies: " +
                string.Join("; ", freqs.Select(f => $"{f:0.00}")));

            ConsoleUtils.PrintLine();
            Console.WriteLine();
        }
    }
}
