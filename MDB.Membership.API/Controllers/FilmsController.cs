using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MDB.Membership.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        private readonly IDBService _db;

        public FilmsController(IDBService db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IResult> Get()
        {
            try
            {
                _db.Include<Film>();
                _db.Include<FilmGenre>();
                var films = await _db.GetAsync<Film, FilmDTO>();
                return Results.Ok(films);
            }
            catch (Exception ex)
            {
                return Results.NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IResult> Get(int id)
        {
            try
            {
                _db.Include<Film>();
                _db.Include<FilmGenre>();
                _db.Include<SimilarFilm>();
                var film = await _db.SingleAsync<Film, FilmInfoDTO>(f => f.Id == id);
                return Results.Ok(film);
            }
            catch (Exception ex)
            {
                return Results.NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IResult> Post([FromBody] FilmCreateDTO dto)
        {
            try
            {
                var film = await _db.AddAsync<Film, FilmCreateDTO>(dto);
                var result = await _db.SaveChangesAsync();
                if (!result) return Results.BadRequest();
                return Results.Created(_db.GetURI(film), film);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IResult> Put(int id, [FromBody] FilmEditDTO dto)
        {
            try
            {
                if(id != dto.Id) return Results.BadRequest($"Id mismatch. URI Id:{id}, DTO Id:{dto.Id}");
                var exists = await _db.AnyAsync<Director>(f => f.Id == dto.DirectorId);

                if (!exists) return Results.NotFound ("Director not found.");

                _db.Update<Film, FilmCreateDTO>(id, dto);
                var result = await _db.SaveChangesAsync();
                if (!result) return Results.BadRequest();

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id)
        {
            try
            {
                var exists = await _db.AnyAsync<Film>(f => f.Id == id);
                if (!exists) return Results.NotFound("Film not found.");

                var success = await _db.DeleteAsync<Film>(id);
                if (!success) return Results.NotFound("Film not found.");

                var result = await _db.SaveChangesAsync();
                if (!result) return Results.BadRequest("Could not delete the film.");

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }
    }
}
