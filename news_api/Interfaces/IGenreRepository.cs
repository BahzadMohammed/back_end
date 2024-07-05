using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using news_api.model;

namespace news_api.Interfaces
{
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> GetAllGenresAsync();
        Task<Genre?> GetGenreByIdAsync(int id);
    }
}