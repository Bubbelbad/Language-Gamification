using Application.Dtos.QuestionDtos;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Runtime.CompilerServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Queries.QuizQueries
{
    public class GetNextQuestionQueryHandler : IRequestHandler<GetNextQuestionQuery, OperationResult<GetQuestionDto>>
    {
        private readonly IGenericRepository<Question, int> _questionRepository;
        private readonly IGenericRepository<UserChallenge, int> _userChallengeRepository;
        private readonly IMapper _mapper;
        public GetNextQuestionQueryHandler(IGenericRepository<Question, int> questionRepository,
                                            IGenericRepository<UserChallenge, int> userChallengeRepository,
                                            IMapper mapper)
        {
            _questionRepository = questionRepository;
            _userChallengeRepository = userChallengeRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<GetQuestionDto>> Handle(GetNextQuestionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var userChallenge = await _userChallengeRepository.GetByIdAsync(request.UserChallengeId);
                if (userChallenge == null)
                {
                    return OperationResult<GetQuestionDto>.Failure("The specified user challenge was not found.");
                }

                var questions = userChallenge.Challenge.Questions.OrderBy(q => q.Id).ToList();
                if (request.CurrentQuestionIndex >= questions.Count)
                {
                    return OperationResult<GetQuestionDto>.Failure("No more questions are available for this challenge.");
                }

                var nextQuestion = questions[request.CurrentQuestionIndex];
                var questionDto = _mapper.Map<GetQuestionDto>(nextQuestion);

                return OperationResult<GetQuestionDto>.Success(questionDto, "Next question retrieved successfully.");
            }
            catch (Exception ex)
            {
                return OperationResult<GetQuestionDto>.Failure($"An error occurred: {ex.Message}");
            }
        }
    }
}
