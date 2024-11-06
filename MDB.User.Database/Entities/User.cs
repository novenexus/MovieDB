using MDB.User.Database.Entities;
using Microsoft.AspNetCore.Identity;

namespace MDB.Membership.Database.Entities;

public class User : IdentityUser
{
    public string? Initials { get; set; }
    //[Required]
    //public string Name { get; set; }
    public ICollection<Genre> FavouriteGenres { get; set; } = new List<Genre>();
}
