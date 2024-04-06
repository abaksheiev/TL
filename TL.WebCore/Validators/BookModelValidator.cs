using TL.Contracts.Models;

namespace TL.WebCore.Validators
{
    public class BookModelValidator : IValidator<BookModel>
    {
        private const int MinimumLengthAuthor = 5;

        public bool Validate(BookModel entity, out List<string> errors)
        {
            errors = [];

            if (string.IsNullOrWhiteSpace(entity.Title))
            {
                errors.Add("Validation failed: Title is required.");
               
            }

            if (string.IsNullOrWhiteSpace(entity.Author))
            {
                errors.Add("Validation failed: Author is required.");
                
            }

            if (!entity.Author.Split(' ').Where(x => x.Length >= MinimumLengthAuthor).Any())
            {
                errors.Add("Validation failed: Author should be at least 5 chars.");

            }

            if (entity.PublishedOn == null)
            {
                errors.Add("Validation failed: PublishedOn is required.");
            }


            if (entity.PublishedOn > DateTime.UtcNow)
            {
                errors.Add("Validation failed: PublishedOn must be in the past.");
            }

            return errors.Any(); // All validation passed
        }
    }
}
