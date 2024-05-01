using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    [Table("Genre")]
    public class Genre : BaseEntity
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; } = null!;

    }
}
