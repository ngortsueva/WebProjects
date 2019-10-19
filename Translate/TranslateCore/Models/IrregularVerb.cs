using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TranslateCore.Models
{
    public class IrregularVerb
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Infinitive { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Past Simple")]
        public string PastSimple { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Past Participle")]
        public string PastParticiple { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Translate")]
        public string TranslateRu { get; set; }

        
    }
}
