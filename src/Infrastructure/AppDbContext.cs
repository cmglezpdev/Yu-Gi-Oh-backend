using Microsoft.EntityFrameworkCore;
using backend.Infrastructure.Entities;
using backend.Infrastructure.Interfaces;

namespace backend.Infrastructure;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions options) : base(options) { }

  public DbSet<User> Users { get; set; }
  public DbSet<Province> Provinces { get; set; }
  public DbSet<Municipality> Municipalities { get; set; }
  public DbSet<Card> Cards { get; set; }
  public DbSet<MonsterCard> MonsterCards { get; set; }
  public DbSet<SpellCard> SpellCards { get; set; }
  public DbSet<TrapCard> TrapCards { get; set; }
  public DbSet<Archetype> Archetypes { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    foreach (var entityType in modelBuilder.Model.GetEntityTypes())
    {
      if (entityType.ClrType.IsSubclassOf(typeof(PlatformModel)))
      {
        modelBuilder.Entity(entityType.Name).Property<DateTime>("CreatedAt").HasDefaultValueSql("NOW()");
        modelBuilder.Entity(entityType.Name).Property<DateTime>("UpdatedAt").HasDefaultValueSql("NOW()").ValueGeneratedOnAddOrUpdate();
      }
    }
  }
}