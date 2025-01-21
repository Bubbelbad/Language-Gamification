using Application.Dtos.AnswerDtos;
using Domain.Entities;

namespace Application.Dtos.QuestionDtos
{
    public class GetQuestionWithAnswersDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<GetSimpleAnswerDto> Answers { get; set; }
    }
}