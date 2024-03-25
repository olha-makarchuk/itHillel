using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Data.Context
{
    public class OnlineStoreContext : DbContext
    {
        public OnlineStoreContext(DbContextOptions<OnlineStoreContext> options) : base(options)
        {

        }

        public virtual DbSet<Product> Products { get; set;}
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
    }
}
