using AutoMapper;
using Moq;
using Bookstore.Domain.Entities;
using Bookstore.Application.Logics.CategoryLogics.Models;
using Bookstore.Infrastructure.Interfaces;
using Bookstore.Application.Logics.CategoryLogics;
using Bookstore.Application.Exceptions;

namespace Categorystore.Tests.LogicsTest
{
    public class CategoryLogicsTest
    {
        public async Task Handle_ValidCategoryId_ReturnsCategoryResponse()
        {
            // Arrange
            var categoryId = 1;
            var category = new Category { Id = categoryId, Name = "Fiction", Description = "Fictional stories" };
            var categoryResponse = new CategoryModel { Id = categoryId, Name = "Fiction", Description = "Fictional stories" };

            var categoryRepositoryMock = new Mock<IBaseRepository<Category>>();
            categoryRepositoryMock.Setup(repo => repo.GetByIdAsync(categoryId)).ReturnsAsync(category);

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryModel>().ReverseMap();
            });
            var mapper = mapperConfig.CreateMapper();

            var handler = new GetCategoryByIdHandler(categoryRepositoryMock.Object, mapper);

            // Act
            var request = new GetCategoryById { CategoryId = categoryId };
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(categoryResponse.Id, result.Id);
            Assert.Equal(categoryResponse.Name, result.Name);
            Assert.Equal(categoryResponse.Description, result.Description);

        }

        [Fact]
        public async Task Handle_InvalidCategoryId_ThrowsNotFoundException()
        {
            // Arrange
            var categoryId = 1;
            Category nullCategory = null;

            var categoryRepositoryMock = new Mock<IBaseRepository<Category>>();
            categoryRepositoryMock.Setup(repo => repo.GetByIdAsync(categoryId))!.ReturnsAsync(nullCategory);

            var mapperMock = new Mock<IMapper>();

            var handler = new GetCategoryByIdHandler(categoryRepositoryMock.Object, mapperMock.Object);

            // Act
            var request = new GetCategoryById { CategoryId = categoryId };

            // Assert
            await Assert.ThrowsAsync<CategoryNotFoundException>(() => handler.Handle(request, CancellationToken.None));
        }
    }
}
