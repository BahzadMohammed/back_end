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
    [ApiController]
    [Route("api/Genre")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository _genreRepo;
        private readonly IMapper _mapper;

        public GenreController(IGenreRepository genreRepo, IMapper mapper)
        {
            _genreRepo = genreRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreDTO>>> GetGenres()
        {
            var genres = await _genreRepo.GetAllGenresAsync();
            return Ok(_mapper.Map<IEnumerable<GenreDTO>>(genres));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GenreDTO>> GetGenre(int id)
        {
            var genre = await _genreRepo.GetGenreByIdAsync(id);
            if (genre == null) return NotFound();

            return Ok(_mapper.Map<GenreDTO>(genre));
        }
    }
}