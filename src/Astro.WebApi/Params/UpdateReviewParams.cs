﻿using Astro.Domain.Entities;

namespace Astro.WebApi.Params
{
    public class UpdateReviewParams
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
    }
}
