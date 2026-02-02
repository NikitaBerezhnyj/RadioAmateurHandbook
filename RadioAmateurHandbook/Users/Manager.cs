using RadioAmateurHandbook.Radios;
using RadioAmateurHandbook.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioAmateurHandbook.Users
{
    internal class Manager : User
    {
        public Manager(RadioFM fm, RadioAM am) : base(fm, am) { }

        public override bool CanTurnOn() { return false; }

        public override bool CanTurnOff() { return false; }

        public override bool CanSetVolume() { return false; }

        public override bool CanSetFrequency() { return false; }
    }
}
