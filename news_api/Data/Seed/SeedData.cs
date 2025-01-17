using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using news_api.model;
using Microsoft.Extensions.Logging;
using news_api.Models;

namespace news_api.Data.Seed
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();

                // Ensure the database is created
                context.Database.EnsureCreated();


                // Look for any users.
                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User
                        {
                            Email = "admin@example.com",
                            PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456789"),
                            IsAdmin = true
                        }
                    );
                    context.SaveChanges();
                }

                if (!context.Comments.Any())
                {
                    context.Comments.AddRange(
                        new Comment
                        {
                            AnonymousUser = "lion",
                            CommentText = "This is a comment from lion.",
                            NewsId = 1 // Assuming a news item with this ID exists
                        },
                        new Comment
                        {
                            AnonymousUser = "deer",
                            CommentText = "This is a comment from deer.",
                            NewsId = 1 // Assuming a news item with this ID exists
                        },
                        new Comment
                        {
                            AnonymousUser = "wolf",
                            CommentText = "This is a comment from wolf.",
                            NewsId = 2 // Assuming a news item with this ID exists
                        },
                        new Comment
                        {
                            AnonymousUser = "ant",
                            CommentText = "This is a comment from ant.",
                            NewsId = 2 // Assuming a news item with this ID exists
                        },
                        new Comment
                        {
                            AnonymousUser = "fish",
                            CommentText = "This is a comment from fish.",
                            NewsId = 3 // Assuming a news item with this ID exists
                        }
                    );
                    context.SaveChanges();
                }


                // Look for any genres.
                // if (!context.Genres.Any())
                // {
                //     context.Genres.AddRange(
                //         new Genre { Name = "Technology" },
                //         new Genre { Name = "Health" },
                //         new Genre { Name = "Sports" },
                //         new Genre { Name = "Business" },
                //         new Genre { Name = "Politics" },
                //         new Genre { Name = "Science" },
                //         new Genre { Name = "Entertainment" }
                //     );
                //     context.SaveChanges();
                // }

                // Look for any news.
                if (!context.News.Any())
                {
                    var technologyGenre = context.Genres.First(g => g.Name == "Technology");
                    var healthGenre = context.Genres.First(g => g.Name == "Health");
                    var sportsGenre = context.Genres.First(g => g.Name == "Sports");
                    var politicsGenre = context.Genres.First(g => g.Name == "Politics");
                    var businessGenre = context.Genres.First(g => g.Name == "Business");
                    var scienceGenre = context.Genres.First(g => g.Name == "Science");
                    var entertainmentGenre = context.Genres.First(g => g.Name == "Entertainment");

                    context.News.AddRange(
                        new News
                        {
                            Title = "New Tech Innovations",
                            ShortDescription = "Tech innovations are rapidly spreading across the world...",
                            Content = "Latest advancements in technology...",
                            ImageUrl = "https://miro.medium.com/v2/resize:fit:3296/1*TTO1f4XiPkveK2x8ct8SwQ.png",
                            PostDate = DateTime.Now.AddDays(-1),
                            LastUpdateDate = DateTime.Now,
                            NumberOfReads = 50,
                            GenreId = technologyGenre.GenreId
                        },
                        new News
                        {
                            Title = "Health Benefits of Yoga",
                            ShortDescription = "Yoga offers numerous health benefits...",
                            Content = "Yoga offers numerous health benefits...",
                            ImageUrl = "https://miro.medium.com/v2/resize:fit:3296/1*TTO1f4XiPkveK2x8ct8SwQ.png",
                            PostDate = DateTime.Now.AddDays(-2),
                            LastUpdateDate = DateTime.Now,
                            NumberOfReads = 100,
                            GenreId = healthGenre.GenreId
                        },
                        new News
                        {
                            Title = "Startup Ecosystem",
                            ShortDescription = "The startup ecosystem is thriving...",
                            Content = "The startup ecosystem is thriving...",
                            ImageUrl = "https://miro.medium.com/v2/resize:fit:3296/1*TTO1f4XiPkveK2x8ct8SwQ.png",
                            PostDate = DateTime.Now.AddDays(-3),
                            LastUpdateDate = DateTime.Now,
                            NumberOfReads = 75,
                            GenreId = technologyGenre.GenreId
                        },
                        new News
                        {
                            Title = "Business Growth",
                            ShortDescription = "Business growth is rapid...",
                            Content = "Business growth is rapid...",
                            ImageUrl = "https://miro.medium.com/v2/resize:fit:3296/1*TTO1f4XiPkveK2x8ct8SwQ.png",
                            PostDate = DateTime.Now.AddDays(-4),
                            LastUpdateDate = DateTime.Now,
                            NumberOfReads = 150,
                            GenreId = businessGenre.GenreId
                        },
                        new News
                        {
                            Title = "Health Benefits of Yoga",
                            ShortDescription = "Yoga offers numerous health benefits...",
                            Content = "Yoga offers numerous health benefits...",
                            ImageUrl = "https://miro.medium.com/v2/resize:fit:3296/1*TTO1f4XiPkveK2x8ct8SwQ.png",
                            PostDate = DateTime.Now.AddDays(-5),
                            LastUpdateDate = DateTime.Now,
                            NumberOfReads = 200,
                            GenreId = healthGenre.GenreId
                        },
                        new News
                        {
                            Title = "Startup Ecosystem",
                            ShortDescription = "The startup ecosystem is thriving...",
                            Content = "The startup ecosystem is thriving...",
                            ImageUrl = "https://miro.medium.com/v2/resize:fit:3296/1*TTO1f4XiPkveK2x8ct8SwQ.png",
                            PostDate = DateTime.Now.AddDays(-6),
                            LastUpdateDate = DateTime.Now,
                            NumberOfReads = 250,
                            GenreId = businessGenre.GenreId
                        },
                        new News
                        {
                            Title = "Business Growth",
                            ShortDescription = "Business growth is rapid...",
                            Content = "Business growth is rapid...",
                            ImageUrl = "https://miro.medium.com/v2/resize:fit:3296/1*TTO1f4XiPkveK2x8ct8SwQ.png",
                            PostDate = DateTime.Now.AddDays(-7),
                            LastUpdateDate = DateTime.Now,
                            NumberOfReads = 300,
                            GenreId = businessGenre.GenreId
                        },
                        new News
                        {
                            Title = "War in Ukraine",
                            ShortDescription = "War in Ukraine is on the horizon...",
                            Content = "War in Ukraine is on the horizon...",
                            ImageUrl = "https://miro.medium.com/v2/resize:fit:3296/1*TTO1f4XiPkveK2x8ct8SwQ.png",
                            PostDate = DateTime.Now.AddDays(-8),
                            LastUpdateDate = DateTime.Now,
                            NumberOfReads = 350,
                            GenreId = politicsGenre.GenreId
                        },
                        new News
                        {
                            Title = "Fun Inventions",
                            ShortDescription = "Fun Inventions are on the rise...",
                            Content = "Fun Inventions are on the rise...",
                            ImageUrl = "https://miro.medium.com/v2/resize:fit:3296/1*TTO1f4XiPkveK2x8ct8SwQ.png",
                            PostDate = DateTime.Now.AddDays(-9),
                            LastUpdateDate = DateTime.Now,
                            NumberOfReads = 400,
                            GenreId = entertainmentGenre.GenreId
                        },
                        new News
                        {
                            Title = "Science Advancements",
                            ShortDescription = "Science advancements are on the horizon...",
                            Content = "Science advancements are on the horizon...",
                            ImageUrl = "https://miro.medium.com/v2/resize:fit:3296/1*TTO1f4XiPkveK2x8ct8SwQ.png",
                            PostDate = DateTime.Now.AddDays(-10),
                            LastUpdateDate = DateTime.Now,
                            NumberOfReads = 450,
                            GenreId = scienceGenre.GenreId
                        },
                        new News
                        {
                            Title = "Tech Innovations",
                            ShortDescription = "Tech innovations are rapidly spreading across the world...",
                            Content = "Latest advancements in technology...",
                            ImageUrl = "https://miro.medium.com/v2/resize:fit:3296/1*TTO1f4XiPkveK2x8ct8SwQ.png",
                            PostDate = DateTime.Now.AddDays(-11),
                            LastUpdateDate = DateTime.Now,
                            NumberOfReads = 500,
                            GenreId = technologyGenre.GenreId
                        },
                        new News
                        {
                            Title = "Sports Events",
                            ShortDescription = "Sports events are on the rise...",
                            Content = "Sports events are on the rise...",
                            ImageUrl = "https://miro.medium.com/v2/resize:fit:3296/1*TTO1f4XiPkveK2x8ct8SwQ.png",
                            PostDate = DateTime.Now.AddDays(-12),
                            LastUpdateDate = DateTime.Now,
                            NumberOfReads = 550,
                            GenreId = sportsGenre.GenreId
                        },
                        new News
                        {
                            Title = "Entertainment Events",
                            ShortDescription = "Entertainment events are on the rise...",
                            Content = "Entertainment events are on the rise...",
                            ImageUrl = "https://miro.medium.com/v2/resize:fit:3296/1*TTO1f4XiPkveK2x8ct8SwQ.png",
                            PostDate = DateTime.Now.AddDays(-13),
                            LastUpdateDate = DateTime.Now,
                            NumberOfReads = 600,
                            GenreId = entertainmentGenre.GenreId
                        },
                        new News
                        {
                            Title = "Science Advancements",
                            ShortDescription = "Science advancements are on the horizon...",
                            Content = "Science advancements are on the horizon...",
                            ImageUrl = "https://miro.medium.com/v2/resize:fit:3296/1*TTO1f4XiPkveK2x8ct8SwQ.png",
                            PostDate = DateTime.Now.AddDays(-14),
                            LastUpdateDate = DateTime.Now,
                            NumberOfReads = 650,
                            GenreId = scienceGenre.GenreId
                        },
                        new News
                        {
                            Title = "Astronomy Events",
                            ShortDescription = "Astronomy events are on the rise...",
                            Content = "Astronomy events are on the rise...",
                            ImageUrl = "https://miro.medium.com/v2/resize:fit:3296/1*TTO1f4XiPkveK2x8ct8SwQ.png",
                            PostDate = DateTime.Now.AddDays(-15),
                            LastUpdateDate = DateTime.Now,
                            NumberOfReads = 700,
                            GenreId = scienceGenre.GenreId
                        }
                    );
                    context.SaveChanges();
                }

            }
        }
    }
}



