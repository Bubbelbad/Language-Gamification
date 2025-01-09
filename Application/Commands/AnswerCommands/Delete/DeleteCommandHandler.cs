using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using MediatR;

namespace Application.Commands.AnswerCommands.Delete
{
    public class DeleteCommandHandler : IRequestHandler<DeleteCommand, OperationResult<bool>>
    {
        private readonly IGenericRepository<Answer, int> _repository;
        public DeleteCommandHandler(IGenericRepository<Answer, int> repository)
        {
            _repository = repository;
        }
        public async Task<OperationResult<bool>> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deleted = await _repository.DeleteAsync(request.AnswerId);

                if (!deleted)
                {
                    return OperationResult<bool>.Failure("Answer not found or could not be deleted.");
                }

                return OperationResult<bool>.Success(true);
            }

            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while deleting the Answer.", ex);
            }
        }
    }
}
