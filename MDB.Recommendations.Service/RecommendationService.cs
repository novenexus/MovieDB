using MDB.Recommendations.Database;
using MDB.Recommendations.Database.Models;

namespace MDB.Recommendations.Service
{
    public interface IRecommendationService
    {
        Task StoreRecommendationsAsync(List<Recommendation> recommendations);
        Task<List<Recommendation>> GetRecommendationsForUserAsync(int userId);
    }

    public class RecommendationService : IRecommendationService
    {
        private readonly IRecommendationRepository _repository;

        public RecommendationService(IRecommendationRepository repository)
        {
            _repository = repository;
        }

        public async Task StoreRecommendationsAsync(List<Recommendation> recommendations)
        {
            // Validate and process recommendations before storing
            await _repository.StoreRecommendationsAsync(recommendations);
        }

        public async Task<List<Recommendation>> GetRecommendationsForUserAsync(int userId)
        {
            return await _repository.GetRecommendationsForUserAsync(userId);
        }
    }

}
