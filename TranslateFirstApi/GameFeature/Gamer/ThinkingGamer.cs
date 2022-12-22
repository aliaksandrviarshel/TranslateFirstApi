using TranslateFirstApi.GameFeature.WordOption;
using TranslateFirstApi.TranslateFirstHubFeature.Dtos;

namespace TranslateFirstApi.GameFeature.Gamer;

public class ThinkingGamer : IGamer
{
    private Word _word;
    private readonly GamerManager _gameManager;
    private readonly string _nickname;

    public ThinkingGamer(GamerManager gameManager, Guid id, string nickname, int position, Word word, int correctAnswersCount)
    {
        _gameManager = gameManager;
        Id = id;
        _nickname = nickname;
        _word = word;
        _position = position;
        CorrectAnswersCount = correctAnswersCount;
    }

    public Guid Id { get; private set; }
    public int CorrectAnswersCount { get; private set; }

    private int _position;

    public void SetPosition(int value)
    {
        _position = value;
    }

    public void SetCurrentWord(Word word)
    {
        _word = word;
    }

    public bool Answer(Guid translateId)
    {
        var result = _word.IsCorrect(translateId);
        if (result)
        {
            CorrectAnswersCount++;
        }

        _gameManager.ChangeState(new AnsweredGamer(_gameManager, Id, _nickname, _position, CorrectAnswersCount));
        return result;
    }

    public UserRatingDto ToDto()
    {
        return new(Id, _nickname, _position, CorrectAnswersCount);
    }
}
