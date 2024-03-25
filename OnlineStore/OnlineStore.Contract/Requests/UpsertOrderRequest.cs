using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Contract.Requests
{
    public class UpsertOrderRequest
    {
        public int IdOrder { get; set; }

        [Required]
        public int IdProduct { get; set; }

        [Required]
        public int IdCustomer { get; set; }

        [Required]
        public DateTime DateTimeOrder { get; set; }
    }
}
