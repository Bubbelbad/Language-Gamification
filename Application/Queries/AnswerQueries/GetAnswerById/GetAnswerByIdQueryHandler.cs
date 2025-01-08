using Application.Dtos.AnswerDtos;
using Application.Dtos.UserDtos;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.AnswerQueries.GetAnswerById
{
    public class GetAnswerByIdQueryHandler : IRequestHandler<GetAnswerByIdQuery, OperationResult<GetAnswerDto>>
    {
        private readonly IGenericRepository<Answer, int> _repository;
        private readonly IMapper _mapper;

        public GetAnswerByIdQueryHandler(IGenericRepository<Answer, int> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OperationResult<GetAnswerDto>> Handle(GetAnswerByIdQuery request, CancellationToken cancellationToken)
        {

            try
            {
                Answer answer = await _repository.GetByIdAsync(request.Id);
                if (answer == null)
                {
                    return OperationResult<GetAnswerDto>.Failure("Answer does not exist.");
                }
                var mappedAnswer = _mapper.Map<GetAnswerDto>(answer);
                return OperationResult<GetAnswerDto>.Success(mappedAnswer);
            }
            catch (Exception ex)
            {
                return OperationResult<GetAnswerDto>.Failure(ex.Message);

            }
        }
    }
}
