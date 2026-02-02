using RadioAmateurHandbook.App;
using RadioAmateurHandbook.Exceptions;
using RadioAmateurHandbook.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioAmateurHandbook.Actions
{
    internal class UserActions
    {
        public static bool ChangeRadio(ApplicationContext ctx)
        {
            int radioNum = InputUtils.GetNumber(
                $"Select radio [0] {ctx.FmRadio.GetName()} [1] {ctx.AmRadio.GetName()}: "
            );

            if (radioNum != 0 && radioNum != 1)
                throw new UserInputException("Invalid radio selection.");

            ctx.ChangeRadio(radioNum);
            return true;
        }

        public static bool ChangeUser(ApplicationContext ctx)
        {
            int roleNum = InputUtils.GetNumber(
                "Select user role: [0] Client [1] Admin [2] Manager [3] Director: "
            );

            if (roleNum < 0 || roleNum > 3)
                throw new UserInputException("Invalid user role.");

            ctx.ChangeUser(roleNum);
            return true;
        }

        public static bool Exit()
        {
            char confirm = InputUtils.GetChar("Are you sure you want to exit? [y/n]: ");
            if (confirm != 'y')
                return true;

            MessageUtils.SuccessMessage("Thanks for work");
            return false;
        }
    }
}
