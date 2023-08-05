using AutoMapper;
using Bookstore.Application.Logics.CategoryLogics.Models;
using Bookstore.Domain.Entities;
using Bookstore.Infrastructure.Interfaces;
using MediatR;

namespace Bookstore.Application.Logics.CategoryLogics
{
    public class CreateCategory : IRequest<CategoryModel>
    {
        public CategoryModel? Model { get; set; }
    }

    public class CreateCategoryHandler : IRequestHandler<CreateCategory, CategoryModel>
    {
        private readonly IBaseRepository<Category> _repository;
        private readonly IMapper _mapper;

        public CreateCategoryHandler(IBaseRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<CategoryModel> Handle(CreateCategory request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request.Model);
            await _repository.CreateAsync(category);
            return request.Model!;
        }
    }
}
