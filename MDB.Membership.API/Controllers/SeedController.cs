namespace MDB.Membership.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController : Controller
    {
        private readonly IDBService _db;

        public SeedController(IDBService db)
        {
            _db = db;
        }
        [HttpPost]
        public async Task<IResult> Seed()
        {
            try
            {
                await _db.SeedMembershipData();
                return Results.NoContent();
            }
            catch
            {
                return Results.BadRequest();
            }
        }
    }
}
