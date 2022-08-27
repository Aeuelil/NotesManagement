using System;
using NotesManagement.API.Models;
using NotesManagement.Data;

namespace NotesManagement.API.UnitTests
{
    public static class DbContextPopulate
    {
        public static void Seed(this NoteDbContext dbContext)
        {
            // Add entities for DbContext instance

            dbContext.Categories.Add(new Category
            {
                Name = "Phone Call"
            });

            dbContext.Categories.Add(new Category
            {
                Name = "Action"
            });

            dbContext.Categories.Add(new Category
            {
                Name = "Followup"
            });

            var noteCategories = new List<NoteCategories>();
            noteCategories.Add(new NoteCategories
            {
                CategoryId = 1
            });

            var note = new Note
            {
                Body = "New Note",
                NoteCategories = noteCategories
            };

            dbContext.Notes.Add(note);

            dbContext.SaveChanges();
        }
    }
}