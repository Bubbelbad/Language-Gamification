using Application.Dtos.UserDtos;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Queries.UserQueries.GetAllUsers
{
    internal sealed class GetAllUsersQueryHandler(IGenericRepository<User> repository, IMapper mapper) : IRequestHandler<GetAllUsersQuery, OperationResult<List<GetUserDto>>>
    {
        private readonly IGenericRepository<User> _repository = repository;
        private readonly IMapper _mapper = mapper;
        private const string cacheKey = "allUsers";

        public async Task<OperationResult<List<GetUserDto>>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query), "Query cannot be null.");
            }

            try
            {
                var allUsersFromDatabase = await _repository.GetAllAsync();
                var mappedUsersFromDatabase = _mapper.Map<List<GetUserDto>>(allUsersFromDatabase);

                return OperationResult<List<GetUserDto>>.Success(mappedUsersFromDatabase);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving users from collection.", ex);
            }
        }
    }
}
