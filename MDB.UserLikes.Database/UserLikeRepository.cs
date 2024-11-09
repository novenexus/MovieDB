using MDB.Membership.Database.Contexts;
using MDB.UserLikes.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace MDB.UserLikes.Database
{
    public interface IUserLikeRepository
    {
        Task<UserLike?> GetUserLikeAsync(int userId, int filmId);
        Task<IEnumerable<UserLike>> GetUserLikesByUserIdAsync(int userId);
        Task AddUserLikeAsync(UserLike userLike);
        Task RemoveUserLikeAsync(UserLike userLike);
        Task<bool> UserHasLikedFilmAsync(int userId, int filmId);
        Task<IEnumerable<int>> GetUserRecommendations(int userId);
        Task<IEnumerable<int>> GetUserIds();
    }

    public class UserLikeRepository : IUserLikeRepository
    {
        private readonly UserLikeDbContext _context;

        public UserLikeRepository(UserLikeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<int>> GetUserIds()
        {
            return await _context.UserLikes.Select(ul => ul.UserId).Distinct().ToListAsync();
        }

        public async Task<UserLike?> GetUserLikeAsync(int userId, int filmId)
        {
            return await _context.UserLikes
                .FirstOrDefaultAsync(ul => ul.UserId == userId && ul.FilmId == filmId);
        }

        public async Task<IEnumerable<UserLike>> GetUserLikesByUserIdAsync(int userId)
        {
            return await _context.UserLikes
                .Where(ul => ul.UserId == userId)
                .ToListAsync();
        }

        public async Task AddUserLikeAsync(UserLike userLike)
        {
            _context.UserLikes.Add(userLike);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveUserLikeAsync(UserLike userLike)
        {
            _context.UserLikes.Remove(userLike);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserHasLikedFilmAsync(int userId, int filmId)
        {
            return await _context.UserLikes
                .AnyAsync(ul => ul.UserId == userId && ul.FilmId == filmId);
        }

        public async Task<IEnumerable<int>> GetUserRecommendations(int userId)
        {
            var recommendationsDateTimeThresh = DateTime.UtcNow - TimeSpan.FromDays(10);
            var recentlyLikedFilmIds = await _context.UserLikes.Where(ul => ul.UserId == userId)
                .Where(ul => ul.CreatedAt >= recommendationsDateTimeThresh)
                .Select(ul => ul.FilmId)
                .ToListAsync();

            return await _context.UserLikes
                .Where(ul => !recentlyLikedFilmIds.Contains(ul.FilmId) && ul.UserId != userId)
                .GroupBy(ul => ul.FilmId)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .Take(10)
                .ToListAsync();
        }
    }

}
