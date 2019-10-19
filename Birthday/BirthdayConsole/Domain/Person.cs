using System;
using System.Collections.Generic;
using System.Text;

namespace BirthdayConsole.Domain
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Relation { get; set; }
        public DateTime Birthday { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: {1} {2} {3} {4}", Id, FirstName, LastName, Relation, Birthday);
        }
    }
}
