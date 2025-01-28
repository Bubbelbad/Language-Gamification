using Application.Commands.QuizCommands.StartChallenge;
using Application.Commands.QuizCommands.SubmitAnswer;
using Application.Queries.QuizQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<QuestionController> _logger;

        public QuizController(IMediator mediator, ILogger<QuestionController> logger)
        {
            _mediator = mediator;
            _logger = logger;

        }

        [HttpPost]
        [Route("StartChallenge")]
        public async Task<IActionResult> StartChallenge(int challengeId, Guid userId)
        {
            _logger.LogInformation("Starting a new challenge for UserId: {userId} at {time}", userId, DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
            try
            {
                var command = new StartChallengeCommand(challengeId, userId.ToString());
                var operationResult = await _mediator.Send(command);
                if (!operationResult.IsSuccess)
                {
                    return BadRequest(new { message = operationResult.Message, errors = operationResult.ErrorMessage });
                }
                return Ok(operationResult.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        [Route("GetNextQuestion")]
        public async Task<IActionResult> GetNextQuestion(int userChallengeId)
        {
            _logger.LogInformation("Fetching the next question for UserChallengeId: {userChallengeId} at {time}", userChallengeId, DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));

            try
            {
                var query = new GetNextQuestionQuery(userChallengeId);
                var operationResult = await _mediator.Send(query);

                if (!operationResult.IsSuccess)
                {
                    _logger.LogWarning("Failed to fetch next question for UserChallengeId: {userChallengeId}. Reason: {reason}", userChallengeId, operationResult.ErrorMessage);
                    return BadRequest(new { message = operationResult.Message, errors = operationResult.ErrorMessage });
                }

                _logger.LogInformation("Successfully fetched the next question for UserChallengeId: {userChallengeId}", userChallengeId);
                return Ok(operationResult.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the next question for UserChallengeId: {userChallengeId} at {time}", userChallengeId, DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPut]
        [Route("SubmitAnswer")]
        public async Task<IActionResult> SubmitAnswer(int userChallengeId, int selectedAnswerId)
        {
            try
            {
                var command = new SubmitAnswerCommand(userChallengeId, selectedAnswerId);
                var operationResult = await _mediator.Send(command);

                if (!operationResult.IsSuccess)
                {
                    return BadRequest(new { message = operationResult.Message, errors = operationResult.ErrorMessage });
                }

                return Ok(operationResult.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
