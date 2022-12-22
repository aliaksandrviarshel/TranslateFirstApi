namespace TranslateFirstApi.TranslateFirstHubFeature.Dtos;

public class UserDto
{
    public UserDto(Guid id, string nickname)
    {
        Id = id;
        Nickname = nickname;
    }

    public Guid Id { get; set; }
    public string Nickname { get; set; }
}
