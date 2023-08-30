using AutoMapper;
using TaskThinkland.Api.DTOs.ProductDTOs;
using TaskThinkland.Api.DTOs.UserDTOs;
using TaskThinkland.Api.Entities;

namespace TaskThinkland.Api.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<User, CreateUserDto>().ReverseMap();
        CreateMap<UserDto, User>().ReverseMap();

        CreateMap<ProductDto, Product>().ReverseMap();
    }
}