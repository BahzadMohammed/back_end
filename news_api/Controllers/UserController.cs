using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using news_api.DTOs;
using news_api.Interfaces;
using news_api.model;


namespace news_api.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        // #1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var users = await _userRepo.GetAllUsersAsync();
            return Ok(_mapper.Map<IEnumerable<UserDTO>>(users));
        }

        // #2 
        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserDTO>> GetUser([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userRepo.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            return Ok(_mapper.Map<UserDTO>(user));
        }

        // #3
        [HttpPost]
        public async Task<ActionResult<UserDTO>> CreateUser([FromBody] CreateUserDTO createUserDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = _mapper.Map<User>(createUserDTO);
            await _userRepo.AddUserAsync(user);

            return CreatedAtAction(
                nameof(GetUser),
                new { id = user.UserId },
                _mapper.Map<UserDTO>(user)
            );
        }

        // #4
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] CreateUserDTO updateUserDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = _mapper.Map<User>(updateUserDTO);
            var updatedUser = await _userRepo.UpdateUserAsync(id, user);

            if (updatedUser == null) return NotFound();

            return Ok(_mapper.Map<UserDTO>(updatedUser));
        }

        // #5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _userRepo.DeleteUserAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }


        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}