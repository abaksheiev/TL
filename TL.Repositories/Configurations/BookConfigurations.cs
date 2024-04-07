using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TL.Repositories.Models;

namespace TL.Repositories.Configurations
{
    internal class BookConfigurations : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("books");

            builder.HasKey(c => c.Id);

            builder
                .Property(c => c.Title)
                .IsRequired();

            builder
                .Property(c => c.Author)
                .IsRequired();

            builder
                .Property(c => c.PublishedOn)
                .IsRequired();

        }
    }
}
