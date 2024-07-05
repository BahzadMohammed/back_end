// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.IdentityModel.Tokens;
// using System;
// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using System.Text;
// using System.Threading.Tasks;
// using news_api.Interfaces;
// using news_api.DTOs;
// using Microsoft.Extensions.Configuration;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Http.HttpResults;
// using news_api.Helper;
// using news_api.model;

// namespace news_api.Controllers
// {
//     [ApiController]
//     [Route("api/auth")]
//     public class AuthController : ControllerBase
//     {

//         private readonly IUserRepository _userRepo;
//         private readonly IConfiguration _configuration;

//         public AuthController(IUserRepository userRepo, IConfiguration configuration)
//         {
//             _userRepo = userRepo;
//             _configuration = configuration;
//         }

//         [HttpPost("login")]
//         public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
//         {
//             var user = await _userRepo.AuthenticateUserAsync(loginDTO.Username, loginDTO.PasswordHash);
//             if (user == null)
//             {
//                 return Unauthorized("Invalid credentials");
//             }

//             var tokenHandler = new JwtSecurityTokenHandler();
//             var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
//             var tokenDescriptor = new SecurityTokenDescriptor
//             {
//                 Subject = new ClaimsIdentity(new[]
//                 {
//                     new Claim(ClaimTypes.Name, user.Username),
//                     new Claim(ClaimTypes.Role, user.IsAdmin ? "SuperAdmin" : "Admin")
//                 }),
//                 Expires = DateTime.UtcNow.AddDays(7),
//                 SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
//                 Issuer = _configuration["Jwt:Issuer"],
//                 Audience = _configuration["Jwt:Audience"]
//             };
//             var token = tokenHandler.CreateToken(tokenDescriptor);
//             var tokenString = tokenHandler.WriteToken(token);

//             return Ok(new { Token = tokenString });
//         }

//     }
// }






using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using news_api.Interfaces;
using news_api.DTOs;
using Microsoft.Extensions.Configuration;

namespace news_api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        
        private readonly IUserRepository _userRepo;
        private readonly IConfiguration _configuration;

        public AuthController(IUserRepository userRepo, IConfiguration configuration)
        {
            _userRepo = userRepo;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            try {
                
                var user = await _userRepo.AuthenticateUserAsync(loginDTO.Username, loginDTO.PasswordHash);
                if (user == null)
                {
                    return Unauthorized("Invalid credentials");
                }

                var jwtSection = _configuration.GetSection("Jwt");
                var key = jwtSection.GetValue<string>("Key");
                var issuer = jwtSection.GetValue<string>("Issuer");
                var audience = jwtSection.GetValue<string>("Audience");

                if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))
                {
                    return StatusCode(500, "JWT settings are not configured properly.");
                }

                var keyBytes = Encoding.ASCII.GetBytes(key);
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Role, user.IsAdmin ? "SuperAdmin" : "Admin")
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(jwtSection.GetValue<int>("ExpirationMinutes")),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature),
                    Issuer = issuer,
                    Audience = audience
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new { Token = tokenString });

            } catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
            
        }
    }
}

