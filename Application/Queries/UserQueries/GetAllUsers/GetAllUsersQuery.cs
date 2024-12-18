using Application.Models;
using Domain.Entities;
using MediatR;

namespace Application.Queries.UserQueries.GetAllUsers
{
    public class GetAllUsersQuery() : IRequest<OperationResult<List<User>>>
    {

    }
}
