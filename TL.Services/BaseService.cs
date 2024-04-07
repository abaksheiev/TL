using AutoMapper;

namespace TL.Services
{
    public abstract class BaseService
    {
        protected readonly IMapper Mapper;
        public BaseService(IMapper mapper) => Mapper = mapper;

    }
}
