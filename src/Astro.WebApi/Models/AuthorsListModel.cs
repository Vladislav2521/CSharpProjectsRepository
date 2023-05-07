namespace Astro.WebApi.Models
{
    public class AuthorsListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BooksNumber { get; set; }
        public int AuthorTotalReviews { get; set; }
    }
}
