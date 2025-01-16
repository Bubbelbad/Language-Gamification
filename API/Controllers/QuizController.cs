using Application.Queries.QuestionQueries.GetAll;
using Application.Queries.QuizQueries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
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
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("GetNextQuestion")]
        public async Task<IActionResult> GetNextQuestion(int userChallengeId, int currentQuestionIndex)
        {
            _logger.LogInformation("Fetching the next question for UserChallengeId: {userChallengeId} at {time}", userChallengeId, DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));

            try
            {
                var query = new GetNextQuestionQuery(userChallengeId, currentQuestionIndex);
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
        public async Task<IActionResult> SubmitAnswer(int questionId, int selectedAnswerId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("CompleteChallenge")]
        public async Task<IActionResult> CompleteChallenge(string userId, int challengeId, int score)
        {
            throw new NotImplementedException();
        }
    }
}
