using Confluent.Kafka;
using MDB.Recommendations.Database;
using MDB.Recommendations.Database.Models;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace MDB.Recommendations.Service
{
    public interface IRecommendationService
    {
        Task StoreRecommendationsAsync(int userId, List<int> filmIds);
        Task<List<Recommendation>> GetRecommendationsForUserAsync(int userId);
        Task ConsumeRecommendations();
    }

    public class RecommendationService : IRecommendationService
    {
        private readonly IRecommendationRepository _repository;
        private readonly IConsumer<string, string> _consumer;
        private readonly string _topicName;

        public RecommendationService(IRecommendationRepository repository, IConsumer<string, string> consumer, IConfiguration configuration)
        {
            _repository = repository;
            _consumer = consumer;
            _topicName = configuration["Kafka:TopicName"] ?? "recommendations";
        }

        public async Task ConsumeRecommendations()
        {
            _consumer.Subscribe(_topicName);

            try
            {
                while (true)
                {
                    var consumeResult = _consumer.Consume(TimeSpan.FromMilliseconds(100));
                    if (consumeResult == null)
                    {
                        continue;
                    }
                    
                    var recommendationMessage = consumeResult.Message.Value;
                    var userRecommendations = JsonSerializer.Deserialize<UserRecommendationMessage>(recommendationMessage);

                    if (userRecommendations != null)
                    {
                        int userId = userRecommendations.UserId;
                        List<int> recommendedFilmIds = userRecommendations.Recommendations;

                        if (recommendedFilmIds.Any())
                        {
                            await StoreRecommendationsAsync(userId, recommendedFilmIds);
                        }
                    }
                }
            }
            catch (ConsumeException e)
            {
                Console.WriteLine($"Error consuming message: {e.Error.Reason}");
            }
            finally
            {
                _consumer.Commit();
                _consumer.Close();
            }
        }

        public async Task StoreRecommendationsAsync(int userId, List<int> filmIds)
        {
            var recommendations = filmIds.Select(filmId => new Recommendation
            {
                UserId = userId,
                FilmId = filmId,
                CreatedAt = DateTime.UtcNow
            }).ToList();

            await _repository.StoreRecommendationsAsync(recommendations);
        }

        public async Task<List<Recommendation>> GetRecommendationsForUserAsync(int userId)
        {
            return await _repository.GetRecommendationsForUserAsync(userId);
        }
    }
}


