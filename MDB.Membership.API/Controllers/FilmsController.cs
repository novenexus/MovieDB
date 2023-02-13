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
                _db.HttpInclude<FilmGenre>();
                _db.HttpInclude<SimilarFilm>();
                var film = await _db.SingleAsync<Film, FilmDTO>(f => f.Id == id);
                return Results.Ok(film);
            }
            catch (Exception ex)
            {
                return Results.NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task Post([FromBody] FilmCreateDTO dto)
        {
            //try
            //{
            //    var film = await _db.AddAsync<Film, FilmCreateDTO>(dto);
            //    return Results.Ok(film);
            //}
            //catch (Exception ex)
            //{
            //    return Results.NotFound(ex.Message);
            //}
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
