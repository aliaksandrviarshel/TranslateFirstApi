namespace TranslateFirstApi.TranslateFirstHubFeature.Dtos;

public class UserAnsweredDto
{
    public UserAnsweredDto(bool isCorrect)
    {
        IsCorrect = isCorrect;
    }

    public bool IsCorrect { get; set; }
}
