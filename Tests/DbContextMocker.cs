using Microsoft.EntityFrameworkCore;
using NotesManagement.API.Models;
using NotesManagement.Data;

namespace NotesManagement.API.UnitTests
{
    public static class DbContextMocker
    {
        public static NoteDbContext GetNoteDbContext(string dbName)
        {
            // Create options for DbContext instance
            var options = new DbContextOptionsBuilder<NoteDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            // Create instance of DbContext
            var dbContext = new NoteDbContext(options);

            // Add entities in memory
            dbContext.Seed();

            return dbContext;
        }
    }
}