namespace MDB.Common.DTOs;

public class FilmGenreDTO
{
    public int FilmId { get; set; }
    public int GenreId { get; set; }
    public FilmDTO? Film { get; set; }
    public GenreDTO? Genre { get; set; }
}
