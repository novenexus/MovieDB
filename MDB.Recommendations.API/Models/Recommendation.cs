namespace MDB.Recommendations.Database.Models
{
    public class Recommendation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FilmId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
