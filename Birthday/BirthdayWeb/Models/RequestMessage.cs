using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BirthdayWeb.Models
{
    public class RequestMessage
    {        
        public int Id { get; set; }
        public string ObjectId { get; set; }
        public string ObjectType { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
}
