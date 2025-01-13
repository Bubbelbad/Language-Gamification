namespace Application.Dtos.UserChallengeDtos
{
    public class GetUserChallengeDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ChallengeId { get; set; }
        public int Score { get; set; }
        public DateTime CompletedAt { get; set; }
    }
}
