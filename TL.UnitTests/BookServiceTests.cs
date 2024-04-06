using AutoMapper;
using FluentAssertions;
using Moq;
using TL.Contracts.Models;
using TL.Contracts.Repositories;
using TL.Repositories;
using TL.Repositories.Models;
using TL.Services;
using TL.WebInit;

namespace TL.UnitTests
{
    public class BookServiceTests
    {
        Mock<IBookRepository<Book>> bookRepository = new Mock<IBookRepository<Book>>();
        IMapper mapper;
       public BookServiceTests() {
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            mapper = mockMapper.CreateMapper();
        }

        [Fact]
        [Trait("Books", "BookService")]
        public void WhenGetBookDoesNotExist_ThenReturnNotExistError()
        {
            bookRepository
                .Setup(_ => _.GetById(It.IsAny<int>()))
                .Returns((Book)null);

            var service = new BookService(bookRepository.Object, mapper);

            var result = service.GetBook(It.IsAny<int>());

            result.ErrorMessage.Should().NotBeNullOrWhiteSpace();
            result.Success.Should().BeFalse();
        }

        [Fact]
        [Trait("Books", "BookService")]
        public void WhenGetBookDoesExists_ThenReturnItem()
        {
            var mockItem = new Book
            {
                Id = It.IsAny<int>(),
                Author = It.IsAny<string>(),
                Title = It.IsAny<string>(),
                PublishedOn = It.IsAny<DateTime>(),
            };

            bookRepository
                .Setup(_ => _.GetById(It.IsAny<int>()))
                .Returns(mockItem);

            var service = new BookService(bookRepository.Object, mapper);

            var result = service.GetBook(It.IsAny<int>());

            result.ErrorMessage.Should().BeNullOrWhiteSpace();
            result.Success.Should().BeTrue();

            result.Data.Id.Should().Be(mockItem.Id);
            result.Data.Title.Should().Be(mockItem.Title);
            result.Data.Author.Should().Be(mockItem.Author);
            result.Data.PublishedOn.Should().Be(mockItem.PublishedOn);
        }
    }
}