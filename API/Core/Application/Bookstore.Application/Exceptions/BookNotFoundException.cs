namespace Bookstore.Application.Exceptions
{

    public sealed class BookNotFoundException : NotFoundException
    {
        public BookNotFoundException(int bookId) : base($"Book with id: {bookId} not found.")
        {
        }
    }
}

