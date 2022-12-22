namespace TranslateFirstApi.TranslateFirstHubFeature.Dtos;

public class GetGameDto
{
    public GetGameDto(Guid userId, Guid gameId)
    {
        UserId = userId;
        GameId = gameId;
    }

    public Guid UserId { get; set; }
    public Guid GameId { get; set; }
}
