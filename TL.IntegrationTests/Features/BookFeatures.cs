using Bogus;
using TL.Contracts.Models;

namespace TL.IntegrationTests.Features
{
    public class BookFeatures
    {
        public static IEnumerable<object[]> GetIncorrectBookModelData()
        {
            yield return new object[] { new BookModel { Id = 1, Title = string.Empty, Author = "Author", PublishedOn = DateTime.Now.AddDays(-1) } };
            yield return new object[] { new BookModel { Id = 1, Title = "Title", Author = string.Empty, PublishedOn = DateTime.Now.AddDays(-1) } };
            yield return new object[] { new BookModel { Id = 1, Title = "Title", Author = "Author", PublishedOn = DateTime.Now.AddDays(+1) } };
        }

        public static BookModel GetBook()
        {
            return BookFeatures.GetBooks(1).Single();
        }

        public static List<BookModel> GetBooks(int amount = 1)
        {
            return new Faker<BookModel>()
               .RuleFor(u => u.Id, f => f.Random.Number(int.MinValue, int.MaxValue))
               .RuleFor(u => u.Title, f => f.Name.JobTitle())
               .RuleFor(u => u.Author, f => f.Name.FullName())
               .RuleFor(u => u.PublishedOn, f => f.Date.Past())
               .Generate(amount);
        }
    }
}