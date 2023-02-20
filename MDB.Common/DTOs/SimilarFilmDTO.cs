using System.ComponentModel.DataAnnotations.Schema;

namespace MDB.Common.DTOs;

public class SimilarFilmDTO
{
    public int FilmId { get; set; }
    public int SimilarFilmId { get; set; }
    public FilmDTO? Film { get; set; }
    public FilmDTO? Similar { get; set; }
}
