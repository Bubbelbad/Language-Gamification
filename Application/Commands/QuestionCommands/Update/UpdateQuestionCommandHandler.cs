using Application.Dtos.QuestionDtos;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.QuestionCommands.Update
{
    internal class UpdateQuestionCommandHandler(IGenericRepository<Question, int> repository, IMapper mapper) : IRequestHandler<UpdateQuestionCommand, OperationResult<GetQuestionDto>>
    {
        private readonly IGenericRepository<Question, int> _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<OperationResult<GetQuestionDto>> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var questionToUpdate = new Question
                {
                    Id = request.Dto.Id,
                    Text = request.Dto.Text,
                    CorrectAnswerId = request.Dto.CorrectAnswerId,
                };

                var updatedQuestion = await _repository.UpdateAsync(questionToUpdate);
                if (updatedQuestion is null)
                {
                    return OperationResult<GetQuestionDto>.Failure("Operation failed");
                }
                var mappedQuestion = _mapper.Map<GetQuestionDto>(updatedQuestion);

                return OperationResult<GetQuestionDto>.Success(mappedQuestion);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while updating Question.", ex);
            }
        }
    }
}
