using RadioAmateurHandbook.App;
using RadioAmateurHandbook.Data;
using RadioAmateurHandbook.Exceptions;
using RadioAmateurHandbook.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioAmateurHandbook.Flow
{
    internal class StartupFlow
    {
        private readonly ApplicationContext _ctx;

        public StartupFlow(ApplicationContext ctx)
        {
            _ctx = ctx;
        }

        public void Run()
        {
            LoadData();
            SelectRadio();
            SelectUser();
        }

        private void LoadData()
        {
            var filename = DataManager.GetFilename();

            if (!File.Exists(filename))
            {
                DataManager.CreateData();
                return;
            }

            while (true)
            {
                char choice = InputUtils.GetChar("Do you want to load radio state from saves? [y - Yes; n - No]: ");
                if (choice == 'y')
                {
                    if (!DataManager.LoadData(_ctx.FmRadio, _ctx.AmRadio))
                        MessageUtils.WarningMessage("Failed to load data.");
                    break;
                }
                if (choice == 'n') break;

                MessageUtils.WarningMessage("Invalid input.");
                ConsoleUtils.WaitForEnter();
            }
        }

        private void SelectRadio()
        {
            while (true)
            {
                try
                {
                    int radioNum = InputUtils.GetNumber(
                        $"Select radio [0] {_ctx.FmRadio.GetName()} [1] {_ctx.AmRadio.GetName()}: "
                    );

                    if (radioNum != 0 && radioNum != 1)
                        throw new UserInputException("Invalid radio selection.");

                    _ctx.ChangeRadio(radioNum);
                    return;
                }
                catch (UserInputException ex)
                {
                    MessageUtils.PanicMessage(ex.Message);
                    ConsoleUtils.WaitForEnter();
                }
            }
        }

        private void SelectUser()
        {
            while (true)
            {
                try
                {
                    int roleNum = InputUtils.GetNumber("Select user role: [0] Client [1] Admin [2] Manager [3] Director: ");

                    if (roleNum < 0 || roleNum > 3)
                        throw new UserInputException("Invalid user role.");

                    _ctx.ChangeUser(roleNum);
                    return;
                }
                catch (UserInputException ex)
                {
                    MessageUtils.PanicMessage(ex.Message);
                    ConsoleUtils.WaitForEnter();
                }
            }
        }
    }

}
