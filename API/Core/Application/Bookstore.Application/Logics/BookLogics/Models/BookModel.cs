namespace Bookstore.Application.Logics.BookLogics.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }
}
