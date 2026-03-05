using RadioAmateurHandbook.App;
using RadioAmateurHandbook.Exceptions;
using RadioAmateurHandbook.Utils;

namespace RadioAmateurHandbook.Actions
{
    internal class RadioActions
    {
        private const int MaxSavedFrequencies = 5;

        public static bool TurnOff(ApplicationContext ctx)
        {
            if (!ctx.ActiveUser.CanTurnOff())
            {
                return NoPermission(ctx);
            }

            ctx.ActiveRadio.TurnOff();
            return true;
        }

        public static bool TurnOn(ApplicationContext ctx)
        {
            if (!ctx.ActiveUser.CanTurnOn())
            {
                return NoPermission(ctx);
            }

            ctx.ActiveRadio.TurnOn();
            return true;
        }

        public static bool SetVolume(ApplicationContext ctx)
        {
            if (!ctx.ActiveUser.CanSetVolume())
            {
                return NoPermission(ctx);
            }

            int volume = InputUtils.GetNumber("Enter volume (db): ");

            if (volume == -1)
            {
                throw new UserInputException("Invalid volume value.");
            }

            try
            {
                ctx.ActiveRadio.SetVolume(volume);
            }
            catch (Exception ex)
            {
                return InvalidArgument(ex.Message);
            }

            return true;
        }

        public static bool SetFrequency(ApplicationContext ctx)
        {
            if (!ctx.ActiveUser.CanSetFrequency())
            {
                return NoPermission(ctx);
            }

            double frequency = InputUtils.GetDouble("Enter frequency (MHz): ");

            if (frequency == -1)
            {
                throw new UserInputException("Invalid frequency value.");
            }

            try
            {
                ctx.ActiveRadio.SetFrequency(frequency);
            }
            catch (Exception ex)
            {
                return InvalidArgument(ex.Message);
            }

            return true;
        }

        public static bool SaveFrequency(ApplicationContext ctx)
        {
            if (!ctx.ActiveUser.CanSaveFrequency())
            {
                return NoPermission(ctx);
            }

            double frequency = InputUtils.GetDouble("Enter frequency to save: ");

            if (frequency == -1)
            {
                throw new UserInputException("Invalid frequency value.");
            }

            int index = InputUtils.GetNumber("Enter index [1-5]: ");

            if (index < 1 || index > MaxSavedFrequencies)
            {
                throw new UserInputException("Index must be between 1 and 5.");
            }

            try
            {
                ctx.ActiveRadio.SaveFrequency(index - 1, frequency);
            }
            catch (Exception ex)
            {
                return InvalidArgument(ex.Message);
            }

            return true;
        }

        public static bool LoadFrequency(ApplicationContext ctx)
        {
            if (!ctx.ActiveUser.CanLoadFrequency())
            {
                return NoPermission(ctx);
            }

            int index = InputUtils.GetNumber("Enter index [1-5]: ");

            if (index < 1 || index > MaxSavedFrequencies)
            {
                throw new UserInputException("Index must be between 1 and 5.");
            }

            try
            {
                ctx.ActiveRadio.LoadFrequency(index);
            }
            catch (Exception ex)
            {
                return InvalidArgument(ex.Message);
            }

            return true;
        }

        public static bool ResetRadios(ApplicationContext ctx)
        {
            char confirm = InputUtils.GetChar("Are you sure you want to reset radios? [y/n]: ");

            if (confirm != 'y')
            {
                return true;
            }

            ctx.FmRadio.Reset();
            ctx.AmRadio.Reset();
            MessageUtils.SuccessMessage("All radios have been reset.");
            return true;
        }

        private static bool NoPermission(ApplicationContext ctx)
        {
            MessageUtils.WarningMessage($"{ctx.ActiveUser.Role} has no permission to perform this action.");
            ConsoleUtils.WaitForEnter();
            return true;
        }

        private static bool InvalidArgument(string message)
        {
            MessageUtils.PanicMessage(message);
            ConsoleUtils.WaitForEnter();
            return true;
        }
    }
}
