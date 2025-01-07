using Application.Dtos.QuestionDtos;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.QuestionCommands.Add
{
    internal class AddQuestionCommandHandler(IGenericRepository<Question> repository, IMapper mapper) : IRequestHandler<AddQuestionCommand, OperationResult<GetQuestionDto>>
    {
        IGenericRepository<Question> _repository = repository;
        IMapper _mapper = mapper;

        public async Task<OperationResult<GetQuestionDto>> Handle(AddQuestionCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Request cannot be null.");
            }

            try
            {
                var questionToAdd = new Question
                {
                    Text = request.Dto.Text,
                    CorrectAnswerId = request.Dto.CorrectAnswerId,
                };

                var addedQuestion = await _repository.AddAsync(questionToAdd);
                var mappedQuestion = _mapper.Map<GetQuestionDto>(addedQuestion);

                return OperationResult<GetQuestionDto>.Success(mappedQuestion);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding new Question.", ex);
            }
        }
    }
}
