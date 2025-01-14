namespace Application.Dtos.UserChallengeDtos
{
    public class AddUserChallengeDto
    {
        public string UserId { get; set; }
        public int ChallengeId { get; set; }
        public int Score { get; set; }
        public DateTime CompletedAt { get; set; }
    }
}
