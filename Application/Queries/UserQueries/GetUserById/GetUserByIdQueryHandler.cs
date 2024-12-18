using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;


namespace Application.Queries.UserQueries.GetUserById
{
    internal class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, OperationResult<User>>
    {
        private readonly IGenericRepository<User> _repository;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IGenericRepository<User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OperationResult<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                throw new ArgumentException("Invalid user id");
            }
            try
            {
                User user = await _repository.GetByIdAsync(request.Id);
                if (user == null)
                {
                    return OperationResult<User>.Failure("User does not exist.");
                }
                var mappedUser = _mapper.Map<User>(user);
                return OperationResult<User>.Success(mappedUser);
            }
            catch (Exception ex)
            {
                return OperationResult<User>.Failure(ex.Message);
            }
        }
    }
}
