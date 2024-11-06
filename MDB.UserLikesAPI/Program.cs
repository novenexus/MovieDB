using MDB.Membership.Database.Contexts;
using MDB.UserLikes.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserLikeRepository, UserLikeRepository>();
builder.Services.AddScoped<IUserLikeService, UserLikeService>();

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
