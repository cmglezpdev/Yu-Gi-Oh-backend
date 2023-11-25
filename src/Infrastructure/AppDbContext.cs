using backend.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using backend.Infrastructure.Entities;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

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
  public DbSet<Deck> Decks { get; set; }
  public DbSet<Tournament> Tournaments { get; set; }
  public DbSet<DuelsEntity> Duels { get; set; }
  
  public DbSet<TournamentInscriptions> TournamentInscriptions { get; set; }

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