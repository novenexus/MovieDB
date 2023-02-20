﻿// <auto-generated />
using System;
using MDB.Membership.Database.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MDB.Membership.Database.Migrations
{
    [DbContext(typeof(MDBContext))]
    [Migration("20230219115555_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MDB.Membership.Database.Entities.Director", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Directors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Director Name"
                        });
                });

            modelBuilder.Entity("MDB.Membership.Database.Entities.Film", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("DirectorId")
                        .HasColumnType("int");

                    b.Property<string>("FilmUrl")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<bool>("Free")
                        .HasColumnType("bit");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Released")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("DirectorId");

                    b.ToTable("Films");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "The film is set in a dystopian future Los Angeles of 2019",
                            DirectorId = 1,
                            FilmUrl = "https://www.youtube.com/embed/eogpIG53Cis",
                            Free = true,
                            Released = new DateTime(1982, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Blade Runner"
                        },
                        new
                        {
                            Id = 2,
                            Description = "The film is set on the fictional island of Isla Nublar",
                            DirectorId = 1,
                            FilmUrl = "https://www.youtube.com/embed/eogpIG53Cis",
                            Free = false,
                            Released = new DateTime(1993, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Jurassic Park"
                        },
                        new
                        {
                            Id = 3,
                            Description = "K, an officer with the Los Angeles Police Department",
                            DirectorId = 1,
                            FilmUrl = "https://www.youtube.com/embed/eogpIG53Cis",
                            Free = false,
                            Released = new DateTime(2017, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Blade Runner 2049"
                        },
                        new
                        {
                            Id = 4,
                            Description = "John Hammond along with few other members try to explore the Jurassic Park's second site",
                            DirectorId = 1,
                            FilmUrl = "https://www.youtube.com/embed/eogpIG53Cis",
                            Free = true,
                            Released = new DateTime(1997, 9, 19, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "The Lost World: Jurassic Park"
                        });
                });

            modelBuilder.Entity("MDB.Membership.Database.Entities.FilmGenre", b =>
                {
                    b.Property<int>("FilmId")
                        .HasColumnType("int");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.HasKey("FilmId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("FilmGenres", (string)null);

                    b.HasData(
                        new
                        {
                            FilmId = 1,
                            GenreId = 1
                        },
                        new
                        {
                            FilmId = 1,
                            GenreId = 2
                        },
                        new
                        {
                            FilmId = 2,
                            GenreId = 1
                        },
                        new
                        {
                            FilmId = 3,
                            GenreId = 1
                        },
                        new
                        {
                            FilmId = 4,
                            GenreId = 2
                        });
                });

            modelBuilder.Entity("MDB.Membership.Database.Entities.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Action"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Sci-Fi"
                        });
                });

            modelBuilder.Entity("MDB.Membership.Database.Entities.SimilarFilm", b =>
                {
                    b.Property<int>("ParentFilmId")
                        .HasColumnType("int");

                    b.Property<int>("SimilarFilmId")
                        .HasColumnType("int");

                    b.HasKey("ParentFilmId", "SimilarFilmId");

                    b.HasIndex("SimilarFilmId");

                    b.ToTable("SimilarFilms");

                    b.HasData(
                        new
                        {
                            ParentFilmId = 1,
                            SimilarFilmId = 3
                        },
                        new
                        {
                            ParentFilmId = 2,
                            SimilarFilmId = 4
                        });
                });

            modelBuilder.Entity("MDB.Membership.Database.Entities.Film", b =>
                {
                    b.HasOne("MDB.Membership.Database.Entities.Director", "Director")
                        .WithMany("Films")
                        .HasForeignKey("DirectorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Director");
                });

            modelBuilder.Entity("MDB.Membership.Database.Entities.FilmGenre", b =>
                {
                    b.HasOne("MDB.Membership.Database.Entities.Film", "Film")
                        .WithMany()
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MDB.Membership.Database.Entities.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Film");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("MDB.Membership.Database.Entities.SimilarFilm", b =>
                {
                    b.HasOne("MDB.Membership.Database.Entities.Film", "Film")
                        .WithMany("SimilarFilms")
                        .HasForeignKey("ParentFilmId")
                        .IsRequired();

                    b.HasOne("MDB.Membership.Database.Entities.Film", "Similar")
                        .WithMany()
                        .HasForeignKey("SimilarFilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Film");

                    b.Navigation("Similar");
                });

            modelBuilder.Entity("MDB.Membership.Database.Entities.Director", b =>
                {
                    b.Navigation("Films");
                });

            modelBuilder.Entity("MDB.Membership.Database.Entities.Film", b =>
                {
                    b.Navigation("SimilarFilms");
                });
#pragma warning restore 612, 618
        }
    }
}