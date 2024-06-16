using AutoMapper;
using Domain.Models;

namespace Infrastructure.Mappings;

public class DbMappings : Profile
{   
    public DbMappings()
    {
        CreateMap<UserEntity, User>().ReverseMap();
    }
}
