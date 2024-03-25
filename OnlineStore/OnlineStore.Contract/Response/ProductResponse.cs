using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Contract.Response
{
    public class ProductResponse
    {
        public int IdProduct { get; set; }

        public int IdCategory { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        public virtual CategoryResponse CategoryResponse { get; set; }
    }
}
