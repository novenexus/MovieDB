using MDB.Recommendations.Database.Contexts;
using MDB.Recommendations.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace MDB.Recommendations.Database
{
    public interface IRecommendationRepository
    {
        Task StoreRecommendationsAsync(List<Recommendation> recommendations);
        Task<List<Recommendation>> GetRecommendationsForUserAsync(int userId);
    }

    public class RecommendationRepository : IRecommendationRepository
    {
        private readonly RecommendationContext _context;

        public RecommendationRepository(RecommendationContext context)
        {
            _context = context;
        }

        public async Task StoreRecommendationsAsync(List<Recommendation> recommendations)
        {
            await _context.Recommendations.AddRangeAsync(recommendations);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Recommendation>> GetRecommendationsForUserAsync(int userId)
        {
            return await _context.Recommendations
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }
    }

}
