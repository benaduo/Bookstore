namespace Bookstore.Application.Exceptions
{

    public sealed class CategoryNotFoundException : NotFoundException
    {
        public CategoryNotFoundException(int categoryId) : base($"Category with id: {categoryId} not found.")
        {
        }
    }
}

