﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using news_api.Data;

#nullable disable

namespace news_api.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20240711094822_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("news_api.model.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GenreId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("GenreId");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            GenreId = 1,
                            Name = "Technology"
                        },
                        new
                        {
                            GenreId = 2,
                            Name = "Health"
                        },
                        new
                        {
                            GenreId = 3,
                            Name = "Sports"
                        },
                        new
                        {
                            GenreId = 4,
                            Name = "Politics"
                        },
                        new
                        {
                            GenreId = 5,
                            Name = "Business"
                        },
                        new
                        {
                            GenreId = 6,
                            Name = "Science"
                        },
                        new
                        {
                            GenreId = 7,
                            Name = "Entertainment"
                        });
                });

            modelBuilder.Entity("news_api.model.News", b =>
                {
                    b.Property<int>("NewsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NewsId"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("LastUpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("NumberOfReads")
                        .HasColumnType("int");

                    b.Property<DateTime>("PostDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("NewsId");

                    b.HasIndex("GenreId");

                    b.ToTable("News");
                });

            modelBuilder.Entity("news_api.model.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("news_api.model.News", b =>
                {
                    b.HasOne("news_api.model.Genre", "Genre")
                        .WithMany("News")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("news_api.model.Genre", b =>
                {
                    b.Navigation("News");
                });
#pragma warning restore 612, 618
        }
    }
}
