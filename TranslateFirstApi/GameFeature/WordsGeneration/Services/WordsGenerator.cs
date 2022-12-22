using Newtonsoft.Json;
using TranslateFirstApi.GameFeature.WordOption;
using TranslateFirstApi.GameFeature.WordsGeneration.Words.Services;

namespace TranslateFirstApi.GameFeature.WordsGeneration.Services;

public class WordsGenerator : IWordsGenerator
{
    private readonly int _wordsCount;
    private readonly WordsService _wordsService = new();
    private int _wordsCounter = 0;

    private readonly Lazy<List<Word>> _words;

    public WordsGenerator(int wordsCount)
    {
        _wordsCount = wordsCount;
        _words = new Lazy<List<Word>>(() =>
        {
            var optionsCount = 6;
            var wordEntities = _wordsService.GetAll();
            var shuffledWords = wordEntities.OrderBy(a => Guid.NewGuid()).Take(_wordsCount * optionsCount);
            return shuffledWords.Select((value, index) => (value, index)).GroupBy(x => x.index / optionsCount)
                                .Select(x => x.Select(y => y.value))
                                .Select(x =>
                                {
                                    var wrongTranslates = x.Skip(1).Select(x => new Translate(x.Translate)).ToList();
                                    var correctWord = x.First();
                                    var correctTranslate = new Translate(correctWord.Translate);
                                    return new Word(correctWord.Foreign, wrongTranslates, correctTranslate);
                                })
                                .ToList();
        });
    }

    public Word GenerateNext()
    {
        var word = _words.Value[_wordsCounter];
        _wordsCounter++;
        return word;
    }

    public bool IsDone() => _wordsCounter == _wordsCount;
}
