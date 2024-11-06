using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MDB.Membership.Database.Contexts;

public class UserDbContext : IdentityDbContext<MDB.Membership.Database.Entities.User>
{
        public UserDbContext(DbContextOptions<UserDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<MDB.Membership.Database.Entities.User>().Property(u => u.Initials).HasMaxLength(5);

        //builder.Entity<MDB.Membership.Database.Entities.User>()
        //       .Property(u => u.Name)
        //       .IsRequired();

        builder.HasDefaultSchema("identity");
    }
}
