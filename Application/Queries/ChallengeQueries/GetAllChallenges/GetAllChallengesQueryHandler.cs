using Application.Dtos.ChallengeDtos;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Queries.ChallengeQueries.GetAllChallenges
{
    public class GetAllChallengesQueryHandler(IGenericRepository<Challenge, int> repository, IMapper mapper) : IRequestHandler<GetAllChallengesQuery, OperationResult<List<GetChallengeDto>>>
    {
        private readonly IGenericRepository<Challenge, int> _repository = repository;
        private readonly IMapper _mapper = mapper;
        public async Task<OperationResult<List<GetChallengeDto>>> Handle(GetAllChallengesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var allChallengesFromDatabase = await _repository.GetAllAsync();
                var mappedChallengesFromDatabase = _mapper.Map<List<GetChallengeDto>>(allChallengesFromDatabase);

                return OperationResult<List<GetChallengeDto>>.Success(mappedChallengesFromDatabase);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving challenges from collection.", ex);
            }
        }
    }
}
