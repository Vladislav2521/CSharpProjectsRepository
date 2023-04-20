using Astro.Domain.Entities;

namespace Astro.WebApi.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Author Author { get; set; }
        public string Genre { get; set; }
        public double Price { get; set; }
    }
}
