using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using BirthdayConsole.Domain;

namespace BirthdayConsole.Commands
{
    public class CommandFactory : ICommandFactory
    {
        private IPersonRepository repository;
        
        public CommandFactory(IPersonRepository repo)
        {
            repository = repo;            
        }
        public ICommand GetCommand(string cmd)
        {
            switch (cmd)
            {
                case CommandType.ADD: return new CommandAdd(repository);
                case CommandType.DEL: return new CommandDel(repository);
                case CommandType.SET: return new CommandSet(repository);
                case CommandType.LIST: return new CommandList(repository);
                default: return new CommandNull();
            }
        }
    }
}
