using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TranslateAPI.Models;

namespace TranslateAPI.Domain
{
    public class TranslateDb : DbContext
    {
        public TranslateDb(DbContextOptions<TranslateDb> options) : base(options) { }
        public DbSet<Word> Words { get; set; }
        public DbSet<WordType> WordTypes { get; set; }
        public DbSet<WordCategory> WordCategories { get; set; }
        public DbSet<WordSubCategory> WordSubCategories { get; set; }
        public DbSet<Language> Languages { get; set; }
    }
}
