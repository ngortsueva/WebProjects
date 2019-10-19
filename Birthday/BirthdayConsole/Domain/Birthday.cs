using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace BirthdayConsole.Domain
{
    class Birthday : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Data Source=GNOME;Initial Catalog = Birthday; Persist Security Info = True;
                  User ID = sa;
                  Password = '12345';");
        }
    }
}
