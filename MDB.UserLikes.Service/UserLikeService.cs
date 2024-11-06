using MDB.UserLikes.Database;
using MDB.UserLikes.Database.Models;

public interface IUserLikeService
{
    Task<bool> AddUserLike(int userId, int filmId);
    Task<bool> RemoveUserLike(int userId, int filmId);
    Task<bool> IsUserLikedFilm(int userId, int filmId);
    Task<IEnumerable<int>> GetLikedFilmsByUser(int userId);
    Task<IEnumerable<int>> GetRecommendedFilms(int userId);
    Task<IEnumerable<int>> GetUserIds();
}

public class UserLikeService : IUserLikeService
{
    private readonly IUserLikeRepository _userLikeRepository;

    public UserLikeService(IUserLikeRepository userLikeRepository)
    {
        _userLikeRepository = userLikeRepository;
    }

    public async Task<IEnumerable<int>> GetUserIds()
    {
        return await _userLikeRepository.GetUserIds();
    }

    public async Task<bool> AddUserLike(int userId, int filmId)
    {
        if (await IsUserLikedFilm(userId, filmId))
        {
            return false;
        }

        var userLike = new UserLike { UserId = userId, FilmId = filmId };
        await _userLikeRepository.AddUserLikeAsync(userLike);
        return true;
    }

    public async Task<bool> RemoveUserLike(int userId, int filmId)
    {
        var userLike = await _userLikeRepository.GetUserLikeAsync(userId, filmId);
        if (userLike == null)
        {
            return false;
        }

        await _userLikeRepository.RemoveUserLikeAsync(userLike);
        return true;
    }

    public async Task<bool> IsUserLikedFilm(int userId, int filmId)
    {
        var userLike = await _userLikeRepository.GetUserLikeAsync(userId, filmId);
        return userLike != null;
    }

    public async Task<IEnumerable<int>> GetLikedFilmsByUser(int userId)
    {
        var likedFilms = await _userLikeRepository.GetUserLikesByUserIdAsync(userId);
        return likedFilms.Select(f => f.FilmId);
    }

    public async Task<IEnumerable<int>> GetRecommendedFilms(int userId)
    {
        var recommendations = await _userLikeRepository.GetUserRecommendations(userId);
        return recommendations;
    }
}
