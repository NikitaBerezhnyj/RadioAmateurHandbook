using RadioAmateurHandbook.Radios;
using RadioAmateurHandbook.Utils;

namespace RadioAmateurHandbook.UI
{
    internal class RadioInfoRenderer
    {

        public static void Print(Radio radio)
        {
            ConsoleUtils.PrintLine();
            ConsoleUtils.PrintCentered($"State of {radio.Name}");
            ConsoleUtils.PrintLine();

            Console.WriteLine($"Power: {(radio.IsPoweredOn ? "On" : "Off")}");
            Console.WriteLine($"Volume: {radio.Volume} Db");
            Console.WriteLine($"Frequency: {radio.Frequency:0.00}");

            var freqs = radio.InstalledFrequency;
            Console.WriteLine("Saved frequencies: " + string.Join("; ", freqs.Select(f => $"{f:0.00}")));

            ConsoleUtils.PrintLine();
            Console.WriteLine();
        }
    }
}
