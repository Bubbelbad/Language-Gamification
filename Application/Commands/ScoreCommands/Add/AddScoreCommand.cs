using Application.Dtos.ScoreDtos;
using Application.Models;
using MediatR;

namespace Application.Commands.ScoreCommands.Add
{
    public sealed class AddScoreCommand(AddScoreDto dto) : IRequest<OperationResult<GetScoreDto>>
    {
        public AddScoreDto Dto { get; set; } = dto;
    }
}
