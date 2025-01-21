using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using MediatR;

namespace Application.Commands.QuizCommands.StartChallenge
{
    internal sealed class StartChallengeCommandHandler(
        IGenericRepository<User, string> userRepository, 
        IGenericRepository<Challenge, int> challengeRepository,
        IGenericRepository<UserChallenge, int> userChallengeRepository) 
        : IRequestHandler<StartChallengeCommand, OperationResult<int>>
    {
        IGenericRepository<User, string> _userRepository = userRepository;
        IGenericRepository<Challenge, int> _challengeRepository = challengeRepository;
        IGenericRepository<UserChallenge, int> _userChallengeRepository = userChallengeRepository;

        public async Task<OperationResult<int>> Handle(StartChallengeCommand request, CancellationToken cancellationToken)
        {
            // Create UserChallenge? 
            var user = await _userRepository.GetByIdAsync(request.UserId);
            var challenge = await _challengeRepository.GetByIdAsync(request.ChallengeId);

            if (user == null)
            {
                return OperationResult<int>.Failure("User not found.");
            }

            else if (challenge == null)
            {
                return OperationResult<int>.Failure("Challenge not found.");
            }

            var userChallengeToAdd = new UserChallenge()
            {
                ChallengeId = request.ChallengeId,
                UserId = request.UserId,
                CurrentQuestionIndex = 0,
                Score = null!,
                CompletedAt = null!
            };

            var addedUserChallenge = await _userChallengeRepository.AddAsync(userChallengeToAdd);
            if (addedUserChallenge == null)
            {
                return OperationResult<int>.Failure("An error occurred while adding new Challenge.");
            }
            return OperationResult<int>.Success(addedUserChallenge.Id);
        }
    }
}
