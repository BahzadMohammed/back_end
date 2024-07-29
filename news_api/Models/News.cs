using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using news_api.Models;

namespace news_api.model
{
    public class News
    {
        

        [Key]
        public int NewsId { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        public string ShortDescription { get; set; } = string.Empty;
        
        [Required]
        [StringLength(2000)]
        public string Content { get; set; } = string.Empty;
        
        [StringLength(200)]
        public string ImageUrl { get; set; } = string.Empty;
        
        [Required]
        public int NumberOfReads { get; set; } = 0;

        [Required]
        public DateTime PostDate { get; set; } = DateTime.Now;

        [Required]
        public DateTime LastUpdateDate { get; set; } = DateTime.Now;
        
        [Required] 
        public int GenreId { get; set; } 
        
        [ForeignKey("GenreId")]
        public Genre Genre { get; set; } = null!;

        // Collection of comments
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    }
}


