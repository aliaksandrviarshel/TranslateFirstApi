using TranslateFirstApi.GameFeature.WordOption;

namespace TranslateFirstApi.GameFeature.WordsGeneration.Services;

public interface IWordsGenerator
{
    Word GenerateNext();
    bool IsDone();
}
