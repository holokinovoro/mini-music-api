using Application.Dto;
using AutoMapper;
using Domain.Models;

namespace Application.Mappings;

public class DtoMap : Profile
{
    public DtoMap()
    {
        CreateMap<User, UserDto>().ReverseMap();
    }
}
