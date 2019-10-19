using System;
using System.Collections.Generic;
using System.Text;
using BirthdayConsole.Domain;

namespace BirthdayConsole.Commands
{
    public class CommandList : ICommand
    {
        private IPersonRepository repository;

        public CommandList(IPersonRepository repo)
        {
            repository = repo;
        }
        public void Execute()
        {
            foreach (var p in repository.Persons)
                Console.WriteLine(p);
        }
    }
}
