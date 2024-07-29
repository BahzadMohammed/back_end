using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace news_api.DTOs
{

    public class CommentDTO
    {
        public int CommentId { get; set; }
        public string AnonymousUser { get; set; } = string.Empty;
        public string CommentText { get; set; } = string.Empty;
        public int NewsId { get; set; }
    }

    public class CreateCommentDTO
    {
        [Required]
        [StringLength(20)]
        public string AnonymousUser { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public string CommentText { get; set; } = string.Empty;

        [Required]
        public int NewsId { get; set; }
    }

    public class UpdateCommentDTO
    {
        [Required]
        [StringLength(20)]
        public string AnonymousUser { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public string CommentText { get; set; } = string.Empty;

        [Required]
        public int NewsId { get; set; }
    }

}

