using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using news_api.DTOs;
using news_api.Interfaces;
using news_api.model;

namespace news_api.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepo;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;

        public NewsService(INewsRepository newsRepository, IWebHostEnvironment environment, IMapper mapper)
        {
            _newsRepo = newsRepository;
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            _mapper = mapper;

            Console.WriteLine($"WebRootPath: {_environment.WebRootPath}");
        }

        // public async Task<NewsDTO> CreateNewsAsync(CreateNewsWithImageDTO createNewsWithImageDTO)
        // {

        //     // Ensure the WebRootPath is not null
        //     if (string.IsNullOrEmpty(_environment.WebRootPath))
        //     {
        //         throw new InvalidOperationException("WebRootPath is not set.");
        //     }

        //     var uploadsFolderPath = Path.Combine(_environment.WebRootPath, "uploads");
        //     Directory.CreateDirectory(uploadsFolderPath);

        //     var filePath = Path.Combine(uploadsFolderPath, createNewsWithImageDTO.Image!.FileName);
        //     using (var stream = new FileStream(filePath, FileMode.Create))
        //     {
        //         await createNewsWithImageDTO.Image.CopyToAsync(stream);
        //     }

        //     var news = _mapper.Map<News>(createNewsWithImageDTO);
        //     news.ImageUrl = $"/uploads/{createNewsWithImageDTO.Image.FileName}";
        //     var createdNews = await _newsRepo.AddNewsAsync(news);

        //     return _mapper.Map<NewsDTO>(createdNews);
        // }


        public async Task<NewsDTO> CreateNewsAsync(CreateNewsWithImageDTO createNewsWithImageDTO)
        {
            var uploadsFolderPath = Path.Combine(_environment.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadsFolderPath);

            var newFileName = Guid.NewGuid().ToString() + Path.GetExtension(createNewsWithImageDTO.Image!.FileName);
            var filePath = Path.Combine(uploadsFolderPath, newFileName);

            Console.WriteLine($"newFileName: {newFileName}");
            Console.WriteLine($"filePath: {filePath}");

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await createNewsWithImageDTO.Image.CopyToAsync(stream);
            }

            var news = _mapper.Map<News>(createNewsWithImageDTO);
            news.ImageUrl = $"/uploads/{newFileName}";
            var createdNews = await _newsRepo.AddNewsAsync(news);

            return _mapper.Map<NewsDTO>(createdNews);
        }



        public async Task<NewsDTO> UpdateNewsAsync(int id, UpdateNewsDTO updateNewsDTO)
        {
            var news = await _newsRepo.GetNewsByIdAsync(id);
            if (news == null)
            {
                throw new Exception("News not found");
            }

            news.Title = updateNewsDTO.Title;
            news.ShortDescription = updateNewsDTO.ShortDescription;
            news.Content = updateNewsDTO.Content;
            news.GenreId = updateNewsDTO.GenreId;

            if (updateNewsDTO.Image != null)
            {
                if (updateNewsDTO.Image.Length > 5000000)
                {
                    throw new Exception("Image is too large. Maximum size is 5MB.");
                }

                var allowedExtensions = new[] { ".jpeg", ".jpg", ".png" };
                var extension = Path.GetExtension(updateNewsDTO.Image.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(extension))
                {
                    throw new Exception("Unsupported file format. Only JPEG and PNG are allowed.");
                }

                var uploadsFolderPath = Path.Combine(_environment.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadsFolderPath);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(updateNewsDTO.Image.FileName);
                var filePath = Path.Combine(uploadsFolderPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await updateNewsDTO.Image.CopyToAsync(stream);
                }

                // Remove the old image
                if (!string.IsNullOrEmpty(news.ImageUrl))
                {
                    var oldImagePath = Path.Combine(_environment.WebRootPath, news.ImageUrl.TrimStart('/'));
                    if (File.Exists(oldImagePath))
                    {
                        File.Delete(oldImagePath);
                    }
                }

                news.ImageUrl = $"/uploads/{fileName}";
            }

            await _newsRepo.UpdateNewsAsync(id, news);

            return _mapper.Map<NewsDTO>(news);
        }


    }
}