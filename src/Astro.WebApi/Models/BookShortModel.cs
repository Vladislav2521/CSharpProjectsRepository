using Astro.Domain.Entities;

namespace Astro.WebApi.Models
{
    public class BookShortModel
    { 
        public string Title { get; set; }
        public Author Author { get; set; }
    }
}
