using RadioAmateurHandbook.Actions;
using RadioAmateurHandbook.App;
using RadioAmateurHandbook.Data;
using RadioAmateurHandbook.Exceptions;
using RadioAmateurHandbook.UI;
using RadioAmateurHandbook.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioAmateurHandbook.Flow
{
    internal class MainLoop
    {
        private readonly ApplicationContext _ctx;

        public MainLoop(ApplicationContext ctx)
        {
            _ctx = ctx;
        }

        public void Run()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Render();
                int choice = ReadChoice();

                isRunning = HandleChoice(choice);

                DataManager.SaveData(_ctx.FmRadio, _ctx.AmRadio);
            }
        }

        private void Render()
        {
            var radio = _ctx.ActiveUser.GetActiveRadio();

            ConsoleUtils.ClearScreen();
            MenuRenderer.Render(_ctx.ActiveUserRole, radio.GetName());
            RadioInfoRenderer.Print(radio);
        }

        private int ReadChoice()
        {
            while (true)
            {
                int choice = InputUtils.GetNumber("Enter task code: ");
                if (choice != -1)
                    return choice;

                MessageUtils.WarningMessage("Invalid input.");
                ConsoleUtils.WaitForEnter();
            }
        }

        private bool HandleChoice(int choice)
        {
            try
            {
                return choice switch
                {
                    0 => RadioActions.TurnOff(_ctx),
                    1 => RadioActions.TurnOn(_ctx),
                    2 => RadioActions.SetVolume(_ctx),
                    3 => RadioActions.SetFrequency(_ctx),
                    4 => RadioActions.SaveFrequency(_ctx),
                    5 => RadioActions.LoadFrequency(_ctx),
                    6 => UserActions.ChangeRadio(_ctx),
                    7 => UserActions.ChangeUser(_ctx),
                    8 => RadioActions.ResetRadios(_ctx),
                    9 => UserActions.Exit(),
                    10 => ShowHelp(),
                    _ => InvalidChoice()
                };
            }
            catch (UserInputException ex)
            {
                MessageUtils.PanicMessage(ex.Message);
                ConsoleUtils.WaitForEnter();
                return true;
            }
        }

        private bool ShowHelp()
        {
            HelpRenderer.Render();
            ConsoleUtils.WaitForEnter();
            return true;
        }

        private bool InvalidChoice()
        {
            MessageUtils.WarningMessage("Invalid choice.");
            ConsoleUtils.WaitForEnter();
            return true;
        }
    }

}
