using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using NotesManagement.API.Models;
using System.Reflection.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace NotesManagement.Data
{
    public class NoteDbContext : DbContext
    {
        public NoteDbContext(DbContextOptions<NoteDbContext> options) : base(options)
        {
        }

        public DbSet<Note> Notes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<NoteCategories> NoteCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NoteCategories>()
            .HasKey(bc => new { bc.NoteId, bc.CategoryId });

            modelBuilder.Entity<NoteCategories>()
                .HasOne(bc => bc.Note)
                .WithMany(b => b.NoteCategories)
                .HasForeignKey(bc => bc.NoteId);

            modelBuilder.Entity<NoteCategories>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.NoteCategories)
                .HasForeignKey(bc => bc.CategoryId);

            modelBuilder.Entity<Note>().ToTable("Note");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<NoteCategories>().ToTable("NoteCategories");
        }
    }
}
