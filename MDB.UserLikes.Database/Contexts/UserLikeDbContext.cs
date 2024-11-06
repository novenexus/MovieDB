using MDB.UserLikes.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace MDB.Membership.Database.Contexts;

public class UserLikeDbContext : DbContext
{
    public UserLikeDbContext(DbContextOptions<UserLikeDbContext> options)
    : base(options)
    {
    }

    public DbSet<UserLike> UserLikes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserLike>().HasKey(ul => new { ul.UserId, ul.FilmId });
    }
}