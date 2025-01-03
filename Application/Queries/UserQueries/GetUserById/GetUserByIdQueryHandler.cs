using Application.Dtos.UserDtos;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;


namespace Application.Queries.UserQueries.GetUserById
{
    internal class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, OperationResult<GetUserDto>>
    {
        private readonly IGenericRepository<User> _repository;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IGenericRepository<User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OperationResult<GetUserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
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
                    return OperationResult<GetUserDto>.Failure("User does not exist.");
                }
                var mappedUser = _mapper.Map<GetUserDto>(user);
                return OperationResult<GetUserDto>.Success(mappedUser);
            }
            catch (Exception ex)
            {
                return OperationResult<GetUserDto>.Failure(ex.Message);
            }
        }
    }
}
