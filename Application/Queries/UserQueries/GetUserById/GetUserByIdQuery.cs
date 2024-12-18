using Application.Models;
using Domain.Entities;
using MediatR;

namespace Application.Queries.UserQueries.GetUserById
{
    public class GetUserByIdQuery(Guid id) : IRequest<OperationResult<User>>
    {
        public Guid Id { get; set; } = id; 
    }
}
