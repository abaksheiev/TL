using AutoMapper;
using MediatR;
using TL.Contracts.Models;
using TL.Contracts;
using TL.Contracts.Queries;
using TL.Contracts.Services;
using TL.Contracts.Repositories;
using TL.Repositories.Models;

namespace TL.Services.Handlers
{
    public class GetBookQueryHandler : IRequestHandler<GetBookQuery, GetBookQueryResponse>
    {
        private readonly IBookRepository<Book> _bookRepository;
        private readonly IMapper _mapper;

        public GetBookQueryHandler(IBookRepository<Book> bookService, IMapper mapper)
        {
            _bookRepository = bookService ?? throw new ArgumentNullException(nameof(bookService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<GetBookQueryResponse> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {

            var itemDB = _bookRepository.GetById(request.BookId);
            if (itemDB == null)
            {
                return new GetBookQueryResponse();
            }

            var bookModel = _mapper.Map<GetBookQueryResponse>(itemDB);

            return bookModel;


        }
    }
}
