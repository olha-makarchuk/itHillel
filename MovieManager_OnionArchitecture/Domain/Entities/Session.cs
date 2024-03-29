using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Common;

namespace Domain.Entities
{
    [Table("Session")]
    public class Session: BaseEntity
    {
        [Required]
        public int MovieId { get; set; }

        [Required]
        [StringLength(255)]
        public string RoomName { get; set; }

        [Required]
        public DateTime StartDateTime { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
