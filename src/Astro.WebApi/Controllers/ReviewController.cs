using Astro.Database;
using Astro.Domain.Entities;
using Astro.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Astro.WebApi.Services;
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
        public IActionResult CreateReview(CreateReviewParams reviewToCreate)
        {
            bool reviewCreated = reviewService.CreateReview(reviewToCreate);
            if (reviewCreated == true)
            {
                return Ok("Отзыв успешно создан");
            }
            else return Content("Не удалось создать отзыв");
        }
        // IActionResult не используется в методах GET.

        [HttpGet("GetReviewInfo")]
        public ReviewModel GetReviewInfo(int id)
        {
            var review = reviewService.GetReviewInfo(id);
            return review;
        }

        [HttpPut("UpdateReview")]
        public IActionResult UpdateReview(UpdateReviewParams reviewToUpdate)
        {
            bool reviewUpdated = reviewService.UpdateReview(reviewToUpdate);
            if (reviewUpdated == true)
            {
                return Ok("Отзыв успешно отредактирован");
            }
            else return Content("Не удалось отредактировать отзыв");
        }

        [HttpDelete("DeleteReview")]
        public IActionResult DeleteReview(int id)
        {
            bool reviewDeleted = reviewService.DeleteReview(id);
            if (reviewDeleted == true)
            {
                return Ok("Отзыв успешно удален");
            }
            else return Content("Невозможно удалить несуществующий отзыв");

        }
    }
}
