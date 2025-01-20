using Application.Dtos.ScoreDtos;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.ScoreCommands.Add
{
    internal sealed class AddScoreCommandHandler(IGenericRepository<Score, int> repository, IMapper mapper) : IRequestHandler<AddScoreCommand, OperationResult<GetScoreDto>>
    {
        private readonly IGenericRepository<Score, int> _repository = repository;
        private readonly IMapper _mapper = mapper;
        public async Task<OperationResult<GetScoreDto>> Handle(AddScoreCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var scoreToAdd = new Score
                {
                    UserId = request.Dto.UserId,
                    ChallengeId = request.Dto.ChallengeId,
                    Points = request.Dto.Points,
                    CompletedAt = request.Dto.CompletedAt
                };

                var addedScore = await _repository.AddAsync(scoreToAdd);
                var mappedScore = _mapper.Map<GetScoreDto>(addedScore);

                return OperationResult<GetScoreDto>.Success(mappedScore);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding new Score", ex);
            }
        }
    }
}
