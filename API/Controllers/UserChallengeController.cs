using Application.Dtos.QuestionDtos;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> CreateUserChallenge()
        {
            throw new NotImplementedException();
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
