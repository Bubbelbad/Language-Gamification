using Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Question : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public int ChallengeId { get; set; }

        // Navigation properties
        public ICollection<Answer> Answers { get; set; }
        public Challenge Challenge { get; set; }


        int IEntity<int>.Id
        {
            get => Id;
            set => Id = value;
        }
    }
}
