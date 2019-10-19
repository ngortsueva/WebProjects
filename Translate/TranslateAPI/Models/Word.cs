using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TranslateAPI.Models
{
    public class Word
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string WordEng { get; set; }

        [Required]
        public string WordRu { get; set; }

        public WordType WordType { get; set; }

        public WordCategory Category { get; set; }
    }
}
