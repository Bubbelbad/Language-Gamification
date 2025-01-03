using Application.Dtos.UserDtos;
using Application.Models;
using Domain.Entities;
using MediatR;

namespace Application.Queries.UserQueries.GetUserById
{
    public class GetUserByIdQuery(Guid id) : IRequest<OperationResult<GetUserDto>>
    {
        public Guid Id { get; set; } = id; 
    }
}
