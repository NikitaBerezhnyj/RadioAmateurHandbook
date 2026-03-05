using RadioAmateurHandbook.App;
using RadioAmateurHandbook.Data;
using RadioAmateurHandbook.Exceptions;
using RadioAmateurHandbook.Utils;

namespace RadioAmateurHandbook.Flow
{
    internal class StartupFlow
    {
        private readonly ApplicationContext _ctx;
        private readonly RadioPersistenceService _persistence;

        public StartupFlow(ApplicationContext ctx, RadioPersistenceService persistence)
        {
            _ctx = ctx;
            _persistence = persistence;
        }

        public void Run()
        {
            LoadData();
            SelectRadio();
            SelectUser();
        }

        private void LoadData()
        {
            _persistence.EnsureCreated();

            if (!_persistence.HasSavedData())
            {
                return;
            }

            while (true)
            {
                char choice = InputUtils.GetChar("Do you want to load radio state from saves? [y - Yes; n - No]: ");

                if (choice == 'y')
                {
                    var data = _persistence.TryLoad();

                    if (data != null)
                    {
                        _ctx.SetRadios(data.Value.fm, data.Value.am);
                    }
                    else
                    {
                        MessageUtils.WarningMessage("Failed to load data.");
                    }

                    break;
                }

                if (choice == 'n')
                {
                    break;
                }

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
                    int radioNum = InputUtils.GetNumber($"Select radio [0] {_ctx.FmRadio.Name} [1] {_ctx.AmRadio.Name}: ");

                    if (radioNum != 0 && radioNum != 1)
                    {
                        throw new UserInputException("Invalid radio selection.");
                    }

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
