using MDB.UserLikes.Service;

namespace MDB.UserLikes.WorkerService
{
    public class RecommendationBackgroundService : BackgroundService
    {
        private readonly RecommendationGeneratorService _recommendationGeneratorService;
        private readonly ILogger<RecommendationBackgroundService> _logger;

        public RecommendationBackgroundService(
            RecommendationGeneratorService recommendationGeneratorService,
            ILogger<RecommendationBackgroundService> logger)
        {
            _recommendationGeneratorService = recommendationGeneratorService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var delayInterval = TimeSpan.FromHours(1);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Starting recommendation generation...");

                    await _recommendationGeneratorService.GenerateRecommendationsAsync(stoppingToken);

                    _logger.LogInformation("Recommendation generation completed.");

                    await Task.Delay(delayInterval, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error in RecommendationBackgroundService: {ex.Message}");
                }
            }
        }
    }
}
