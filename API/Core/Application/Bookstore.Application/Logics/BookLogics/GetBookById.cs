using AutoMapper;
using Bookstore.Application.Exceptions;
using Bookstore.Application.Logics.BookLogics.Models;
using Bookstore.Domain.Entities;
using Bookstore.Infrastructure.Interfaces;
using MediatR;

namespace Bookstore.Application.Logics.BookLogics
{
    public class GetBookById : IRequest<BookModel>
    {
        public int BookId { get; set; }
    }

    public class GetBookByIdHandler : IRequestHandler<GetBookById, BookModel>
    {
        private readonly IBaseRepository<Book> _repository;
        private readonly IMapper _mapper;

        public GetBookByIdHandler(IBaseRepository<Book> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BookModel> Handle(GetBookById request, CancellationToken cancellationToken)
        {
            var book = await _repository.GetByIdAsync(request.BookId);
            if (book == null) { throw new BookNotFoundException(request.BookId); }
            var bookModel = _mapper.Map<BookModel>(book);
            return bookModel;
        }
    }
}
