using MDB.Membership.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Reflection.Emit;

namespace MDB.Membership.Database.Contexts;

public class MDBContext : DbContext
{
    public DbSet<Film> Films => Set<Film>();
    public DbSet<FilmGenre> FilmGenres => Set<FilmGenre>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<Director> Directors => Set<Director>();
    public DbSet<SimilarFilm> SimilarFilms => Set<SimilarFilm>();

    public MDBContext(DbContextOptions<MDBContext> options)
: base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
        builder.Entity<FilmGenre>().HasKey(fg =>
            new { fg.FilmId, fg.GenreId });
        builder.Entity<SimilarFilm>().HasKey(sf =>
            new { sf.ParentFilmId, sf.SimilarFilmId });
        builder.Entity<Film>(entity =>
        {
            entity
                // For each film in the Film Entity,
                // reference related films in the SimilarFilms entity
                // with the ICollection<SimilarFilms>
                .HasMany(d => d.SimilarFilms)
                .WithOne(p => p.Film)
                .HasForeignKey(d => d.ParentFilmId)
                // To prevent cycles or multiple cascade paths.
                // Neded when seeding similar films data.
                .OnDelete(DeleteBehavior.ClientSetNull);

            // Configure a many-to-many realtionship between genres
            // and films using the FilmGenre connection entity.
            entity.HasMany(d => d.Genres)
                  .WithMany(p => p.Films)
                  .UsingEntity<FilmGenre>()
                  // Specify the table name for the connection table
                  // to avoid duplicate tables (FilmGenre and FilmGenres)
                  // in the database.
                  .ToTable("FilmGenres");
        });
        builder.Entity<Director>().HasData(
            new { Id = 1, Name = "Director Name" });

        builder.Entity<Film>().HasData(
            new
            {
                Id = 1,
                Title = "Blade Runner",
                Released = new DateTime(1982, 11, 11),
                DirectorId = 1,
                Free = true,
                Description = "The film is set in a dystopian future Los Angeles of 2019",
                FilmUrl = "https://www.youtube.com/embed/eogpIG53Cis"
            },
            new
            {
                Id = 2,
                Title = "Jurassic Park",
                Released = new DateTime(1993, 09, 30),
                DirectorId = 1,
                Free = false,
                Description = "The film is set on the fictional island of Isla Nublar",
                FilmUrl = "https://www.youtube.com/embed/eogpIG53Cis"
            },
            new
            {
                Id = 3,
                Title = "Blade Runner 2049",
                Released = new DateTime(2017, 10, 05),
                DirectorId = 1,
                Free = false,
                Description = "K, an officer with the Los Angeles Police Department",
                FilmUrl = "https://www.youtube.com/embed/eogpIG53Cis"
            },
            new
            {
                Id = 4,
                Title = "The Lost World: Jurassic Park",
                Released = new DateTime(1997, 09, 19),
                DirectorId = 1,
                Free = true,
                Description = "John Hammond along with few other members try to explore the Jurassic Park's second site",
                FilmUrl = "https://www.youtube.com/embed/eogpIG53Cis"
            });
        {
            builder.Entity<SimilarFilm>().HasData(
                new SimilarFilm { ParentFilmId = 1, SimilarFilmId = 3 },
                new SimilarFilm { ParentFilmId = 2, SimilarFilmId = 4 });

            builder.Entity<Genre>().HasData(
                new { Id = 1, Name = "Action" },
                new { Id = 2, Name = "Sci-Fi" });

            builder.Entity<FilmGenre>().HasData(
                new FilmGenre { FilmId = 1, GenreId = 1 },
                new FilmGenre { FilmId = 1, GenreId = 2 },
                new FilmGenre { FilmId = 2, GenreId = 1 },
                new FilmGenre { FilmId = 3, GenreId = 1 },
                new FilmGenre { FilmId = 4, GenreId = 2 });
        }

    }
}
