using Microsoft.EntityFrameworkCore;
using ShopAPI.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopAPI.API.Services
{
    public class ShopContext : DbContext
    {
        public ShopContext()
        {
            //Empty needed
        }

        public ShopContext(DbContextOptions<ShopContext> o) : base(o)
        {
            //Empty needed
        }

        public DbSet<Product> Products {get;set;}
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ByteImage> ByteImages { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
