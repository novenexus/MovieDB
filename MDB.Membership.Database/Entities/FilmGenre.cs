namespace MDB.Membership.Database.Entities;

public class FilmGenre : IReferenceEntity
{
    public int FilmId { get; set; }
    public int GenreId { get; set; }
}
