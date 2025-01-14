using Application.Dtos.UserChallengeDtos;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Queries.UserChallengeQueries.GetAll
{
    public class GetAllUserChallengesQueryHandler : IRequestHandler<GetAllUserChallengesQuery, OperationResult<List<GetUserChallengeDto>>>
    {
        private readonly IGenericRepository<UserChallenge, int> _repository;
        private readonly IMapper _mapper;

        public GetAllUserChallengesQueryHandler(IGenericRepository<UserChallenge, int> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<OperationResult<List<GetUserChallengeDto>>> Handle(GetAllUserChallengesQuery query, CancellationToken cancellationToken)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query), "Query cannot be null.");
            }
            try
            {
                var allUserChallengesFromDatabase = await _repository.GetAllAsync();
                var mappedUserChallengesFromDatabase = _mapper.Map<List<GetUserChallengeDto>>(allUserChallengesFromDatabase);
                return OperationResult<List<GetUserChallengeDto>>.Success(mappedUserChallengesFromDatabase);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving user challenges from collection.", ex);
            }
        }
    }
}
    
    

