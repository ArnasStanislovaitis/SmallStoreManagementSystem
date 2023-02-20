using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmallStoreManagementSystem.Models;

namespace SmallStoreManagementSystem.Data
{
    public class SmallStoreManagementSystemContext : DbContext
    {
        public SmallStoreManagementSystemContext(DbContextOptions<SmallStoreManagementSystemContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; } = default!;
        public DbSet<UserProductHistory> ProductsHistory { get; set; } = default!;
    }
}