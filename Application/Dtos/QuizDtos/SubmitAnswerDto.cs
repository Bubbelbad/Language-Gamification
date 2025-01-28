using Application.Dtos.ScoreDtos;

namespace Application.Dtos.QuizDtos
{
    public class SubmitAnswerDto
    {
        public bool IsCorrect { get; set; }
        public GetScoreDto? Score { get; set; }
    }
}
