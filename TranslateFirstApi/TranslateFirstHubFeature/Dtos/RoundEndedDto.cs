namespace TranslateFirstApi.TranslateFirstHubFeature.Dtos;

public class RoundEndedDto
{
    public RoundEndedDto(List<UserRatingDto> users, Guid correctAnswer, WordDto nextWord)
    {
        Users = users;
        CorrectAnswerId = correctAnswer;
        NextWord = nextWord;
    }

    public List<UserRatingDto> Users { get; set; }
    public Guid CorrectAnswerId { get; set; }
    public WordDto NextWord { get; set; }
}
