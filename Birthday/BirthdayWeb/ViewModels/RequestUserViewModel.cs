using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BirthdayWeb.Models;
using System.ComponentModel.DataAnnotations;

namespace BirthdayWeb.ViewModels
{
    public class RequestUserViewModel
    {
        [Display(Name = "Message")]
        public RequestMessage requestMessage { get; set; }

        [Display(Name = "Username")]
        public string UserName;

        [Display(Name = "Email")]
        public string UserEmail;
    }
}
