using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace news_api.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public bool IsAdmin { get; set; } = true;
    }

    public class CreateUserDTO
    {
        [Required]
        [EmailAddress]
        // [MaxLength(100, ErrorMessage = "Email is too long")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        // [MaxLength(256, ErrorMessage = "Password is too long")]
        [MinLength(8, ErrorMessage = "The password is too short")]
        public string PasswordHash { get; set; } = string.Empty;
    }

    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        // [MaxLength(100, ErrorMessage = "Email is too long")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        // [MaxLength(256)]
        public string PasswordHash { get; set; } = string.Empty;
    }

}