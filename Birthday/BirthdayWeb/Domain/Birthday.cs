using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BirthdayWeb.Models;

namespace BirthdayWeb.Domain
{
    public class Birthday : DbContext
    {
        public Birthday(DbContextOptions<Birthday> options) : base(options) { }
        public DbSet<Person> Persons { get; set; }
        public DbSet<LogMessage> Logs { get; set; }
        public DbSet<RequestMessage> Requests { get; set; }
        public DbSet<UserNote> Notes { get; set; }
        public DbSet<UserTask> Tasks { get; set; }
        public DbSet<UserEvent> Events { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Region> Regions { get; set; }
        //public DbSet<City> Cities { get; set; }
        //public DbSet<Street> Streets { get; set; }
        //public DbSet<Address> Addresses { get; set; }
        //public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LogMessage>()
                .HasDiscriminator<string>("message_type")
                .HasValue<LoginSuccess>("login_success")
                .HasValue<LoginFailed>("login_failed")
                .HasValue<Logout>("logout")
                .HasValue<AdminSuccess>("admin_success")
                .HasValue<AdminFailed>("admin_failed")                   
                .HasValue<RoleAdminSuccess>("role_admin_success")
                .HasValue<RoleAdminFailed>("role_admin_failed")
                .HasValue<RequestCreateSuccess>("request_create")
                .HasValue<RequestApproveSuccess>("request_approve")
                .HasValue<RequestApproveFailed>("request_failed");
        }

        public void ClearLogs()
        {
            this.Database.ExecuteSqlCommand("DELETE FROM Logs");
        }

        public void ClearRequests()
        {
            this.Database.ExecuteSqlCommand("DELETE FROM Requests");
        }
    }
}
