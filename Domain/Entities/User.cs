using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User() : IdentityUser
    { 
        public int TotalPoints { get; set; }

        public ICollection<Score> Scores { get; set; }
        public ICollection<UserChallenge> UserChallenges { get; set; }
    }
}
