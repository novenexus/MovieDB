using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace MDB.UserLikes.Service
{
    public class RecommendationGeneratorService
    {
        private readonly IUserLikeService _userLikeService;
        private readonly KafkaProducer _kafkaProducer;
        private readonly ILogger<RecommendationGeneratorService> _logger;
        private readonly string _topicName = "recommendations";

        public RecommendationGeneratorService(
            IUserLikeService userLikeService,
            KafkaProducer kafkaProducer,
            ILogger<RecommendationGeneratorService> logger)
        {
            _userLikeService = userLikeService;
            _kafkaProducer = kafkaProducer;
            _logger = logger;
        }

        public async Task GenerateRecommendationsAsync(CancellationToken stoppingToken)
        {
            try
            {
                var userIds = await _userLikeService.GetUserIds();
                if (userIds != null && userIds.Any())
                {
                    foreach (var userId in userIds)
                    {
                        if (stoppingToken.IsCancellationRequested) return;

                        var recommendedFilms = await _userLikeService.GetRecommendedFilms(userId);

                        if (recommendedFilms != null && recommendedFilms.Any())
                        {
                            var recommendationsJson = JsonSerializer.Serialize(new
                            {
                                UserId = userId,
                                Recommendations = recommendedFilms
                            });

                            await _kafkaProducer.ProduceMessageAsync(_topicName, userId.ToString(), recommendationsJson);
                            _logger.LogInformation($"Published recommendations for user {userId} to Kafka.");
                        }
                        else
                        {
                            _logger.LogWarning($"No recommendations found for user {userId}.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error generating recommendations: {ex.Message}");
            }
        }
    }
}