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
        public string ActiveUserRole { get; set; }

        public Client Client { get; }
        public Admin Admin { get; }
        public Manager Manager { get; }
        public Director Director { get; }

        public ApplicationContext()
        {
            FmRadio = new RadioFM();
            AmRadio = new RadioAM();

            Client = new Client(FmRadio, AmRadio);
            Admin = new Admin(FmRadio, AmRadio);
            Manager = new Manager(FmRadio, AmRadio);
            Director = new Director(FmRadio, AmRadio);

            ActiveUser = Client;
            ActiveUserRole = "Client";
        }

        public void ChangeUser(int roleNum)
        {
            string currentRadioType = ActiveUser.GetActiveRadioType();

            ActiveUser = roleNum switch
            {
                0 => Client,
                1 => Admin,
                2 => Manager,
                3 => Director,
                _ => ActiveUser
            };

            ActiveUserRole = roleNum switch
            {
                0 => "Client",
                1 => "Admin",
                2 => "Manager",
                3 => "Director",
                _ => ActiveUserRole
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

        private void RestoreRadio(string radioType)
        {
            if (radioType == "AM")
                ActiveUser.SelectAM();
            else
                ActiveUser.SelectFM();
        }
    }
}
