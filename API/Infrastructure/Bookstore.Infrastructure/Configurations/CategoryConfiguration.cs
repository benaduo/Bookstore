using Bookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore.Infrastructure.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> entity)
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Name).IsRequired();
            entity.HasData(
                new Category
                {
                    Id = 1,
                    Name = "Fiction",
                    Description = "Books on fictional characters"
                },
                new Category
                {
                    Id = 2,
                    Name = "Religion",
                    Description = "Books on religious faith"
                },
                new Category
                {
                    Id = 3,
                    Name = "Science",
                    Description = "Books on science and nature"
                }
               );
        }
    }
}
