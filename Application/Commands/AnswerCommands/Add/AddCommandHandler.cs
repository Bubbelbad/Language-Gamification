using Application.Dtos.AnswerDtos;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.AnswerCommands.Add
{
    public class AddCommandHandler : IRequestHandler<AddCommand, OperationResult<GetAnswerDto>>
    {
        private readonly IGenericRepository<Answer, int> _repository;
        private readonly IMapper _mapper;

        public AddCommandHandler(IGenericRepository<Answer, int> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OperationResult<GetAnswerDto>> Handle(AddCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var answerToCreate = _mapper.Map<Answer>(request.NewAnswer);

                var createdAnswer = await _repository.AddAsync(answerToCreate);
                var mappedAnswer = _mapper.Map<GetAnswerDto>(createdAnswer);

                return OperationResult<GetAnswerDto>.Success(mappedAnswer);


            }

            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding new Answer.", ex);
            }
        }
    }
}
