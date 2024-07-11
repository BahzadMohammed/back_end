using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace news_api.model
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        
        [Required]
        // [StringLength(100)]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        // [StringLength(256)]
        public string PasswordHash { get; set; } = string.Empty;
        
        [Required]
        public bool IsAdmin { get; set; } = true;
    }
}



