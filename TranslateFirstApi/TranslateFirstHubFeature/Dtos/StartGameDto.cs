namespace TranslateFirstApi.TranslateFirstHubFeature.Dtos;

public class StartGameDto
{
    public StartGameDto(Guid userId, string code)
    {
        UserId = userId;
        Code = code;
    }
    public Guid UserId { get; set; }

    public string Code { get; set; }
}
