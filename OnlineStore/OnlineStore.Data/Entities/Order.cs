using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Data.Entities
{
    [Table("Order")]
    public class Order
    {
        [Key]
        public int IdOrder { get; set; }
        
        [Required]
        public int IdProduct { get; set; }
        
        [Required]
        public int IdCustomer { get; set; }

        [Required]
        public DateTime DateTimeOrder{ get; set; }

        [ForeignKey("IdProduct")]
        public virtual Product Product { get; set; }

        [ForeignKey("IdCustomer")]
        public virtual Customer Customer { get; set; }
    }
}
