namespace backend.Infrastructure.Seed.Interfaces;

public interface ISeedCommand
{
  Task<bool> Execute();
}
