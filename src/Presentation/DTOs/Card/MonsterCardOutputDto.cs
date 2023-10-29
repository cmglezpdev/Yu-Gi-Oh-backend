namespace backend.Presentation.DTOs;


#pragma warning disable CS8618
public class MonsterCardOutputDto
{
    public Guid Id { get; set; }
    public Guid CardId { get; set; }
    public string Rice { get; set; }
    public int Level { get; set; }
    public int Atk { get; set; }
    public int Def { get; set; }
}