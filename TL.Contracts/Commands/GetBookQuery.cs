using MediatR;

namespace TL.Contracts.Queries
{
    public class GetBookQuery : IRequest<GetBookQueryResponse>
    {
        public int BookId { get; set; }
    }
}
