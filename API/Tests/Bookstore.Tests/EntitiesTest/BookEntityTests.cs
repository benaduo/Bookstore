using Bookstore.Domain.Entities;

namespace Bookstore.Tests.EntitiesTest
{
    public class BookEntityTests
    {
        [Fact]
        public void Book_SetName_ValidName_Success()
        {
            // Arrange
            var book = new Book();

            // Act
            book.Name = "The seven pillars of wisdom";

            // Assert
            Assert.Equal("The seven pillars of wisdom", book.Name);
        }

    }
}
