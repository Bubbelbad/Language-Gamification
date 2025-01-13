﻿using Domain.Interfaces;

namespace Domain.Entities
{
    public class UserChallenge : IEntity<int>
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ChallengeId { get; set; }
        public int Score { get; set; }
        public DateTime CompletedAt { get; set; }

        // Navigation Properties
        public Challenge Challenge { get; set; }
        public User user { get; set; }

        int IEntity<int>.Id
        {
            get => Id;
            set => Id = value;
        }
    }
}