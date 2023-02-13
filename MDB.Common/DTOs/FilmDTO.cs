using System.ComponentModel.DataAnnotations;
using System.IO;

namespace MDB.Common.DTOs;

public class FilmDTO
{
    public int Id { get; set; }
    [MaxLength(50), Required]
    public string? Title { get; set; }
    public DateTime Released { get; set; }
    public int DirectorId { get; set; }
    [MaxLength(80), Required]
    public bool Free { get; set; }
    [MaxLength(200), Required]
    public string? Description { get; set; }
    [MaxLength(1024), Required]
    public string? FilmUrl { get; set; }
    public virtual DirectorDTO Director { get; set; } = null!;
}

public class FilmCreateDTO
{
    [MaxLength(50), Required]
    public string? Title { get; set; }
    public DateTime Released { get; set; }
    public int DirectorId { get; set; }
    [MaxLength(80), Required]
    public bool Free { get; set; }
    [MaxLength(200), Required]
    public string? Description { get; set; }
    [MaxLength(1024), Required]
    public string? FilmUrl { get; set; }
}

public class FilmEditDTO : FilmCreateDTO
{
    public int Id { get; set; }
}