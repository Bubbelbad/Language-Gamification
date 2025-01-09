using Application.Dtos.AnswerDtos;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Queries.AnswerQueries.GetAllAnswers
{
    public class GetAllAnswersQueryHandler(IGenericRepository<Answer, int> repository, IMapper mapper) : IRequestHandler<GetAllAnswersQuery, OperationResult<List<GetAnswerDto>>>
    {
        private readonly IGenericRepository<Answer, int> _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<OperationResult<List<GetAnswerDto>>> Handle(GetAllAnswersQuery query, CancellationToken cancellationToken)
        {
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
