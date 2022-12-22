using TranslateFirstApi.GameFeature.WordOption;
using TranslateFirstApi.TranslateFirstHubFeature.Dtos;

namespace TranslateFirstApi.GameFeature.Gamer;

public class GamerManager
{
    private IGamer _gamer;

    public GamerManager(Guid id, string nickname, int position)
    {
        _gamer = new InitialGamer(this, id, nickname, position);
    }

    public Guid Id => _gamer.Id;
    public int CorrectAnswersCount => _gamer.CorrectAnswersCount;

    public bool IsAnswered => _gamer is AnsweredGamer;

    public void ChangeState(IGamer partipiant) => _gamer = partipiant;

    public void SetPosition(int value) => _gamer.SetPosition(value);

    public bool Answer(Guid translateId) => _gamer.Answer(translateId);

    public void SetCurrentWord(Word word) => _gamer.SetCurrentWord(word);

    public UserRatingDto ToDto() => _gamer.ToDto();
}
