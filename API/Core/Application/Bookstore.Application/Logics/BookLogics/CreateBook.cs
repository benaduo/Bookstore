using AutoMapper;
using Bookstore.Application.Logics.BookLogics.Models;
using Bookstore.Domain.Entities;
using Bookstore.Infrastructure.Interfaces;
using MediatR;

namespace Bookstore.Application.Logics.BookLogics
{
    public class CreateBook : IRequest<BookModel>
    {
        public BookModel? Model { get; set; }
    }

    public class CreateBookHandler : IRequestHandler<CreateBook, BookModel>
    {
        private readonly IBaseRepository<Book> _repository;
        private readonly IMapper _mapper;

        public CreateBookHandler(IBaseRepository<Book> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<BookModel> Handle(CreateBook request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Book>(request.Model);
            await _repository.CreateAsync(category);
            return request.Model!;
        }
    }
}
