using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext(options)
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Table> Tables { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Reservation>()
            .HasOne(n => n.Customer)
            .WithMany(n => n.Reservations)
            .HasForeignKey(n => n.CustomerId);

        modelBuilder.Entity<Reservation>()
            .HasOne(n => n.Table)
            .WithMany(n => n.Reservations)
            .HasForeignKey(n => n.TableId);
    }
}