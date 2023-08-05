using AutoMapper;
using Bookstore.Application.Logics.CategoryLogics.Models;
using Bookstore.Domain.Entities;
using Bookstore.Infrastructure.Interfaces;
using MediatR;

namespace Bookstore.Application.Logics.CategoryLogics
{
    public class GetCategories : IRequest<List<CategoryModel>> { }

    public class GetCategoriesHandler : IRequestHandler<GetCategories, List<CategoryModel>>
    {
        private readonly IBaseRepository<Category> _repository;
        private readonly IMapper _mapper;
        public GetCategoriesHandler(IBaseRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CategoryModel>> Handle(GetCategories request, CancellationToken cancellationToken)
        {
            var categories = await _repository.GetAllAsync();
            var categoriesModel = _mapper.Map<List<CategoryModel>>(categories);
            return categoriesModel;

        }
    }
}
