using Application.Queries.AnswerQueries.GetAllAnswers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController(IMediator mediator, ILogger<UserController> logger) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<UserController> _logger = logger;

        [HttpGet]
        [Route("GetAllAnswers")]
        public async Task<IActionResult> GetAllAnswers()
        {
            _logger.LogInformation("Fetching all Answers at {time}", DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
            try
            {
                var operationResult = await _mediator.Send(new GetAllAnswersQuery());
                _logger.LogInformation("Successfully retrieved all Answers");
                return Ok(operationResult.Data);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all Answers at {time}", DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        [Route("GetAnswerById")]
        public async Task<IActionResult> GetAnswerById(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add()
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update()
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}
