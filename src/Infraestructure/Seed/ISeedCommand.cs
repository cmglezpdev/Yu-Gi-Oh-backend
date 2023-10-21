namespace backend.Infrastructure.Seed;

public interface ISeedCommand
{
  Task<bool> Execute();
}
