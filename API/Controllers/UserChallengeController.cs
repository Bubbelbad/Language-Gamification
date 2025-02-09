﻿using Application.Commands.UserChallengeCommands.Add;
using Application.Commands.UserChallengeCommands.Delete;
using Application.Commands.UserChallengeCommands.Update;
using Application.Dtos.UserChallengeDtos;
using Application.Queries.UserChallengeQueries.GetAll;
using Application.Queries.UserChallengeQueries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
            try
            {
                var operationResult = await _mediator.Send(new GetAllUserChallengesQuery());
                if (operationResult.IsSuccess)
                {
                    return Ok(operationResult.Data);
                }
                return BadRequest(new { message = operationResult.Message, errors = operationResult.ErrorMessage });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all User Challenges at {time}", DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                return BadRequest(ex.InnerException);
            }
        }

        [HttpGet]
        [Route("GetById{id}")]
        public async Task<IActionResult> GetUserChallengeById(int id)
        {
            try
            {
                var operationResult = await _mediator.Send(new GetUserChallengeByIdQuery(id));
                if (operationResult.IsSuccess)
                {
                    return Ok(operationResult.Data);
                }
                return BadRequest(new { message = operationResult.Message, errors = operationResult.ErrorMessage });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching user challenge at {time}", DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                return BadRequest(ex.InnerException);
            }
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
        public async Task<IActionResult> UpdateUserChallenge(UpdateUserChallengeDto dto)
        {
            _logger.LogInformation("Updating UserChallenge {id}", dto.Id);
            try
            {
                var operationResult = await _mediator.Send(new UpdateUserChallengeCommand(dto));
                if (operationResult.IsSuccess)
                {
                    _logger.LogInformation("User Challenge {id} updated successfully", dto.Id);
                    return Ok(operationResult.Data);
                }
                return BadRequest(operationResult.ErrorMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while updating User Challenge {id}", dto.Id);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete]
        [Route("Delete{id}")]
        public async Task<IActionResult> DeleteUserChallenge(int id)
        {
            try
            {
                var operationResult = await _mediator.Send(new DeleteUserChallengeCommand(id));
                if (operationResult.IsSuccess)
                {
                    return Ok(operationResult.Message);
                }
                return BadRequest(new { message = operationResult.Message, errors = operationResult.ErrorMessage });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting a user challenge at {time}", DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                return BadRequest(ex.InnerException);
            }
        }
    }

}
