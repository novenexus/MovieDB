namespace MDB.Recommendations.Database.Models
{
    public class UserRecommendationMessage
    {
        public int UserId { get; set; }
        public List<int> Recommendations { get; set; }
    }
}
