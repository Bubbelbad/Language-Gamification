using Application.Dtos.AnswerDtos;
using Application.Dtos.QuestionDtos;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Queries.QuizQueries
{
    public class GetNextQuestionQueryHandler : IRequestHandler<GetNextQuestionQuery, OperationResult<GetQuestionWithAnswersDto>>
    {
        private readonly IGenericRepository<Question, int> _questionRepository;
        private readonly IGenericRepository<UserChallenge, int> _userChallengeRepository;
        private readonly IGenericRepository<Challenge, int> _challengeRepository;
        private readonly IMapper _mapper;
        public GetNextQuestionQueryHandler(IGenericRepository<Question, int> questionRepository,
                                            IGenericRepository<UserChallenge, int> userChallengeRepository,
                                            IGenericRepository<Challenge, int> challengeRepository,
                                            IMapper mapper)
        {
            _questionRepository = questionRepository;
            _userChallengeRepository = userChallengeRepository;
            _challengeRepository = challengeRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<GetQuestionWithAnswersDto>> Handle(GetNextQuestionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var userChallenge = await _userChallengeRepository.GetByIdAsync(request.UserChallengeId);
                if (userChallenge == null)
                {
                    return OperationResult<GetQuestionWithAnswersDto>.Failure("The specified user challenge was not found.");
                }

                var challenge = await _challengeRepository.GetByIdAsync(userChallenge.ChallengeId, q => q.Questions);

                var challengeQuestions = challenge.Questions.ToList();
                var currentQuestion = challengeQuestions[userChallenge.CurrentQuestionIndex];

                var answers = _questionRepository.GetByIdAsync(currentQuestion.Id, q => q.Answers).Result.Answers.ToList();

                var mappedCurrentQuestion = _mapper.Map<GetQuestionWithAnswersDto>(currentQuestion);
                mappedCurrentQuestion.Answers = _mapper.Map<List<GetSimpleAnswerDto>>(answers);
                return OperationResult<GetQuestionWithAnswersDto>.Success(mappedCurrentQuestion);
            }
            catch (Exception ex)
            {
                return OperationResult<GetQuestionWithAnswersDto>.Failure($"An error occurred: {ex.Message}");
            }
        }
    }
}
