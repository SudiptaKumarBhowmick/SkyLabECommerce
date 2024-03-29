﻿using Data.Entities;
using Data.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new SoftDeleteInterceptor());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminUser>().HasQueryFilter(x => x.IsDeleted == false);
        modelBuilder.Entity<User>().HasQueryFilter(x => x.IsDeleted == false);
        modelBuilder.Entity<UserType>().HasQueryFilter(x => x.IsDeleted == false);
        modelBuilder.Entity<Order>().HasQueryFilter(x => x.IsDeleted == false);
        modelBuilder.Entity<OrderStatus>().HasQueryFilter(x => x.IsDeleted == false);
        modelBuilder.Entity<Product>().HasQueryFilter(x => x.IsDeleted == false);
        modelBuilder.Entity<ProductImage>().HasQueryFilter(x => x.IsDeleted == false);
        modelBuilder.Entity<ProductCategory>().HasQueryFilter(x => x.IsDeleted == false);
        modelBuilder.Entity<ProductSubCategory>().HasQueryFilter(x => x.IsDeleted == false);
    }
}
