using Domain.Interfaces;

namespace Domain.Entities
{
    public class Question : IEntity<int>
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int CorrectAnswerId { get; set; }
        public ICollection<Answer> Answers { get; set; }


        int IEntity<int>.Id
        {
            get => Id;
            set => Id = value;
        }
    }
}
