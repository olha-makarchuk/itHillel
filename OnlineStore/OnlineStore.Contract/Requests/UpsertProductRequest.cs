using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Contract.Requests
{
    public class UpsertProductRequest
    {
        public int IdProduct { get; set; }

        [Required]
        public int IdCategory { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }
    }
}
