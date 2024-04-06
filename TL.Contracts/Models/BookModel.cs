namespace TL.Contracts.Models
{
    public class BookModel: BaseModel
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public DateTime? PublishedOn { get; set; }
    }
}
