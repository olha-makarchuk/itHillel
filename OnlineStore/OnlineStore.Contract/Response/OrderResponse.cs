using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Contract.Response
{
    public class OrderResponse
    {
        public int IdOrder { get; set; }

        public int IdProduct { get; set; }

        public int IdCustomer { get; set; }

        public DateTime DateTimeOrder { get; set; }

        public virtual ProductResponse ProductResponse { get; set; }
        public virtual CustomerResponse CustomerResponse { get; set; }
    }
}
