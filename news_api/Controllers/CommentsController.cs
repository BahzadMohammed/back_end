using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using news_api.Data;
using news_api.DTOs;
using news_api.Interfaces;
using news_api.Models;

namespace news_api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public CommentsController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            var comments = await _context.Comments.Include(c => c.News).ToListAsync();
            return Ok(_mapper.Map<IEnumerable<CommentDTO>>(comments));
        }

        // GET: api/Comments/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment(int id)
        {
            var comment = await _context.Comments.Include(c => c.News).FirstOrDefaultAsync(c => c.CommentId == id);
            if (comment == null)
                return NotFound();
            
            return Ok(_mapper.Map<CommentDTO>(comment));
        }

        // POST: api/Comments
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentDTO createCommentDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = _mapper.Map<Comment>(createCommentDTO);
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetComment), new { id = comment.CommentId }, _mapper.Map<CommentDTO>(comment));
        }

        // PUT: api/Comments/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] UpdateCommentDTO updateCommentDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingComment = await _context.Comments.FindAsync(id);
            if (existingComment == null)
                return NotFound();

            existingComment.AnonymousUser = updateCommentDTO.AnonymousUser;
            existingComment.CommentText = updateCommentDTO.CommentText;
            existingComment.NewsId = updateCommentDTO.NewsId;

            _context.Comments.Update(existingComment);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<CommentDTO>(existingComment));
        }

        // DELETE: api/Comments/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
                return NotFound();

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return NoContent();
        }


        // Add a new route to get comments by news id
        [HttpGet("{newsId:int}/comments")]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetCommentsByNewsId([FromRoute] int newsId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var comments = await _context.Comments
                .Where(c => c.NewsId == newsId)
                .OrderByDescending(c => c.CommentId)
                .ToListAsync();

            if (comments == null || !comments.Any()) return NotFound("No comments found for this news.");

            return Ok(_mapper.Map<IEnumerable<CommentDTO>>(comments));
        }

    }
}