using Application.Dtos.AnswerDtos;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Queries.AnswerQueries.GetAnswersByQuestionId
{
    public class GetAnswersByQuestionIdQueryHandler : IRequestHandler<GetAnswersByQuestionIdQuery, OperationResult<List<GetSimpleAnswerDto>>>
    {
        private readonly IGenericRepository<Question, int> _questionRepository;
        private readonly IGenericRepository<Answer, int> _answerRepository;
        private readonly IMapper _mapper;

        public GetAnswersByQuestionIdQueryHandler(
            IGenericRepository<Question, int> questionRepository, 
            IGenericRepository<Answer, int> answerRepository, 
            IMapper mapper)
        {
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<List<GetSimpleAnswerDto>>> Handle(GetAnswersByQuestionIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var question = await _questionRepository.GetByIdAsync(request.QuestionId, q => q.Answers);
                if (question == null)
                {
                    return OperationResult<List<GetSimpleAnswerDto>>.Failure("Question not found");

                }
                var answerDtos = _mapper.Map<List<GetSimpleAnswerDto>>(question.Answers);

                return OperationResult<List<GetSimpleAnswerDto>>.Success(answerDtos);
            }

            catch (Exception e)
            {
                return OperationResult<List<GetSimpleAnswerDto>>.Failure(e.Message);
            }
        }
    }
}
