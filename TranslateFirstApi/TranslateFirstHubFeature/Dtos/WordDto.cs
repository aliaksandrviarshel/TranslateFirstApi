namespace TranslateFirstApi.TranslateFirstHubFeature.Dtos;

public class WordDto
{
    public WordDto(string foreign, List<TranslateDto> translations, Guid correctId)
    {
        Foreign = foreign;
        Translations = translations;
        CorrectId = correctId;
    }

    public string Foreign { get; set; }
    public List<TranslateDto> Translations { get; set; }
    public Guid CorrectId { get; }
}
