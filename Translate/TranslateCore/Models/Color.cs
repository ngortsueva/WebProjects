using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TranslateCore.Models
{
    public class Color
    {
        [Key]
        public int Id { get; set; }
        public string ColorEng { get; set; }
        public string ColorRu { get; set; }
    }
}
