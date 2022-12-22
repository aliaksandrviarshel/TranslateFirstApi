namespace TranslateFirstApi.GameFeature.WordsGeneration.Words.Entities;

public class WordEntity
{
    public WordEntity(string foreign, string translate)
    {
        Foreign = foreign;
        Translate = translate;
    }

    public string Foreign { get; set; }
    public string Translate { get; set; }
}
