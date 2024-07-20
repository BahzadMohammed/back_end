using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace news_api.DTOs
{
    public class NewsDTO
    {
        public int NewsId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime PostDate { get; set; } = DateTime.Now;
        public DateTime LastUpdateDate { get; set; } = DateTime.Now;
        public int NumberOfReads { get; set; }
        public string GenreName { get; set; } = string.Empty;
    }

    public class CreateNewsDTO
    {
        [Required]
        [MaxLength(200, ErrorMessage = "Title is too long")]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string ShortDescription { get; set; } = string.Empty;

        [Required]
        [MaxLength(2000, ErrorMessage = "Content is too long")]
        public string Content { get; set; } = string.Empty;

        [Url]
        // [MaxLength(200, ErrorMessage = "ImageUrl is too long")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        public int GenreId { get; set; }
    }

    public class UpdateNewsDTO
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string ShortDescription { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        [Required]
        public IFormFile? Image { get; set; }

        [Required]
        public int GenreId { get; set; }
    }

    public class CreateNewsWithImageDTO
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string ShortDescription { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        [Required]
        public IFormFile? Image { get; set; }

        [Required]
        public int GenreId { get; set; }
    }


}