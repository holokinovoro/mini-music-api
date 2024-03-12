using AutoMapper;
using MusicAPI.Dto;
using MusicAPI.Models;

namespace MusicAPI.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<Artist, ArtistDto>();
            CreateMap<Song, SongDto>();
            CreateMap<Genre, GenreDto>();
        }
    }
}
