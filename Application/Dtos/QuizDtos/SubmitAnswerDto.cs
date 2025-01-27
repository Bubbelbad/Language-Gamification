using Domain.Entities;

namespace Application.Dtos.QuizDtos
{
    public class SubmitAnswerDto
    {
        public bool IsCorrect { get; set; }
        public Score? Score { get; set; }
    }
}
