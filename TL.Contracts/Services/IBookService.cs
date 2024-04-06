using TL.Contracts.Models;

namespace TL.Contracts.Services
{
    public interface IBookService
    {
        /// <summary>
        /// Add new Book item
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Result with Id of added object</returns>
        ServiceResult<int> AddBook(BookModel item);

        ServiceResult<int> DeleteBook(int item);

        ServiceResult<BookModel> UpdateBook(BookModel item);

        ServiceResult<BookModel> GetBook(int Id);

        ServiceResult<IEnumerable<BookModel>>  GetBooks();
    }
}
