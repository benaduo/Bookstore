using Bookstore.Application.Exceptions;
using Bookstore.Domain.Entities;
using Bookstore.Infrastructure.Interfaces;
using MediatR;

namespace Bookstore.Application.Logics.CategoryLogics
{
    public class DeleteCategory : IRequest<Unit>
    {
        public int CategoryId { get; set; }
    }

    public class DeleteCategoryHandler : IRequestHandler<DeleteCategory, Unit>
    {
        private readonly IBaseRepository<Category> _repository;

        public DeleteCategoryHandler(IBaseRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteCategory request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(request.CategoryId);
            if (category == null) { throw new CategoryNotFoundException(request.CategoryId); }
            await _repository.DeleteAsync(category);

            return Unit.Value;
        }
    }
}
