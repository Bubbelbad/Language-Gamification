using Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Challenge : IEntity<int>
    {
        [Key]
        [Required(ErrorMessage = "{0} is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} cant be longer than 50 characters")]
        public required string Title { get; set; }

        public string? Description { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<UserChallenge> UserChallenges { get; set; }

        int IEntity<int>.Id
        {
            get => Id;
            set => Id = value;
        }

        public bool IsLastQuestion(int currentIndex)
        {
            if (currentIndex == Questions.Count - 1)
            {
                return true;
            }
            return false;
        }
    }
}
