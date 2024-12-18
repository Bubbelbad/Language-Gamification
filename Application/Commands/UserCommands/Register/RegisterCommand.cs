using Application.Dtos.UserDtos;
using Application.Models;
using Domain.Entities;
using MediatR;

namespace Application.Commands.UserCommands.Register
{
    public class RegisterCommand(UserDto newUser) : IRequest<OperationResult<User>>
    {
        public UserDto NewUser { get; set; } = newUser;
    }
}