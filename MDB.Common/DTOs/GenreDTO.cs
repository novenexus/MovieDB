using System.ComponentModel.DataAnnotations;

namespace MDB.Common.DTOs;

public class GenreDTO
{
    public int Id { get; set; }
    [MaxLength(50), Required]
    public string? Name { get; set; }
}
