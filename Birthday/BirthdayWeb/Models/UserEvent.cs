using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BirthdayWeb.Models
{
    public class UserEvent
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreateTime { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime ModifyTime { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime BeginTime { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }
        public bool Notify { get; set; }
        public bool RepeatNotify { get; set; }
        public int  RepeatCount { get; set; }
        public string UserName { get; set; }
    }
}
