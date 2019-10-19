using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BirthdayConsole.Domain
{
    public class PersonRepository : IPersonRepository
    {
        private Birthday db = new Birthday();
        public IQueryable<Person> Persons { get { return db.Persons; } }

        public PersonRepository() { }

        public void SavePerson(Person person)
        {
            if(person.Id == 0)
            {
                db.Persons.Add(person);
            }
            else
            {
                Person find_p = db.Persons.FirstOrDefault(p => p.Id == person.Id);
                find_p.FirstName = person.FirstName;
                find_p.LastName = person.LastName;
                find_p.Relation = person.Relation;
                find_p.Birthday = person.Birthday;
            }
            db.SaveChanges();
        }
        public void DeletePerson(Person person)
        {
            db.Persons.Remove(person);
            db.SaveChanges();
        }

        public void DeletePerson(int id)
        {
            Person person = db.Persons.FirstOrDefault(p => p.Id == id);
            db.Persons.Remove(person);
            db.SaveChanges();
        }
    }
}
