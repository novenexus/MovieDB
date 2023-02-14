namespace MDB.Membership.Database.Entities;

public class Genre : IEntity
{
    public int Id { get; set; }
    [MaxLength(50), Required]
    public string? Name { get; set; }
    public virtual ICollection<Film> Films { get; set; }
    public Genre()
    {
        Films = new HashSet<Film>();
    }
}
