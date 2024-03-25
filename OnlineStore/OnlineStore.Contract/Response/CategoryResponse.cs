using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Contract.Response
{
    public class CategoryResponse
    {
        public int IdCategory { get; set; }

        public string Name { get; set; }
    }
}
