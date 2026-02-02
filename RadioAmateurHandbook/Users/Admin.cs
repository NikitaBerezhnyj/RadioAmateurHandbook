using RadioAmateurHandbook.Radios;
using RadioAmateurHandbook.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioAmateurHandbook.Users
{
    internal class Admin : User
    {
        public Admin(RadioFM fm, RadioAM am) : base(fm, am) { }

        public override bool CanSetVolume() { return false; }

        public override bool CanSetFrequency() { return false; }

        public override bool CanSaveFrequency() { return false; }

        public override bool CanLoadFrequency() { return false; }
    }
}
