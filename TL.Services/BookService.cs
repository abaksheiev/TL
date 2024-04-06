using AutoMapper;
using System.Reflection;
using TL.Contracts.Models;
using TL.Contracts.Repositories;
using TL.Contracts.Services;
using TL.Repositories.Models;

namespace TL.Services
{
    public class BookService : BaseService, IBookService
    {
        private readonly IBookRepository<Book> _bookRepository;
        public BookService(IBookRepository<Book> bookRepository, IMapper mapper) : base(mapper)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }


        public ServiceResult<int> AddBook(BookModel user)
        {
            var userDB = Mapper.Map<Book>(user);

            _bookRepository.Add(userDB);
            _bookRepository.SaveChanges();

            return ServiceResult<int>.BuildSuccess(userDB.Id);

        }

        public ServiceResult<int> DeleteBook(int itemId)
        {
            var itemDB = _bookRepository.GetById(itemId);
            if (itemDB == null)
            {
               return ServiceResult<int>.BuildError($"book with id = '{itemId}' does not exist");
            }

            _bookRepository.Remove(itemId);
            _bookRepository.SaveChanges();

            return ServiceResult<int>.BuildSuccess(itemId);
        }

        public ServiceResult<BookModel> GetBook(int itemId)
        {
            var resultDB = _bookRepository.GetById(itemId);
            var bookModel = Mapper.Map<BookModel>(resultDB);

            return ServiceResult<BookModel>
                .BuildSuccess(bookModel);
        }

        public ServiceResult<IEnumerable<BookModel>> GetBooks()
        {
            var resultDB = _bookRepository.GetAll();
            var bookModel = Mapper.Map<IEnumerable<BookModel>>(resultDB);

            return ServiceResult<IEnumerable<BookModel>>
                .BuildSuccess(bookModel);
        }

        public ServiceResult<BookModel> UpdateBook(BookModel item)
        {
            var itemDb = Mapper.Map<Book>(item);
            _bookRepository.Update(itemDb);

            var bookModel = Mapper.Map<BookModel>(itemDb);

            return ServiceResult<BookModel>
                .BuildSuccess(bookModel);
        }
    }
}
