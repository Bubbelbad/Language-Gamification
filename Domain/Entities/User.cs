﻿using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class User() : IdentityUser
    {
        [Key]
        public override string Id { get; set; }
        public int TotalPoints { get; set; }

        public ICollection<Score> Scores { get; set; }
        public ICollection<UserChallenge> UserChallenges { get; set; }
    }
}
