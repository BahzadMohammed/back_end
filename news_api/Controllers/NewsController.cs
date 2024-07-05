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
        public int pageSize { get; set; } = 10;

        public NewsController(INewsRepository newsRepo, IMapper mapper)
        {
            _newsRepo = newsRepo;
            _mapper = mapper;
        }

        // #1
        // [HttpGet]
        // [AllowAnonymous] // Accessible by everyone
        // public async Task<ActionResult<IEnumerable<NewsDTO>>> GetNews()
        // {
        //     if(!ModelState.IsValid) return BadRequest(ModelState);

        //     var news = await _newsRepo.GetAllNewsAsync();
        //     return Ok(_mapper.Map<IEnumerable<NewsDTO>>(news));
        // }

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
        [Authorize(Roles = "SuperAdmin,Admin")] // Accessible by authenticated users
        public async Task<ActionResult<NewsDTO>> CreateNews([FromBody] CreateNewsDTO createNewsDTO)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var news = _mapper.Map<News>(createNewsDTO);
            await _newsRepo.AddNewsAsync(news);

            return CreatedAtAction(
                nameof(GetNews), 
                new { id = news.NewsId }, 
                _mapper.Map<NewsDTO>(news)
            );
        }

        // #4
        [HttpPut("{id:int}")]
        [Authorize(Roles = "SuperAdmin,Admin")] // Accessible by authenticated users
        public async Task<IActionResult> UpdateNews([FromRoute] int id, [FromBody] UpdateNewsDTO updateNewsDTO)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var news = _mapper.Map<News>(updateNewsDTO);
            var existNews = await _newsRepo.UpdateNewsAsync(id, news);

            return Ok(_mapper.Map<NewsDTO>(existNews));
        }

        // #5
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "SuperAdmin,Admin")] // Accessible by authenticated users
        public async Task<IActionResult> DeleteNews(int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

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
        [AllowAnonymous] // Accessible by everyone
        public async Task<ActionResult<PagedResult<NewsDTO>>> GetNewsWithPagination([FromQuery] QueryObject queryObject)
        {
            var (news, totalNews) = await _newsRepo.GetNewsWithSortAsync(queryObject);
            var totalPages = (int)System.Math.Ceiling(totalNews / (double) pageSize);

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




        // >> NOT USED
        // [HttpPut("updateNumberOfReads/{id}")]
        // public async Task<IActionResult> UpdateNumberOfReads([FromRoute] int id, [FromBody] UpdateNumberOfReadsDTO updateNumberOfReadsDTO)
        // {
        //     var news = _mapper.Map<News>(updateNumberOfReadsDTO);
        //     var existNews = await _newsRepo.UpdateNumberOfReadsAsync(id, news);

        //     return Ok(_mapper.Map<NewsDTO>(existNews));
        // }
    }
}