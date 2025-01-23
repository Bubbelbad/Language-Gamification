using Application.Models;
using MediatR;

namespace Application.Commands.AnswerCommands.Submit
{
    public class SubmitCommand : IRequest<OperationResult<bool>>
    {
        public Guid UserChallengeId { get; set; }
        public int SelectedAnswerId { get; set; }

        public SubmitCommand (Guid userChallengeId, int selectedAnswerId)
        {
            UserChallengeId = userChallengeId;
            SelectedAnswerId = selectedAnswerId;
        }
    }
}
