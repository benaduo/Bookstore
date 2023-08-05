using AutoMapper;
using Bookstore.Application.Exceptions;
using Bookstore.Application.Logics.BookLogics;
using Bookstore.Application.Logics.BookLogics.Models;
using Bookstore.Domain.Entities;
using Bookstore.Infrastructure.Interfaces;
using Moq;

namespace Bookstore.Tests.LogicsTest
{
    public class BookLogicsTest
    {
        [Fact]
        public async Task Handle_ValidBookId_ReturnsBookResponse()
        {
            // Arrange
            var bookId = 1;
            var book = new Book { Id = bookId, Name = "Constitution of Ghana", Price = 10.99, Description = "Sample description" };
            var bookResponse = new BookModel { Id = bookId, Name = "Constitution of Ghana", Price = 10.99, Description = "Sample description" };

            var bookRepositoryMock = new Mock<IBaseRepository<Book>>();
            bookRepositoryMock.Setup(repo => repo.GetByIdAsync(bookId)).ReturnsAsync(book);

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Book, BookModel>().ReverseMap();
            });
            var mapper = mapperConfig.CreateMapper();

            var handler = new GetBookByIdHandler(bookRepositoryMock.Object, mapper);

            // Act
            var request = new GetBookById { BookId = bookId };
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bookResponse.Id, result.Id);
            Assert.Equal(bookResponse.Name, result.Name);
            Assert.Equal(bookResponse.Price, result.Price);
            Assert.Equal(bookResponse.Description, result.Description);

        }

        [Fact]
        public async Task Handle_InvalidBookId_ThrowsNotFoundException()
        {
            // Arrange
            var bookId = 1;
            Book nullBook = null;

            var bookRepositoryMock = new Mock<IBaseRepository<Book>>();
            bookRepositoryMock.Setup(repo => repo.GetByIdAsync(bookId))!.ReturnsAsync(nullBook);

            var mapperMock = new Mock<IMapper>();

            var handler = new GetBookByIdHandler(bookRepositoryMock.Object, mapperMock.Object);

            // Act
            var request = new GetBookById { BookId = bookId };

            // Assert
            await Assert.ThrowsAsync<BookNotFoundException>(() => handler.Handle(request, CancellationToken.None));
        }

    }
}
