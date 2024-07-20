using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using news_api.DTOs;

namespace news_api.Interfaces
{
    public interface INewsService
    {
        // Task<NewsDTO> CreateNewsAsync(CreateNewsDTO createNewsDTO, IFormFile image);
        Task<NewsDTO> CreateNewsAsync(CreateNewsWithImageDTO createNewsWithImageDTO);
        Task<NewsDTO> UpdateNewsAsync(int id, UpdateNewsDTO updateNewsDTO);

    }
}