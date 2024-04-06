using AutoMapper;
using TL.Contracts.Models;
using TL.Repositories.Models;

namespace TL.WebInit
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookModel>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(d => d.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(d => d.PublishedOn, opt => opt.MapFrom(src => src.PublishedOn ));


            CreateMap<BookModel, Book>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(d => d.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(d => d.PublishedOn, opt => opt.MapFrom(src => src.PublishedOn));
        }
    }
}
