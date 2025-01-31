using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using MediatR;

namespace Application.Commands.QuestionCommands.Delete
{
    public sealed class DeleteQuestionCommandHandler(IGenericRepository<Question, int> repository) : IRequestHandler<DeleteQuestionCommand, OperationResult<bool>>
    {
        private readonly IGenericRepository<Question, int> _repository = repository;

        public async Task<OperationResult<bool>> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
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
                throw new ApplicationException("An error occurred while adding new Question.", ex);
            }
        }
    }
}
