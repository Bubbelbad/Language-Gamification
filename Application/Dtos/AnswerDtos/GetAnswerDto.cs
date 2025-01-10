namespace Application.Dtos.AnswerDtos
{
    public class GetAnswerDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int QuestionId { get; set; }
        public bool IsCorrect { get; set; }
    }
}
