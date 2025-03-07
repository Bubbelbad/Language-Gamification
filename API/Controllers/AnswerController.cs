﻿using Application.Commands.AnswerCommands.Add;
using Application.Commands.AnswerCommands.Delete;
using Application.Commands.AnswerCommands.Update;
using Application.Dtos.AnswerDtos;
using Application.Queries.AnswerQueries.GetAllAnswers;
using Application.Queries.AnswerQueries.GetAnswerById;
using Application.Queries.AnswerQueries.GetAnswersByQuestionId;
using Application.Queries.QuestionQueries.GetById;
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
        [Route("GetAll")]
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

        [HttpGet]
        [Route("GetAnswersByQuestionId{id}")]
        public async Task<IActionResult> GetAnswersByQuestionId(int id)
        {
            try
            {
                var operationResult = await _mediator.Send(new GetAnswersByQuestionIdQuery(id));
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
        public async Task<IActionResult> Update([FromBody, Required] UpdateAnswerDto answerToUpdate)
        {
            try
            {
                var operationResult = await _mediator.Send(new UpdateCommand(answerToUpdate));
                if (operationResult.IsSuccess)
                {
                    return Ok(operationResult.Data);
                }

                return BadRequest(new { message = operationResult.Message, errors = operationResult.ErrorMessage });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating AnswerId at {time}", DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting Answer with ID: {Id}", id);

            try
            {
                var operationResult = await _mediator.Send(new DeleteCommand(id));

                if (!operationResult.IsSuccess)
                {
                    _logger.LogWarning("Failed to delete Answer with ID: {Id}. Reason: {Reason}", id, operationResult.ErrorMessage);
                    return NotFound(operationResult.ErrorMessage);
                }

                _logger.LogInformation("Answer with ID {Id} deleted successfully.", id);
                return Ok(new { Message = "Answer deleted successfully." });
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the Answer with ID: {Id}", id);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

    }
}
