using Application.Dtos.AnswerDtos;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.AnswerCommands.Update
{
    public class UpdateCommandHandler : IRequestHandler<UpdateCommand, OperationResult<GetAnswerDto>>
    {
        private readonly IGenericRepository<Answer, int> _repository;
        private readonly IMapper _mapper;

        public UpdateCommandHandler(IGenericRepository<Answer, int> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OperationResult<GetAnswerDto>> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Answer answerToUpdate = new()
                {
                    Id = request.NewAnswer.Id,
                    Text = request.NewAnswer.Text,
                    QuestionId = request.NewAnswer.QuestionId
                };

                var updatedAnswer = await _repository.UpdateAsync(answerToUpdate);
                var mappedAnswer = _mapper.Map<GetAnswerDto>(updatedAnswer);

                return OperationResult<GetAnswerDto>.Success(mappedAnswer);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating Answer.", ex);
            }
        }
    }
}
