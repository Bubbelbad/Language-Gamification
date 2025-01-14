using Application.Dtos.UserChallengeDtos;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Queries.UserChallengeQueries.GetById
{
    public class GetUserChallengeByIdQueryHandler : IRequestHandler<GetUserChallengeByIdQuery, OperationResult<GetUserChallengeDto>>
    {
        private readonly IGenericRepository<UserChallenge, int> _repository;
        private readonly IMapper _mapper;

        public GetUserChallengeByIdQueryHandler(IGenericRepository<UserChallenge, int> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OperationResult<GetUserChallengeDto>> Handle(GetUserChallengeByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var userChallenge = await _repository.GetByIdAsync(request.Id);

                if (userChallenge == null)
                {
                    return OperationResult<GetUserChallengeDto>.Failure("User challenge does not exist.");
                }
                var mappedUserChallenge = _mapper.Map<GetUserChallengeDto>(userChallenge);
                return OperationResult<GetUserChallengeDto>.Success(mappedUserChallenge);
            }
            catch (Exception ex)
            {
                return OperationResult<GetUserChallengeDto>.Failure(ex.Message);
            }
        }
    }
}
