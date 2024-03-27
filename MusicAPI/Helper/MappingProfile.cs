using AutoMapper;
using MusicAPI.Dto;
using MusicAPI.Models;

namespace MusicAPI.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Artist, ArtistDto>().ReverseMap();
            CreateMap<Song, SongDto>().ReverseMap();
            CreateMap<Genre, GenreDto>().ReverseMap();
        }
    }
}
