using System;
using System.Collections.Generic;
using System.Text;

namespace BirthdayConsole.Commands
{
    public interface ICommandFactory
    {
        ICommand GetCommand(string cmd);
    }
}
