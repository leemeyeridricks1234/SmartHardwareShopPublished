using SmartHardwareShop.API.Models;
using Microsoft.EntityFrameworkCore;

namespace SmartHardwareShop.API.Repositories
{
    public class SmartHardwareShopContext : DbContext
    {
        public SmartHardwareShopContext(DbContextOptions<SmartHardwareShopContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<CartItem> CartItem { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
    }
}
