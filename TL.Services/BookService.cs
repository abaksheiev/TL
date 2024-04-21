using AutoMapper;
using TL.Contracts;
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
        public ServiceResult<int> AddBook(BookModel item)
        {
            var book = _bookRepository.GetById(item.Id);
            if (book != null)
            {
                return ServiceResult<int>.BuildError(ErrorCodes.ItemAlreadyExists);
            }

            var itemDb = Mapper.Map<Book>(item);

            _bookRepository.Add(itemDb);
            _bookRepository.SaveChanges();

            return ServiceResult<int>.BuildSuccess(itemDb.Id);

        }

        public ServiceResult<int> DeleteBook(int itemId)
        {
            var itemDb = _bookRepository.GetById(itemId);
            if (itemDb == null)
            {
               return ServiceResult<int>.BuildError($"Item with id = '{itemId}' does not exist");
            }

            _bookRepository.Remove(itemId);
            _bookRepository.SaveChanges();

            return ServiceResult<int>.BuildSuccess(itemId);
        }

        public ServiceResult<BookModel> GetBook(int itemId)
        {
            var itemDB = _bookRepository.GetById(itemId);
            if (itemDB == null)
            {
                return ServiceResult<BookModel>.BuildError(ErrorCodes.ItemDoesNotExists);
            }

            var bookModel = Mapper.Map<BookModel>(itemDB);

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
            var itemDB = _bookRepository.GetById(item.Id);
            if (itemDB == null)
            {
                return ServiceResult<BookModel>
                    .BuildError(ErrorCodes.ItemDoesNotExists);
            }

            var itemDb = Mapper.Map<Book>(item);
            _bookRepository.Update(itemDb);
            _bookRepository.SaveChanges();

            var bookModel = Mapper.Map<BookModel>(itemDb);

            return ServiceResult<BookModel>
                .BuildSuccess(bookModel);
        }
    }
}
