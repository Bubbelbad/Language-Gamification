using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using MediatR;

namespace Application.Commands.UserCommands.Delete
{
    public class DeleteCommandHandler : IRequestHandler<DeleteCommand, OperationResult<bool>>
    {
        private readonly IGenericRepository<User, Guid> _repository;

        public DeleteCommandHandler(IGenericRepository<User, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult<bool>> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deleted = await _repository.DeleteAsync(request.UserId);

                if (!deleted)
                {
                    return OperationResult<bool>.Failure("User not found or could not be deleted.");
                }

                return OperationResult<bool>.Success(true);

            }

            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while deleting the User.", ex);
            }
        }
    }
}
