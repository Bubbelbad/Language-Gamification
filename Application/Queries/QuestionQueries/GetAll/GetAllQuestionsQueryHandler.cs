using Application.Dtos.QuestionDtos;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Queries.QuestionQueries.GetAll
{
    internal class GetAllQuestionsQueryHandler(IGenericRepository<Question> repository, IMapper mapper) : IRequestHandler<GetAllQuestionsQuery, OperationResult<List<GetQuestionDto>>>
    {
        private readonly IGenericRepository<Question> _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<OperationResult<List<GetQuestionDto>>> Handle(GetAllQuestionsQuery query, CancellationToken cancellationToken)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query), "Query cannot be null.");
            }

            try
            {
                var allQuestionsFromDatabase = await _repository.GetAllAsync();
                var mappedQuestionsFromDatabase = _mapper.Map<List<GetQuestionDto>>(allQuestionsFromDatabase);

                return OperationResult<List<GetQuestionDto>>.Success(mappedQuestionsFromDatabase);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving questions from collection.", ex);
            }
        }
    }
}
