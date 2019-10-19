using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TranslateCore.Models
{
    public class Number
    {
        [Key]
        public int Id { get; set; }
        public string Word { get; set; }
        public string WordRu { get; set; }
    }
}
