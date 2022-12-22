namespace TranslateFirstApi.TranslateFirstHubFeature.Dtos;

public class TranslateDto
{
    public TranslateDto(Guid id, string text)
    {
        Id = id;
        Text = text;
    }

    public Guid Id { get; set; }
    public string Text { get; set; }
}