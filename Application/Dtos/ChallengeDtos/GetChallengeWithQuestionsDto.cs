using Application.Dtos.QuestionDtos;

namespace Application.Dtos.ChallengeDtos
{
    public class GetChallengeWithQuestionsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<GetQuestionWithAnswersDto> Questions { get; set; }
    }
}
