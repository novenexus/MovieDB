namespace MDB.Common.DTOs;

public class FilmDTO
{
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
    public virtual DirectorDTO Director { get; set; } = null!;
    public List<GenreDTO>? Genres { get; set; } = new();
    //public List<SimilarFilmDTO>? SimilarFilms { get; set; } = new();
}


public class FilmCreateDTO
{
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
}

public class FilmEditDTO : FilmCreateDTO
{
    public int Id { get; set; }
}
public class FilmInfoDTO : FilmEditDTO
{
    public List<SimilarFilmDTO>? SimilarFilms { get; set; } = new();
    public List<GenreDTO>? Genres { get; set; } = new();
    public virtual DirectorDTO Director { get; set; } = null!;
}