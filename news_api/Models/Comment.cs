using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using news_api.model;


namespace news_api.Models
{
    public class Comment
    {
        
        [Key]
        public int CommentId { get; set; }
        
        [Required]
        [StringLength(20)]
        public string AnonymousUser { get; set; } = string.Empty; // New property for custom user names
        
        [Required]
        [StringLength(1000)]
        public string CommentText { get; set; } = string.Empty;
        
        [Required]
        public int NewsId { get; set; }

        [ForeignKey("NewsId")]
        public News News { get; set; } = null!;
    }
}

