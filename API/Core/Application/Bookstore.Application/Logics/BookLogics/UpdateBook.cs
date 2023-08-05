using AutoMapper;
using Bookstore.Application.Logics.BookLogics.Models;
using Bookstore.Domain.Entities;
using Bookstore.Infrastructure.Interfaces;
using MediatR;

namespace Bookstore.Application.Logics.BookLogics
{
    public class UpdateBook : IRequest<BookModel>
    {
        public BookModel Model { get; set; }
    }
    public class UpdateBookHandler : IRequestHandler<UpdateBook, BookModel>
    {
        private readonly IBaseRepository<Book> _repository;
        private readonly IMapper _mapper;

        public UpdateBookHandler(IBaseRepository<Book> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BookModel> Handle(UpdateBook request, CancellationToken cancellationToken)
        {
            var book = _mapper.Map<Book>(request.Model);
            await _repository.UpdateAsync(book);
            return request.Model!;
        }
    }
}
