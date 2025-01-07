using Application.Commands.QuestionCommands.Add;
using Application.Dtos.QuestionDtos;
using Application.Queries.QuestionQueries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<QuestionController> _logger;

        public QuestionController(IMediator mediator, ILogger<QuestionController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllQuestions()
        {
            try
            {
                var operationResult = await _mediator.Send(new GetAllQuestionsQuery());
                if (operationResult.IsSuccess)
                {
                    return Ok(operationResult.Data);
                }
                return BadRequest(new { message = operationResult.Message, errors = operationResult.ErrorMessage });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all Questions at {time}", DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                return BadRequest(ex.InnerException);
            }
        }

        [HttpGet]
        [Route("GetById{id}")]
        public async Task<IActionResult> GetQuestionById(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateQuestion([FromBody] AddQuestionDto dto)
        {
            try
            {
                var operationResult = await _mediator.Send(new AddQuestionCommand(dto));

                if (operationResult.IsSuccess)
                {
                    return Ok(operationResult.Data);
                }
                return BadRequest(new { message = operationResult.Message, errors = operationResult.ErrorMessage });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new Question at {time}", DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                return BadRequest(ex.InnerException);
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateQuestion([FromBody] string updateQuestionDto)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("Delete{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            throw new NotImplementedException();
        }
    }
}
