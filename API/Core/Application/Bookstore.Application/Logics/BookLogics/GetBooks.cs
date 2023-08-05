using AutoMapper;
using Bookstore.Application.Logics.BookLogics.Models;
using Bookstore.Domain.Entities;
using Bookstore.Infrastructure.Interfaces;
using MediatR;

namespace Bookstore.Application.Logics.BookLogics
{
    public class GetBooks : IRequest<List<BookModel>> { }

    public class GetBooksHandler : IRequestHandler<GetBooks, List<BookModel>>
    {
        private readonly IBaseRepository<Book> _repository;
        private readonly IMapper _mapper;
        public GetBooksHandler(IBaseRepository<Book> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<BookModel>> Handle(GetBooks request, CancellationToken cancellationToken)
        {
            var books = await _repository.GetAllAsync();
            var booksModel = _mapper.Map<List<BookModel>>(books);
            return booksModel;

        }
    }
}
