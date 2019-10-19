using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace BirthdayWeb.Models
{
    public class UserNote
    {
        public int Id { get; set; }

        [Required]
        public string Caption { get; set; }

        [Required]
        [Display(Name = "Note")]
        public string Message { get; set; }

        public string Category { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "0:{dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        public string UserName { get; set; }
    }
}
