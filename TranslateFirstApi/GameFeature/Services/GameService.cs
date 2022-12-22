using TranslateFirstApi.TranslateFirstHubFeature.Dtos;
using TranslateFirstApi.WaitingRoomFeature;

namespace TranslateFirstApi.GameFeature.Services;

public class GameService : IGameService
{
    private List<Game> _games = new();

    public Task StartGame(WaitingRoom waitingRoom)
    {
        var game = waitingRoom.StartGame();
        _games.Add(game);
        return game.StartAsync();
    }

    public Task<UserAnsweredDto> Answer(Guid userId, Guid translateId)
    {
        var game = _games.First(x => x.ContainsGamer(userId));
        var result = game.Answer(userId, translateId);
        return Task.FromResult(new UserAnsweredDto(result));
    }

    public Task<GameDto> GetGame(Guid userId, Guid gameId)
    {
        return Task.FromResult(_games.First(x => x.Id == gameId && x.ContainsGamer(userId)).ToDto());
    }

    public Task Leave(Guid userId)
    {
        try
        {
            var gameToLeave = _games.First(x => x.IncludesUser(userId));
            gameToLeave.Leave(userId);
            if (!gameToLeave.HasAnyUser)
            {
                _games.Remove(gameToLeave);
            }
            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex);
            return Task.CompletedTask;
        }
    }
}
