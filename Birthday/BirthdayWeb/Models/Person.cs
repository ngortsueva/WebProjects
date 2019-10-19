using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BirthdayWeb.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Relation")]
        public string Relation { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "0:{dd/MM/yyyy}")]
        public DateTime Birthday { get; set; }

        public string UserName { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: {1} {2} {3} {4}", Id, FirstName, LastName, Relation, Birthday);
        }
    }
}
