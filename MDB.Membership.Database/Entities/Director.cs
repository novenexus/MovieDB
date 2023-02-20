namespace MDB.Membership.Database.Entities;

public class Director : IEntity
{
    public int Id { get; set; }
    [MaxLength(50), Required]
    public string? Name { get; set; }
    public string? Avatar { get; set; }
    public string? Description { get; set; }
    public virtual ICollection<Film> Films { get; set; }
}
