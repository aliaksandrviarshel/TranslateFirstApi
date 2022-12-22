namespace TranslateFirstApi.TranslateFirstHubFeature.Dtos;

public class GameEndedDto
{
    public GameEndedDto(List<UserRatingDto> users, Guid correctAnswerId)
    {
        Users = users;
        CorrectAnswerId = correctAnswerId;
    }

    public List<UserRatingDto> Users { get; set; }
    public Guid CorrectAnswerId { get; set; }
}
