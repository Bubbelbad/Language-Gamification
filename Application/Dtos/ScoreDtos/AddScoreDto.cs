namespace Application.Dtos.ScoreDtos
{
    public sealed class AddScoreDto
    {
        public string UserId { get; set; }
        public int ChallengeId { get; set; }
        public int Points { get; set; }
        public DateTime CompletedAt { get; set; }
    }
}
