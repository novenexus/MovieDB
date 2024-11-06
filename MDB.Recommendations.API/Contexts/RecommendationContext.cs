using MDB.Recommendations.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace MDB.Recommendations.Database.Contexts;

public class RecommendationContext : DbContext
{
    public RecommendationContext(DbContextOptions<RecommendationContext> options)
    : base(options)
    {
    }

    public DbSet<Recommendation> Recommendations { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Recommendation>()
            .HasKey(r => r.Id);
    }
}