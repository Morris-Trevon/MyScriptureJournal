using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyScriptureJournal.Models;

namespace MyScriptureJournal.Data
{
    public class JournalContext : DbContext
    {
        public JournalContext (DbContextOptions<JournalContext> options)
            : base(options)
        {
        }

        public DbSet<Journal> Journals { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Reference> References { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Prophets> Prophet { get; set; }
        public DbSet<PriesthoodOffice> PriesthoodOffice { get; set; }
        public DbSet<DispensationLinks> DispensationLinks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Journal>().ToTable("Journal");
            modelBuilder.Entity<Note>().ToTable("Note");
            modelBuilder.Entity<Reference>().ToTable("Reference");
            modelBuilder.Entity<City>().ToTable("City");
            modelBuilder.Entity<Prophets>().ToTable("Prophet");
            modelBuilder.Entity<PriesthoodOffice>().ToTable("PriesthoodOffice");
            modelBuilder.Entity<DispensationLinks>().ToTable("DispensationLinks");

            modelBuilder.Entity<DispensationLinks>()
                .HasKey(c => new { c.ReferenceID, c.ProphetID });
        }
    }
}
