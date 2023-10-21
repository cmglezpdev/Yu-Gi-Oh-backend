namespace backend.Infraestructure.Seed;

public interface ISeedCommand
{
  Task<bool> Execute();
}
