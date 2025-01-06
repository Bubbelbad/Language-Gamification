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
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetQuestionById(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateQuestion([FromBody]string addQuestionDto)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateQuestion([FromBody] string updateQuestionDto)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            throw new NotImplementedException();
        }
    }
}
