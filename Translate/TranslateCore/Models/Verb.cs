using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TranslateCore.Models
{
    public class Verb
    {
        [Key]
        public int Id { get; set; }
        public string VerbEng { get; set; }
        public string VerbRu { get; set; }
    }
}
