using System;
using System.Collections.Generic;
using System.Text;
using BirthdayConsole.Domain;

namespace BirthdayConsole.Commands
{
    public class CommandAdd : ICommand
    {
        private IPersonRepository repository;

        public CommandAdd(IPersonRepository repo)
        {
            repository = repo;
        }
        public void Execute()
        {
            var person = new Person();

            Console.Write(">First Name: ");
            person.FirstName = Console.ReadLine();

            Console.Write(">Last Name: ");
            person.LastName = Console.ReadLine();

            Console.Write(">Relation: ");
            person.Relation = Console.ReadLine();

            Console.Write(">Birthday: ");
            person.Birthday = Convert.ToDateTime(Console.ReadLine());

            repository.SavePerson(person);
        }
    }
}
