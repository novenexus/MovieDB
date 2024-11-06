using MDB.UserLikes.Service;
using Microsoft.AspNetCore.Mvc;

namespace MDB.UserLikes.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserLikeController : ControllerBase
    {
        private readonly IUserLikeService _userLikeService;
        private readonly RecommendationGeneratorService _recommendationGeneratorService;
        public UserLikeController(IUserLikeService userLikeService, RecommendationGeneratorService recommendationGeneratorService)
        {
            _userLikeService = userLikeService;
            _recommendationGeneratorService = recommendationGeneratorService;
        }

        [HttpPost("like")]
        public async Task<IActionResult> AddUserLike(int userId, int filmId)
        {
            var result = await _userLikeService.AddUserLike(userId, filmId);
            if (!result)
            {
                return BadRequest("User has already liked this film.");
            }
            return Ok("Like added successfully.");
        }

        [HttpDelete("unlike")]
        public async Task<IActionResult> RemoveUserLike(int userId, int filmId)
        {
            var result = await _userLikeService.RemoveUserLike(userId, filmId);
            if (!result)
            {
                return NotFound("Like not found for the specified user and film.");
            }
            return Ok("Like removed successfully.");
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetLikeStatus(int userId, int filmId)
        {
            var isLiked = await _userLikeService.IsUserLikedFilm(userId, filmId);
            return Ok(new { Liked = isLiked });
        }

        [HttpGet("user/{userId}/likes")]
        public async Task<IActionResult> GetUserLikedFilms(int userId)
        {
            var likedFilms = await _userLikeService.GetLikedFilmsByUser(userId);
            return Ok(likedFilms);
        }

        [HttpGet("user/{userId}/recommendations")]
        public async Task<IActionResult> GetUserRecommendations(int userId)
        {
            var recommendedFilms = await _userLikeService.GetRecommendedFilms(userId);
            if (recommendedFilms == null || !recommendedFilms.Any())
            {
                return NotFound("No recommendations found for the user.");
            }
            return Ok(recommendedFilms);
        }

        [HttpPost("generate-recommendations")]
        public async Task<IActionResult> GenerateRecommendations(CancellationToken cancellationToken)
        {
            try
            {
                await _recommendationGeneratorService.GenerateRecommendationsAsync(cancellationToken);
                return Ok("Recommendations are being generated and sent to Kafka.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

}
