using Application.Commands.AnswerCommands.Add;
using Application.Dtos.AnswerDtos;
using Application.Dtos.UserDtos;
using Application.Queries.AnswerQueries.GetAllAnswers;
using Application.Queries.AnswerQueries.GetAnswerById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
        [Route("GetById")]
        public async Task<IActionResult> GetAnswerById(int id)
        {
            //if (id == Guid.Empty)
            //{
            //    _logger.LogWarning("Invalid input data");
            //    return BadRequest("Invalid input data.");
            //}

            _logger.LogInformation("Fetching Answer at {time}", DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));

            try
            {
                var operationResult = await _mediator.Send(new GetAnswerByIdQuery(id));
                _logger.LogInformation("Successfully retrieved Answer");
                return Ok(operationResult.Data);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching Answer at {time}", DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody, Required] AddAnswerDto newAnswer)
        {
            _logger.LogInformation("Adding new Answer {text}", newAnswer.Text);
            try
            {
                var operationResult = await _mediator.Send(new AddCommand(newAnswer));
                _logger.LogInformation("Answer {text} added successfully", operationResult.Data.Text);
                return Ok(operationResult.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while adding new Answer");
                return StatusCode(500, "An error occurred while processing your request.");
            }

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
