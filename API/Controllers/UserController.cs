using Application.Commands.UserCommands.Delete;
using Application.Commands.UserCommands.Register;
using Application.Dtos.UserDtos;
using Application.Queries.UserQueries.GetAllUsers;
using Application.Queries.UserQueries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IMediator mediator, ILogger<UserController> logger) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<UserController> _logger = logger;

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllUsers()
        {
            _logger.LogInformation("Fetching all Users at {time}", DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
            try
            {
                var operationResult = await _mediator.Send(new GetAllUsersQuery());
                _logger.LogInformation("Successfully retrieved all Users");
                return Ok(operationResult.Data);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all Users at {time}", DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogWarning("Invalid input data");
                return BadRequest("Invalid input data.");
            }

            _logger.LogInformation("Fetching Users at {time}", DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
            try
            {
                var operationResult = await _mediator.Send(new GetUserByIdQuery(id));
                _logger.LogInformation("Successfully retrieved all Users");
                return Ok(operationResult.Data);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all Users at {time}", DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody, Required] RegisterUserDto newUser)
        {
            _logger.LogInformation("Adding new User {username}", newUser.UserName);
            try
            {
                var operationResult = await _mediator.Send(new RegisterCommand(newUser));
                _logger.LogInformation("User {username} added successfully", operationResult.Data.UserName);
                return Ok(operationResult.Data);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding new User");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("Deleting User with ID: {Id}", id);
            try
            {
                var operationResult = await _mediator.Send(new DeleteCommand(id));
                _logger.LogInformation("User {username} deleted successfully", operationResult);
                return Ok(operationResult.Data);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while Deleting User");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
