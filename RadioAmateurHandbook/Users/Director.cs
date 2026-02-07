using RadioAmateurHandbook.Radios;
using RadioAmateurHandbook.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioAmateurHandbook.Users
{
    internal class Director: User
    {
        public Director(RadioFM fm, RadioAM am) : base(fm, am, "Director") { }

        public override bool CanTurnOn() { return false; }

        public override bool CanTurnOff() { return false; }

        public override bool CanSaveFrequency() { return false; }

        public override bool CanLoadFrequency() { return false; }
    }
}
