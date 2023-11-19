namespace backend.Presentation.DTOs.Card;


#pragma warning disable CS8618
public class MonsterCardOutputDto
{
    public Guid Id { get; set; }
    public Guid CardId { get; set; }
    public string Race { get; set; }
    public int Level { get; set; }
    public int Atk { get; set; }
    public int Def { get; set; }
}