using TranslateFirstApi.TranslateFirstHubFeature.Dtos;

namespace TranslateFirstApi.GameFeature.WordOption;

public class Translate
{
    public Guid Id { get; set; }
    public string Text { get; }

    public Translate(string text)
    {
        Id = Guid.NewGuid();
        Text = text;
    }

    public TranslateDto ToDto()
    {
        return new TranslateDto(Id, Text);
    }
}
