using MDB.UserLikes.WorkerService;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((hostContext, services) =>
{
    services.AddSingleton<MDB.UserLikes.Service.KafkaProducer>();

    services.AddHostedService<RecommendationBackgroundService>();
});

var host = builder.Build();
host.Run();
