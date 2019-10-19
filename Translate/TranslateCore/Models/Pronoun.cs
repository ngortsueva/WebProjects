using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TranslateCore.Models
{
    public class Pronoun
    {
        [Key]
        public int Id { get; set; }
        public string PronounEng { get; set; }
        public string PronounRu { get; set; }
    }
}
