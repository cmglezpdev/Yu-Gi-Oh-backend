namespace backend.database;

public interface ISeedCommand
{
  Task<bool> Execute();
}


public class Localization
{
  public string nombre { get; set; }
  public List<string> municipios { get; set; }
}