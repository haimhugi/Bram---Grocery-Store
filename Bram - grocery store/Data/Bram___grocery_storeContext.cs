using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bram___grocery_store.Models;

namespace Bram___grocery_store.Data
{
    public class Bram___grocery_storeContext : DbContext
    {
        public Bram___grocery_storeContext (DbContextOptions<Bram___grocery_storeContext> options)
            : base(options)
        {
        }

        public DbSet<Bram___grocery_store.Models.Product> Product { get; set; }

        public DbSet<Bram___grocery_store.Models.Cart> Cart { get; set; }

        public DbSet<Bram___grocery_store.Models.User> User { get; set; }

        public DbSet<Bram___grocery_store.Models.ProductCart> ProductCart { get; set; }
    }
}
