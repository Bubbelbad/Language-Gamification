using Application.Dtos.ChallengeDtos;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.ChallengeCommands.Add
{
    public class AddChallengeCommandHandler : IRequestHandler<AddChallengeCommand, OperationResult<GetChallengeDto>>
    {
        private readonly IGenericRepository<Challenge, int> _repository;
        private readonly IMapper _mapper;

        public AddChallengeCommandHandler(IGenericRepository<Challenge, int> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<OperationResult<GetChallengeDto>> Handle(AddChallengeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var challengeToCreate = _mapper.Map<Challenge>(request.Dto);

                var createdChallenge = await _repository.AddAsync(challengeToCreate);
                var mappedChallenge = _mapper.Map<GetChallengeDto>(createdChallenge);

                return OperationResult<GetChallengeDto>.Success(mappedChallenge);
            }

            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding new Challenge", ex);

            }
        }
    }
}
