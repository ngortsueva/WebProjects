using System;
using System.Collections.Generic;
using System.Text;
using BirthdayConsole.Domain;

namespace BirthdayConsole.Commands
{
    public class CommandDel : ICommand
    {
        private IPersonRepository repository;

        public CommandDel(IPersonRepository repo)
        {
            repository = repo;
        }
        public void Execute()
        {
            Console.Write(">Id: ");
            string id = Console.ReadLine();

            if (!string.IsNullOrEmpty(id))
            {
                repository.DeletePerson(int.Parse(id));
            }
        }
    }
}
