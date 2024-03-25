using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Data.Entities
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int IdCategory { get; set; }
        
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
    }
}
