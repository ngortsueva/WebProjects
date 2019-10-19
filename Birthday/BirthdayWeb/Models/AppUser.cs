using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BirthdayWeb.Models
{
    public class AppUser : IdentityUser
    {
        public bool Enabled { get; set; }

        public bool Approve { get; set; }
    }
}
