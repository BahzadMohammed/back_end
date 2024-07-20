using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using news_api.Data;
using news_api.Helper;
using news_api.Interfaces;
using news_api.model;

namespace news_api.Repository
{
    public class NewsRepository : INewsRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly IWebHostEnvironment _environment;
        private int pageSize = 10;

        public NewsRepository(ApplicationDBContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // #1
        public async Task<IEnumerable<News?>> GetAllNewsAsync()
        {
            return await _context.News.Include(n => n.Genre).ToListAsync();
        }

        // #2
        public async Task<News?> GetNewsByIdAsync(int id)
        {
            var news = await _context.News.Include(n => n.Genre).FirstOrDefaultAsync(n => n.NewsId == id);
            if (news == null) return null;
            news.NumberOfReads++;
            await _context.SaveChangesAsync();
            return news;
        }

        // #3
        public async Task<News?> AddNewsAsync(News news)
        {
            _context.News.Add(news);
            await _context.SaveChangesAsync();
            return news;
        }

        // #4
        // public async Task<News?> UpdateNewsAsync(int id, News news)
        // {
        //     var existNews = await _context.News.FindAsync(id);
        //     if (existNews == null) return null;

        //     existNews.Title = news.Title;
        //     existNews.ShortDescription = news.ShortDescription;
        //     existNews.Content = news.Content;
        //     existNews.ImageUrl = news.ImageUrl;
        //     existNews.LastUpdateDate = DateTime.Now;
        //     existNews.GenreId = news.GenreId;

        //     await _context.SaveChangesAsync();
        //     return existNews;
        // }

        public async Task<News?> UpdateNewsAsync(int id, News news)
        {
            var existingNews = await _context.News.FindAsync(id);
            if (existingNews == null)
            {
                return null;
            }

            _context.Entry(existingNews).CurrentValues.SetValues(news);
            await _context.SaveChangesAsync();

            return existingNews;
        }


        // #5
        // public async Task<bool> DeleteNewsAsync(int id)
        // {
        //     var news = await _context.News.FindAsync(id);
        //     if (news == null) return false;

        //     _context.News.Remove(news);
        //     await _context.SaveChangesAsync();
        //     return true;
        // }

        public async Task<bool> DeleteNewsAsync(int id)
        {
            var news = await _context.News.FindAsync(id);
            if (news == null) return false;

            // Remove the image file
            if (!string.IsNullOrEmpty(news.ImageUrl))
            {
                var imagePath = Path.Combine(_environment.WebRootPath, news.ImageUrl.TrimStart('/'));
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
            }

            _context.News.Remove(news);
            await _context.SaveChangesAsync();
            return true;
        }


        // #6
        public async Task<(IEnumerable<News?>?, int)> GetNewsByGenreAsync(int genreId, QueryObject queryObject)
        {
            var totalNews = 0;
            var news = new List<News>();

            if (!string.IsNullOrEmpty(genreId.ToString()))
            {
                // Get the total count of news items that match the genreId
                totalNews = await _context.News.CountAsync(n => n.GenreId == genreId);

                if (!string.IsNullOrEmpty(queryObject.sortBy))
                {
                    if (queryObject.sortBy.Equals("numberOfReads", StringComparison.OrdinalIgnoreCase))
                    {
                        news = await _context.News
                            .Include(n => n.Genre)
                            .Where(n => n.GenreId == genreId)
                            .OrderByDescending(n => n.NumberOfReads)
                            .Skip((queryObject.pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();
                    }
                    else if (queryObject.sortBy.Equals("postDate", StringComparison.OrdinalIgnoreCase))
                    {
                        news = await _context.News
                            .Include(n => n.Genre)
                            .Where(n => n.GenreId == genreId)
                            .OrderByDescending(n => n.PostDate)
                            .Skip((queryObject.pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();
                    }
                    else
                    {
                        return (null, 0);
                    }
                }
                else
                {
                    news = await _context.News
                        .Include(n => n.Genre)
                        .Where(n => n.GenreId == genreId)
                        .OrderByDescending(n => n.PostDate)
                        .Skip((queryObject.pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
                }

                return (news, totalNews);
            }

            return (null, 0);
        }


        // #7
        public async Task<(IEnumerable<News?>?, int)> GetNewsWithSortAsync(QueryObject queryObject)
        {
            var totalNews = await _context.News.CountAsync();
            var news = new List<News>();

            if (!string.IsNullOrEmpty(queryObject.sortBy))
            {
                if (queryObject.sortBy.Equals("numberOfReads", StringComparison.OrdinalIgnoreCase))
                {
                    news = await _context.News
                        .Include(n => n.Genre)
                        .OrderByDescending(n => n.NumberOfReads)
                        .Skip((queryObject.pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();

                    return (news, totalNews);
                }
            }

            news = await _context.News
                .Include(n => n.Genre)
                .OrderByDescending(n => n.PostDate)
                .Skip((queryObject.pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (news, totalNews);
        }

        // #8
        public async Task<IEnumerable<News?>?> SearchNewsAsync(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var news = await _context.News
                    .Include(n => n.Genre)
                    .Where(n =>
                        n.Title.Contains(search) ||
                        n.Content.Contains(search) ||
                        n.ShortDescription.Contains(search)
                    ).ToListAsync();

                return news;
            }

            return null;
        }






        // >> NOT USED
        // public async Task<News?> UpdateNumberOfReadsAsync(int id, News news)
        // {
        //     var existNews = await _context.News.FindAsync(id);
        //     if (existNews == null) return null;

        //     existNews.NumberOfReads = news.NumberOfReads;

        //     await _context.SaveChangesAsync();
        //     return existNews;
        // }


    }
}