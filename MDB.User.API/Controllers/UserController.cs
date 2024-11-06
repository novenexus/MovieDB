//using Microsoft.AspNetCore.Mvc;

//namespace MDB.Membership.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UsersController : ControllerBase
//    {
//        private readonly IDBService _db;

//        public UsersController(IDBService db)
//        {
//            _db = db;
//        }

//        [HttpGet]
//        public async Task<IResult> Get()
//        {
//            try
//            {
//                _db.Include<Director>();
//                var Directors = await _db.GetAsync<Director, DirectorDTO>();
//                return Results.Ok(Directors);
//            }
//            catch (Exception ex)
//            {
//                return Results.NotFound(ex.Message);
//            }
//        }

//        [HttpGet("{id}")]
//        public async Task<IResult> Get(int id)
//        {
//            try
//            {
//                _db.Include<Director>();
//                var Director = await _db.SingleAsync<Director, DirectorDTO>(f => f.Id == id);
//                return Results.Ok(Director);
//            }
//            catch (Exception ex)
//            {
//                return Results.NotFound(ex.Message);
//            }
//        }

//        [HttpPost]
//        public async Task<IResult> Post([FromBody] DirectorDTO dto)
//        {
//            try
//            {
//                var Director = await _db.AddAsync<Director, DirectorDTO>(dto);
//                var result = await _db.SaveChangesAsync();
//                if (!result) return Results.BadRequest();
//                return Results.Created(_db.GetURI(Director), Director);
//            }
//            catch (Exception ex)
//            {
//                return Results.BadRequest(ex.Message);
//            }
//        }

//        [HttpPut("{id}")]
//        public async Task<IResult> Put(int id, [FromBody] DirectorDTO dto)
//        {
//            try
//            {
//                if (id != dto.Id) return Results.BadRequest($"Id mismatch. URI Id:{id}, DTO Id:{dto.Id}");
//                var exists = await _db.AnyAsync<Director>(f => f.Id == dto.Id);

//                if (!exists) return Results.NotFound("Director not found.");

//                _db.Update<Director, DirectorDTO>(id, dto);
//                var result = await _db.SaveChangesAsync();
//                if (!result) return Results.BadRequest();

//                return Results.NoContent();
//            }
//            catch (Exception ex)
//            {
//                return Results.BadRequest(ex.Message);
//            }
//        }

//        [HttpDelete("{id}")]
//        public async Task<IResult> Delete(int id)
//        {
//            try
//            {
//                var exists = await _db.AnyAsync<Director>(f => f.Id == id);
//                if (!exists) return Results.NotFound("Director not found.");

//                var success = await _db.DeleteAsync<Director>(id);
//                if (!success) return Results.NotFound("Director not found.");

//                var result = await _db.SaveChangesAsync();
//                if (!result) return Results.BadRequest("Could not delete the Director.");

//                return Results.NoContent();
//            }
//            catch (Exception ex)
//            {
//                return Results.BadRequest(ex.Message);
//            }
//        }
//    }
//}
