using Astro.Domain.Entities;

namespace Astro.WebApi.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; } // Entities не могут быть внутри модели
        public string Genre { get; set; }
        public double Price { get; set; }
    }
}
