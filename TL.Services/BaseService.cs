using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TL.Services
{
    public abstract class BaseService
    {
        protected readonly IMapper Mapper;
        public BaseService(IMapper mapper) => Mapper = mapper;

    }
}
