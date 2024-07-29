using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using news_api.DTOs;
using news_api.model;
using news_api.Models;

namespace news_api.Mapper
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<CreateUserDTO, User>();
            CreateMap<News, NewsDTO>()
                .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments));
            CreateMap<CreateNewsDTO, News>();
            CreateMap<UpdateNewsDTO, News>();
            CreateMap<Genre, GenreDTO>();
            CreateMap<CreateNewsWithImageDTO, News>();

            // CreateMap<Comment, CommentDTO>();
            // CreateMap<CreateCommentDTO, Comment>();
            // CreateMap<UpdateCommentDTO, Comment>();


            CreateMap<Comment, CommentDTO>().ReverseMap();
            CreateMap<Comment, CreateCommentDTO>().ReverseMap();
            CreateMap<Comment, UpdateCommentDTO>().ReverseMap();
        }
    }
}