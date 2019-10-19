using System;
using System.Collections.Generic;
using System.Text;
using BirthdayConsole.Domain;

namespace BirthdayConsole.Commands
{
    public class CommandSet : ICommand
    {
        private IPersonRepository repository;

        public CommandSet(IPersonRepository repo)
        {
            repository = repo;
        }
        public void Execute()
        {
            Console.Write(">Id: ");
            string id = Console.ReadLine();

            if (!string.IsNullOrEmpty(id))
            {
                var person = new Person();
                person.Id = int.Parse(id);

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
}
