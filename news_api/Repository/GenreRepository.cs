using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using news_api.Data;
using news_api.Interfaces;
using news_api.model;

namespace news_api.Repository
{
    public class GenreRepository : IGenreRepository
    {

        private readonly ApplicationDBContext _context;

        public GenreRepository(ApplicationDBContext context) {
            _context = context;
        }

        // #1
        public async Task<IEnumerable<Genre>> GetAllGenresAsync()
        {
            return await _context.Genres.ToListAsync();
        }

        // #2
        public async Task<Genre?> GetGenreByIdAsync(int id)
        {
            return await _context.Genres.FindAsync(id);
        }
    }
}