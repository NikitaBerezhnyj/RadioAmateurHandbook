using RadioAmateurHandbook.Domain;
using RadioAmateurHandbook.Radios;
using RadioAmateurHandbook.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioAmateurHandbook.App
{
    internal class ApplicationContext
    {
        public RadioFM FmRadio { get; }
        public RadioAM AmRadio { get; }
        public User ActiveUser { get; set; }

        private readonly Client _client;
        private readonly Admin _admin;
        private readonly Manager _manager;
        private readonly Director _director;

        public ApplicationContext()
        {
            FmRadio = new RadioFM();
            AmRadio = new RadioAM();

            _client = new Client(FmRadio, AmRadio);
            _admin = new Admin(FmRadio, AmRadio);
            _manager = new Manager(FmRadio, AmRadio);
            _director = new Director(FmRadio, AmRadio);

            ActiveUser = _client;
        }

        public void ChangeUser(int roleNum)
        {
            RadioType currentRadioType = ActiveUser.GetActiveRadioType();
            ActiveUser = roleNum switch
            {
                0 => _client,
                1 => _admin,
                2 => _manager,
                3 => _director,
                _ => ActiveUser
            };
            RestoreRadio(currentRadioType);
        }

        public void ChangeRadio(int radioNum)
        {
            if (radioNum == 0)
                ActiveUser.SelectFM();
            else
                ActiveUser.SelectAM();
        }

        private void RestoreRadio(RadioType radioType)
        {
            if (radioType == RadioType.AM)
                ActiveUser.SelectAM();
            else
                ActiveUser.SelectFM();
        }
    }
}
