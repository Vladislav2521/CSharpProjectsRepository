using Astro.Database;
using Astro.Domain.Entities;
using Astro.WebApi.Models;
using Astro.WebApi.Params;


namespace Astro.WebApi.Services
{
    public class ReviewService
    {
        private LibraryDbContext DbContext;
        

        public ReviewService(LibraryDbContext DbContext)
        {
            this.DbContext = DbContext;
        }
        // Все методы по Review функционируют исправно

        // метод CreateReview ничего не отдаёт, поэтому Model тут не нужна

        //отдающий тип заменить на void

        public bool CreateReview(CreateReviewParams reviewToCreate) { 
            Review newReview = new Review();

            newReview.Text = reviewToCreate.Text;
            newReview.Rating = reviewToCreate.Rating;
            newReview.BookId = reviewToCreate.BookId;
            newReview.UserId = reviewToCreate.UserId;
            newReview.PublishedDateTime = DateTime.Now; // чтобы убрать значение "-infinity" даты и времени в таблице, следует установить значение свойства PublishedDateTime.

            DbContext.Set<Review>().Add(newReview);
            DbContext.SaveChanges();

            return true;
        }

        public ReviewModel GetReviewInfo(int id)
        {
            var review = DbContext.Set<Review>().FirstOrDefault(review => review.Id == id); // сырой отзыв
            var reviewModel = new ReviewModel(); // создание экземпляра класса модели, которую мы будем отдавать
            reviewModel.Id = id;
            reviewModel.Text = review.Text;
            reviewModel.Rating = review.Rating;
            reviewModel.BookId = review.BookId;
            reviewModel.UserId = review.UserId;
            reviewModel.PublishedDateTime = review.PublishedDateTime;

            return reviewModel;
        }

        public List<ReviewModel> GetReviewsOfTheMostPopularBook() // не написан контроллер
        {
            var mostPopularBook = DbContext.Set<Book>()
                .OrderByDescending(o => o.Reviews.Count)
                .FirstOrDefault();
            var mostPopularBookReviews = DbContext.Set<Review>()
                .Where(w => w.Book == mostPopularBook);
            var reviewModel = mostPopularBookReviews.Select(s => new ReviewModel
            {
                Id = s.Id,
                Text = s.Text,
                Rating = s.Rating,
                BookId = s.BookId,
                UserId = s.UserId,
                PublishedDateTime = s.PublishedDateTime,
            })
            .ToList();
            return reviewModel;
        }

        public void UpdateReview(UpdateReviewParams reviewToUpdate)
        {
            var existingReview = DbContext.Set<Review>().FirstOrDefault(review => review.Id == reviewToUpdate.Id);
            if (existingReview != null)
            {
                existingReview.Text = reviewToUpdate.Text;
                existingReview.Rating = reviewToUpdate.Rating;
                existingReview.BookId = reviewToUpdate.BookId;
                existingReview.UserId = reviewToUpdate.UserId;
                existingReview.PublishedDateTime = DateTime.Now; // при обновлении отзыва, существующему отзыву нужно присвоить текущее время редактирования
                
                DbContext.SaveChanges();   
            }
        }

        public void DeleteReview(int id)
        {
            var reviewToDelete = DbContext.Set<Review>().FirstOrDefault(review => review.Id == id);
            if (reviewToDelete != null)
            {
                DbContext.Set<Review>().Remove(reviewToDelete);
                DbContext.SaveChanges();   
            }
        }

    }


}
