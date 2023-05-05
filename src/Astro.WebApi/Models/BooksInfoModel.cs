namespace Astro.WebApi.Models
{
    public class BooksInfoModel
    {
        public int TotalBooksCount { get; set; }
        public int BooksWithTwoOrMoreReviewsCount { get; set; }
        public  int TotalReviewsCount { get; set; }
    }
}
