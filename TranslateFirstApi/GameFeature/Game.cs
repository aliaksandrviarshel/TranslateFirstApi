using TranslateFirstApi.GameFeature.Gamer;
using TranslateFirstApi.GameFeature.WordOption;
using TranslateFirstApi.GameFeature.WordsGeneration.Services;
using TranslateFirstApi.TranslateFirstHubFeature.Dtos;
using TranslateFirstApi.TranslateFirstHubFeature.HubSender;

namespace TranslateFirstApi.GameFeature;

public class Game
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public bool HasAnyUser => _gamers.Any();
    private List<GamerManager> _gamers = new();
    private Word? _currentWord;
    private readonly IRoomHubSender _waitingRoomHubSender;
    private readonly IWordsGenerator _wordsGenerator;

    public Game(IEnumerable<GamerManager> gamers, IRoomHubSender waitingRoomHubSender)
    {
        _gamers = gamers.ToList();
        _waitingRoomHubSender = waitingRoomHubSender;
        _wordsGenerator = new WordsGenerator(12);
    }

    public async Task StartAsync()
    {
        SetNextWord();
        await _waitingRoomHubSender.ToAllAsync("StartGame", Id);
    }

    public bool IncludesUser(Guid userId)
    {
        return _gamers.Any(x => x.Id == userId);
    }

    public void Leave(Guid userId)
    {
        _gamers = _gamers.Where(x => x.Id != userId).ToList();
        _waitingRoomHubSender.RemoveUser(userId);
        _waitingRoomHubSender.ToAllAsync("GameUserLeft", userId);
        CheckAndHandleAllUsersAnswered();
    }

    public bool Answer(Guid userId, Guid translateId)
    {
        var gamers = _gamers.First(x => x.Id == userId);

        if (gamers.Answer(translateId))
        {
            CalculateUsersRating();
            if (_wordsGenerator.IsDone())
            {
                _waitingRoomHubSender.ToAllAsync("GameEnded", GetGameEndedDto(translateId));
                return true;
            }

            SetNextWord();
            _waitingRoomHubSender.ToAllAsync("RoundEnded", GetRoundEndedDto(translateId));
            return true;
        }

        return CheckAndHandleAllUsersAnswered();
    }

    public bool ContainsGamer(Guid id) => _gamers.Any(x => x.Id == id);

    public GameDto ToDto()
    {
        _ = _currentWord ?? throw new ArgumentNullException("Current word is not set");

        return new(Id, _gamers.Select(x => x.ToDto()).ToList(), _currentWord.ToDto());
    }

    private bool CheckAndHandleAllUsersAnswered()
    {
        if (_gamers.All(x => x.IsAnswered))
        {
            CalculateUsersRating();
            var correctTranslateId = _currentWord.GetCorrectId();
            if (_wordsGenerator.IsDone())
            {
                _waitingRoomHubSender.ToAllAsync("GameEnded", GetGameEndedDto(correctTranslateId));
                return false;
            }

            SetNextWord();
            _waitingRoomHubSender.ToAllAsync("RoundEnded", GetRoundEndedDto(correctTranslateId));
        }

        return false;
    }

    private void CalculateUsersRating()
    {
        var orderedGamers = _gamers.OrderByDescending(x => x.CorrectAnswersCount).ToList();
        for (int i = 0; i < orderedGamers.Count; i++)
        {
            orderedGamers[i].SetPosition(i + 1);
        }

        _gamers = orderedGamers;
    }

    private void SetNextWord()
    {
        _currentWord = _wordsGenerator.GenerateNext();
        _gamers.ForEach(x => x.SetCurrentWord(_currentWord));
    }

    private RoundEndedDto GetRoundEndedDto(Guid correctTranslateId)
    {
        _ = _currentWord ?? throw new ArgumentNullException("Current word is not set");

        return new RoundEndedDto(_gamers.Select(x => x.ToDto()).ToList(), correctTranslateId, _currentWord.ToDto());
    }

    private GameEndedDto GetGameEndedDto(Guid correctTranslateId)
    {
        _ = _currentWord ?? throw new ArgumentNullException("Current word is not set");

        return new GameEndedDto(_gamers.Select(x => x.ToDto()).ToList(), correctTranslateId);
    }
}
