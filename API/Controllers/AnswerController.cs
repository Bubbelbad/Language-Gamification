using MediatR;
using Microsoft.AspNetCore.Http;
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
            throw new NotImplementedException();
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
