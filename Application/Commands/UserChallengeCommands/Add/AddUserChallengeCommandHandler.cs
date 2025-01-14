using Application.Dtos.UserChallengeDtos;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.UserChallengeCommands.Add
{
    public class AddUserChallengeCommandHandler : IRequestHandler<AddUserChallengeCommand, OperationResult<GetUserChallengeDto>>
    {
        private readonly IGenericRepository<UserChallenge, int> _repository;
        private readonly IMapper _mapper;

        public AddUserChallengeCommandHandler(IGenericRepository<UserChallenge, int> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OperationResult<GetUserChallengeDto>> Handle(AddUserChallengeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userChallengeToCreate = _mapper.Map<UserChallenge>(request.Dto);

                var createdUserChallenge = await _repository.AddAsync(userChallengeToCreate);
                var mappedUserChallenge = _mapper.Map<GetUserChallengeDto>(createdUserChallenge);

                return OperationResult<GetUserChallengeDto>.Success(mappedUserChallenge);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding new UserChallenge.", ex);
            }
        }
    }
}
