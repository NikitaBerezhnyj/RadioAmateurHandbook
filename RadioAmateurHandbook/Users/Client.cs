using RadioAmateurHandbook.Radios;
using RadioAmateurHandbook.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioAmateurHandbook.Users
{
    internal class Client : User
    {
        public Client(RadioFM fm, RadioAM am) : base(fm, am) { }
    }
}
