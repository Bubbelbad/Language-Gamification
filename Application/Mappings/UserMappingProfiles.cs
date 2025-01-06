using Domain.Entities;
using AutoMapper;
using Application.Dtos.UserDtos;
using Application.Commands.UserCommands.Register;

namespace Application.Mappings
{
    public class UserMappingProfiles : Profile
    {
        public UserMappingProfiles()
        {
            CreateMap<User, GetUserDto>();
            CreateMap<RegisterUserDto, GetUserDto>();
            CreateMap<RegisterUserDto, User>();
        }
    }
}
    