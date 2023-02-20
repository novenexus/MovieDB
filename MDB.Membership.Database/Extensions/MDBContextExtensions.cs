namespace MDB.Membership.Database.Extensions;

public static class MDBContextExtensions
{
    public static async Task SeedMembershipData (this IDBService service)
    { 
        try
		{
            await service.AddAsync<Director, DirectorDTO>(new DirectorDTO
            {
                Name = "Ridley Scott"
            });
            await service.AddAsync<Director, DirectorDTO>(new DirectorDTO
            {
                Name = "Steven Spielberg"
            });
            await service.AddAsync<Director, DirectorDTO>(new DirectorDTO
            {
                Name = "Denis Villeneuve"
            });
            await service.SaveChangesAsync();

            var director1 = await service.SingleAsync<Director, DirectorDTO>(d => d.Name.Equals("Ridley Scott"));
            var director2 = await service.SingleAsync<Director, DirectorDTO>(d => d.Name.Equals("Steven Spielberg"));
            var director3 = await service.SingleAsync<Director, DirectorDTO>(d => d.Name.Equals("Denis Villeneuve"));

            await service.AddAsync<Genre, GenreDTO>(new GenreDTO { Name = "Sci-Fi" });
            await service.AddAsync<Genre, GenreDTO>(new GenreDTO { Name = "Action" });
            await service.AddAsync<Genre, GenreDTO>(new GenreDTO { Name = "Thriller" });
            await service.AddAsync<Genre, GenreDTO>(new GenreDTO { Name = "Cyberpunk" });

            await service.SaveChangesAsync();

            var genre1 = await service.SingleAsync<Genre, GenreDTO>(d => d.Name.Equals("Sci-Fi"));
            var genre2 = await service.SingleAsync<Genre, GenreDTO>(d => d.Name.Equals("Action"));
            var genre3 = await service.SingleAsync<Genre, GenreDTO>(d => d.Name.Equals("Thriller"));
            var genre4 = await service.SingleAsync<Genre, GenreDTO>(d => d.Name.Equals("Cyberpunk"));

            await service.AddAsync<Film, FilmDTO>(new FilmDTO
            {
                Title = "Blade Runner",
                Released = new DateTime(1982, 11, 11),
                DirectorId = director1.Id,
                Free = true,
                Description = "The film is set in a dystopian future Los Angeles of 2019",
                FilmUrl = "https://www.youtube.com/embed/eogpIG53Cis"
            });
            await service.AddAsync<Film, FilmDTO>(new FilmDTO
            {
                Title = "Jurassic Park",
                Released = new DateTime(1993, 09, 30),
                DirectorId = director2.Id,
                Free = false,
                Description = "The film is set on the fictional island of Isla Nublar",
                FilmUrl = "https://www.youtube.com/embed/eogpIG53Cis"
            });
            await service.AddAsync<Film, FilmDTO>(new FilmDTO
            {
                Title = "Blade Runner 2049",
                Released = new DateTime(2017, 10, 05),
                DirectorId = director3.Id,
                Free = false,
                Description = "K, an officer with the Los Angeles Police Department",
                FilmUrl = "https://www.youtube.com/embed/eogpIG53Cis"
            });
            await service.AddAsync<Film, FilmDTO>(new FilmDTO
            {
                Title = "The Lost World: Jurassic Park",
                Released = new DateTime(1997, 09, 19),
                DirectorId = director2.Id,
                Free = true,
                Description = "John Hammond along with few other members try to explore the Jurassic Park's second site",
                FilmUrl = "https://www.youtube.com/embed/eogpIG53Cis"
            });
            await service.SaveChangesAsync();

            var film1 = service.SingleAsync<Film, FilmDTO>(f => f.Title == "Blade Runner");
            var film2 = service.SingleAsync<Film, FilmDTO>(f => f.Title == "Jurassic Park");
            var film3 = service.SingleAsync<Film, FilmDTO>(f => f.Title == "Blade Runner 2049");
            var film4 = service.SingleAsync<Film, FilmDTO>(f => f.Title == "The Lost World: Jurassic Park");

            await service.HttpAddAsync<FilmGenre, FilmGenreDTO>(new FilmGenreDTO { FilmId = film1.Id, GenreId = genre1.Id });
            await service.HttpAddAsync<FilmGenre, FilmGenreDTO>(new FilmGenreDTO { FilmId = film1.Id, GenreId = genre2.Id });
            await service.HttpAddAsync<FilmGenre, FilmGenreDTO>(new FilmGenreDTO { FilmId = film1.Id, GenreId = genre4.Id });
            await service.HttpAddAsync<FilmGenre, FilmGenreDTO>(new FilmGenreDTO { FilmId = film2.Id, GenreId = genre1.Id });
            await service.HttpAddAsync<FilmGenre, FilmGenreDTO>(new FilmGenreDTO { FilmId = film2.Id, GenreId = genre2.Id });
            await service.HttpAddAsync<FilmGenre, FilmGenreDTO>(new FilmGenreDTO { FilmId = film2.Id, GenreId = genre3.Id });
            await service.HttpAddAsync<FilmGenre, FilmGenreDTO>(new FilmGenreDTO { FilmId = film3.Id, GenreId = genre1.Id });
            await service.HttpAddAsync<FilmGenre, FilmGenreDTO>(new FilmGenreDTO { FilmId = film3.Id, GenreId = genre2.Id });
            await service.HttpAddAsync<FilmGenre, FilmGenreDTO>(new FilmGenreDTO { FilmId = film3.Id, GenreId = genre4.Id });
            await service.HttpAddAsync<FilmGenre, FilmGenreDTO>(new FilmGenreDTO { FilmId = film4.Id, GenreId = genre1.Id });
            await service.HttpAddAsync<FilmGenre, FilmGenreDTO>(new FilmGenreDTO { FilmId = film4.Id, GenreId = genre2.Id });
            await service.HttpAddAsync<FilmGenre, FilmGenreDTO>(new FilmGenreDTO { FilmId = film4.Id, GenreId = genre3.Id });

            await service.SaveChangesAsync();

            await service.HttpAddAsync<SimilarFilm, SimilarFilmDTO>(new SimilarFilmDTO { FilmId = film1.Id, SimilarFilmId = film3.Id });
            await service.HttpAddAsync<SimilarFilm, SimilarFilmDTO>(new SimilarFilmDTO { FilmId = film2.Id, SimilarFilmId = film4.Id });
            await service.HttpAddAsync<SimilarFilm, SimilarFilmDTO>(new SimilarFilmDTO { FilmId = film3.Id, SimilarFilmId = film1.Id });
            await service.HttpAddAsync<SimilarFilm, SimilarFilmDTO>(new SimilarFilmDTO { FilmId = film4.Id, SimilarFilmId = film2.Id });

            await service.SaveChangesAsync();
        }
        catch (Exception)
		{

			throw;
		}
    }
}
