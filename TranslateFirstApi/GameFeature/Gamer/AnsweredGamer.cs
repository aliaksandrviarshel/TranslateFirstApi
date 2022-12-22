using TranslateFirstApi.GameFeature.WordOption;
using TranslateFirstApi.TranslateFirstHubFeature.Dtos;

namespace TranslateFirstApi.GameFeature.Gamer;

public class AnsweredGamer : IGamer
{
    private readonly GamerManager _gameManager;
    private readonly string _nickname;
    private int _position;
    public Guid Id { get; private set; }
    public int CorrectAnswersCount { get; private set; }


    public void SetPosition(int value)
    {
        _position = value;
    }

    public AnsweredGamer(GamerManager gameManager, Guid id, string nickname, int position, int correctAnswersCount)
    {
        _gameManager = gameManager;
        Id = id;
        _nickname = nickname;
        SetPosition(position);
        CorrectAnswersCount = correctAnswersCount;
    }

    public bool Answer(Guid translateId)
    {
        throw new InvalidOperationException($"The user with id {Id} has already made his attempt to give an answer");
    }

    public void SetCurrentWord(Word word)
    {
        _gameManager.ChangeState(new ThinkingGamer(_gameManager, Id, _nickname, _position, word, CorrectAnswersCount));
    }

    public UserRatingDto ToDto()
    {
        return new(Id, _nickname, _position, CorrectAnswersCount);
    }
}
