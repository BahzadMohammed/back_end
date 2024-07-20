using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using news_api.Helper;
using news_api.model;

namespace news_api.Interfaces
{
    public interface INewsRepository
    {
        Task<IEnumerable<News?>> GetAllNewsAsync();
        Task<News?> GetNewsByIdAsync(int id);
        Task<News?> AddNewsAsync(News news);
        Task<News?> UpdateNewsAsync(int id, News news);
        Task<bool> DeleteNewsAsync(int id);
        Task<(IEnumerable<News?>?, int)> GetNewsByGenreAsync(int genreId, QueryObject queryObject);
        Task<(IEnumerable<News?>?, int)> GetNewsWithSortAsync(QueryObject queryObject);
        Task<IEnumerable<News?>?> SearchNewsAsync(string search);

    }
}