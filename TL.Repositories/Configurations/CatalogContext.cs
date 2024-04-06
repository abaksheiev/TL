using Microsoft.EntityFrameworkCore;

namespace TL.Repositories.Configurations
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options): base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // It is needed only when app works with real database, in another case 
            // there is conflich between InMemory and SqlRerver options.
            //optionsBuilder.UseSqlServer(@"<CONNECTION STRING>");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BookConfigurations());
        }
    }
}
