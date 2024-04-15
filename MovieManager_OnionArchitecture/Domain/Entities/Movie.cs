using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    [Table("Movie")]
    public class Movie : BaseEntity
    {
        [Required]
        [StringLength(255)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime ReleaseDate { get; set; }
    }
}
