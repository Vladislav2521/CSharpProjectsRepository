using Astro.Database;
using Astro.Domain.Entities;
using Astro.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Astro.WebApi.Params;
using Astro.WebApi.Models;

namespace Astro.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewController : ControllerBase
    {
        private ReviewService reviewService;

        public ReviewController(ReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpPost("CreateReview")]
        public void CreateReview(CreateReviewParams reviewToCreate)
        {
            reviewService.CreateReview(reviewToCreate);
            
        }

        [HttpGet("GetReviewInfo")]
        public ReviewModel GetReviewInfo(int id)
        {
            var review = reviewService.GetReviewInfo(id);
            return review;
        }

        [HttpGet("GetReviewsOfTheMostPopularBook")]
        public List<ReviewModel> GetReviewsOfTheMostPopularBook()
        {
            var reviews = reviewService.GetReviewsOfTheMostPopularBook();
            return reviews;
        }

        [HttpPut("UpdateReview")]
        public void UpdateReview(UpdateReviewParams reviewToUpdate)
        {
            reviewService.UpdateReview(reviewToUpdate);
        }

        [HttpDelete("DeleteReview")]
        public void DeleteReview(int id)
        {
            reviewService.DeleteReview(id);
        }
    }
}
