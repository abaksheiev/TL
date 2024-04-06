using TL.Contracts.Repositories;
using TL.Repositories.Configurations;
using TL.Repositories.Models;

namespace TL.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository<Book> 
    {
        public BookRepository(CatalogContext context) : base(context)
        {

        }
    }
}
