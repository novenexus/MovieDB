using Confluent.Kafka;
using MDB.Recommendations.Database;
using MDB.Recommendations.Database.Contexts;
using MDB.Recommendations.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var kafkaConfig = builder.Configuration.GetSection("Kafka");

builder.Services.AddSingleton<IConsumer<string, string>>(serviceProvider =>
{
    var consumerConfig = new ConsumerConfig
    {
        BootstrapServers = kafkaConfig["BootstrapServers"],
        GroupId = kafkaConfig["GroupId"]
    };

    return new ConsumerBuilder<string, string>(consumerConfig).Build();
});

builder.Services.AddDbContext<RecommendationContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("RecommendationsDbConnection"))
        .EnableSensitiveDataLogging()
        .LogTo(Console.WriteLine, LogLevel.Debug)
);

builder.Services.AddScoped<IRecommendationService, RecommendationService>();
builder.Services.AddScoped<IRecommendationRepository, RecommendationRepository>();
builder.Services.AddHostedService<RecommendationConsumerBackgroundService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
