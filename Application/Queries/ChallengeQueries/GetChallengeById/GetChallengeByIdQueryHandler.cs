using Application.Dtos.ChallengeDtos;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Queries.ChallengeQueries.GetChallengeById
{
    public class GetChallengeByIdQueryHandler : IRequestHandler<GetChallengeByIdQuery, OperationResult<GetChallengeDto>>
    {
        private readonly IGenericRepository<Challenge, int> _repository;
        private readonly IMapper _mapper;

        public GetChallengeByIdQueryHandler(IGenericRepository<Challenge, int> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OperationResult<GetChallengeDto>> Handle(GetChallengeByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var challenge = await _repository.GetByIdAsync(request.Id);
                if (challenge == null)
                {
                    return OperationResult<GetChallengeDto>.Failure("Challenge does not exist.");
                }
                var mappedChallenge = _mapper.Map<GetChallengeDto>(challenge);
                return OperationResult<GetChallengeDto>.Success(mappedChallenge);
            }
            catch (Exception ex)
            {
                return OperationResult<GetChallengeDto>.Failure(ex.Message);

            }
        }
    }
}
