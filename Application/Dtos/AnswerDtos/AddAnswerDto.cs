using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.AnswerDtos
{
    public class AddAnswerDto
    {
        [Required]
        public string Text { get; set; }
        [Required]
        public int QuestionId { get; set; }
    }
}
