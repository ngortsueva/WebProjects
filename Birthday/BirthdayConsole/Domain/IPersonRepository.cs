using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BirthdayConsole.Domain
{
    public interface IPersonRepository
    {
        IQueryable<Person> Persons { get; }
        void SavePerson(Person person);
        void DeletePerson(Person person);
        void DeletePerson(int id);
    }
}
