using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using MediatR;

namespace Application.Commands.UserChallengeCommands.Delete
{
    public class DeleteUserChallengeCommandHandler : IRequestHandler<DeleteUserChallengeCommand, OperationResult<bool>>
    {
        private readonly IGenericRepository<UserChallenge, int> _repository;
        public DeleteUserChallengeCommandHandler(IGenericRepository<UserChallenge, int> repository)
        {
            _repository = repository;
        }
        public async Task<OperationResult<bool>> Handle(DeleteUserChallengeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deleteSuccessful = await _repository.DeleteAsync(request.Id);
                if (deleteSuccessful is false)
                {
                    return OperationResult<bool>.Failure("Operation failed");
                }
                return OperationResult<bool>.Success(deleteSuccessful);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while deleting user challenge.", ex);
            }
        }
    }
}
