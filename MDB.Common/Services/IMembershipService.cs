namespace MDB.Common.Services
{
    public interface IMembershipService
    {
        Task<FilmInfoDTO> GetFilmAsync(int? id);
        Task<List<FilmDTO>> GetFilmsAsync(bool freeOnly);
        Task<List<GenreDTO>> GetGenresAsync();
    }
}