using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using MediatR;

namespace Application.Commands.AnswerCommands.Submit
{
    public class SubmitCommandHandler : IRequestHandler<SubmitCommand, OperationResult<bool>>
    {
        private readonly IGenericRepository<UserChallenge, Guid> _userChallengeRepository;
        private readonly IGenericRepository<Answer, int> _answerRepository;
        private readonly IGenericRepository<Score, int> _scoreRepository;

        public SubmitCommandHandler(IGenericRepository<UserChallenge, Guid> userChallengeRepository, IGenericRepository<Answer, int> answerRepository, IGenericRepository<Score, int> scoreRepository)
        {
            _userChallengeRepository = userChallengeRepository;
            _answerRepository = answerRepository;
            _scoreRepository = scoreRepository;
        }

        public async Task<OperationResult<bool>> Handle(SubmitCommand request, CancellationToken cancellationToken)
        {
           var userChallenge = await _userChallengeRepository.GetByIdAsync(request.UserChallengeId);
            if (userChallenge == null) 
            {
                return OperationResult<bool>.Failure("UserChallenge not found.");
            }

            var challenge = userChallenge.Challenge;
            if (challenge?.Questions == null || !challenge.Questions.Any())
            {
                return OperationResult<bool>.Failure("Challenge or questions not found.");
            }

            var currentQuestion = challenge.Questions
                .OrderBy(q => q.Id) 
                .Skip(userChallenge.CurrentQuestionIndex)
                .FirstOrDefault();

            if (currentQuestion == null)
            {
                return OperationResult<bool>.Failure("No current question available.");
            }

            var selectedAnswer = await _answerRepository.GetByIdAsync(request.SelectedAnswerId);
            if (selectedAnswer == null || selectedAnswer.QuestionId != currentQuestion.Id)
            {
                return OperationResult<bool>.Failure("Invalid answer selected.");
            }

            bool isCorrect = selectedAnswer.IsCorrect;
            userChallenge.Score = (userChallenge.Score ?? 0) + (isCorrect ? 1 : 0);

            if (!userChallenge.IsLastQuestion())
            {
                userChallenge.CurrentQuestionIndex++;
                await _userChallengeRepository.UpdateAsync(userChallenge);
            }

            else
            {
                userChallenge.CompletedAt = DateTime.UtcNow;

                var score = new Score
                {
                    UserId = userChallenge.UserId,
                    ChallengeId = userChallenge.ChallengeId,
                    Points = userChallenge.Score ?? 0,
                    CreatedAt = DateTime.UtcNow
                };

                await _scoreRepository.AddAsync(score);
                await _userChallengeRepository.UpdateAsync(userChallenge);
            }

            return OperationResult<bool>.Success(true, "Answer submitted successfully.");
        }
    }
}
