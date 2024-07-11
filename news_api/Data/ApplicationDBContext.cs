using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using news_api.model;

namespace news_api.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Genres
            modelBuilder.Entity<Genre>().HasData(
                new Genre { GenreId = 1, Name = "Technology" },
                new Genre { GenreId = 2, Name = "Health" },
                new Genre { GenreId = 3, Name = "Sports" },
                new Genre { GenreId = 4, Name = "Politics" },
                new Genre { GenreId = 5, Name = "Business" },
                new Genre { GenreId = 6, Name = "Science" },
                new Genre { GenreId = 7, Name = "Entertainment" }
            );

            

            // Seed data for News
            // modelBuilder.Entity<News>().HasData();

            // >> Set the types of on delete foreign keys
            // modelBuilder.Entity<News>()
            //     .HasOne(n => n.Genre)
            //     .WithMany(g => g.News)
            //     .HasForeignKey(n => n.GenreId)
            //     .OnDelete(DeleteBehavior.Cascade); // Use Cascade, SetNull, NoAction, Restrict, or SetDefault
        }
    }
}