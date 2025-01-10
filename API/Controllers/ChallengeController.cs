﻿using Application.Commands.ChallengeCommands.Add;
using Application.Dtos.ChallengeDtos;
using Application.Queries.ChallengeQueries.GetAllChallenges;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChallengeController(IMediator mediator, ILogger<UserController> logger) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<UserController> _logger = logger;

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllChallenges()
        {
            _logger.LogInformation("Fetching all Challenges at {time}", DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
            try
            {
                var operationResult = await _mediator.Send(new GetAllChallengesQuery());
                _logger.LogInformation("Successfully retrieved all Challenges");
                return Ok(operationResult.Data);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all Challenges at {time}", DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetChallengeById(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody, Required] AddChallengeDto newChallenge)
        {
            _logger.LogInformation("Adding new Challenge {title}", newChallenge.Title);
            try
            {
                var operationResult = await _mediator.Send(new AddChallengeCommand(newChallenge));
                _logger.LogInformation("Challenge {title} added successfully", operationResult.Data.Title);
                return Ok(operationResult.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while adding new Challenge");
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
