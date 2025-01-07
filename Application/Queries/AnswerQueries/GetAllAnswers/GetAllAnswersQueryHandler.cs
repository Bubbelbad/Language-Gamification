using Application.Dtos.AnswerDtos;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Queries.AnswerQueries.GetAllAnswers
{
    internal sealed class GetAllAnswersQueryHandler(IGenericRepository<Answer> repository, IMapper mapper) : IRequestHandler<GetAllAnswersQuery, OperationResult<List<GetAnswerDto>>>
    {
        private readonly IGenericRepository<Answer> _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<OperationResult<List<GetAnswerDto>>> Handle(GetAllAnswersQuery query, CancellationToken cancellationToken)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query), "Query cannot be null.");
            }

            try
            {
                var allAnswersFromDatabase = await _repository.GetAllAsync();
                var mappedAnswersFromDatabase = _mapper.Map<List<GetAnswerDto>>(allAnswersFromDatabase);

                return OperationResult<List<GetAnswerDto>>.Success(mappedAnswersFromDatabase);
            }

            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving answers from collection.", ex);
            }
        }
    }
}
