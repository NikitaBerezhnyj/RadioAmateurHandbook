using RadioAmateurHandbook.Actions;
using RadioAmateurHandbook.App;
using RadioAmateurHandbook.Data;
using RadioAmateurHandbook.Exceptions;
using RadioAmateurHandbook.UI;
using RadioAmateurHandbook.Utils;

namespace RadioAmateurHandbook.Flow
{
    internal class MainLoop
    {
        private readonly ApplicationContext _ctx;
        private readonly RadioPersistenceService _persistence;

        public MainLoop(ApplicationContext ctx, RadioPersistenceService persistence)
        {
            _ctx = ctx;
            _persistence = persistence;
        }

        public void Run()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Render();
                int choice = ReadChoice();

                isRunning = HandleChoice(choice);

                _persistence.TrySave(_ctx.FmRadio, _ctx.AmRadio);
            }
        }

        private void Render()
        {
            var radio = _ctx.ActiveRadio;

            ConsoleUtils.ClearScreen();
            MenuRenderer.Render(_ctx.ActiveUser.Role, radio.Name);
            RadioInfoRenderer.Print(radio);
        }

        private int ReadChoice()
        {
            while (true)
            {
                int choice = InputUtils.GetNumber("Enter task code: ");

                if (choice != -1)
                {
                    return choice;
                }

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
