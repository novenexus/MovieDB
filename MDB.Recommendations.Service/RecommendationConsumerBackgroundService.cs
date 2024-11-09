using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MDB.Recommendations.Service
{
    public class RecommendationConsumerBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<RecommendationConsumerBackgroundService> _logger;

        public RecommendationConsumerBackgroundService(IServiceScopeFactory scopeFactory, ILogger<RecommendationConsumerBackgroundService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var recommendationService = scope.ServiceProvider.GetRequiredService<IRecommendationService>();
                        await recommendationService.ConsumeRecommendations();
                    }

                    await Task.Delay(1000, stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while executing background service: {ex.Message}");
            }
        }
    }

}