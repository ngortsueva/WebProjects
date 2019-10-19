using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BirthdayWeb.Models;

namespace BirthdayWeb.Domain.Abstract
{
    public interface IPersonRepository
    {
        IQueryable<Person> Persons { get; }
        void SavePerson(Person person);
        void DeletePerson(Person person);
        void DeletePerson(int id);
    }
}
