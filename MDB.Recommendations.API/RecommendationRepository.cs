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
            foreach (var recommendation in recommendations)
            {
                var existingRecommendation = await _context.Recommendations
                    .FirstOrDefaultAsync(r => r.UserId == recommendation.UserId && r.FilmId == recommendation.FilmId);

                if (existingRecommendation == null)
                {
                    await _context.Recommendations.AddAsync(recommendation);
                }
            }

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
