using MDB.Membership.Database.Contexts;
using MDB.UserLikes.Database;
using MDB.UserLikes.Service;
using MDB.UserLikes.WorkerService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((hostContext, services) =>
{
    services.AddDbContext<UserLikeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RecommendedFilmsDbConnection"))
);
    services.AddSingleton<MDB.UserLikes.Service.KafkaProducer>();

    services.AddHostedService<RecommendationBackgroundService>();
    services.AddScoped<IUserLikeRepository, UserLikeRepository>();
    services.AddScoped<IUserLikeService, UserLikeService>(); // Adjust as needed
    services.AddScoped<RecommendationGeneratorService>();
});

var host = builder.Build();
host.Run();
