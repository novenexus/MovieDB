using System.ComponentModel.DataAnnotations;

namespace MDB.Common.DTOs;

public class DirectorDTO
{
    public int Id { get; set; }
    [MaxLength(50), Required]
    public string? Name { get; set; }
    public string? Avatar { get; set; }
    public string? Description { get; set; }
}
