using Application.Commands.UserChallengeCommands.Add;
using Application.Dtos.UserChallengeDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserChallengeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<QuestionController> _logger;

        public UserChallengeController(IMediator mediator, ILogger<QuestionController> logger)
        {
            _mediator = mediator;
            _logger = logger;

        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllUserChallenges()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("GetById{id}")]
        public async Task<IActionResult> GetUserChallengeById(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateUserChallenge([FromBody] AddUserChallengeDto newUserChallenge)
        {
            _logger.LogInformation("Adding new UserChallenge {CompletedAt}", newUserChallenge.CompletedAt);
            try
            {
                var operationResult = await _mediator.Send(new AddUserChallengeCommand(newUserChallenge));
                //_logger.LogInformation("UserChallenge {id} added successfully", operationResult.Data.Id);
                return Ok(operationResult.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while adding new UserChallenge");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateUserChallenge()
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("Delete{id}")]
        public async Task<IActionResult> DeleteUserChallenge(int id)
        {
            throw new NotImplementedException();
        }
    }

}
