using MediatR;
using Microsoft.AspNetCore.Mvc;

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
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetChallengeById(int id)
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
