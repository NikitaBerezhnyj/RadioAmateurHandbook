using RadioAmateurHandbook.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioAmateurHandbook.UI
{
    internal static class HelpRenderer
    {
        public static void Render()
        {
            Console.WriteLine("Roles:");
            Console.WriteLine("0 - Client   : Full access (turn on/off, set volume, set frequency, save/load frequency)");
            Console.WriteLine("1 - Admin    : Can turn on/off, view status. Cannot change volume or frequencies.");
            Console.WriteLine("2 - Manager  : Can turn on/off, view status. Cannot change volume or frequencies.");
            Console.WriteLine("3 - Director : Read-only, cannot turn on/off or change anything.\n");
        }

    }
}
