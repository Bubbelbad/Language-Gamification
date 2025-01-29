using Application.Dtos.QuizDtos;
using Application.Dtos.ScoreDtos;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.QuizCommands.SubmitAnswer
{
    internal sealed class SubmitAnswerCommandHandler : IRequestHandler<SubmitAnswerCommand, OperationResult<SubmitAnswerDto>>
    {
        private readonly IGenericRepository<Answer, int> _answerRepository;
        private readonly IGenericRepository<UserChallenge, int> _userChallengeRepository;
        private readonly IGenericRepository<Challenge, int> _challengeRepository;
        private readonly IGenericRepository<User, string> _userRepository;
        private readonly IGenericRepository<Score, int> _scoreRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper; 

        public SubmitAnswerCommandHandler(
            IGenericRepository<Answer, int> answerRepository, 
            IGenericRepository<UserChallenge, int> userChallengeRepository, 
            IGenericRepository<Challenge, int> challengeRepository, 
            IGenericRepository<User, string> userRepository, 
            IGenericRepository<Score, int> scoreRepository, 
            IMediator mediator, 
            IMapper mapper)
        {
            _answerRepository = answerRepository;
            _userChallengeRepository = userChallengeRepository;
            _challengeRepository = challengeRepository;
            _userRepository = userRepository;
            _scoreRepository = scoreRepository;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<OperationResult<SubmitAnswerDto>> Handle(SubmitAnswerCommand request, CancellationToken cancellationToken)
        {
            var isCorrect = false;

            try
            {
                // Get the user challenge
                var userChallenge = await _userChallengeRepository.GetByIdAsync(request.UserChallengeId, u => u.Challenge);
                if (userChallenge == null)
                {
                    return OperationResult<SubmitAnswerDto>.Failure("The specified user challenge was not found.");
                }

                // Get the answer
                var answer = await _answerRepository.GetByIdAsync(request.AnswerId);
                if (answer == null)
                {
                    return OperationResult<SubmitAnswerDto>.Failure("The specified answer was not found.");
                }

                // Get the challenge and include the questions
                var challenge = await _challengeRepository.GetByIdAsync(userChallenge.ChallengeId, c => c.Questions);

                // Check if the answer is correct
                if (answer.IsCorrect)
                {
                    userChallenge.Score = (userChallenge.Score ?? 0) + 1;
                }

                // Check if the user has answered the last question, if so, save the score
                GetScoreDto? scoreDto = null;
                var lastQuestion = challenge.IsLastQuestion(userChallenge.CurrentQuestionIndex);
                if (lastQuestion)
                {
                    var score = new Score
                    {
                        UserId = userChallenge.UserId,
                        ChallengeId = userChallenge.ChallengeId,
                        Points = userChallenge.Score ?? 0,
                        CompletedAt = DateTime.Now,
                        Challenge = userChallenge.Challenge,
                        User = userChallenge.User
                    };

                    await _scoreRepository.AddAsync(score);

                    scoreDto = new GetScoreDto
                    {
                        Id = score.Id,
                        UserId = score.UserId,
                        ChallengeId = score.ChallengeId,
                        Points = score.Points,
                        CompletedAt = score.CompletedAt
                    };

                    var user = await _userRepository.GetByIdAsync(userChallenge.UserId);
                    var userScores = await _userRepository.GetByIdAsync(userChallenge.UserId, u => u.Scores);

                    var totalScore = 0;
                    foreach (var userScore in userScores.Scores)
                    {
                        totalScore += userScore.Points;
                    }

                    user.TotalPoints = totalScore;
                    await _userRepository.UpdateAsync(user);
                }

                else
                {
                    userChallenge.CurrentQuestionIndex++;
                }

                await _userChallengeRepository.UpdateAsync(userChallenge);

                var submitAnswerDto = new SubmitAnswerDto
                {
                    IsCorrect = answer.IsCorrect,
                    Score = scoreDto
                };

                var mappedSubmitAnswerDto = _mapper.Map<SubmitAnswerDto>(submitAnswerDto);
                return OperationResult<SubmitAnswerDto>.Success(mappedSubmitAnswerDto);
            }
            catch (Exception ex)
            {
                return OperationResult<SubmitAnswerDto>.Failure(ex.Message);
            }
        }
    }
}
