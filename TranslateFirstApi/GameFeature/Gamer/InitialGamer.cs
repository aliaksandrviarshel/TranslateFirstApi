using TranslateFirstApi.GameFeature.WordOption;
using TranslateFirstApi.TranslateFirstHubFeature.Dtos;

namespace TranslateFirstApi.GameFeature.Gamer;

public class InitialGamer : IGamer
{
    private readonly GamerManager _gameManager;
    private readonly Guid _id;
    private readonly string _nickname;
    private readonly int _position;

    public InitialGamer(GamerManager gameManager, Guid id, string nickname, int position)
    {
        _gameManager = gameManager;
        _id = id;
        _nickname = nickname;
        _position = position;
    }

    public int CorrectAnswersCount => 0;

    public Guid Id => _id;

    public bool Answer(Guid translateId)
    {
        throw new InvalidOperationException($"The user with id {Id} has initial state");
    }

    public void SetCurrentWord(Word word)
    {
        _gameManager.ChangeState(new ThinkingGamer(_gameManager, Id, _nickname, _position, word, CorrectAnswersCount));
    }

    public void SetPosition(int value)
    {
        throw new InvalidOperationException($"The user with id {Id} has initial state");
    }

    public UserRatingDto ToDto()
    {
        return new(Id, _nickname, _position, CorrectAnswersCount);
    }
}
