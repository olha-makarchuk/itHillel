using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Contract.Requests
{
    public class UpsertCategoryRequest
    {
        public int IdCategory { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }
    }
}
