namespace Astro.WebApi.Models
{
    public class ReviewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public int BookId { get; set; } // убедиться, нужен ли здесь BookId
        public int UserId { get; set; } // убедиться, нужен ли здесь UserId
        public DateTime PublishedDateTime { get; set; }
    }
}
