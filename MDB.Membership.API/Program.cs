using static System.Collections.Specialized.BitVector32;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(policy =>
{
    policy.AddPolicy("CorsAllAccessPolicy", opt =>
    opt.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
    );
});

builder.Services.AddDbContext<MDBContext>(
options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("MDBConnection")));

ConfigureAutoMapper();

builder.Services.AddScoped<IDBService, DBService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsAllAccessPolicy");

app.UseAuthorization();
app.MapControllers();

app.Run();

void ConfigureAutoMapper()
{
    var config = new AutoMapper.MapperConfiguration(cfg =>
    {
        //Director mapping
        cfg.CreateMap<Director, DirectorDTO>()
        .ReverseMap();
        cfg.CreateMap<Genre, GenreDTO>().ReverseMap();
        //Films mapping
        cfg.CreateMap<Film, FilmDTO>().ReverseMap()
        .ForMember(dest => dest.Director, src => src.Ignore());
        cfg.CreateMap<FilmEditDTO, Film>()
        .ForMember(dest => dest.Director, src => src.Ignore())
        .ForMember(dest => dest.Genres, src => src.Ignore())
        .ForMember(dest => dest.SimilarFilms, src => src.Ignore());
        cfg.CreateMap<FilmCreateDTO, Film>()
        .ForMember(dest => dest.Director, src => src.Ignore())
        .ForMember(dest => dest.Genres, src => src.Ignore())
        .ForMember(dest => dest.SimilarFilms, src => src.Ignore());
        cfg.CreateMap<Film, FilmInfoDTO>();
  
        //Genre
        cfg.CreateMap<FilmGenre, FilmGenreDTO>().ReverseMap();
        //SimilarFilms
        cfg.CreateMap<SimilarFilm, SimilarFilmDTO>()
        .ForMember(dest => dest.SimilarFilmTitle, src => src.MapFrom(s => s.Film.Title))
        .ReverseMap()
        .ForMember(dest => dest.Film, src => src.Ignore());
    });
    var mapper = config.CreateMapper();
    builder.Services.AddSingleton(mapper);
}