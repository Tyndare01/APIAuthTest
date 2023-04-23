using DAL.Context.Configs;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context;

public class AuthDbContext : DbContext
{
    
    public DbSet<User> Users { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer($"Server=localhost;Database=MyEntityDemoDatabase2;Trusted_Connection=True;TrustServerCertificate=True");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfig());
    }
    
}