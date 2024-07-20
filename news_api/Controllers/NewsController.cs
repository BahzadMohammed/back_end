using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using news_api.DTOs;
using news_api.Helper;
using news_api.Interfaces;
using news_api.model;

namespace news_api.Controllers
{
    [Route("api/news")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsRepository _newsRepo;
        private readonly IMapper _mapper;
        private readonly INewsService _newsService;
        public int pageSize { get; set; } = 10;

        public NewsController(INewsRepository newsRepo, IMapper mapper, INewsService newsService)
        {
            _newsRepo = newsRepo;
            _mapper = mapper;
            _newsService = newsService;
        }

        // #2
        [HttpGet("{id:int}")]
        [AllowAnonymous] // Accessible by everyone
        public async Task<ActionResult<NewsDTO>> GetNews([FromRoute] int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var news = await _newsRepo.GetNewsByIdAsync(id);
            if (news == null) return NotFound();

            return Ok(_mapper.Map<NewsDTO>(news));
        }

        // #3
        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] CreateNewsWithImageDTO createNewsWithImageDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (createNewsWithImageDTO.Image == null || createNewsWithImageDTO.Image.Length == 0)
                return BadRequest("No file uploaded.");

            // Validate the image type
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(createNewsWithImageDTO.Image.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
            {
                return BadRequest("Invalid image type. Only JPG, JPEG, PNG, and GIF are allowed.");
            }

            // Validate the image size (e.g., max 5 MB)
            var maxSize = 5 * 1024 * 1024;
            if (createNewsWithImageDTO.Image.Length > maxSize)
            {
                return BadRequest("Image size exceeds the maximum allowed size of 5 MB.");
            }

            var createdNews = await _newsService.CreateNewsAsync(createNewsWithImageDTO);
            return CreatedAtAction(nameof(GetNews), new { id = createdNews.NewsId }, createdNews);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateNews([FromRoute] int id, [FromForm] UpdateNewsDTO updateNewsDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updatedNews = await _newsService.UpdateNewsAsync(id, updateNewsDTO);
            return Ok(_mapper.Map<NewsDTO>(updatedNews));
        }


        // #5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteNews([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _newsRepo.DeleteNewsAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }


        [HttpGet("genre/{genreId:int}")]
        [AllowAnonymous] // Accessible by everyone
        public async Task<ActionResult<IEnumerable<NewsDTO>>> GetNewsByGenre([FromRoute] int genreId ,[FromQuery] QueryObject queryObject)
        {
            var (news, totalNews) = await _newsRepo.GetNewsByGenreAsync(genreId, queryObject);
            if (news == null) return NotFound();
            var totalPages = (int) System.Math.Ceiling(totalNews / (double) pageSize);
            var result = new PagedResult<NewsDTO>
            {
                Items = _mapper.Map<IEnumerable<NewsDTO>>(news),
                PageNumber = queryObject.pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalItems = totalNews
            };

            return Ok(result);
        }

        // [HttpGet("sort")]
        [HttpGet("")]
        // [AllowAnonymous] // Accessible by everyone
        public async Task<ActionResult<PagedResult<NewsDTO>>> GetNewsWithPagination([FromQuery] QueryObject queryObject)
        {
            var (news, totalNews) = await _newsRepo.GetNewsWithSortAsync(queryObject);
            var totalPages = (int) System.Math.Ceiling(totalNews / (double) pageSize);

            var result = new PagedResult<NewsDTO>
            {
                Items = _mapper.Map<IEnumerable<NewsDTO>>(news),
                PageNumber = queryObject.pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalItems = totalNews
            };

            return Ok(result);
        }

        [HttpGet("search")]
        [AllowAnonymous] // Accessible by everyone
        public async Task<ActionResult<IEnumerable<NewsDTO>>> SearchNews([FromQuery] string search)
        {
            var news = await _newsRepo.SearchNewsAsync(search);
            if (news == null) return NotFound("No news found");
            return Ok(_mapper.Map<IEnumerable<NewsDTO>>(news));
        }

    }
}