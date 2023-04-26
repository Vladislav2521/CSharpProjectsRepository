using Astro.Database;
using Astro.Domain.Entities;
using Astro.WebApi.Models;
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

        // метод CreateReview ничего не отдаёт, поэтому Model тут не нужна
        public bool CreateReview(CreateReviewParams reviewToCreate) { 
            Review newReview = new Review();

            newReview.Text = reviewToCreate.Text;
            newReview.Rating = reviewToCreate.Rating;
            newReview.BookId = reviewToCreate.BookId;
            newReview.UserId = reviewToCreate.UserId;
            // Добавлять ли тут PublishedDateTime?

            reviewDbContext.Set<Review>().Add(newReview);
            reviewDbContext.SaveChanges();

            return true;
        }
        public ReviewModel GetReviewInfo(int id)
        {
            var review = reviewDbContext.Set<Review>().FirstOrDefault(review => review.Id == id);
            var reviewModel = new ReviewModel(); // создание экземпляра класса модели
            reviewModel.Id = id;
            reviewModel.Text = review.Text;
            reviewModel.Rating = review.Rating;
            reviewModel.BookId = review.BookId;
            reviewModel.UserId = review.UserId;

            return reviewModel;
        }
        public bool UpdateReview(UpdateReviewParams reviewToUpdate)
        {
            var existingReview = reviewDbContext.Set<Review>().FirstOrDefault(review => review.Id == reviewToUpdate.Id);
            if (existingReview != null)
            {
                existingReview.Text = reviewToUpdate.Text;
                existingReview.Rating = reviewToUpdate.Rating;
                existingReview.BookId = reviewToUpdate.BookId;
                existingReview.UserId = reviewToUpdate.UserId;
                // Добавлять ли тут PublishedDateTime?
                reviewDbContext.SaveChanges();
                return true;
            }
            else return false;
        }

        public bool DeleteReview(int id)
        {
            var reviewToDelete = reviewDbContext.Set<Review>().FirstOrDefault(review => review.Id == id);
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
