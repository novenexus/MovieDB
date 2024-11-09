using MDB.UserLikes.Service;

namespace MDB.UserLikes.WorkerService
{
    public class RecommendationBackgroundService : BackgroundService
    {
        private readonly RecommendationGeneratorService _recommendationGeneratorService;
        private readonly ILogger<RecommendationBackgroundService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RecommendationBackgroundService(
            IServiceScopeFactory serviceScopeFactory,
            ILogger<RecommendationBackgroundService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var delayInterval = TimeSpan.FromSeconds(1);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Starting recommendation generation...");

                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var recommendationGeneratorService = scope.ServiceProvider.GetRequiredService<RecommendationGeneratorService>();
                        await recommendationGeneratorService.GenerateRecommendationsAsync(stoppingToken);
                    }

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