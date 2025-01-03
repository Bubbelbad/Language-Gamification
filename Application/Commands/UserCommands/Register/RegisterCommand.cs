using Application.Dtos.UserDtos;
using Application.Models;
using Domain.Entities;
using MediatR;

namespace Application.Commands.UserCommands.Register
{
    public class RegisterCommand(RegisterUserDto newUser) : IRequest<OperationResult<GetUserDto>>
    {
        public RegisterUserDto NewUser { get; set; } = newUser;
    }
}