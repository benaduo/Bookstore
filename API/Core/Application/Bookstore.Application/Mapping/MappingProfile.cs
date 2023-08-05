using AutoMapper;
using Bookstore.Application.Logics.BookLogics.Models;
using Bookstore.Application.Logics.CategoryLogics.Models;
using Bookstore.Domain.Entities;

namespace Bookstore.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryModel, Category>().ReverseMap();
            CreateMap<BookModel, Book>().ReverseMap();
        }
    }
}
