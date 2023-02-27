using MDB.Membership.Database.Entities;
using MDB.Membership.Database.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MDB.Membership.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmGenresController : ControllerBase
    {
        private readonly IDBService _db;

        public FilmGenresController(IDBService db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IResult> Get()
        {
            try
            {
                _db.Include<FilmGenre>();
                var FilmGenres = await _db.GetAsync<FilmGenre, FilmGenreDTO>();
                return Results.Ok(FilmGenres);
            }
            catch (Exception ex)
            {
                return Results.NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IResult> Post([FromBody] FilmGenreDTO dto)
        {
            try
            {
                var FilmGenre = await _db.AddAsync<FilmGenre, FilmGenreDTO>(dto);
                var result = await _db.SaveChangesAsync();
                if (result) return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
            return Results.BadRequest($"Couldn't add the FilmGenre entity.");
        }

        [HttpDelete]
        public async Task<IResult> Delete(FilmGenreDTO dto)
        {
            try
            {
                var success = _db.Delete<FilmGenre, FilmGenreDTO>(dto);

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
