using Astro.Database;
using Astro.Domain.Entities;
using Astro.WebApi.Params;


namespace Astro.WebApi.Services
{
    public class ReviewService
    {
        private LibraryDbContext reviewDbContext;

        public ReviewService(LibraryDbContext reviewDbContext)
        {
            this.reviewDbContext = reviewDbContext;
        }

        public bool CreateReview(CreateReviewParams reviewToCreate) { 
            Review newReview = new Review();
            newReview.BookId = reviewToCreate.BookId;
            newReview.UserId = reviewToCreate.UserId;
            newReview.Text = reviewToCreate.Text;
            newReview.Rating = reviewToCreate.Rating;
            // Добавлять ли тут PublishedDateTime?

            reviewDbContext.Set<Review>().Add(newReview);
            reviewDbContext.SaveChanges();
            return true;
        }
        public Review GetReviewInfo(int id)
        {
            var reviewToGet = reviewDbContext.Set<Review>().FirstOrDefault(review => review.Id == id);
            return reviewToGet;
        }
        public bool UpdateReview(UpdateReviewParams reviewToUpdate)
        {
            var existingReview = reviewDbContext.Set<Review>().FirstOrDefault(review => review.Id == reviewToUpdate.Id);
            if (existingReview != null)
            {
                existingReview.BookId = reviewToUpdate.BookId;
                existingReview.UserId = reviewToUpdate.UserId;
                existingReview.Text = reviewToUpdate.Text;
                existingReview.Rating = reviewToUpdate.Rating;
                // Добавлять ли тут PublishedDateTime?
                reviewDbContext.SaveChanges();
                return true;
            }
            else return false;
        }
        public bool DeleteReview(int id)
        {
            var reviewToDelete = reviewDbContext.Set<Review>().First(review => review.Id == id);
            if (reviewToDelete != null)
            {
                reviewDbContext.Set<Review>().Remove(reviewToDelete);
                reviewDbContext.SaveChanges();
                return true;
            }
            else return false;
        }

    }


}
