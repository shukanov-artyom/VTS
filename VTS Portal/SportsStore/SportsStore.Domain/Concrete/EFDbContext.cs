using System;
using System.Data.Entity;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        // Product is object type
        // Products is table name
        public DbSet<Product> Products { get; set; }
    }
}
