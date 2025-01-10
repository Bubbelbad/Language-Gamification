namespace Application.Dtos.AnswerDtos
{
    public class AddAnswerDto
    {
        public string Text { get; set; }
        public int QuestionId { get; set; }
        public bool IsCorrect { get; set; }
    }
}
