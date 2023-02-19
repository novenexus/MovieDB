namespace MDB.Membership.Database.Entities;

public class Film : IEntity
{
    public Film()
    {
        SimilarFilms = new List<SimilarFilm>();
        Genres = new List<Genre>();
    }
    public int Id { get; set; }
    [MaxLength(50), Required]
    public string? Title { get; set; }
    public DateTime Released { get; set; }
    public int DirectorId { get; set; }
    public bool Free { get; set; }
    [MaxLength(200), Required]
    public string? Description { get; set; }
    [MaxLength(1024), Required]
    public string? FilmUrl { get; set; }
    public string? ImageUrl { get; set; }
    public virtual ICollection<SimilarFilm> SimilarFilms { get; set; }
    public virtual ICollection<Genre> Genres { get; set; }
    public virtual Director Director { get; set; } = null!;
}
