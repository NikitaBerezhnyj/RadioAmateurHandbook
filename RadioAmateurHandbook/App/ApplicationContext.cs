using RadioAmateurHandbook.Domain;
using RadioAmateurHandbook.Radios;
using RadioAmateurHandbook.Users;

namespace RadioAmateurHandbook.App
{
    internal class ApplicationContext
    {
        public RadioFM FmRadio { get; private set; }
        public RadioAM AmRadio { get; private set; }
        public Radio ActiveRadio { get; private set; }
        public User ActiveUser { get; private set; }

        private readonly User[] _users;

        public ApplicationContext()
        {
            FmRadio = new RadioFM();
            AmRadio = new RadioAM();
            ActiveRadio = FmRadio;

            _users = [new Client(), new Admin(), new Manager(), new Director()];
            ActiveUser = _users[0];
        }

        public void ChangeUser(int roleNum)
        {
            var currentType = ActiveRadio == FmRadio ? RadioType.FM : RadioType.AM;

            if (roleNum >= 0 && roleNum < _users.Length)
            {
                ActiveUser = _users[roleNum];
            }
        }

        public void ChangeRadio(int radioNum)
        {
            ActiveRadio = radioNum == 0 ? FmRadio : AmRadio;
        }

        public void SetRadios(RadioFM fm, RadioAM am)
        {
            FmRadio = fm;
            AmRadio = am;

            ActiveRadio = fm;
        }
    }
}
