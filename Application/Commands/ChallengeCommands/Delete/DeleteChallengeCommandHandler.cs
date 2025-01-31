using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using MediatR;

namespace Application.Commands.ChallengeCommands.Delete
{
    public sealed class DeleteChallengeCommandHandler(IGenericRepository<Challenge, int> repository) : IRequestHandler<DeleteChallengeCommand, OperationResult<bool>>
    {
        private readonly IGenericRepository<Challenge, int> _repository = repository;

        public async Task<OperationResult<bool>> Handle(DeleteChallengeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deleteSuccessful = await _repository.DeleteAsync(request.ChallengeId);
                if (!deleteSuccessful)
                {
                    return OperationResult<bool>.Failure("Operation failed");
                }
                return OperationResult<bool>.Success(deleteSuccessful);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while deleting the Challenge.", ex);
            }
            
        }
    }
}
