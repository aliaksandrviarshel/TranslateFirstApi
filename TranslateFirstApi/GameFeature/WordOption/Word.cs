using TranslateFirstApi.GameFeature.WordOption.Shuffling;
using TranslateFirstApi.TranslateFirstHubFeature.Dtos;

namespace TranslateFirstApi.GameFeature.WordOption;

public class Word
{
    public Guid Id { get; set; } = Guid.NewGuid();
    private readonly string _foreign;
    private readonly List<Translate> _translates;
    private readonly Translate _correctTranslate;

    public Word(string foreign, List<Translate> wrongTranslates, Translate correctTranslate)
    {
        _foreign = foreign;
        _correctTranslate = correctTranslate;
        _translates = new Shuffler().Shuffle(wrongTranslates.Append(correctTranslate).ToList());
    }

    public bool IsCorrect(Guid translateId)
    {
        if (!_translates.Any(x => x.Id == translateId))
        {
            throw new ArgumentException($"No translate found with id: {translateId}", nameof(translateId));
        }

        return _correctTranslate.Id == translateId;
    }

    public Guid GetCorrectId() => _correctTranslate.Id;

    public WordDto ToDto()
    {
        return new WordDto(_foreign, _translates.Select(x => x.ToDto()).ToList(), _correctTranslate.Id);
    }
}
