using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MDB.UserLikes.Database.Models
{
    public class UserLike
    {
        [Key]
        [Column(Order = 1)]
        public int UserId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int FilmId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
