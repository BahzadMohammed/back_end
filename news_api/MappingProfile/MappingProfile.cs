using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using news_api.DTOs;
using news_api.model;

namespace news_api.Mapper
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<CreateUserDTO, User>();
            CreateMap<News, NewsDTO>()
                .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<CreateNewsDTO, News>();
            CreateMap<UpdateNewsDTO, News>();
            CreateMap<Genre, GenreDTO>();
            CreateMap<CreateNewsWithImageDTO, News>();
            // CreateMap<UpdateNumberOfReadsDTO, News>();
        }
    }
}