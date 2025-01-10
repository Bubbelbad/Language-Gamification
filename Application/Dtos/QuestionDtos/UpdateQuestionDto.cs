namespace Application.Dtos.QuestionDtos
{
    public class UpdateQuestionDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int ChallengeId { get; set; }
    }
}
