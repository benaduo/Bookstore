using AutoMapper;
using Bookstore.Application.Logics.CategoryLogics.Models;
using Bookstore.Domain.Entities;
using Bookstore.Infrastructure.Interfaces;
using MediatR;

namespace Bookstore.Application.Logics.CategoryLogics
{
    public class UpdateCategory : IRequest<CategoryModel>
    {
        public CategoryModel Model { get; set; }
    }
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategory, CategoryModel>
    {
        private readonly IBaseRepository<Category> _repository;
        private readonly IMapper _mapper;

        public UpdateCategoryHandler(IBaseRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CategoryModel> Handle(UpdateCategory request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request.Model);
            await _repository.UpdateAsync(category);
            return request.Model!;
        }
    }
}
