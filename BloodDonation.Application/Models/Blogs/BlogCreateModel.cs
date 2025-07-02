﻿namespace BloodDonation.Application.Models.Blogs
{
    public class BlogCreateModel
    {
        public string Content { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
    }
}
