using Application.Dtos.QuestionDtos;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Queries.QuestionQueries.GetById
{
    internal sealed class GetQuestionByIdQueryHandler(IGenericRepository<Question,int> repository, IMapper mapper) : IRequestHandler<GetQuestionByIdQuery, OperationResult<GetQuestionDto>>
    {
        private readonly IGenericRepository<Question,int> _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<OperationResult<GetQuestionDto>> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
        {
            var question = await _repository.GetByIdAsync(request.Id);
            if (question == null)
            {
                return OperationResult<GetQuestionDto>.Failure("Question not found");
            }

            var questionDto = _mapper.Map<GetQuestionDto>(question);
            return OperationResult<GetQuestionDto>.Success(questionDto);
        }
    }
}
