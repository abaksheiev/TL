using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using TL.Contracts.Models;
using TL.IntegrationTests.Features;

namespace TL.IntegrationTests
{
    public class BookApi : IClassFixture<WebApplicationFactory<TL.WebInit.Startup>>
    {
        private const string ApiPath = "/api/books";

        private readonly WebApplicationFactory<TL.WebInit.Startup> _factory;

        private readonly HttpClient _httpClient;

        public BookApi(WebApplicationFactory<TL.WebInit.Startup> factory)
        {
            _factory = factory;
            _httpClient = _factory.CreateClient();
        }

        [Fact]
        [Trait("Books", "CRUD")]
        public async Task WhenBookDoesNotExist_ShouldBeReturnNotFound()
        {
            // Act
            HttpResponseMessage response = await _httpClient.GetAsync($"{ApiPath}/0");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        [Trait("Books", "CRUD")]
        public async Task WhenBookAdded_ShouldBeReturnedById()
        {
            //Create Book
            var singleBook = BookFeatures.GetBook();
            await _httpClient.PostAsync($"{ApiPath}/", new StringContent(JsonConvert.SerializeObject(singleBook), Encoding.UTF8, "application/json"));

            // Act
            HttpResponseMessage response = await _httpClient.GetAsync($"{ApiPath}/{singleBook.Id}");

            // Assert request
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();
            var parsedModel = JsonConvert.DeserializeObject<BookModel>(content);

            // Assert model based on Json in case there are a lot of properties
            JsonConvert.SerializeObject(singleBook)
                .Should()
                .BeEquivalentTo(JsonConvert.SerializeObject(parsedModel));

            // Validate model property by property
            parsedModel.Should().NotBeNull();
            parsedModel.Id.Should().Be(singleBook.Id);
            parsedModel.Title.Should().Be(singleBook.Title);
            parsedModel.Author.Should().Be(singleBook.Author);
            parsedModel.PublishedOn.Should().Be(singleBook.PublishedOn);
        }

        [Theory]
        [Trait("Books", "CRUD")]
        [MemberData(nameof(BookFeatures.GetIncorrectBookModelData), MemberType = typeof(BookFeatures))]

        public async Task WhenAnyFieldIsEmpty_ShouldReturnBadRequest(BookModel item)
        {
            var result = await _httpClient.PostAsync($"{ApiPath}/", new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json"));

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [Trait("Books", "CRUD")]
        [InlineData("A", HttpStatusCode.BadRequest)]
        [InlineData("A B", HttpStatusCode.BadRequest)]
        [InlineData("AAAA BBBB", HttpStatusCode.BadRequest)]
        [InlineData("AAAAA", HttpStatusCode.OK)]
        [InlineData("AAAAA BBBBB", HttpStatusCode.OK)]

        public async Task WhenAuthorLessFiveCharsOrEmpty_ShouldReturnBadRequest(string fakeAuthor, HttpStatusCode expectedResult) {
            //Create Book
            var singleBook = BookFeatures.GetBook();
            singleBook.Author = fakeAuthor;

            var response = await _httpClient.PostAsync($"{ApiPath}/", new StringContent(JsonConvert.SerializeObject(singleBook), Encoding.UTF8, "application/json"));
         
            // Assert request
            response.StatusCode.Should().Be(expectedResult);
        }


        [Fact]
        [Trait("Books", "CRUD")]
        public async Task WhenGetAllBooks_ShouldBeReturnedAllBooks()
        {
            const int bookAmount = 100;
            //Create Book
            var books = BookFeatures.GetBooks(bookAmount).DistinctBy(x=> new{ x.Id });

            foreach (var item in books) {
                await _httpClient.PostAsync($"{ApiPath}/", new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json"));
            }
           
            // Act
            HttpResponseMessage response = await _httpClient.GetAsync($"{ApiPath}");

            // Assert request
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();
            var parsedModels = JsonConvert.DeserializeObject<IEnumerable<BookModel>>(content);

            // validate if all items were added and returned
            foreach (var item in books) {
                parsedModels.Should().Contain(item=>item.Id == item.Id);
            }
        }
    }
}