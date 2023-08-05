using Bookstore.Application.Exceptions;
using Bookstore.Domain.Entities;
using Bookstore.Infrastructure.Interfaces;
using MediatR;

namespace Bookstore.Application.Logics.BookLogics
{
    public class DeleteBook : IRequest<Unit>
    {
        public int BookId { get; set; }
    }

    public class DeleteBookHandler : IRequestHandler<DeleteBook, Unit>
    {
        private readonly IBaseRepository<Book> _repository;

        public DeleteBookHandler(IBaseRepository<Book> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteBook request, CancellationToken cancellationToken)
        {
            var book = await _repository.GetByIdAsync(request.BookId);
            if (book == null) { throw new BookNotFoundException(request.BookId); }
            await _repository.DeleteAsync(book);

            return Unit.Value;
        }
    }
}
