using Application.Dtos.AnswerDtos;
using Application.Dtos.ChallengeDtos;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Queries.ChallengeQueries.GetChallengeWithQuestions
{
    internal sealed class GetChallengeWithQuestionsQueryHandler : IRequestHandler<GetChallengeWithQuestionsQuery, OperationResult<GetChallengeWithQuestionsDto>>
    {
        private readonly IGenericRepository<Challenge, int> _challengeRepository;
        private readonly IGenericRepository<Question, int> _questionRepository;
        private readonly IMapper _mapper;

        public GetChallengeWithQuestionsQueryHandler(
            IGenericRepository<Challenge, int> challengeRepository, 
            IGenericRepository<Question, int> questionRepository, 
            IMapper mapper)
        {
            _challengeRepository = challengeRepository;
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<GetChallengeWithQuestionsDto>> Handle(GetChallengeWithQuestionsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var challenge = await _challengeRepository.GetByIdAsync(request.ChallengeId, c => c.Questions);

                if (challenge == null)
                {
                    return OperationResult<GetChallengeWithQuestionsDto>.Failure("Challenge not found");
                }

                var challengeDto = _mapper.Map<GetChallengeWithQuestionsDto>(challenge);

                foreach(var question in challengeDto.Questions)
                {
                    var questions = await _questionRepository.GetByIdAsync(question.Id, q => q.Answers);
                    var listedQuestions = questions.Answers.ToList();
                    question.Answers = _mapper.Map<List<GetSimpleAnswerDto>>(listedQuestions);
                }

                var mappedChallengeDto = _mapper.Map<GetChallengeWithQuestionsDto>(challengeDto);
                return OperationResult<GetChallengeWithQuestionsDto>.Success(mappedChallengeDto);
            }
            catch (Exception ex)
            {
                return OperationResult<GetChallengeWithQuestionsDto>.Failure(ex.Message);
            }
        }
    }
}
