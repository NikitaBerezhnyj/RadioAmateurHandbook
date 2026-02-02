using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioAmateurHandbook.Exceptions
{
    internal class UserInputException : Exception
    {
        public UserInputException(string message)
            : base(message)
        {
        }
    }
}
