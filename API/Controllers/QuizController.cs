using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<QuestionController> _logger;

        public QuizController(IMediator mediator, ILogger<QuestionController> logger)
        {
            _mediator = mediator;
            _logger = logger;

        }

        [HttpPost]
        [Route("StartChallenge")]
        public async Task<IActionResult> StartChallenge(int challengeId, Guid userId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("GetNextQuestion")]
        public async Task<IActionResult> GetNextQuestion(int userChallengeId)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("SubmitAnswer")]
        public async Task<IActionResult> SubmitAnswer(int questionId, int selectedAnswerId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("CompleteChallenge")]
        public async Task<IActionResult> CompleteChallenge(string userId, int challengeId, int score)
        {
            throw new NotImplementedException();
        }
    }
}
