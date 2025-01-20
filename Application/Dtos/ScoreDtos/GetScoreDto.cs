namespace Application.Dtos.ScoreDtos
{
    public sealed class GetScoreDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ChallengeId { get; set; }
        public int Points { get; set; }
        public DateTime CompletedAt { get; set; }
    }
}
