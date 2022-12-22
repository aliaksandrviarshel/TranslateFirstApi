namespace TranslateFirstApi.TranslateFirstHubFeature.Dtos;

public class GameDto
{
    public GameDto(Guid id, List<UserRatingDto> users, WordDto word)
    {
        Id = id;
        Users = users;
        Word = word;
    }

    public Guid Id { get; set; }
    public List<UserRatingDto> Users { get; set; }
    public WordDto Word { get; set; }

}
