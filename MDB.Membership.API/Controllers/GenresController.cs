using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MDB.Membership.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IDBService _db;

        public GenresController(IDBService db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IResult> Get()
        {
            try
            {
                _db.Include<Genre>();
                var Genres = await _db.GetAsync<Genre, GenreDTO>();
                return Results.Ok(Genres);
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
                _db.Include<Genre>();
                var Genre = await _db.SingleAsync<Genre, GenreDTO>(f => f.Id == id);
                return Results.Ok(Genre);
            }
            catch (Exception ex)
            {
                return Results.NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IResult> Post([FromBody] GenreDTO dto)
        {
            try
            {
                var Genre = await _db.AddAsync<Genre, GenreDTO>(dto);
                var result = await _db.SaveChangesAsync();
                if (!result) return Results.BadRequest();
                return Results.Created(_db.GetURI(Genre), Genre);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IResult> Put(int id, [FromBody] GenreDTO dto)
        {
            try
            {
                if(id != dto.Id) return Results.BadRequest($"Id mismatch. URI Id:{id}, DTO Id:{dto.Id}");
                var exists = await _db.AnyAsync<Genre>(f => f.Id == dto.Id);

                if (!exists) return Results.NotFound ("Genre not found.");

                _db.Update<Genre, GenreDTO>(id, dto);
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
                var exists = await _db.AnyAsync<Genre>(f => f.Id == id);
                if (!exists) return Results.NotFound("Genre not found.");

                var success = await _db.DeleteAsync<Genre>(id);
                if (!success) return Results.NotFound("Genre not found.");

                var result = await _db.SaveChangesAsync();
                if (!result) return Results.BadRequest("Could not delete the Genre.");

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }
    }
}
