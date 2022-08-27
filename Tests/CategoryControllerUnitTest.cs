
namespace NotesManagement.API.UnitTests
{
    public class CategoryControllerUnitTest
    {
        [Fact]
        public async Task TestGetCategoryAsync()
        {
            // Arrange
            var dbContext = DbContextMocker.GetNoteDbContext(nameof(TestGetCategoryAsync));
            var controller = new CategoriesController(dbContext);

            // Act
            var response = await controller.Get();
            dbContext.Dispose();

            // Assert
            Assert.NotNull(response);
            Assert.True(response.Count() > 0);
        }

        [Fact]
        public async Task TestPostCategoryAsync()
        {
            // Arrange
            var dbContext = DbContextMocker.GetNoteDbContext(nameof(TestPostCategoryAsync));
            var controller = new CategoriesController(dbContext);

            var request = new Category
            {
                Name = "To Do"
            };

            // Act
            var response = await controller.Post(request);
            dbContext.Dispose();

            // Assert
            Assert.NotNull(response);
        }
    }
}