using MDB.Membership.Database.Entities;
using MDB.Membership.Database.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MDB.Membership.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimilarFilmsController : ControllerBase
    {
        private readonly IDBService _db;

        public SimilarFilmsController(IDBService db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IResult> Get()
        {
            try
            {
                _db.Include<SimilarFilm>();
                var SimilarFilms = await _db.GetAsync<SimilarFilm, SimilarFilmDTO>();
                return Results.Ok(SimilarFilms);
            }
            catch (Exception ex)
            {
                return Results.NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IResult> Post([FromBody] SimilarFilmDTO dto)
        {
            try
            {
                var SimilarFilm = await _db.AddAsync<SimilarFilm, SimilarFilmDTO>(dto);
                var result = await _db.SaveChangesAsync();
                if (result) return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
            return Results.BadRequest($"Couldn't add the SimilarFilm entity.");
        }

        [HttpDelete]
        public async Task<IResult> Delete(SimilarFilmDTO dto)
        {
            try
            {
                var success = _db.Delete<SimilarFilm, SimilarFilmDTO>(dto);

                if (await _db.SaveChangesAsync())
                    return Results.NoContent();
            }
            catch (Exception)
            {
                throw;
            }

            return Results.BadRequest();
        }
    }
}
