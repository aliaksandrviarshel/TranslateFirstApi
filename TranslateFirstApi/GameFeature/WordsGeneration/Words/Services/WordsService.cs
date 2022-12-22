using Newtonsoft.Json;
using TranslateFirstApi.GameFeature.WordsGeneration.Words.Entities;

namespace TranslateFirstApi.GameFeature.WordsGeneration.Words.Services;

public class WordsService
{
    public List<WordEntity> GetAll()
    {
        var path = Environment.CurrentDirectory;
        return JsonConvert.DeserializeObject<List<WordEntity>>(File.ReadAllText($"{path}/words.json"));
    }
}
