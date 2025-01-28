using Application.Dtos.AnswerDtos;
using Application.Dtos.QuestionDtos;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Queries.QuestionQueries.GetQuestionsByChallengeId
{
    internal sealed class GetQuestionsByChallengeIdQueryHandler : IRequestHandler<GetQuestionsByChallengeIdQuery, OperationResult<List<GetQuestionWithAnswersDto>>>
    {
        private readonly IGenericRepository<Challenge, int> _challengeRepository;
        private readonly IGenericRepository<Answer, int> _answernRepository;
        private readonly IMapper _mapper;

        public GetQuestionsByChallengeIdQueryHandler(
            IGenericRepository<Challenge, int> challengeRepository, 
            IGenericRepository<Answer, int> answernRepository,
            IMapper mapper)
        {
            _challengeRepository = challengeRepository;
            _answernRepository = answernRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<List<GetQuestionWithAnswersDto>>> Handle(GetQuestionsByChallengeIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var challenge = await _challengeRepository.GetByIdAsync(request.ChallengeId, c => c.Questions);

                if (challenge == null)
                {
                    return OperationResult<List<GetQuestionWithAnswersDto>>.Failure("Challenge not found");
                }

                var questionDtos = _mapper.Map<List<GetQuestionWithAnswersDto>>(challenge.Questions);

                var answers = await _answernRepository.GetAllAsync();
                foreach (var questionDto in questionDtos)
                {
                    var questionAnswers = answers.Where(a => a.QuestionId == questionDto.Id);
                    questionDto.Answers = _mapper.Map<List<GetSimpleAnswerDto>>(questionAnswers.ToList());
                }

                return OperationResult<List<GetQuestionWithAnswersDto>>.Success(questionDtos);
            }
            catch (Exception e)
            {
                return OperationResult<List<GetQuestionWithAnswersDto>>.Failure(e.Message);
            }
        }
    }
}
