namespace backend.Common.Authorization;

public static class Permission
{
    public const string ReadArchetype = nameof(ReadArchetype);
    public const string ReadCard = nameof(ReadCard);
    public const string ReadDeck = nameof(ReadDeck);
    public const string WriteDeck = nameof(WriteDeck);
    public const string ReadDuel = nameof(ReadDuel);
    public const string WriteDuel = nameof(WriteDuel);
    public const string ReadInscription = nameof(ReadInscription);
    public const string WriteInscription = nameof(WriteInscription);
    public const string RejectOrAcceptInscription = nameof(RejectOrAcceptInscription);
    public const string ReadTournament = nameof(ReadTournament);
    public const string WriteTournament = nameof(WriteTournament);
    public const string ReadUser = nameof(ReadUser);
    public const string WriteUser = nameof(WriteUser);
    public const string ReadRole = nameof(ReadRole);
    public const string WriteRole = nameof(WriteRole);
    public const string ReadPermission = nameof(ReadPermission);
}