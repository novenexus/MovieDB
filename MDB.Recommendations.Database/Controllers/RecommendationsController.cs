using MDB.Recommendations.Database.Models;
using MDB.Recommendations.Service;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class RecommendationsController : ControllerBase
{
    private readonly IRecommendationService _recommendationService;

    public RecommendationsController(IRecommendationService recommendationService)
    {
        _recommendationService = recommendationService;
    }

    [HttpPost]
    public async Task<IActionResult> StoreRecommendations([FromBody] List<Recommendation> recommendations)
    {
        await _recommendationService.StoreRecommendationsAsync(recommendations);
        return Ok();
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetRecommendationsForUser(int userId)
    {
        var recommendations = await _recommendationService.GetRecommendationsForUserAsync(userId);
        return Ok(recommendations);
    }
}
