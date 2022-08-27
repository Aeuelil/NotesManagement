
using Microsoft.AspNetCore.Mvc;

namespace NotesManagement.API.UnitTests
{
    public class NoteControllerUnitTest
    {
        [Fact]
        public async Task TestGetNotesAsync()
        {
            // Arrange
            var dbContext = DbContextMocker.GetNoteDbContext(nameof(TestGetNotesAsync));
            var controller = new NotesController(dbContext);

            // Act
            var response = await controller.Get();
            dbContext.Dispose();

            // Assert
            Assert.NotNull(response);
            Assert.True(response.Count() > 0);
        }

        [Fact]
        public async Task TestPostNoteAsync()
        {
            // Arrange
            var dbContext = DbContextMocker.GetNoteDbContext(nameof(TestPostNoteAsync));
            var controller = new NotesController(dbContext);

            var noteCategories = new List<NoteCategories>();
            noteCategories.Add(new NoteCategories
            {
                CategoryId = 1
            });

            var request = new Note
            {
                Body = "New Note",
                NoteCategories = noteCategories
            };

            // Act
            var response = await controller.Post(request);
            dbContext.Dispose();

            // Assert
            Assert.NotNull(response);
        }

        [Fact]
        public async Task TestPutStockItemAsync()
        {
            // Arrange
            var dbContext = DbContextMocker.GetNoteDbContext(nameof(TestPutStockItemAsync));
            var controller = new NotesController(dbContext);

            var id = 1;
            var request = new Note
            {
                Body = "Updated Note"
            };

            // Act
            var response = await controller.Put(id, request);
            var okResult = response as OkResult;
            dbContext.Dispose();

            // Assert
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task TestDeleteStockItemAsync()
        {
            // Arrange
            var dbContext = DbContextMocker.GetNoteDbContext(nameof(TestDeleteStockItemAsync));
            var controller = new NotesController(dbContext);

            var id = 1;

            // Act
            var response = await controller.Delete(id);
            var okResult = response as OkResult;
            dbContext.Dispose();

            // Assert
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}