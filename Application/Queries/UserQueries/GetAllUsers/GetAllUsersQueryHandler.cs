using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Queries.UserQueries.GetAllUsers
{
    internal sealed class GetAllUsersQueryHandler(IGenericRepository<User> repository, IMapper mapper) : IRequestHandler<GetAllUsersQuery, OperationResult<List<User>>>
    {
        private readonly IGenericRepository<User> _repository = repository;
        private readonly IMapper _mapper = mapper;
        private const string cacheKey = "allUsers";

        public async Task<OperationResult<List<User>>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query), "Query cannot be null.");
            }

            try
            {

                var allUsersFromDatabase = await _repository.GetAllAsync();
                var mappedUsersFromDatabase = _mapper.Map<List<User>>(allUsersFromDatabase);

                return OperationResult<List<User>>.Success(mappedUsersFromDatabase);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving users from collection.", ex);
            }
        }
    }
}
