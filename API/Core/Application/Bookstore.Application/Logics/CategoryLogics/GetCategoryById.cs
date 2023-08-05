using AutoMapper;
using Bookstore.Application.Exceptions;
using Bookstore.Application.Logics.CategoryLogics.Models;
using Bookstore.Domain.Entities;
using Bookstore.Infrastructure.Interfaces;
using MediatR;

namespace Bookstore.Application.Logics.CategoryLogics
{
    public class GetCategoryById : IRequest<CategoryModel>
    {
        public int CategoryId { get; set; }
    }

    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryById, CategoryModel>
    {
        private readonly IBaseRepository<Category> _repository;
        private readonly IMapper _mapper;

        public GetCategoryByIdHandler(IBaseRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CategoryModel> Handle(GetCategoryById request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(request.CategoryId);
            if (category == null) { throw new CategoryNotFoundException(request.CategoryId); }
            var categoryModel = _mapper.Map<CategoryModel>(category);
            return categoryModel;
        }
    }
}
