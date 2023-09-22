using backend.users;
using Microsoft.EntityFrameworkCore;

namespace backend.database;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions options) : base(options) { }

  public DbSet<User> Users { get; set; }
  public DbSet<Province> Provinces { get; set; }
  public DbSet<Municipality> Municipalities { get; set; }
}