using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<AdminUser> AdminUser { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<UserType> UserType { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<OrderStatus> OrderStatus { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<ProductImage> ProductImage { get; set; }
    public DbSet<ProductCategory> ProductCategory { get; set; }
    public DbSet<ProductSubCategory> ProductSubCategory { get; set; }

    public override int SaveChanges()
    {
        var entries = ChangeTracker.Entries().Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            ((BaseEntity)entry.Entity).UpdatedAt = DateTime.Now;

            if (entry.State == EntityState.Added)
            {
                ((BaseEntity)entry.Entity).CreatedAt = DateTime.Now;
            }
        }

        return base.SaveChanges();
    }
}
