using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
