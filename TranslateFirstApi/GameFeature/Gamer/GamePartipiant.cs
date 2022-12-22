using TranslateFirstApi.GameFeature.WordOption;
using TranslateFirstApi.TranslateFirstHubFeature.Dtos;

namespace TranslateFirstApi.GameFeature.Gamer;

public class GamePartipiant
{
    private Word _currentWord;
    private readonly string _nickname;

    public GamePartipiant(Guid id, string nickname, int position)
    {
        Id = id;
        _nickname = nickname;
        Position = position;
    }

    public Guid Id { get; private set; }
    public int CorrectAnswersCount { get; private set; }
    public int Position { private get; set; }
    public bool IsAnswered { get; private set; }

    public void SetCurrentWord(Word word)
    {
        IsAnswered = false;
        _currentWord = word;
    }

    public bool Answer(Guid translateId)
    {
        if (IsAnswered)
        {
            throw new InvalidOperationException("The user has already made his attempt to give an answer");
        }

        IsAnswered = true;
        var result = _currentWord.IsCorrect(translateId);
        if (result)
        {
            CorrectAnswersCount++;
        }

        return result;
    }

    public UserRatingDto ToDto()
    {
        return new(Id, _nickname, Position, CorrectAnswersCount);
    }
}
