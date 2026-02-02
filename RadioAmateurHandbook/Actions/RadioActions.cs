using RadioAmateurHandbook.App;
using RadioAmateurHandbook.Exceptions;
using RadioAmateurHandbook.Users;
using RadioAmateurHandbook.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioAmateurHandbook.Actions
{
    internal class RadioActions
    {
        public static bool TurnOff(ApplicationContext ctx)
        {
            if (!ctx.ActiveUser.CanTurnOff())
            {
                NoPermission(ctx);
                return true;
            }

            ctx.ActiveUser.TurnOff();
            return true;
        }

        public static bool TurnOn(ApplicationContext ctx)
        {
            if (!ctx.ActiveUser.CanTurnOn())
            {
                NoPermission(ctx);
                return true;
            }

            ctx.ActiveUser.TurnOn();
            return true;
        }

        public static bool SetVolume(ApplicationContext ctx)
        {
            if (!ctx.ActiveUser.CanSetVolume())
            {
                NoPermission(ctx);
                return true;
            }

            int volume = InputUtils.GetNumber("Enter volume (db): ");
            if (volume == -1)
                throw new UserInputException("Invalid volume value.");

            ctx.ActiveUser.SetVolume(volume);
            return true;
        }

        public static bool SetFrequency(ApplicationContext ctx)
        {
            if (!ctx.ActiveUser.CanSetFrequency())
            {
                NoPermission(ctx);
                return true;
            }

            double frequency = InputUtils.GetDouble("Enter frequency (MHz): ");
            if (frequency == -1)
                throw new UserInputException("Invalid frequency value.");

            ctx.ActiveUser.SetFrequency(frequency);
            return true;
        }

        public static bool SaveFrequency(ApplicationContext ctx)
        {
            if (!ctx.ActiveUser.CanSaveFrequency())
            {
                NoPermission(ctx);
                return true;
            }

            double frequency = InputUtils.GetDouble("Enter frequency to save: ");
            if (frequency == -1)
                throw new UserInputException("Invalid frequency value.");

            int index = InputUtils.GetNumber("Enter index [1-5]: ");
            if (index < 1 || index > 5)
                throw new UserInputException("Index must be between 1 and 5.");

            ctx.ActiveUser.SaveFrequency(index - 1, frequency);
            return true;
        }

        public static bool LoadFrequency(ApplicationContext ctx)
        {
            if (!ctx.ActiveUser.CanLoadFrequency())
            {
                NoPermission(ctx);
                return true;
            }

            int index = InputUtils.GetNumber("Enter index [1-5]: ");
            if (index < 1 || index > 5)
                throw new UserInputException("Index must be between 1 and 5.");

            ctx.ActiveUser.LoadFrequency(index);
            return true;
        }

        public static bool ResetRadios(ApplicationContext ctx)
        {
            char confirm = InputUtils.GetChar("Are you sure you want to reset radios? [y/n]: ");
            if (confirm != 'y')
                return true;

            ctx.FmRadio.Reset();
            ctx.AmRadio.Reset();

            MessageUtils.SuccessMessage("All radios have been reset.");
            return true;
        }

        private static void NoPermission(ApplicationContext ctx)
        {
            MessageUtils.WarningMessage(
                $"{ctx.ActiveUserRole} has no permission to perform this action."
            );
            ConsoleUtils.WaitForEnter();
        }
    }
}
