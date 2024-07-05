using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace news_api.DTOs
{
    public class NewsDTO
    {
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
        [MaxLength(50, ErrorMessage = "Title is too long")]
        [MinLength(5, ErrorMessage = "Title is too short")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(100, ErrorMessage = "Short description is too long")]
        [MinLength(20, ErrorMessage = "Short description is too short")]
        public string ShortDescription { get; set; } = string.Empty;

        [Required]
        [MaxLength(2000, ErrorMessage = "Content is too long")]
        [MinLength(100, ErrorMessage = "Content is too short")]
        public string Content { get; set; } = string.Empty;

        [Url]
        [MaxLength(200, ErrorMessage = "ImageUrl is too long")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        public int GenreId { get; set; }
    }

    public class UpdateNewsDTO 
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Title is too long")]
        [MinLength(5, ErrorMessage = "Title is too short")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(100, ErrorMessage = "Short description is too long")]
        [MinLength(10, ErrorMessage = "Short description is too short")]
        public string ShortDescription { get; set; } = string.Empty;

        [Required]
        [MaxLength(2000, ErrorMessage = "Content is too long")]
        [MinLength(10, ErrorMessage = "Content is too short")]
        public string Content { get; set; } = string.Empty;

        [MaxLength(200, ErrorMessage = "ImageUrl is too long")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        public int GenreId { get; set; }
    }

    // public class UpdateNumberOfReadsDTO {
    //     public int NumberOfReads { get; set; }
    // }

}