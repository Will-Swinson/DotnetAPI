using DotnetAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DotnetAPI.Data
{
  public class DataContextEF : DbContext
  {
    private readonly IConfiguration _config;
    private readonly string? _connectionString;
    public DataContextEF(IConfiguration config)
    {
      _config = config;
      _connectionString = config.GetConnectionString("DefaultConnection");
    }

    public DbSet<User> Users { get; set; }

    public DbSet<UserSalary> UserSalary { get; set; }
    public DbSet<UserJobInfo> UserJobInfo { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        optionsBuilder
        .UseSqlServer(
          _connectionString,
          optionsBuilder => optionsBuilder.EnableRetryOnFailure()
          );
      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.HasDefaultSchema("TutorialAppSchema");

      modelBuilder.Entity<User>()
        .ToTable("Users", "TutorialAppSchema")
        .HasKey(u => u.UserId);

      modelBuilder.Entity<UserSalary>()
      .HasKey(us => us.UserId);

      modelBuilder.Entity<UserJobInfo>()
      .HasKey(uji => uji.UserId);
    }
  }
}