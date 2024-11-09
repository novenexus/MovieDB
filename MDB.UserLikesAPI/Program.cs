using Confluent.Kafka;
using MDB.Membership.Database.Contexts;
using MDB.UserLikes.Database;
using MDB.UserLikes.Service;
using MDB.UserLikes.WorkerService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var kafkaConfig = builder.Configuration.GetSection("Kafka").Get<ConsumerConfig>();

builder.Services.AddSingleton(kafkaConfig);
builder.Services.AddSingleton<IConsumer<string, string>>(provider =>
{
    var config = provider.GetRequiredService<ConsumerConfig>();
    return new ConsumerBuilder<string, string>(config).Build();
});

builder.Services.AddScoped<IUserLikeRepository, UserLikeRepository>();
builder.Services.AddScoped<IUserLikeService, UserLikeService>();
builder.Services.AddScoped<RecommendationGeneratorService>();
builder.Services.AddSingleton<KafkaProducer>();
builder.Services.AddHostedService<RecommendationBackgroundService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UserLikeDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("RecommendedFilmsDbConnection"))
        .EnableSensitiveDataLogging()
        .LogTo(Console.WriteLine, LogLevel.Debug)
);

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
