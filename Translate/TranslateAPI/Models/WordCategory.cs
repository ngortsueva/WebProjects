using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TranslateAPI.Models
{
    public class WordCategory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameRu { get; set; }        
    }
}
