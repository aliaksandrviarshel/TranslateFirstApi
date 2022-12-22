namespace TranslateFirstApi.TranslateFirstHubFeature.Dtos;

public class UserRatingDto
{
    public UserRatingDto(Guid id, string nickname, int position, int correctAnswersCount)
    {
        Id = id;
        Nickname = nickname;
        Position = position;
        CorrectAnswersCount = correctAnswersCount;
    }

    public Guid Id { get; set; }
    public string Nickname { get; set; }
    public int Position { get; set; }
    public int CorrectAnswersCount { get; set; }
}
