using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TranslateCore.Models;

namespace TranslateCore.Domain
{
    public class TranslateDb : DbContext
    {
        public TranslateDb(DbContextOptions<TranslateDb> options) : base(options) { }
        public DbSet<Word> Words { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<IrregularVerb> IrregularVerbs { get; set; }
        public DbSet<Verb> Verbs { get; set; }
        public DbSet<Pronoun> Pronouns { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Number> Numbers { get; set; }
        public DbSet<WordType> WordTypes { get; set; }
        public DbSet<WordCategory> WordCategories { get; set; }
        public DbSet<WordSubCategory> WordSubCategories { get; set; }
    }
}
